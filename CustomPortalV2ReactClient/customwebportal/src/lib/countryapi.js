import axios from "axios"; 

async function   GetCountryForSale()
{ 
  var result= await axios.get(process.env.REACT_APP_APIURL+"/api/Country/GetForSaleProduct")
   
  return result;

}

async function   GetCountryCity(countryId)
{ 
  var result= await axios.get(process.env.REACT_APP_APIURL+'/api/Country/GetContryCity?countryId='+countryId)
   
  return result;

}

export {GetCountryForSale,GetCountryCity};