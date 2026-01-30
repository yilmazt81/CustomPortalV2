
import { GetRequest,Post } from "./Apibase";

export async function CreateProduct() {
 
    const { data } = await GetRequest(`/api/CustomProduct/CreateProduct`);
    return data;
  };
  
  export async function GetCompanyProducts() {
 
    const { data } = await GetRequest(`/api/CustomProduct`);
    return data;
  };

  export async function GetCompanyProduct(id) {
 
    const { data } = await GetRequest(`/api/CustomProduct/${id}`);
    return data;
  };

  export async function Save(customProduct) {
 
    const { data } = await Post(`/api/CustomProduct`,customProduct);

    return data;

  };

  
  export async function DeleteProduct(id) {
 
    const { data } = await GetRequest(`/api/CustomProduct/DeleteProduct/${id}`);
    return data;
  };
 

  export async function FilterProduct(filterReq) {
 
    const { data } = await Post(`/api/CustomProduct/FilterProduct`,filterReq);
    return data;
  };
 
  export async function GetAutoComplateProduct(formdefinationid,productidlist) {
 
    const { data } = await GetRequest(`/api/CustomProduct/GetAutoComplateProduct/${formdefinationid}/${productidlist}`);
    return data;
  };
 
  


