import React, { useEffect, useState } from 'react'

import {
  CButton,
  CButtonGroup,
  CCard,
  CCardBody,
  CCol,
  CAlert,
  CRow, 

} from '@coreui/react'
 
import { GetUserList, GetUser,CreateNewUser,DeleteUser } from '../../lib/userapi';

import {
  MaterialReactTable,
  useMaterialReactTable,


} from 'material-react-table';

import {
  Box,
  IconButton


} from '@mui/material';
import {
  Edit as EditIcon,
  Delete as DeleteIcon,
} from '@mui/icons-material';
import { Gridcolumns } from './DataGrid';

import { useTranslation } from "react-i18next";

import  EditModal  from './editmodal';
import  DeleteModal from 'src/components/DeleteModal'

const WorkFlow = () => { 



    return (
        <>
        
        </>
    )

}

export default WorkFlow;
