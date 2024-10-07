import React, { useEffect, useMemo, useState,useContext } from 'react'

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
  CContainer


} from '@coreui/react'
import { CChartLine } from '@coreui/react-chartjs'
import { getStyle, hexToRgba } from '@coreui/utils'
import CIcon from '@coreui/icons-react'
import { cilNoteAdd } from '@coreui/icons'
import Lottie from 'lottie-react';

import ProcessAnimation from "../../content/animation/Process.json";

import { useDispatch, useSelector } from 'react-redux';
import { useNavigate } from "react-router-dom";

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


import { useTranslation } from "react-i18next";

import { Gridcolumns } from './DataGrid';
import { GetBranchList, GetBranch, SaveBranch, DeleteBranch, CreateNewBranch } from '../../lib/companyapi';
 

import AppBreadcrumb from 'src/components/AppBreadcrumb';

import DeleteModal from 'src/components/DeleteModal'

import BranchEditModal from './branchEditModal'


import { UrlContext } from 'src/lib/URLContext';


const BranchDefination = () => {

  const { t } = useTranslation();
  const [branchList, setBranchList] = useState([]);

  const [editBranch, setEditBranch] = useState(null);

  const [visible, setVisible] = useState(false)
  const [visibleDelete, setVisibleDelete] = useState(false)

  const { dispatch } = useContext(UrlContext); 

  const [saveStart, setsaveStart] = useState(false);
  const [deleteStart, setdeleteStart] = useState(false);

  const [saveError, setSaveError] = useState(null);


  //Bu sekilde redux tan okunacak
  const userToken = useSelector(state => state.userToken);

  async function LoadBranchList() {
    try {
      setVisible(false);
      var companyBranchList = await GetBranchList();

      if (companyBranchList.returnCode === 1) {
        setBranchList(companyBranchList.data);
      } else {
        setSaveError(companyBranchList.returnMessage);
      }

    } catch (error) {
      setSaveError(error.message);
    }

  }


  function SetLocationAdress(){

    dispatch({type:'reset'})

    dispatch({
      type: 'Add',
      payload: {pathname:"./BranchDefination",name:t("BranchDefination"),active:false}
    });   
  }


  useEffect(() => {

    try {
      SetLocationAdress();
      LoadBranchList(); 

    } catch (error) {
      console.log(error);
    }

  }, []);

  async function EditData(row) {
    var id = row.original["id"];
    setsaveStart(true);
    try {

      var editbranch = await GetBranch(id);
      if (editbranch.returnCode === 1) {
        setEditBranch(editbranch.data);
        setVisible(!visible);
      } else {
        setSaveError(editbranch.ReturnMessage);
      }
    } catch (error) {
      setSaveError(error.message);
    } finally {
      setsaveStart(false);
    }
  }

  async function DeleteData(row) {
    var id = row.original["id"];
    setdeleteStart(true);

    setSaveError(null);
    try {

      var editbranch = await GetBranch(id);
      if (editbranch.returnCode === 1) {
        setEditBranch(editbranch.data); 
        setVisibleDelete(true);
      } else {
        setSaveError(editbranch.ReturnMessage);
      }
    } catch (error) {
      setSaveError(error.message);
    } finally {
      setdeleteStart(false);
    }
  }

 
  

  async function DeleteDataServer() {
    try {
      var deleteBranchReturn = await DeleteBranch(editBranch.id);
      if (deleteBranchReturn.returnCode === 1) {
        setVisibleDelete(!visibleDelete);
        LoadBranchList();
      } else {
        setSaveError(deleteBranchReturn.returnMessage);
      }
    } catch (error) {
      setSaveError(error.message);
    }
  }

  async function NewBranch() {
    setsaveStart(false);
    setSaveError(null);

    try {
      var createBranchReturn = await CreateNewBranch();
      if (createBranchReturn.returnCode === 1) {

        setEditBranch(createBranchReturn.data);
      } else {
        setSaveError(createBranchReturn.ReturnMessage);
      }
    } catch (error) {
      setSaveError(error.message);
    }
    setVisible(!visible);
  }

  const table = useMaterialReactTable({
    columns: Gridcolumns,
    data: branchList,
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

  return (<>




    <DeleteModal title={t("BranchDelete")}
      message={t("DeleteMessage1")}
      message2={t("DeleteMessage2")}
      visiblep={visibleDelete}
      OnClickOk={() => DeleteDataServer()}
      OnClickCancel={() => setVisibleDelete(!visibleDelete)}
    ></DeleteModal>

    <BranchEditModal visiblep={visible} editBranchp={editBranch}  OnCloseModal={()=>setVisible(false)} setFormData={()=>LoadBranchList()} ></BranchEditModal>

   

    <CCard className="mb-4">
      <CCardBody>
        <CRow>
          <CCol>
            <CButtonGroup role="group">
              <CButton color="primary" shape='rounded-3' onClick={() => NewBranch()} > {t("AddNew")}</CButton>
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

  </>
  )
}

export default BranchDefination;