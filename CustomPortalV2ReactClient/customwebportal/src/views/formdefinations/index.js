import React, { useEffect, useState } from 'react'

import {
  CButton,
  CButtonGroup,
  CCard,
  CCardBody,
  CCol,
  CRow,
  CAlert,
  CHeaderDivider

} from '@coreui/react'

import { useNavigate } from "react-router-dom";
import { DataGrid } from '@mui/x-data-grid'

import Gridcolumns from './DataGrid'


import { useTranslation } from "react-i18next";

import DeleteModal from '../../components/DeleteModal';
import EditModal from './editmodal';
import AppBreadcrumb from 'src/components/AppBreadcrumb';
 
import { CreateNewFormDefination, GetFormDefinations, GetSector, GetFormDefination } from '../../lib/formdef';

const FormDefination = () => {
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

  async function EditData(id) {


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
  


  async function ImportDefinationFromFile() {

  }

  async function ImportBaseDefination() {

  }
  

  const optionClick = (option, id) => {
    if (option == 'Edit') {
      EditData(id);
    } else if (option == 'Delete') {

    }
    //EditGroupDefination(option === 'Delete', id);
  }


  const gridColumns = Gridcolumns(optionClick);



  return (
    <>
 
     
      <CCard className="mb-4">
        <CCardBody>
          <CRow>
            <CCol>
              <CButtonGroup role="group">
                <CButton color="primary" shape='rounded-3' onClick={() => NewDefination()} > {t("AddNewFormDefination")}</CButton>
                <CButton color="primary" shape='rounded-3' onClick={() => ImportDefinationFromFile()} > {t("ImportFormDefinations")}</CButton>
                <CButton color="primary" shape='rounded-3' onClick={() => ImportBaseDefination()} > {t("ImportBaseDefination")}</CButton>
                <CButton color="primary" shape='rounded-3' onClick={() => ImportBaseDefination()} > {t("CreatePrivateType")}</CButton>

              </CButtonGroup>
            </CCol>
          </CRow>
          <CRow>
            <CCol>


              <div style={{ height: 450, width: '100%' }}>
                <DataGrid rows={formDefinations}
                  columns={gridColumns}
                
                />
              </div>
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

export default FormDefination;