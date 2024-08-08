import axios from "axios";  

export async function Post(apiAdress, postData) {
    var lastToken = localStorage.getItem("LastToken");
    axios.defaults.headers.common['Authorization'] = `Bearer ${lastToken}`;
    axios.defaults.headers.common['Content-Type'] = 'application/json';
    const data = await axios.post(process.env.REACT_APP_APIURL + apiAdress, postData);
    return data;
}
export async function GetRequest(apiAdress) {
    var lastToken = localStorage.getItem("LastToken");
    axios.defaults.headers.common['Authorization'] = `Bearer ${lastToken}`;
    axios.defaults.headers.common['Content-Type'] = 'application/json';
    const  data   = await axios.get(process.env.REACT_APP_APIURL + apiAdress);
    return data;
  }