
import { GetRequest,Post } from "./Apibase";

export async function CreateProduct() {
 
    const { data } = await GetRequest(`/api/CustomProduct/CreateProduct`);
    return data;
  };
  
  export async function GetCompanyProducts() {
 
    const { data } = await GetRequest(`/api/CustomProduct`);
    return data;
  };

  export async function Save(customProduct) {
 
    const { data } = await Post(`/api/CustomProduct`,customProduct);
    
    return data;

  };
