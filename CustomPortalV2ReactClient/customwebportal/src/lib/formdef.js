import axios from "axios";


export async function CreateNewFormDefination() {
  var lastToken = localStorage.getItem("LastToken");
  axios.defaults.headers.common['Authorization'] = `Bearer ${lastToken}`;
  const { data } = await axios.get(process.env.REACT_APP_APIURL + `/api/FormDefination/CreateFormDefination`);
  return data;
};


export async function GetFormDefinations() {
  var lastToken = localStorage.getItem("LastToken");
  axios.defaults.headers.common['Authorization'] = `Bearer ${lastToken}`;
  const { data } = await axios.get(process.env.REACT_APP_APIURL + `/api/FormDefination`);
  return data;
};

export async function GetSector() {
  var lastToken = localStorage.getItem("LastToken");
  axios.defaults.headers.common['Authorization'] = `Bearer ${lastToken}`;
  const { data } = await axios.get(process.env.REACT_APP_APIURL + `/api/FormDefination/GetSectors`);
  return data;
};

export async function SaveFormDefination(formdefination) {
  var lastToken = localStorage.getItem("LastToken");
  axios.defaults.headers.common['Authorization'] = `Bearer ${lastToken}`;
  const { data } = await axios.post(process.env.REACT_APP_APIURL + `/api/FormDefination`, formdefination);
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
  var lastToken = localStorage.getItem("LastToken");
  axios.defaults.headers.common['Authorization'] = `Bearer ${lastToken}`;
  const { data } = await axios.get(process.env.REACT_APP_APIURL + `/api/FormDefination/CreateFormDefinationGroup?formDefinationId=${formdefinationId}`);
  return data;
};

export async function SaveFormGroup(formGroup) {
  var lastToken = localStorage.getItem("LastToken");
  axios.defaults.headers.common['Authorization'] = `Bearer ${lastToken}`;
  const { data } = await axios.post(process.env.REACT_APP_APIURL + `/api/FormDefination/SaveGroup`, formGroup);
  return data;
};
export async function GetFormGroupFields(formgroupid) {
  var lastToken = localStorage.getItem("LastToken");
  axios.defaults.headers.common['Authorization'] = `Bearer ${lastToken}`;
  axios.defaults.headers.common['Content-Type'] = 'application/json';
  const { data } = await axios.get(process.env.REACT_APP_APIURL + `/api/FormDefination/GetGroupFields/${formgroupid}`);
  return data;
};

async function GetRequest(apiAdress) {
  var lastToken = localStorage.getItem("LastToken");
  axios.defaults.headers.common['Authorization'] = `Bearer ${lastToken}`;
  axios.defaults.headers.common['Content-Type'] = 'application/json';
  const  data   = await axios.get(process.env.REACT_APP_APIURL + apiAdress);
  return data;
}
export async function GetFormGroup(id) {

  const { data } = await GetRequest(`/api/FormDefination/GetFormGroup/${id}`);
  return data;
};

export async function DeleteFormGroup(formdefinationid,groupid) {

  const { data } = await GetRequest(`/api/FormDefination/DeleteGroup?formDefinationId=${formdefinationid}&groupId=${groupid}`);
  return data;
};

export async function CreateNewGroupField(formdefinationid,groupid) {

  const { data } = await GetRequest(`api/FormDefination/CreateNewGroupField?formDefinationId=${formdefinationid}&formGroupId=${groupid}`);
  return data;
};
