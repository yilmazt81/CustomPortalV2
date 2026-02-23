
import { GetRequest,Post } from "./Apibase";

export async function CreateWork() {
 
    const { data } = await GetRequest(`/api/CustomWork/CreateWork`);
    return data;
  };
  
  export async function GetCompanyWorks() {
 
    const { data } = await GetRequest(`/api/CustomWork`);
    return data;
  };

  export async function GetCompanyWork(id) {
 
    const { data } = await GetRequest(`/api/CustomWork/${id}`);
    return data;
  };

  export async function Save(customWork) {
 
    const { data } = await Post(`/api/CustomWork`,customWork);

    return data;

  };

  
  export async function DeleteWork(id) {
 
    const { data } = await GetRequest(`/api/CustomWork/DeleteWork/${id}`);
    return data;
  };
 

 
 
  


