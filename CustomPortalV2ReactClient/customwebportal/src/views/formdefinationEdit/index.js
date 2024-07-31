import React, { useEffect, useState } from 'react'

import {
  CButton,
  CButtonGroup,
  CCard,
  CCardBody,
  CCol,
  CRow,
  CAlert,

} from '@coreui/react'


import { useNavigate } from "react-router-dom";
import { Gridcolumns } from './DataGrid'
import {
  MaterialReactTable,
  useMaterialReactTable,
  MRT_ActionMenuItem
} from 'material-react-table';

import {
  Box,
  IconButton


} from '@mui/material';
import {
  Edit as EditIcon,
  Delete as DeleteIcon,
  DynamicForm
} from '@mui/icons-material';
import { useTranslation } from "react-i18next";

import DeleteModal from '../../components/DeleteModal';
import EditModal from './editmodal';

import { CreateNewFormDefination, GetFormDefinations, GetSector,GetFormDefination } from '../../lib/formdef';

const FormDefinationEdit = () => {
  const navigate = useNavigate();
  const { t } = useTranslation();


  const [visibleDelete, setVisibleDelete] = useState(false);
  const [formdefinationEdit, setFormDefinationEdit] = useState({ id: 0, formName: "" });
  const [formDefinations, setFormDefinations] = useState([]);
  const [customSectors, setCustomSectors] = useState([]);
  const [saveError, setSaveError] = useState(null);
  const [deleteError, setDeleError] = useState(null);
  const [deleteStart, setDeleteStart] = useState(null);
  const [Error, setError] = useState(null);

  const [visible, setVisible] = useState(false);

  async function LoadFormDefinations() {

    try {
      var fdefinationService = await GetFormDefinations();
      if (fdefinationService.returnCode === 1) {
        setFormDefinations(fdefinationService.data);
      } else {
        setError(fdefinationService.returnMessage);
      }
    } catch (error) {
      setError(error.message);
    }
  }

  async function LoadCustomSectors() {

    try {
      var fSectorService = await GetSector();
      if (fSectorService.returnCode === 1) {
        setCustomSectors(fSectorService.data);
      } else {
        setError(fSectorService.returnMessage);
      }
    } catch (error) {
      setError(error.message);
    }
  }



  useEffect(() => {


    LoadFormDefinations();

    LoadCustomSectors();

  }, []);

  async function EditData(row) {
    var id = row.original["id"];
   
    try {
      setDeleteStart(false);
      setVisible(false);
      setSaveError(null);
      var getdefinationReturn = await GetFormDefination(id);
      
      if (getdefinationReturn.returnCode === 1) {
        setFormDefinationEdit(getdefinationReturn.data);
        setVisible(true);
      } else {
        setSaveError(getdefinationReturn.returnMessage);
      }
    } catch (error) {
      setSaveError(error.message);
    } finally {
     // setdeleteStart(false);
    }  
  }
  async function DeleteAccepted(data) {
    setDeleteStart(true);
    /*
        try{
          if (data==null)
            return;
            var deleteUserReturn = await DeleteUser(useredit.id);
          
          if (deleteUserReturn.returnCode === 1) {
            LoadUserList();
            setVisibleDelete(false);
          } else {
            setSaveError(deleteUserReturn.returnMessage);
          }
        } catch (error) {
          setVisibleDelete(error.message);
        } finally {
          setDeleteStart(false);
        }
    */
    setDeleteStart(false);
  }

  async function NewDefination() {
    try {
      setVisible(false);
      var editformdefination = await CreateNewFormDefination();

      if (editformdefination.returnCode === 1) {
        setFormDefinationEdit(editformdefination.data);
        setVisible(true);
      } else {
        setSaveError(editformdefination.returnMessage);
      }
    } catch (error) {
      setSaveError(error.message);
    } finally {
      // setdeleteStart(false);
    }
  }
  
  async function SaveComplated() {
    LoadFormDefinations();
  }

  async function DeleteData(row) {
    var id = row.original["id"];
    /*
     try {
       setVisibleDelete(false);
       setVisible(false);
       var editUser = await GetUser(id);
       
       if (editUser.returnCode === 1) {
         setuseredit(editUser.data);
         setVisibleDelete(true);
       } else {
         setSaveError(editUser.returnMessage);
       }
     } catch (error) {
       setVisibleDelete(error.message);
     } finally {
       setDeleteStart(false);
     }
 */
  }
  const table = useMaterialReactTable({
    columns: Gridcolumns,
    data: formDefinations,
    enableRowActions: true,
    layoutMode: "grid",
    
    positionActionsColumn: 'last',
    renderRowActions: ({ row }) => (
      <Box sx={{ display: 'flex', flexWrap: 'nowrap', gap: '0rem' }}>
      <IconButton onClick={() => EditData(row)} >
        <EditIcon />
      </IconButton>
 
      <IconButton>
        <DynamicForm></DynamicForm>
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

      <EditModal visiblep={visible}
        formdefinationp={formdefinationEdit}
        customSectorList={customSectors}
        setFormData={() => SaveComplated()}></EditModal>
      <DeleteModal visiblep={visibleDelete}
        OnClickOk={(data) => DeleteAccepted(data)}
        title={t("UserDelete")}
        message={formdefinationEdit.FormName}
        message2={t("UserDeleteMessage")} saveError={deleteError} saveStart={deleteStart}></DeleteModal>



    </>
  )
}

export default FormDefinationEdit;