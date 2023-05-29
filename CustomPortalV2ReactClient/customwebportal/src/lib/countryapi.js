import axios from "axios"; 

async function   GetCountryForSale()
{ 
  var result= await axios.get('https://localhost:7232/api/Country/GetForSaleProduct')
   
  return result;

}

async function   GetCountryCity(countryId)
{ 
  var result= await axios.get('https://localhost:7232/api/Country/GetContryCity?countryId='+countryId)
   
  return result;

}

export {GetCountryForSale,GetCountryCity};