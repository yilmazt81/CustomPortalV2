import axios from "axios"; 

async function LoginUser(logindata)
{
   
 
  var result= await axios.post(process.env.REACT_APP_APIURL+'/api/Login',logindata);
    

  return result;
   
}


export {LoginUser};