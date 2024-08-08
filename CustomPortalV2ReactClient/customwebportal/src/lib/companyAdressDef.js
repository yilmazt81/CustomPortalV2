
import { GetRequest,Post } from "./Apibase";
 
export async function  CreateDefinationTypes() {    
  const { data } =  await GetRequest (`/api/CompanyDefination/GetDefinationTypes`);
  return data;
};
 

export async function  NewCompanyDefination() {   
  const { data } =  await GetRequest(`/api/CompanyDefination/NewDefination`);
  return data;
};

export async function  GetUserCompanyDefinations() {    
  const { data } =  await GetRequest(`/api/CompanyDefination`);
  return data;
};
 
 export async function SaveDefination(defination){


  const { data } =  await Post((defination.id==0?"/api/CompanyDefination":"/api/CompanyDefination/UpdateAdress"),defination);
  return data;
 }

 export async function  GetCompanyDefination(id) {    
  const { data } =  await  GetRequest(`/api/CompanyDefination/${id}`);
  return data;
};

export async function DeleteCompanyDefination(id){
  
  const { data } =  await GetRequest(`/api/CompanyDefination/DeleteCompanyDefination/${id}`);
  return data;

}
export async function FilterCompanyDefination(filterData){
  
  const { data } =  await Post(`/api/CompanyDefination/FilterCompanyDefination`,filterData);
  return data;

}

 

export async function GetAutoComplateAdress(formdefinationTypeid,id){
  
  const { data } =  await GetRequest(`/api/CompanyDefination/GetAutoComplateAdress/${formdefinationTypeid}/${id}`);
  return data;

}