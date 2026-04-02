
import { GetRequest,Post } from "./Apibase";

export async function GetDashBoard() {
 
    const { data } = await GetRequest(`/api/Dashboard`);
    return data;
  };

  export async function GetLastForms() {
 
    const { data } = await GetRequest(`/api/Dashboard/GetLastForms`);
    return data;
  };
  