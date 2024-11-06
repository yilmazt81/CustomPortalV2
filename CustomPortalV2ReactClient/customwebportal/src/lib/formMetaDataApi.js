import axios from "axios"; 
import React,{useEffect} from "react";
import { useDispatch, useSelector } from 'react-redux'; 
import { Post,GetRequest } from "./Apibase";
 
 


export async function  GetBrachData() {   
  
  const { data } =  await GetRequest ('/api/FormMetaData');
  return data;
};
 
export async function  GetFormMetaDataById(id) {   
  
  const { data } =  await GetRequest (`/api/FormMetaData/${id}`);
  return data;
};

export async function  GetConvertFileList(id) {   
  
  const { data } =  await GetRequest (`/api/FormMetaData/GetConvertFileList/${id}`);
  return data;
};
 
export async function  CloneForm(sourceformid) {   
  
  const { data } =  await GetRequest (`/api/FormMetaData/CloneForm/${sourceformid}`);
  return data;
};
export async function  DeleteForm(formid) {   
  
  const { data } =  await GetRequest (`/api/FormMetaData/DeleteForm/${formid}`);
  return data;
};



export async function SaveMetaData(formid,fordefinationtypeid,fieldList,isdefault,workid,custimefieldList) {
 
  var postobj=  {id:formid,
                 formDefinationTypeid:fordefinationtypeid,
                 fieldValues:fieldList,
                 customeFieldfieldValues:custimefieldList,
                 isDefault:isdefault,
                 workid:workid,
                 userId:0,
                 userName:"",
                 companyId:0,
                 brachId:0}; 
  const { data } =  await Post (`/api/FormMetaData`,postobj);
  return data;
}