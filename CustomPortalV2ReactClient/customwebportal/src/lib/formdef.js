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
 
export async function  GetSector() {   
  var lastToken=localStorage.getItem("LastToken");
  axios.defaults.headers.common['Authorization'] = `Bearer ${lastToken}`;
  const { data } =  await axios.get(process.env.REACT_APP_APIURL+`/api/FormDefination/GetSectors`);
  return data;
};

export async function  SaveFormDefination(formdefination) {   
  var lastToken=localStorage.getItem("LastToken");
  axios.defaults.headers.common['Authorization'] = `Bearer ${lastToken}`;
  const { data } =  await axios.post(process.env.REACT_APP_APIURL+`/api/FormDefination`,formdefination);
  return data;
};

export async function  GetFormDefination(id) {   
  var lastToken=localStorage.getItem("LastToken");
  axios.defaults.headers.common['Authorization'] = `Bearer ${lastToken}`;
  const { data } =  await axios.get(process.env.REACT_APP_APIURL+`/api/FormDefination/${id}`);
  return data;
};
 
export async function  GetFormGroups(formdefinationId) {   
  var lastToken=localStorage.getItem("LastToken");
  axios.defaults.headers.common['Authorization'] = `Bearer ${lastToken}`;
  const { data } =  await axios.get(process.env.REACT_APP_APIURL+`/api/FormDefination/GetFormGroup?formDefinationId=${formdefinationId}`);
  return data;
};
 



 