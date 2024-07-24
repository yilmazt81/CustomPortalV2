import axios from "axios"; 
import React,{useEffect} from "react";
import { useDispatch, useSelector } from 'react-redux'; 
export async function LoginUser(logindata)
{ 
 
  const{ data:result}= await axios.post(process.env.REACT_APP_APIURL+'/api/Login',logindata);
    

  return result;
   
}
export async function  getUserMenu() {   
  var lastToken=localStorage.getItem("LastToken");
  axios.defaults.headers.common['Authorization'] = `Bearer ${lastToken}`;
  const { data } =  await axios.get(process.env.REACT_APP_APIURL+'/api/User/GetUserMenu');
  return data;
};


export async function  getUserRoles() {   
  var lastToken=localStorage.getItem("LastToken");
  axios.defaults.headers.common['Authorization'] = `Bearer ${lastToken}`;
  const { data } =  await axios.get(process.env.REACT_APP_APIURL+'/api/User/GetUserRoles');
  return data;
};

export async function  GetBranchpackages() {   
  var lastToken=localStorage.getItem("LastToken");
  axios.defaults.headers.common['Authorization'] = `Bearer ${lastToken}`;
  const { data } =  await axios.get(process.env.REACT_APP_APIURL+'/api/User/GetBranchpackages');
  return data;
};

export async function  GetUserList() {   
  var lastToken=localStorage.getItem("LastToken");
  axios.defaults.headers.common['Authorization'] = `Bearer ${lastToken}`;
  const { data } =  await axios.get(process.env.REACT_APP_APIURL+'/api/User');
  return data;
};


export async function  GetUser(id) {   
  var lastToken=localStorage.getItem("LastToken");
  axios.defaults.headers.common['Authorization'] = `Bearer ${lastToken}`;
  const { data } =  await axios.get(process.env.REACT_APP_APIURL+`/api/User/${id}`);
  return data;
};

export async function  CreateNewUser() {   
  var lastToken=localStorage.getItem("LastToken");
  axios.defaults.headers.common['Authorization'] = `Bearer ${lastToken}`;
  const { data } =  await axios.get(process.env.REACT_APP_APIURL+`/api/User/CreateNewUser`);
  return data;
};

export async function  SaveUser(user) {   
  var lastToken=localStorage.getItem("LastToken");
  axios.defaults.headers.common['Authorization'] = `Bearer ${lastToken}`;
  const { data } =  await axios.post(process.env.REACT_APP_APIURL+(user.id==0?"/api/User":"/api/User/UpdateUser"),user);
  return data;
};

export async function  DeleteUser(id) { 
  var lastToken=localStorage.getItem("LastToken");
  axios.defaults.headers.common['Authorization'] = `Bearer ${lastToken}`;
  const { data } =  await axios.get(process.env.REACT_APP_APIURL+`/api/User/DeleteUser?id=${id}`);
  return data;
}


 



 