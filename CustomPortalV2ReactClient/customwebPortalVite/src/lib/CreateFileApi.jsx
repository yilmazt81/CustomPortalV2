import { Post,GetRequest } from "./Apibase";


export async function CreateFormAttachment(id,attachmentid) {
  
    const { data } = await GetRequest(`/api/CreateFile/CreateFormAttachment/${id}/${attachmentid}`);
    return data;
  }

  export async function CreateFormVersion(id,versionid) {
  
    const { data } = await GetRequest(`/api/CreateFile/CreateFormVersion/${id}/${versionid}`);
    return data;
  }

