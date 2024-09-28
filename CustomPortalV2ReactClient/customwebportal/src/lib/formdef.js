 
import { GetRequest,Post,PostFile } from "./Apibase";


export async function CreateNewFormDefination() { 
  const { data } = await GetRequest(`/api/FormDefination/CreateFormDefination`);
  return data;
};


export async function GetFormDefinations() { 
  const { data } = await  GetRequest(`/api/FormDefination`);
  return data;
};

export async function GetSector() { 
  const { data } = await GetRequest(`/api/FormDefination/GetSectors`);
  return data;
};

export async function SaveFormDefination(formdefination) {
   
  const { data } = await  PostFile(`/api/FormDefination`, formdefination);
  return data;
};

export async function GetFormDefination(id) { 
  const { data } = await GetRequest(`/api/FormDefination/${id}`);
  return data;
};

export async function GetFormGroups(formdefinationId) { 
  const { data } = await GetRequest(`/api/FormDefination/GetFormGroups?formDefinationId=${formdefinationId}`);
  return data;
};



export async function CreateNewFormDefinationGroup(formdefinationId) { 
  const { data } = await  GetRequest(`/api/FormDefination/CreateFormDefinationGroup?formDefinationId=${formdefinationId}`);
  return data;
};

export async function SaveFormGroup(formGroup) { 
  const { data } = await  Post(process.env.REACT_APP_APIURL + `/api/FormDefination/SaveGroup`, formGroup);
  return data;
};

export async function SaveFormDefinationField(formdefinationField) {
 
  const { data } = await  Post(process.env.REACT_APP_APIURL + `/api/FormDefination/SaveFormDefinationField`, formdefinationField);
  return data;
};


export async function SaveComboBoxItem(comboboxItem) { 
  const { data } = await  Post(process.env.REACT_APP_APIURL + `/api/FormDefination/SaveComboBoxItem`, comboboxItem);
  return data;
};



export async function GetFormGroupFields(formgroupid) {
 
  const { data } = await GetRequest(`/api/FormDefination/GetGroupFields/${formgroupid}`);
  return data;
};

 

 
export async function GetFormGroup(id) {

  const { data } = await GetRequest(`/api/FormDefination/GetFormGroup/${id}`);
  return data;
};

export async function DeleteFormGroup(formdefinationid,groupid) {

  const { data } = await GetRequest(`/api/FormDefination/DeleteGroup?formDefinationId=${formdefinationid}&groupId=${groupid}`);
  return data;
};

export async function CreateNewGroupField(formdefinationid,groupid) {
                           
  const { data } = await GetRequest(`/api/FormDefination/CreateNewGroupField?formDefinationId=${formdefinationid}&formGroupId=${groupid}`);
  return data;
};

export async function GetFieldTypes() {
                          
  const { data } = await GetRequest(`/api/FormDefination/GetFieldTypes`);
  return data;
};
export async function GetFonts() {
                           
  const { data } = await GetRequest(`/api/FormDefination/GetFonts`);
  return data;
};



export async function GetFormDefinationField(id) {
  
  const { data } = await GetRequest(`/api/FormDefination/GetFormDefinationField/${id}`);
  return data;
}

export async function GetComboBoxItems(fieldTag) {
  
  const { data } = await GetRequest(`/api/FormDefination/GetComboBoxItems/${fieldTag}`);
  return data;
}

export async function CreateComboBoxItem(fieldTag) {
  
  const { data } = await GetRequest(`/api/FormDefination/CreateComboBoxItem/${fieldTag}`);
  return data;
}

export async function GetFormDefinationBySector(sectorid) {
  
  const { data } = await GetRequest(`/api/FormDefination/GetFormDefinationBySector/${sectorid}`);
  return data;
}
export async function GetFormGroupFormApp(formdefinationTypeId) {
  
  const { data } = await GetRequest(`/api/FormDefination/GetFormGroupFormApp/${formdefinationTypeId}`);
  return data;
}




export async function GetAutoComlateField(formdefinationTypeId) {
  
  const { data } = await GetRequest(`/api/FormDefination/GetAutoComlateField/${formdefinationTypeId}`);
  return data;
}
export async function GetAutoComlateFieldMaps(formdefinationTypeId) {
  
  const { data } = await GetRequest(`/api/FormDefination/GetAutoComlateFieldMaps/${formdefinationTypeId}`);
  return data;
}
/*
export async function CreateAutoComplateField(formdefinationFieldId,complateObject,filterValue) {
  
  const { data } = await GetRequest(`/api/CreateAutoComplateField/${formdefinationFieldId}/${complateObject}/${filterValue}`);
  return data;
}*/
export async function GetReflectionFields(complateObject) {
  
  const { data } = await GetRequest(`/api/FormDefination/GetReflectionFields?complateObject=${complateObject}`);
  return data;
}


export async function SaveAutoComplateField(complateObject) {
  
  const { data } = await Post(`/api/FormDefination/SaveAutoComplateField`,complateObject);
  return data;
}

export async function DeleteAutoComplateFieldMap(formdefinationId,autcomplatefieldmapid) {
  
  const { data } = await GetRequest(`/api/FormDefination/DeleteAutoComplateFieldMap?formdefinationId=${formdefinationId}&autoComplateMapid=${autcomplatefieldmapid}`);
  return data;
}

export async function GetFormDefinationTemplate(formdefinationId) {
  
  const { data } = await GetRequest(`/api/FormDefination/GetFormDefinationTemplate/${formdefinationId}`);
  return data;
}

export async function GetFormDefinationVersions(formdefinationId) {
  
  const { data } = await GetRequest(`/api/FormDefination/GetFormDefinationVersions/${formdefinationId}`);
  return data;
}

 
export async function GetFormDefinationVersion(id) {
  
  const { data } = await GetRequest(`/api/FormDefination/GetFormDefinationVersion/${id}`);
  return data;
}

export async function SaveFormVersion(formData) {
   
  
  const { data } = await PostFile(`/api/FormDefination/SaveFormVersion`, formData);
  return data;
};

export async function GetFormDefinationAttachments(formdefinationId) {
  
  const { data } = await GetRequest(`/api/FormDefination/GetFormDefinationAttachments/${formdefinationId}`);
  return data;
}

export async function GetFormDefinationAttachment(id) {
  
  const { data } = await GetRequest(`/api/FormDefination/GetFormDefinationAttachment/${id}`);
  return data;
}

export async function SaveFormAttachment(formData) {
   
  
  const { data } = await PostFile(`/api/FormDefination/SaveFormAttachment`, formData);
  return data;
};