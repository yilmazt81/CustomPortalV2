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




 