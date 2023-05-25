import axios from "axios"; 

function LoginUser(logindata)
{
   
 
  console.log(logindata);
   axios.post('https://localhost:7232/api/Login', logindata)
  .then(function (response) {
    console.log(response);
  })
  .catch(function (error) {
    console.log(error);
  });
}


export {LoginUser};