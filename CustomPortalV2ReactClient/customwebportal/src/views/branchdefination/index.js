import React, { useEffect, useMemo, useState } from 'react'

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
  CCardText
  

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
import { GetBranchList, GetBranch, SaveBranch ,DeleteBranch} from '../../lib/companyapi';
import { getUserRoles, GetBranchpackages } from '../../lib/userapi';



const BranchDefination = () => {
  const navigate = useNavigate();

  const { t } = useTranslation();
  const [branchList, setBranchList] = useState([]);
  const [userRoles, setUserRoles] = useState([]);
  const [branchPackages, setBranchPackages] = useState([]);

  const [editBranch, setEditBranch] = useState(null);

  const [visible, setVisible] = useState(false)
  const [visibleDelete, setVisibleDelete] = useState(false)


  const [saveStart, setsaveStart] = useState(false);
  const [deleteStart, setdeleteStart] = useState(false);

  const [saveError, setSaveError] = useState(null);


  //Bu sekilde redux tan okunacak
  const userToken = useSelector(state => state.userToken);

  async function LoadBranchList() {
    try {

      var companyBranchList = await GetBranchList();

      if (companyBranchList.returnCode === 1) {
        setBranchList(companyBranchList.data);
      } else {
        setSaveError(companyBranchList.ReturnMessage);
      }

    } catch (error) {
      setSaveError(error.message);
    }

  }

  async function LoadUserRoles() {

    try {
      var userRolesList = await getUserRoles();
      if (userRolesList.returnCode === 1) {
        setUserRoles(userRolesList.data);
      } else {
        setSaveError(userRolesList.ReturnMessage);
      }
    } catch (error) {
      setSaveError(error.message);
    }

  }

  async function LoadBrachPackages() {

    try {
      var branchPackagesList = await GetBranchpackages();
      if (branchPackagesList.returnCode === 1) {
        setBranchPackages(branchPackagesList.data);
      } else {
        setSaveError(branchPackagesList.ReturnMessage);
      }
    } catch (error) {
      setSaveError(error.message);
    }

  }
  useEffect(() => {

    try {

      LoadBranchList();

      LoadUserRoles();

      LoadBrachPackages();

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
    try {
       
      var editbranch = await GetBranch(id);
      if (editbranch.returnCode === 1) {
        setEditBranch(editbranch.data);
        setVisibleDelete(!visibleDelete);
      } else {
        setSaveError(editbranch.ReturnMessage);
      }
    } catch (error) {
      setSaveError(error.message);
    } finally {
      setdeleteStart(false);
    }
  }

  function handleChange(event) {
    const { name, value } = event.target;
    setEditBranch({ ...editBranch, [name]: value });

  }

  async function SaveData() {

    try {
      var saveBranchReturn = await SaveBranch(editBranch);
      if (saveBranchReturn.returnCode === 1) {
        setVisible(!visible);
        LoadBranchList();
      } else {
        setSaveError(saveBranchReturn.ReturnMessage);
      }
    } catch (error) {
      setSaveError(error.message);
    }

  }

  async function DeleteDataServer(){
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
    setEditBranch({
      branchPackageId: 0,
      id: 0,
      mainCompanyId: 0,
      userRuleId: 0,
      companyAdmin: false,
      branchPackageName: "",
      deleted: false,
      userRuleName: "",
      sysAdmin: false,
    });
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


    <CModal
      backdrop="static"
      visible={visibleDelete}
      onClose={() => setVisibleDelete(false)}
      alignment='center'
      

    >
      <CModalHeader>
        <CModalTitle>{t("BranchDelete")}</CModalTitle>
      </CModalHeader>
      <CModalBody>

        <CRow>
            <CCardText>{t("DeleteMessage1")}</CCardText>
        </CRow>
        <CRow>
            <CCardText>{t("DeleteMessage2")}</CCardText>
        </CRow>

        <CRow xs={{ cols: 4 }}>
          <CCol> </CCol>
          <CCol>
            {
              saveStart ? <Lottie animationData={ProcessAnimation} loop={true} style={{ width: "80%", height: "80%" }} ></Lottie> : ""
            }
          </CCol>
          <CCol> </CCol>
          <CCol> </CCol>



        </CRow>
        <CRow>
          {saveError != null ?
            <CAlert color="warning">{saveError}</CAlert>
            : ""
          }
        </CRow>

      </CModalBody>
      <CModalFooter>
        <CButton color="secondary" onClick={() => setVisibleDelete(!visibleDelete)}>{t("Close")}</CButton>
        <CButton color="primary" onClick={() => DeleteDataServer()}>{t("Ok")}</CButton>
      </CModalFooter>

    </CModal>

    <CModal
      backdrop="static"
      visible={visible}
      onClose={() => setVisible(false)}

    >
      <CModalHeader>
        <CModalTitle>{t("BranchEdit")}</CModalTitle>
      </CModalHeader>
      <CModalBody>

        <CRow className="mb-12">
          <CFormLabel htmlFor="txtBranchName" className="col-sm-3 col-form-label">{t("BranchName")}</CFormLabel>
          <CCol sm={9}>
            <CFormInput type="text" id='txtBranchName' name="Name"
              onChange={e => handleChange(e)} value={editBranch?.name} />
          </CCol>
        </CRow>

        <CRow className="mb-12">
          <CFormLabel htmlFor="txtEMail" className="col-sm-3 col-form-label">{t("Email")}</CFormLabel>
          <CCol sm={9}>
            <CFormInput type="text" id='txtEMail' name="Email"
              onChange={e => handleChange(e)} value={editBranch?.email} />
          </CCol>
        </CRow>

        <CRow className="mb-12">
          <CFormLabel htmlFor="txtEMailPassword" className="col-sm-3 col-form-label">{t("EMailPassword")}</CFormLabel>
          <CCol sm={9}>
            <CFormInput type="Password" id='txtEMailPassword' name="EMailPassword"
              onChange={e => handleChange(e)} />
          </CCol>
        </CRow>

        <CRow className="mb-12">
          <CFormLabel htmlFor="txtPhoneNumber" className="col-sm-3 col-form-label">{t("PhoneNumber")}</CFormLabel>
          <CCol sm={9}>
            <CFormInput type="text" id='txtPhoneNumber' name="PhoneNumber"
              onChange={e => handleChange(e)} value={editBranch?.phoneNumber} />
          </CCol>
        </CRow>

        <CRow className="mb-12">
          <CFormLabel htmlFor="cmbPackageName" className="col-sm-3 col-form-label">{t("PackedName")}</CFormLabel>
          <CCol sm={9}>
            <CFormSelect type="text" id='cmbPackageName' name="branchPackageId"
              onChange={e => handleChange(e)} value={editBranch?.branchPackageId}    >

              <option value="0">Seçiniz</option>
              {branchPackages.map(item => {
                return (<option key={item.id} value={item.id}  >{item.name}</option>);
              })}
            </CFormSelect>

          </CCol>
        </CRow>

        <CRow className="mb-12">
          <CFormLabel htmlFor="cmbUserRuleName" className="col-sm-3 col-form-label">{t("UserRuleName")}</CFormLabel>
          <CCol sm={9}>
            <CFormSelect id='cmbUserRuleName' name="userRuleId" value={editBranch?.userRuleId}
              onChange={e => handleChange(e)}    >

              <option value="0">Seçiniz</option>
              {userRoles.map(item => {
                return (<option key={item.id} value={item.id}  >{item.name}</option>);
              })}
            </CFormSelect>
          </CCol>
        </CRow>

        <CRow xs={{ cols: 4 }}>
          <CCol> </CCol>
          <CCol>
            {
              saveStart ? <Lottie animationData={ProcessAnimation} loop={true} style={{ width: "80%", height: "80%" }} ></Lottie> : ""
            }
          </CCol>
          <CCol> </CCol>
          <CCol> </CCol>



        </CRow>
        <CRow>
          {saveError != null ?
            <CAlert color="warning">{saveError}</CAlert>
            : ""
          }
        </CRow>

      </CModalBody>

      <CModalFooter>
        <CButton color="secondary" onClick={() => setVisible(!visible)}>{t("Close")}</CButton>
        <CButton color="primary" onClick={() => SaveData()}>{t("Save")}</CButton>
      </CModalFooter>
    </CModal>

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