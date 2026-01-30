import React, { useEffect, useState, useRef } from 'react'

import {
    CButton,
    CCol,
    CAlert,
    CRow,
    CModal,
    CModalHeader,
    CModalTitle,
    CModalFooter,
    CModalBody,
    CFormLabel,
    CFormInput,
    CFormSwitch,
    CFormSelect,
    CForm,
    CListGroup,
    CListGroupItem

} from '@coreui/react'
import { DataGrid } from '@mui/x-data-grid';
import { CloneFormGroup, GetFormGroups, GetFormDefinationBySector, GetSector } from '../../lib/formdef.jsx';
import Lottie from 'lottie-react';



import ProcessAnimation from "../../content/animation/Process.json";
import datagridFormGroupCheckBox from './datagridFormGroupCheckBox';

import { useTranslation } from "react-i18next";

const CloneFormGroupModal = ({ visiblep, formdefinationP, setFormData, setClosed }) => {

    const formRef = useRef(null); // Form referansını oluşturun

    const [visible, setvisible] = useState(visiblep);

    const [saveError, setSaveError] = useState(null);
    const [formdefinations, setformdefinations] = useState(null);
    const [customSectors, setcustomSectors] = useState([]);
    const [filterItem, setfilterItem] = useState({ customSectorId: 0, formDefinationTypeId: 0, targetFormDefinationId: formdefinationP?.id });


    const [formgroups, setformGroups] = useState(null);
    const [saveStart, setsaveStart] = useState(false);
    const [validated, setValidated] = useState(false)
    const [canSaveForm, setCanSaveForm] = useState(false);
    const [loading, setLoading] = useState(false);
    const [rowSelectionModel, setRowSelectionModel] = React.useState({
        type: 'include',
        ids: new Set(),
    });

    const { t } = useTranslation();


    const gridcolumnFormgroups = datagridFormGroupCheckBox();

    function handleChange(event) {
        const { name, value } = event.target;
        setfilterItem({ ...filterItem, [name]: value });
        if (name == 'customSectorId') {
            GetSectorFormDefinations(value);
        }
        if (name == "formDefinationTypeId") {
            GetGroups(value);
        }

    }


    async function ClosedClick() {
        setvisible(false);
        setClosed();
    }


    useEffect(() => {
        setSaveError(null);
        setvisible(visiblep);
        LoadCustomSectors();

        // GetGroups();

        //setformdefinationField(formDefinationFieldp);
        // setfieldTypes(fieldTypesp);
        //setformGroup(formGroupp);
        //setfontTypes(fontTypesp);
        //console.log(formdefinationField);

        //LoadBranchList();

    }, [visiblep])

    const handleSubmit = (event) => {
        const form = event.currentTarget


        setCanSaveForm(form.checkValidity())
        if (canSaveForm === false) {

            event.preventDefault()
            event.stopPropagation()
        }
        setValidated(true)
    }

    async function GetGroups(formdefinationid) {

        try {
            var getgroupsReturn = await GetFormGroups(formdefinationid);

            if (getgroupsReturn.returnCode === 1) {
                setformGroups(getgroupsReturn.data);

            } else {
                setformGroups(getgroupsReturn.returnMessage);
            }
        } catch (error) {
            setSaveError(error.message);
        } finally {

        }
    }

    async function GetSectorFormDefinations(customSectorId) {

        try {
            var getgroupsReturn = await GetFormDefinationBySector(customSectorId);

            if (getgroupsReturn.returnCode === 1) {
                setformdefinations(getgroupsReturn.data);

            } else {
                setSaveError(getgroupsReturn.returnMessage);
            }
        } catch (error) {
            setSaveError(error.message);
        } finally {

        }
    }



    async function LoadCustomSectors() {

        try {
            setSaveError(null);
            setLoading(true);
            var fSectorService = await GetSector();
            if (fSectorService.returnCode === 1) {
                setcustomSectors(fSectorService.data);
            } else {
                setSaveError(fSectorService.returnMessage);
            }
        } catch (error) {
            setSaveError(error.message);
        } finally {

            setLoading(false);
        }
    }

    async function SaveData() {

        try {

            try {
                setSaveError(null);
                setsaveStart(true);
                filterItem.targetFormDefinationId = formdefinationP?.id;
                filterItem.formGroupList = rowSelectionModel;
                setfilterItem(filterItem);
             
                var savedefinationResult = await CloneFormGroup(filterItem);

                if (savedefinationResult.returnCode === 1) {
                    setFormData(savedefinationResult.data);
                    setvisible(false);
                } else {
                    setSaveError(savedefinationResult.returnMessage);
                } 

            } catch (error) {
                setSaveError(error.message);
            } finally {

                setsaveStart(false);
            }

        } catch (error) {
            setSaveError(error.message);
        } finally {

            setsaveStart(false);
        }

    }

    const handleExternalSubmit = () => {
        SaveData();
         /* if (formRef.current) {
             formRef.current.dispatchEvent(new Event('submit', { cancelable: true, bubbles: true }));
         }
 
         if (canSaveForm) {
             SaveData();
         }*/
    };

    return (

        <>

            <CModal
                backdrop="static"
                visible={visible}
                onClose={() => ClosedClick()}

            >
                <CModalHeader>
                    <CModalTitle>{t("CloneFormGroupModalTitle")}</CModalTitle>
                </CModalHeader>
                <CModalBody>
                    <CForm
                        className="row g-3 needs-validation"
                        validated={validated}
                        onSubmit={handleSubmit}
                        ref={formRef}
                    >


                        <CRow className="mb-12">

                            <CFormLabel htmlFor="cmbCustomSector" className="col-sm-3 col-form-label">{t("CustomSectorName")}</CFormLabel>

                            <CCol sm={9}>

                                <CFormSelect type="text" id='cmbCustomSector' name="customSectorId"
                                    onChange={e => handleChange(e)}      >

                                    <option value="0">Seçiniz</option>
                                    {customSectors?.map(item => {
                                        return (<option key={item.id} value={item.id}  >{item.name}</option>);
                                    })}
                                </CFormSelect>
                            </CCol>
                        </CRow>
                        <CRow className="mb-12">
                            <CFormLabel htmlFor="lblFormdefinationName" className="col-sm-3 col-form-label">{t("FormName")}</CFormLabel>
                            <CCol sm={9}>


                                <CFormSelect type="text" id='cmbFormDefinationType' name="formDefinationTypeId"
                                    onChange={e => handleChange(e)}      >
                                    <option value="0">Seçiniz</option>
                                    {formdefinations?.map(item => {
                                        return (<option key={item.id} value={item.id}  >{item.formName}</option>);
                                    })}

                                </CFormSelect>

                            </CCol>
                        </CRow>
                        <CRow className="mb-12">

                            <DataGrid columns={gridcolumnFormgroups}
                                checkboxSelection
                                rows={formgroups}

                                disableColumnFilter
                                disableColumnMenu
                                initialState={{
                                    pagination: { paginationModel: { pageSize: 5 } },
                                }}
                                onRowSelectionModelChange={(newRowSelectionModel) => {
                                    setRowSelectionModel(newRowSelectionModel);
                                }}
                                rowSelectionModel={rowSelectionModel}
                            ></DataGrid>
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
                    </CForm >
                </CModalBody>

                <CModalFooter>
                    <CButton color="secondary" onClick={() => ClosedClick()}  >{t("Close")}</CButton>
                    <CButton color="primary" type='button' onClick={() => handleExternalSubmit()}    >{t("Copy")} </CButton>
                </CModalFooter>

            </CModal>

        </>
    )


}


export default CloneFormGroupModal;




