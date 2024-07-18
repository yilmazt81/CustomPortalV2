import axios from "axios"; 
import React,{useEffect} from "react";
import { useDispatch, useSelector } from 'react-redux'; 
export async function LoginUser(logindata)
{
   

 
  const{ data:result}= await axios.post(process.env.REACT_APP_APIURL+'/api/Login',logindata);
    

  return result;
   
}
export async function  getUserMenu() {   
  
  const { data } =  await axios.get(process.env.REACT_APP_APIURL+'/api/User/GetUserMenu');
  return data;
};

 