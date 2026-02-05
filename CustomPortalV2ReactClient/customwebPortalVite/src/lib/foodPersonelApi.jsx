
import { GetRequest,Post } from "./Apibase";

 /*
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
  }; */
 
  export async function FilterPersonel(filterReq) {
 
    const { data } = await Post(`/api/FoodPersonel/FilterPersonel`,filterReq);
    return data;
  };
 
  export async function GetAutoComplatePersonel(formdefinationid,personelid) {
 
    const { data } = await GetRequest(`/api/FoodPersonel/GetAutoComplatePersonel/${formdefinationid}/${personelid}`);
    return data;
  };
 
  


