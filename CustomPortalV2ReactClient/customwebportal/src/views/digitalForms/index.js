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
import { DataGrid } from '@mui/x-data-grid';

import { Link } from 'react-router-dom';
import { useSearchParams } from 'react-router-dom';





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

import { GetSector, GetFormDefinationBySector } from 'src/lib/formdef'
import { GetBrachData } from '../../lib/formMetaDataApi';

import GridColumsDigitalForm from './GridColumsDigitalForm';

const DigitalForms = () => {
    const { t } = useTranslation();

    const [saveError, setSaveError] = useState(null);
    const [customSectors, setCustomSectors] = useState([]);
    const [branchFormMetaData, setBranchFormMetaData] = useState([]);
    const [filterItem, setfilterItem] = useState({ customSectorId: 0, formDefinationTypeId: 0, companyName: "" });
    const [searchParams, setSearchParams] = useSearchParams();
    const [formDefinationTypes, setFormDefinationTypes] = useState([]);

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
        }
    }

    async function GetBranchFormMetaData() {
        try {
            var formmetaDataReturn = await GetBrachData();
            if (formmetaDataReturn.returnCode === 1) {
                setBranchFormMetaData(formmetaDataReturn.data);
            } else {
                setSaveError(formmetaDataReturn.returnMessage);
            }
        } catch (error) {
            setSaveError(error.message);
        }
    }


    function handleChange(event) {
        const { name, value } = event.target;
        setfilterItem({ ...filterItem, [name]: value });
        if (name == 'customSectorId') {
            GetformDefinationTypes(value);
        }
    }

    useEffect(() => {

        const id = searchParams.get('id');


        // LoadFormDefinations();

        LoadCustomSectors();

        GetBranchFormMetaData();

    }, []);

    const optionClick = (option, id) => {
        //    EditGroupDefination(option === 'Delete', id);
    }


    const gridDigitalForm = GridColumsDigitalForm(optionClick);


    return (
        <> <CCard className="mb-4">
            <CCardBody>
                <CRow>
                    <CCol>
                        <CButtonGroup role="group">

                            <Link to={{
                                pathname: '/digitalFormEdit',
                                search: '?id=0',
                            }} >
                                <CButton color="primary" shape='rounded-3'   > {t("AddNewFormDefination")}</CButton></Link>

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
                        <CButton color="primary" shape='rounded-3'   > {t("Filter")}</CButton>

                    </CCol>
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


        </>
    )

}

export default DigitalForms;