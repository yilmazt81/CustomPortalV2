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

import { GetSector, GetFormDefinationBySector, GetFormGroup,GetFormGroupFormApp ,GetFormGroupFields } from 'src/lib/formdef'
import { GetFormMetaDataById } from 'src/lib/formMetaDataApi'
import { useSearchParams } from 'react-router-dom';
import DynamicForm from './dynamicForm';

const DigitalFormEdit = () => {

    const { t } = useTranslation();

    const [searchParams, setSearchParams] = useSearchParams();
    const [saveError, setSaveError] = useState(null);
    const [customSectors, setCustomSectors] = useState([]);
    const [formMetaData, setformMetaData] = useState(null);
    const [formDefinationTypes, setFormDefinationTypes] = useState([]);
    const [formdefinationGroups, setFormDefinationGroups] = useState([]);
    const [useTemplate, setuseTemplate] = useState(false);
    const [formdefinationType, setformdefinationType] = useState(0);

    async function LoadCustomSectors() {

        try {
            var fSectorService = await GetSector();
            if (fSectorService.returnCode === 1) {
                setCustomSectors(fSectorService.data);
            } else {
                setSaveError(fSectorService.returnMessage);
            }
        } catch (error) {
            setSaveError(error.message);
            console.log(error);

        }
    }


    async function GetformDefinationTypes(sectorid) {

        try {
            var fSectorService = await GetFormDefinationBySector(sectorid);
            if (fSectorService.returnCode === 1) {
                setFormDefinationTypes(fSectorService.data);
            } else {
                setSaveError(fSectorService.returnMessage);
            }
        } catch (error) {
            setSaveError(error.message);
            console.log(error);

        }
    }


    async function GetFormMetaData(id) {

        try {
            var fSectorService = await GetFormMetaDataById(id);
            if (fSectorService.returnCode === 1) {
                setformMetaData(fSectorService.data);
            } else {
                setSaveError(fSectorService.returnMessage);
            }
        } catch (error) {
            setSaveError(error.message);
            console.log(error);

        }
    }


    useEffect(() => {

        const id = searchParams.get('id');
        GetFormMetaData(id)

        LoadCustomSectors();


    }, []);
    async function GetGroupList(id) {

        if (id == 0) {
            return null;
        }
        var getgroupsReturn = await GetFormGroupFormApp(id);
 
        if (getgroupsReturn.returnCode === 1) {
            setFormDefinationGroups(getgroupsReturn.data);

        } else {
            setSaveError(getgroupsReturn.returnMessage);

            return null;
        }
    }

    function handleChange(event) {
        const { name, value } = event.target;
        setformMetaData({ ...formMetaData, [name]: value });

        if (name == 'customSectorId') {
            GetformDefinationTypes(value);
        }

        if (name == 'formDefinationTypeId') {

            if (value != 0) { 
                var formDef = formDefinationTypes.find(s => s.id == value);
                setformdefinationType(formDef.id);
                setuseTemplate(formDef.desingTemplate)
                GetGroupList(formDef.id);
            }else{
                setFormDefinationGroups([]);
            }

        }
    }


    return (
        <><CCard className="mb-4">
            <CCardBody>
                <CRow>

                    <CCol sm={3}>
                        <CFormLabel htmlFor="cmbCustomSector" className="form-label">{t("CustomSectorName")}</CFormLabel>
                    </CCol>
                    <CCol sm={3}>
                        <CFormSelect type="text" id='cmbCustomSector' name="customSectorId"
                            onChange={e => handleChange(e)}     >

                            <option value="0">Seçiniz</option>
                            {customSectors.map(item => {
                                return (<option key={item.id} value={item.id}  >{item.name}</option>);
                            })}
                        </CFormSelect>
                    </CCol>

                </CRow>
                <CRow>
                    <CCol sm={3}>
                        <CFormLabel htmlFor="cmbFormDefinationType" className="form-label">{t("CustomSectorName")}</CFormLabel>
                    </CCol>
                    <CCol sm={3}>
                        <CFormSelect type="text" id='cmbFormDefinationType' name="formDefinationTypeId"
                            onChange={e => handleChange(e)}      >

                            <option value="0">Seçiniz</option>
                            {formDefinationTypes.map(item => {
                                return (<option key={item.id} value={item.id}  >{item.formName}</option>);
                            })}
                        </CFormSelect>
                    </CCol>


                </CRow>
                <CRow>
                    {
                        saveError != null ?
                            <CAlert color="warning">{saveError}</CAlert>
                            : ""
                    }
                </CRow>

                <CRow>

                    <DynamicForm formdefinationTypeIdp={formdefinationType} formgroups={formdefinationGroups}></DynamicForm> :


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

export default DigitalFormEdit;