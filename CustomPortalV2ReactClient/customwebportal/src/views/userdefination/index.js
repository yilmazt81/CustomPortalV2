import React, { useEffect, useState } from 'react'

import {
  CAvatar,
  CButton,
  CButtonGroup,
  CCard,
  CCardBody,
  CCardFooter,
  CCardHeader,
  CCol,
  CAlert,
  CProgress,
  CRow,
  CTable,
  CTableBody,
  CTableDataCell,
  CTableHead,
  CTableHeaderCell,
  CTableRow,
  CModal,
  CModalHeader,
  CModalTitle,
  CModalFooter,
  CModalBody,
  CFormLabel,
  CFormInput,
  CFormSelect,
  CCardText,



} from '@coreui/react'
import { CChartLine } from '@coreui/react-chartjs'
import { getStyle, hexToRgba } from '@coreui/utils'
import CIcon from '@coreui/icons-react'

import { useDispatch, useSelector } from 'react-redux';
import { useNavigate } from "react-router-dom";
import { GetUserList, GetUser,CreateNewUser } from '../../lib/userapi';
import Lottie from 'lottie-react';

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
  IconButton


} from '@mui/material';
import {
  Edit as EditIcon,
  Delete as DeleteIcon,
  Email as EmailIcon,
  AlignHorizontalCenter,
} from '@mui/icons-material';
import ProcessAnimation from "../../content/animation/Process.json";
import { Gridcolumns } from './DataGrid';

import { useTranslation } from "react-i18next";

import  EditModal  from './editmodal';

const UserDefination = () => {
  const navigate = useNavigate();
  const { t } = useTranslation();

  //Bu sekilde redux tan okunacak
  const userToken = useSelector(state => state.userToken);
  const [users, setUsers] = useState([]);
  const [error, setError] = useState(null);
  const [useredit, setuseredit] = useState({id:0});
  const [saveError, setSaveError] = useState(null);
  const [visible, setVisible] = useState(false);
  async function LoadUserList() {

    try {
      var userList = await GetUserList();
      if (userList.returnCode === 1) {
        setUsers(userList.data);
      } else {
        setError(userList.ReturnMessage);
      }
    } catch (error) {
      setError(error.message);
    }

  }
  async function NewUser() {
    try {
      setVisible(false);
      var editUser = await CreateNewUser();
      
      if (editUser.returnCode === 1) {
        setuseredit(editUser.data);
        setVisible(true);
      } else {
        setSaveError(editUser.returnMessage);
      }
    } catch (error) {
      setSaveError(error.message);
    } finally {
     // setdeleteStart(false);
    }

   

  }

  async function EditData(row) { 
    var id = row.original["id"];
    
    try {
      setVisible(false);
      var editUser = await GetUser(id);
      
      if (editUser.returnCode === 1) {
        setuseredit(editUser.data);
        setVisible(true);
      } else {
        setSaveError(editUser.returnMessage);
      }
    } catch (error) {
      setSaveError(error.message);
    } finally {
     // setdeleteStart(false);
    }
    
  }

  async function DeleteData(row) {
    var id = row.original["id"];
   /* 
    try {
       
      var editUser = await GetUser(id);
      if (editUser.returnCode === 1) {
        setuseredit(editUser.data);
        setVisible(!visible);
      } else {
        setSaveError(editUser.returnMessage);
      }
    } catch (error) {
      setSaveError(error.message);
    } finally {
     // setdeleteStart(false);
    }*/
    
  }

  useEffect(() => {

    try {

      LoadUserList();


    } catch (error) {
      console.log(error);
    }

  }, []);


  const table = useMaterialReactTable({
    columns: Gridcolumns,
    data: users,
    enableRowActions: true,
    layoutMode: "grid",
    positionActionsColumn: 'last',
    renderRowActions: ({ row }) => (
      <Box sx={{ display: 'flex', flexWrap: 'nowrap', gap: '3px' }}>
        <IconButton onClick={() => EditData(row)}>
          <EditIcon />
        </IconButton>
        <IconButton onClick={() => DeleteData(row)}>
          <DeleteIcon />
        </IconButton>

      </Box>
    ),

  });

  return (
    <>
      
      <CCard className="mb-4">
        <CCardBody>
          <CRow>
            <CCol>
              <CButtonGroup role="group">
                <CButton color="primary" shape='rounded-3' onClick={() => NewUser()} > {t("AddNew")}</CButton>
              </CButtonGroup>
            </CCol>
          </CRow>
          <CRow>
            <CCol>

              <MaterialReactTable table={table} />
            </CCol>
          </CRow>
          <CRow>
            {
              saveError != null ?
                <CAlert color="warning">{saveError}</CAlert>
                : ""
            }
          </CRow>
        </CCardBody>
      </CCard>

      <EditModal user={useredit} visible={visible} ></EditModal>

    </>
  )
}

export default UserDefination;


