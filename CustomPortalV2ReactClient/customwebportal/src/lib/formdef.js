import axios from "axios";  
 
 
export async function  CreateNewFormDefination() {   
  var lastToken=localStorage.getItem("LastToken");
  axios.defaults.headers.common['Authorization'] = `Bearer ${lastToken}`;
  const { data } =  await axios.get(process.env.REACT_APP_APIURL+`/api/FormDefination/CreateFormDefination`);
  return data;
};
 

export async function  GetFormDefinations() {   
  var lastToken=localStorage.getItem("LastToken");
  axios.defaults.headers.common['Authorization'] = `Bearer ${lastToken}`;
  const { data } =  await axios.get(process.env.REACT_APP_APIURL+`/api/FormDefination`);
  return data;
};
 
 export async function SaveDefination(defination){
  
 }