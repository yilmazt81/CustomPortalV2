import axios from "axios"
import {Endpoint} from './common/endpoints-constant';

export const api = axios.create({
  withCredentials: true,
  headers:{ "Accept": 'application/json',
            'Content-Type': 'application/json'},
  baseURL: Endpoint.AUTH.base,
})


// defining a custom error handler for all APIs
const errorHandler = (error) => {
  const statusCode = error.response?.status

  // logging only errors that are not 401
  if (statusCode && statusCode !== 401) {
    console.error(error)
  }

  return Promise.reject(error)
}

// registering the custom error handler to the
// "api" axios instance
api.interceptors.response.use(undefined, (error) => {
  return errorHandler(error)
})