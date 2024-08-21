import { Post,GetRequest } from "./Apibase";


export async function CreateFormAttachment(id,attachmentid) {
  
    const { data } = await GetRequest(`/api/CreateFile/CreateFormAttachment/${id}/${attachmentid}`);
    return data;
  }