 
   
import  i18   from  '../../translation/i18.jsx'
 
import React from "react";
  
export  const Gridcolumns = [
  {
    accessorKey: 'id', //access nested data with dot notation
    header: 'Id',
    size: 50,
  },
  {
    accessorKey: 'companyName',
    header:  i18.t('CompanyName'),
    size: 150,
  }, 
  {
    accessorKey: 'adress',
    header:  i18.t('Adress'),
    size: 150,
  },
  {
    accessorKey: 'country',
    header:  i18.t('Country'),
    size: 150,
  },
  {
    accessorKey: 'isoCode',
    size: 150,
    
    header:  i18.t('IsoCode'),
  
  },
  {
    accessorKey: 'factoryNumber',
    header:  i18.t('FactoryNumber'),
    size: 150,
  },
  {
    accessorKey: 'email',
    header:  i18.t('Email'),
    size: 150,
  }, 
  {
    accessorKey: 'definationTypeName',
    header:  i18.t('DefinationTypeName'),
    size: 150,
  }, 
]; 

export default {
  Gridcolumns,
}


