import React, { useEffect, useState,useContext } from 'react'

import {
    CButton,
    CButtonGroup,
    CCard,
    CCardBody,
    CCol,
    CAlert,
    CRow,
    CFormLabel,
    CFormInput,
    CFormSelect
} from '@coreui/react'


import { DataGrid } from '@mui/x-data-grid';

import { Link } from 'react-router-dom';
import { useSearchParams } from 'react-router-dom';


import LoadingAnimation from 'src/components/LoadingAnimation';


import { useTranslation } from "react-i18next";

import { GetSector, GetFormDefinationBySector } from 'src/lib/formdef'
import { GetBrachData,DeleteForm } from '../../lib/formMetaDataApi';

import GridColumsDigitalForm from './GridColumsDigitalForm';

import FormActionModal from './FormActionModal';
import FormCopyModal from './FormCopyModal';
import DeleteModal from 'src/components/DeleteModal';

import { UrlContext } from 'src/lib/URLContext';


const DigitalForms = () => {
    const { t } = useTranslation();
    const { dispatch } = useContext(UrlContext);

    const [saveError, setSaveError] = useState(null);
    const [customSectors, setCustomSectors] = useState([]);
    const [branchFormMetaData, setBranchFormMetaData] = useState([]);
    const [filterItem, setfilterItem] = useState({ customSectorId: 0, formDefinationTypeId: 0, companyName: "" });
    const [searchParams, setSearchParams] = useSearchParams();
    const [formDefinationTypes, setFormDefinationTypes] = useState([]);
    const [loading, setLoading] = useState(false);
    const [formActionModal, setFormActionModal] = useState(false);
    const [formCopyModal, setformCopyModal] = useState(false);
    const [formDeleteModal, setformDeleteModal] = useState(false);
    const [selectedFormId, setSelectedFormId] = useState(0);

    async function LoadCustomSectors() {

        try {
            setSaveError(null);
            setLoading(true);
            var fSectorService = await GetSector();
            if (fSectorService.returnCode === 1) {
                setCustomSectors(fSectorService.data);
            } else {
                setSaveError(fSectorService.returnMessage);
            }
        } catch (error) {
            setSaveError(error.message);
        } finally {

            setLoading(false);
        }
    }
    async function GetformDefinationTypes(sectorid) {

        try {
            setSaveError(null);
            setLoading(true);
            var fSectorService = await GetFormDefinationBySector(sectorid);
            if (fSectorService.returnCode === 1) {
                setFormDefinationTypes(fSectorService.data);
            } else {
                setSaveError(fSectorService.returnMessage);
            }
        } catch (error) {
            setSaveError(error.message);
        } finally {
            setLoading(false);
        }
    }

    async function GetBranchFormMetaData() {
        try {
            setSaveError(null);
            setLoading(true);

            var formmetaDataReturn = await GetBrachData();
            if (formmetaDataReturn.returnCode === 1) {
                setBranchFormMetaData(formmetaDataReturn.data);
            } else {
                setSaveError(formmetaDataReturn.returnMessage);
            }
        } catch (error) {
            setSaveError(error.message);
        } finally {
            setLoading(false);
        }
    }


    function handleChange(event) {
        const { name, value } = event.target;
        setfilterItem({ ...filterItem, [name]: value });
        if (name == 'customSectorId') {
            GetformDefinationTypes(value);
        }
    }


    function SetLocationAdress() {

        dispatch({ type: 'reset' })
    
        dispatch({
          type: 'Add',
          payload: { pathname: "/digitalForms", name: t("DigitalForms"), active: false }
        });
      }

      
    useEffect(() => {

        const id = searchParams.get('id');
        SetLocationAdress();
        LoadCustomSectors();

        GetBranchFormMetaData();

    }, []);

    async function DeleDocumentAccept(data){
   

        try {
            setSaveError(null);
            setLoading(true);
            var formmetaDataReturn = await DeleteForm(selectedFormId);
            if (formmetaDataReturn.returnCode === 1) {
                GetBranchFormMetaData(); 
                setformDeleteModal(false);

            } else {
                setSaveError(formmetaDataReturn.returnMessage);
            }
        } catch (error) {
            setSaveError(error.message);
        } finally {
            setLoading(false);
        }
    }

    const optionClick = (option, id) => {
        setSelectedFormId(id);
        if (option === "Download") {
            setFormActionModal(true);
        } else if (option === "Copy") {
            setformCopyModal(true);
        } else if (option === "Delete") {
            setformDeleteModal(true);
        }
    }


    const gridDigitalForm = GridColumsDigitalForm(optionClick);


    return (
        <> <CCard className="mb-8">
            <CCardBody>
                <CRow>
                    <CCol>
                        <CButtonGroup role="group">

                            <Link to={{
                                pathname: '/digitalFormEdit',
                                search: '?id=0',
                            }} >
                                <CButton color="primary" shape='rounded-3'   > {t("AddNewFormDefination")}</CButton>
                            </Link>

                        </CButtonGroup>
                    </CCol>
                </CRow>
                <CRow>

                    <CCol sm={2}>
                        <CFormLabel htmlFor="cmbCustomSector" className="form-label">{t("CustomSectorName")}</CFormLabel>
                    </CCol>
                    <CCol sm={2}>
                        <CFormSelect type="text" id='cmbCustomSector' name="customSectorId"
                            onChange={e => handleChange(e)}      >

                            <option value="0">Seçiniz</option>
                            {customSectors.map(item => {
                                return (<option key={item.id} value={item.id}  >{item.name}</option>);
                            })}
                        </CFormSelect>
                    </CCol>
                    <CCol sm={2}>
                        <CFormLabel htmlFor="cmbFormDefinationType" className="form-label">{t("FormDefinationName")}</CFormLabel>
                    </CCol>
                    <CCol sm={2}>
                        <CFormSelect type="text" id='cmbFormDefinationType' name="formDefinationTypeId"
                            onChange={e => handleChange(e)}      >

                            <option value="0">Seçiniz</option>
                            {formDefinationTypes.map(item => {
                                return (<option key={item.id} value={item.id}  >{item.formName}</option>);
                            })}

                        </CFormSelect>
                    </CCol>

                    <CCol sm={2}>
                        <CFormLabel htmlFor="txtCompanyName" className="form-label">{t("CompanyName")}</CFormLabel>

                    </CCol>
                    <CCol sm={2}>
                        <CFormInput type="text" id='txtCompanyName' name="companyName"
                            onChange={e => handleChange(e)} />
                    </CCol>


                </CRow>
                <CRow>

                    <CCol>
                        <CButton color="primary" shape='rounded-3'> {t("Filter")}</CButton>

                    </CCol>
                </CRow>
                <CRow>
                    <LoadingAnimation loading={loading} size={"%10"}></LoadingAnimation>
                </CRow>
                <CRow>

                    <CCol>

                        <DataGrid rows={branchFormMetaData}
                            columns={gridDigitalForm}
                            slotProps={{
                                toolbar: {
                                    showQuickFilter: true,
                                },
                            }}
                        // onRowClick={handleSelectFormGroupClick}
                        // onRowSelectionModelChange={(e) => GridGroupRowChange(e)}
                        />

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

            <FormActionModal foreditForm={false} visiblep={formActionModal} OnClose={() => setFormActionModal(false)} formidp={selectedFormId} ></FormActionModal>
            <FormCopyModal visiblep={formCopyModal} OnClose={() => setformCopyModal(false)} formidp={selectedFormId}></FormCopyModal>

            <DeleteModal

                visiblep={formDeleteModal}
                title={t("DeleteFormModalTitle")}
                message={t("DeleteQuestionLabel")} 
                OnClickOk={(data)=>DeleDocumentAccept(data)}
                OnClickCancel={()=>setformDeleteModal(false)}
                saveStart={loading}
                deleteError={saveError}
                message2=''

                >


            </DeleteModal>
        </>
    )

}

export default DigitalForms;