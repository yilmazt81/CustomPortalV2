import axios from "axios";
import { getAccessToken } from "./tokenStorage.jsx";

export async function Post(apiAdress, postData) {
    var lastToken = getAccessToken();
    axios.defaults.headers.common['Authorization'] = `Bearer ${lastToken}`;
    axios.defaults.headers.common['Content-Type'] = 'application/json';
    const data = await axios.post(import.meta.env.VITE_APIURL + apiAdress, postData);
    return data;
}


export async function PostFile(apiAdress, postData) {
    var lastToken = getAccessToken();
    var  data   = await axios.post(import.meta.env.VITE_APIURL + apiAdress, postData, {
        headers: {
            'Content-Type': 'multipart/form-data',
            'Authorization': `Bearer ${lastToken}`
        }
    });

    return data;
}
export async function GetRequest(apiAdress) {
    var lastToken = getAccessToken();
    axios.defaults.headers.common['Authorization'] = `Bearer ${lastToken}`;
    axios.defaults.headers.common['Content-Type'] = 'application/json';
    const data = await axios.get(import.meta.env.VITE_APIURL + apiAdress);
    return data;
}




