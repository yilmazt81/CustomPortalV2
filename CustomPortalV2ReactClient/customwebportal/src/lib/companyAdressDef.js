import axios from "axios";  
 
 
export async function  CreateDefinationTypes() {   
  var lastToken=localStorage.getItem("LastToken");
  axios.defaults.headers.common['Authorization'] = `Bearer ${lastToken}`;
  const { data } =  await axios.get(process.env.REACT_APP_APIURL+`/api/CompanyDefination/GetDefinationTypes`);
  return data;
};
 

export async function  NewCompanyDefination() {   
  var lastToken=localStorage.getItem("LastToken");
  axios.defaults.headers.common['Authorization'] = `Bearer ${lastToken}`;
  const { data } =  await axios.get(process.env.REACT_APP_APIURL+`/api/CompanyDefination/NewDefination`);
  return data;
};

export async function  GetUserCompanyDefinations() {   
  var lastToken=localStorage.getItem("LastToken");
  axios.defaults.headers.common['Authorization'] = `Bearer ${lastToken}`;
  const { data } =  await axios.get(process.env.REACT_APP_APIURL+`/api/CompanyDefination`);
  return data;
};
 
 export async function SaveDefination(defination){

  var lastToken=localStorage.getItem("LastToken");
  axios.defaults.headers.common['Authorization'] = `Bearer ${lastToken}`;
  const { data } =  await axios.post(process.env.REACT_APP_APIURL+(defination.id==0?"/api/CompanyDefination":"/api/CompanyDefination/UpdateAdress"),defination);
  return data;
 }

 export async function  GetCompanyDefination(id) {   
  var lastToken=localStorage.getItem("LastToken");
  axios.defaults.headers.common['Authorization'] = `Bearer ${lastToken}`;
  const { data } =  await axios.get(process.env.REACT_APP_APIURL+`/api/CompanyDefination/${id}`);
  return data;
};

export async function DeleteCompanyDefination(id){
  var lastToken=localStorage.getItem("LastToken");
  axios.defaults.headers.common['Authorization'] = `Bearer ${lastToken}`;

  const { data } =  await axios.get(process.env.REACT_APP_APIURL+`/api/CompanyDefination/DeleteCompanyDefination/${id}`);
  return data;

}
