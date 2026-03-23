import axios from "axios";
import {
  clearTokens,
  getAccessToken,
  getRefreshToken,
  setTokens,
} from "./tokenStorage.jsx";

let isRefreshing = false;
let pendingRequests = [];

function resolvePendingRequests(newToken) {
  pendingRequests.forEach(({ resolve }) => resolve(newToken));
  pendingRequests = [];
}

function rejectPendingRequests(error) {
  pendingRequests.forEach(({ reject }) => reject(error));
  pendingRequests = [];
}

function subscribeTokenRefresh(resolve, reject) {
  pendingRequests.push({ resolve, reject });
}

function extractTokenPayload(responseData) {
  const payload = responseData?.data || responseData;
  return {
    accessToken:
      payload?.token || payload?.accessToken || payload?.jwtToken,
    refreshToken:
      payload?.refreshToken || payload?.newRefreshToken || payload?.refresh,
  };
}

async function requestNewAccessToken() {
  const currentRefreshToken = getRefreshToken();
  const currentAccessToken = getAccessToken();

  if (!currentRefreshToken) {
    throw new Error("Refresh token is missing");
  }

  const refreshPath = import.meta.env.VITE_REFRESH_PATH || "/api/Login/RefreshToken";
  const requestBody = {
    refreshToken: currentRefreshToken,
    token: currentAccessToken,
    accessToken: currentAccessToken,
  };

  const { data } = await axios.post(
    `${import.meta.env.VITE_APIURL}${refreshPath}`,
    requestBody,
    {
      skipAuthRefresh: true,
      headers: {
        "Content-Type": "application/json",
      },
    }
  );

  const tokens = extractTokenPayload(data);
  if (!tokens.accessToken) {
    throw new Error("Access token could not be refreshed");
  }

  setTokens(tokens.accessToken, tokens.refreshToken || currentRefreshToken);
  axios.defaults.headers.common["Authorization"] = `Bearer ${tokens.accessToken}`;
  return tokens.accessToken;
}

function redirectToLogin() {
  clearTokens();
  delete axios.defaults.headers.common["Authorization"];
  window.location.hash = "/login";
}

export function setupAuthInterceptor() {
  const startupToken = getAccessToken();
  if (startupToken) {
    axios.defaults.headers.common["Authorization"] = `Bearer ${startupToken}`;
  }

  axios.interceptors.request.use((config) => {
    if (config.skipAuthRefresh) {
      return config;
    }
    const token = getAccessToken();
    if (token) {
      config.headers = config.headers || {};
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  });

  axios.interceptors.response.use(
    (response) => response,
    async (error) => {
      const originalRequest = error?.config;
      const status = error?.response?.status;

      if (!originalRequest || originalRequest.skipAuthRefresh) {
        return Promise.reject(error);
      }

      if (status !== 401 || originalRequest._retry) {
        return Promise.reject(error);
      }

      originalRequest._retry = true;

      if (isRefreshing) {
        return new Promise((resolve, reject) => {
          subscribeTokenRefresh(
            (newToken) => {
              originalRequest.headers = originalRequest.headers || {};
              originalRequest.headers.Authorization = `Bearer ${newToken}`;
              resolve(axios(originalRequest));
            },
            (refreshError) => reject(refreshError)
          );
        });
      }

      isRefreshing = true;

      try {
        const refreshedToken = await requestNewAccessToken();
        resolvePendingRequests(refreshedToken);
        originalRequest.headers = originalRequest.headers || {};
        originalRequest.headers.Authorization = `Bearer ${refreshedToken}`;
        return axios(originalRequest);
      } catch (refreshError) {
        rejectPendingRequests(refreshError);
        redirectToLogin();
        return Promise.reject(refreshError);
      } finally {
        isRefreshing = false;
      }
    }
  );
}
