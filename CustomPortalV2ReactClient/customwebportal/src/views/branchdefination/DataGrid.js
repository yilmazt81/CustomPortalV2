import React,{useEffect,useMemo } from 'react'

import {
    MaterialReactTable,
    useMaterialReactTable,
    MRT_ColumnDef,
    MRT_GlobalFilterTextField,
    MRT_ToggleFiltersButton,
  } from 'material-react-table';
  
  import {
    Box,
    Button,
    ListItemIcon,
    MenuItem,
    Typography,
    lighten,
  } from '@mui/material';
   
import  i18   from  '../../translation/i18'
 

 
  
  export  const Gridcolumns = [
    {
      accessorKey: 'id', //access nested data with dot notation
      header: 'Id',
      size: 50,
    },
    {
      accessorKey: 'name',
      header:  i18.t('BranchName'),
      size: 150,
    },
    {
      accessorKey: 'userRuleName',
      header:  i18.t('UserRuleName'),
      size: 150,
    },

    {
      accessorKey: 'email', //normal accessorKey
      header: i18.t('Email'),
      size: 200,
    },
    {
      accessorKey: 'phoneNumber',
      header: i18.t('PhoneNumber'),
      size: 150,
    },
    {
      accessorKey: 'branchPackageName',
      header: i18.t('PackedName')  ,
      size: 150,
    },
  ]; 

  export default {
    Gridcolumns,
}