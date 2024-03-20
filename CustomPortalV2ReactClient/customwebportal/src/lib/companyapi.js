import axios from "axios"; 

async function   CreateCompany(company)
{ 
  var result= await axios.post(process.env.REACT_APP_APIURL+"/api/Company",company);
   
  return result;

}

export {CreateCompany};