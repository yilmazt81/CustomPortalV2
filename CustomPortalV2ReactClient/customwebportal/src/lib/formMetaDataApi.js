import axios from "axios"; 
import React,{useEffect} from "react";
import { useDispatch, useSelector } from 'react-redux'; 
 

async function GetRequest(apiAdress) {
  var lastToken = localStorage.getItem("LastToken");
  axios.defaults.headers.common['Authorization'] = `Bearer ${lastToken}`;
  axios.defaults.headers.common['Content-Type'] = 'application/json';
  const  data   = await axios.get(process.env.REACT_APP_APIURL + apiAdress);
  return data;
}


export async function  GetBrachData() {   
  
  const { data } =  await GetRequest ('/api/FormMetaData');
  return data;
};
 
export async function  GetFormMetaDataById(id) {   
  
  const { data } =  await GetRequest (`/api/FormMetaData/${id}`);
  return data;
};