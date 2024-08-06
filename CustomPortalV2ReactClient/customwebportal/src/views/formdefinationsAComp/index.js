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
    CCardText,


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





import { useTranslation } from "react-i18next";

import { GetAutoComlateField, GetAutoComlateFieldMaps } from 'src/lib/formdef'


import Gridcolumns from './DataGrid';

const FormdefinationsAutoComlate = () => {
    const { t } = useTranslation();

    const [saveError, setSaveError] = useState(null);
    const [searchParams, setSearchParams] = useSearchParams();
    const [autoComplateFieldMaps, setautoComplateFieldMaps] = useState([]);
    const [autoComplateField, setautoComplateField] = useState(null);
    const [filterSelectVisible,setfilterSelectVisible]=useState(true);


    async function getAutoComplateDefination(id) {

        try {
            var editformdefination = await GetAutoComlateField(id);

            if (editformdefination.returnCode === 1) {
                setautoComplateField(editformdefination.data);
            } else {
                setSaveError(editformdefination.returnMessage);
            }

        } catch (error) {
            setSaveError(error.message);
        }
    }

    async function getAutoComplateDefinationMaps(id) {

        try {
            var editformdefination = await GetAutoComlateFieldMaps(id);

            if (editformdefination.returnCode === 1) {
                setautoComplateFieldMaps(editformdefination.data);
            } else {
                setSaveError(editformdefination.returnMessage);
            }

        } catch (error) {
            setSaveError(error.message);
        }
    }

    useEffect(() => {

        const id = searchParams.get('formfieldid');
        getAutoComplateDefination(id);

        getAutoComplateDefinationMaps(id);



    }, []);

    const optionClick = (option, id) => {
        // EditGroupDefination(option === 'Delete', id);
    }

    function handleChange(event) {
        const { name, value } = event.target;
        if (name=='selectDefinationType'){

        }
    }

    const gridDigitalForm = Gridcolumns(optionClick);


    return (
        <> <CCard className="mb-4">
            <CCardHeader>{t("AutoComplateTitleDefination")}</CCardHeader>
            <CCardBody>
                <CRow>
                    <CCol sm={3}>
                        <CFormLabel htmlFor="selectDefinationType"
                            className="col-sm-6 col-form-label">{t("AutoComplateDefinationTable")}</CFormLabel>
                    </CCol>

                    <CCol sm={3}>
                        
                        <CFormSelect id='selectDefinationType' onChange={e => handleChange(e)} >
                            <option value='Adress'>{t("AdressDefination")}</option>
                            <option value='Product'>{t("ProductDefination")}</option>
                        </CFormSelect>
                    </CCol>
                    <CCol sm={3}>

                    </CCol>

                </CRow>

                <CRow>
                    <CCol sm={3}>
                        <CFormLabel htmlFor="selectFilterType"
                            className="col-sm-6 col-form-label">{t("AutoComplateDefinationFilter")}</CFormLabel>
                    </CCol>

                    <CCol sm={3}>
                        <CFormSelect id='selectFilterType'>
                            <option value=''></option>
                        </CFormSelect>
                    </CCol>
                    <CCol sm={3}>


                    </CCol>

                </CRow>

                <CRow>
                    <CCol>

                        <CCard className="mb-4">
                            <CCardHeader>{t("DefinationFieldList")}</CCardHeader>
                            <CCardBody>

                                <DataGrid columns={gridDigitalForm}
                                    rows={autoComplateFieldMaps}>

                                </DataGrid>
                            </CCardBody>
                        </CCard>

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

export default FormdefinationsAutoComlate;