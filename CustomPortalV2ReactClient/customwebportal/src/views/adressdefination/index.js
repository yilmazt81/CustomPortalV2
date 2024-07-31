import React, { useState, useEffect } from 'react'

import {
  CButton,
  CButtonGroup,
  CCard,
  CCardBody,
  CCol,
  CRow,
  CAlert
} from '@coreui/react'


import { useSelector } from 'react-redux';
import { useNavigate } from "react-router-dom";
import { useTranslation } from "react-i18next";
import { Gridcolumns } from './DataGrid'
import {
  MaterialReactTable,
  useMaterialReactTable,
  MenuItem

} from 'material-react-table';
import {
  Box,
  IconButton


} from '@mui/material';
import {
  Edit as EditIcon,
  Delete as DeleteIcon,
  AccessibilityNew,
  BorderAll,
} from '@mui/icons-material';
import {
  CreateDefinationTypes,
  NewCompanyDefination,
  GetUserCompanyDefinations,
  GetCompanyDefination,
  DeleteCompanyDefination
} from '../../lib/companyAdressDef';
import EditModal from './editmodal';
import DeleteModal from 'src/components/DeleteModal';
const AdresDefination = () => {
  const navigate = useNavigate();
  //Bu sekilde redux tan okunacak
  const userToken = useSelector(state => state.userToken);
  const { t } = useTranslation();

  const [visibleDelete, setVisibleDelete] = useState(false);
  const [visibleEdit, setVisibleEdit] = useState(false);

  const [saveError, setSaveError] = useState(null);

  const [adressDefinationList, setadressDefinationList] = useState([]);
  const [adressDefinationTypes, setadressDefinationTypes] = useState([]);
  const [updateAdressDefination, setupdateAdressDefination] = useState(null);
  const [deleteStart, setDeleteStart] = useState(false);



  async function NewDefination() {
    try {
      setSaveError(null);

      LoadDefinationTypes();
      var adresDefinationService = await NewCompanyDefination();
      if (adresDefinationService.returnCode === 1) {

        setupdateAdressDefination(adresDefinationService.data);
        setVisibleEdit(true);
      } else {
        setSaveError(adresDefinationService.returnMessage);
      }
    } catch (error) {
      setSaveError(error.message);
    }
  }


  async function LoadDefinationTypes() {

    try {
      var AdresDefinationService = await CreateDefinationTypes();
      if (AdresDefinationService.returnCode === 1) {
        setadressDefinationTypes(AdresDefinationService.data);
      } else {
        setSaveError(AdresDefinationService.returnMessage);
      }
    } catch (error) {
      setSaveError(error.message);
    }
  }

  async function LoadUserAdressDefinations() {

    try {
      var AdresDefinationService = await GetUserCompanyDefinations();
      if (AdresDefinationService.returnCode === 1) {
        setadressDefinationList(AdresDefinationService.data);
      } else {
        setSaveError(AdresDefinationService.returnMessage);
      }
    } catch (error) {
      setSaveError(error.message);
    }
  }

  useEffect(() => {

    try {
      LoadDefinationTypes();
      LoadUserAdressDefinations();


    } catch (error) {
      console.log(error);
    }

  }, []);


  async function EditData(row) {
    var id = row.original["id"];

    try {
      //setDeleteStart(false);

      setSaveError(null);
      setVisibleEdit(false);
      var getcompanyDefinationReturn = await GetCompanyDefination(id);

      if (getcompanyDefinationReturn.returnCode === 1) {
        setupdateAdressDefination(getcompanyDefinationReturn.data);
        setVisibleEdit(true);
      } else {
        setSaveError(getcompanyDefinationReturn.returnMessage);
      }
    } catch (error) {
      setSaveError(error.message);
    } finally {
      // setdeleteStart(false);
    }

  }

  async function DeleteAccepted(data) {
    setDeleteStart(true);

    try {
      setSaveError(null);

      if (data == null)
        return;
      var deleteCompanyReturn = await DeleteCompanyDefination(updateAdressDefination.id);

      if (deleteCompanyReturn.returnCode === 1) {
        LoadUserAdressDefinations();
        setVisibleDelete(false);
      } else {
        setSaveError(deleteCompanyReturn.returnMessage);
      }
    } catch (error) {
      setSaveError(error.message);
    } finally {
      //setDeleteStart(false);
    }

    setDeleteStart(false);
  }
  async function DeleteData(row) {
    var id = row.original["id"];

    try {
      setVisibleEdit(false);
      setVisibleDelete(false);
      var editCompanyDefination = await GetCompanyDefination(id);
      debugger;
      if (editCompanyDefination.returnCode === 1) {
        setupdateAdressDefination(editCompanyDefination.data);
        setVisibleDelete(true);
      } else {
        setSaveError(editCompanyDefination.returnMessage);
      }
    } catch (error) {
      setVisibleDelete(error.message);
    } finally {
      //setDeleteStart(false);
    }

  }
  const table = useMaterialReactTable({
    columns: Gridcolumns,
    data: adressDefinationList,
    enableRowActions: true,
    layoutMode: "grid",
    positionActionsColumn: 'last',
    renderRowActions: ({ row }) => (
   
        <Box ml={{ display: 'flex', flexWrap: 'nowrap', gap: '0rem' }}>
          <IconButton onClick={() => EditData(row)} >
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
                <CButton color="primary" shape='rounded-3' onClick={() => NewDefination()} > {t("AddNewFormDefination")}</CButton>
              </CButtonGroup>
            </CCol>
          </CRow>
          <CRow>
            <CCol>

              <MaterialReactTable table={table} enableColumnResizing={true}/>
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

      <EditModal adressdefinationp={updateAdressDefination}
        visiblep={visibleEdit}
        adresDefinationTypesp={adressDefinationTypes}
        setFormData={e => LoadUserAdressDefinations()}
      ></EditModal>

      <DeleteModal message={updateAdressDefination?.companyName}
        message2={t("CompanyDefinationDelete")}
        visiblep={visibleDelete}
        OnClickOk={(data) => DeleteAccepted(data)}
        saveStart={deleteStart}
        title={t("ModalCompanyDeleteTitle")}
        deleteError={saveError}
       
      ></DeleteModal>
    </>
  )
}

export default AdresDefination;