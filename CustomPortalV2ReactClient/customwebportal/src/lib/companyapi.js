import axios from "axios"; 
import {GetRequest,Post} from './Apibase'

async function   CreateCompany(company)
{ 
  var result= await  Post("/api/Company",company);
   
  return result;

}

async function GetBranchList(){
 
  const { data } =  await GetRequest('/api/Branch');
  return data;

}


async function CreateNewBranch() {
  const { data } =  await GetRequest('/api/Branch/CreateNewBranch');
  return data;
}
async function GetBranch(id){
  var lastToken=localStorage.getItem("LastToken");
  axios.defaults.headers.common['Authorization'] = `Bearer ${lastToken}`;

  const { data } =  await axios.get(process.env.REACT_APP_APIURL+`/api/Branch/${id}`);
  return data;

}
async function DeleteBranch(id){
  var lastToken=localStorage.getItem("LastToken");
  axios.defaults.headers.common['Authorization'] = `Bearer ${lastToken}`;

  const { data } =  await axios.get(process.env.REACT_APP_APIURL+`/api/Branch/DeleteBranch/${id}`);
  return data;

}
async function SaveBranch(savedata)
{
  var lastToken=localStorage.getItem("LastToken");
  axios.defaults.headers.common['Authorization'] = `Bearer ${lastToken}`;
 
  const { data } =  await axios.post(process.env.REACT_APP_APIURL+`/api/Branch`,savedata);
  return data;


}

export {CreateCompany,GetBranchList,GetBranch,SaveBranch,DeleteBranch,CreateNewBranch};