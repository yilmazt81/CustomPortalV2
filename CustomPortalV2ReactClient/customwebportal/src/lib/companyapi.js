import axios from "axios"; 

async function   CreateCompany(company)
{ 
  var result= await axios.post(process.env.REACT_APP_APIURL+"/api/Company",company);
   
  return result;

}

async function GetBranchList(){
  var lastToken=localStorage.getItem("LastToken");
  axios.defaults.headers.common['Authorization'] = `Bearer ${lastToken}`;

  const { data } =  await axios.get(process.env.REACT_APP_APIURL+'/api/Branch');
  return data;

}

async function GetBranch(id){
  var lastToken=localStorage.getItem("LastToken");
  axios.defaults.headers.common['Authorization'] = `Bearer ${lastToken}`;

  const { data } =  await axios.get(process.env.REACT_APP_APIURL+`/api/Branch/${id}`);
  return data;

}

async function SaveBranch(savedata)
{
  var lastToken=localStorage.getItem("LastToken");
  axios.defaults.headers.common['Authorization'] = `Bearer ${lastToken}`;

debugger;
  const { data } =  await axios.post(process.env.REACT_APP_APIURL+`/api/Branch`,savedata);
  return data;


}

export {CreateCompany,GetBranchList,GetBranch,SaveBranch};