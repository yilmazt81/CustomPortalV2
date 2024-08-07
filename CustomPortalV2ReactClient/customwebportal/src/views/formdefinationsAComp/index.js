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
import { nlNL } from '@mui/x-data-grid/locales';




import { useTranslation } from "react-i18next";

import { GetAutoComlateField, GetAutoComlateFieldMaps, GetReflectionFields,GetFormDefinationField } from 'src/lib/formdef'
import { CreateDefinationTypes } from 'src/lib/companyAdressDef';


import Gridcolumns from './DataGrid';
import AutoComplateFieldModal from './autoComplateFieldModal'

const FormdefinationsAutoComlate = () => {
    const { t } = useTranslation();

    const [saveError, setSaveError] = useState(null);
    const [searchParams, setSearchParams] = useSearchParams();
    const [autoComplateFieldMaps, setautoComplateFieldMaps] = useState([]);
    const [autoComplateFieldMap, setautoComplateFieldMap] = useState({ id: 0,fieldCaption:'', formDefinationFieldId: 0, tagName: '', properyValue: '', properyValue2: '', properyValue3: '' });
    const [autoComplateField, setautoComplateField] = useState({ formDefinationFieldId: 0, id: 0, fieldName: '', complateObject: 'CompanyDefination', filterValue: '', relationalFieldName: '' });
    const [filterSelectVisible, setfilterSelectVisible] = useState(true);
    const [autoComplateFieldVisible, setautoComplateFieldVisible] = useState(false);
    const [adressDefinationTypes, setadressDefinationTypes] = useState([]);
    const [objecReflectionFields, setobjecReflectionFields] = useState([]);
    const [formdefinationField,setformdefinationField]=useState(null);

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



    useEffect(() => {

        const id = searchParams.get('formfieldid');

        autoComplateField.formDefinationFieldId = id;
     
        setautoComplateField(autoComplateField);
        getAutoComplateDefination(id);

        getAutoComplateDefinationMaps(id);
        GetFiel(id);

        LoadDefinationTypes();



    }, []);

    async function GetFiel(id) {
        

        try {
            var formdefinationFielReturn = await GetFormDefinationField(id);
            if (formdefinationFielReturn.returnCode === 1) {
                setformdefinationField(formdefinationFielReturn.data);
            } else {
                setSaveError(formdefinationFielReturn.returnMessage);
            }
        } catch (error) {
            setSaveError(error.message);
        }
    }
    const optionClick = (option, id) => {
        // EditGroupDefination(option === 'Delete', id);
    }

    function handleChange(event) {
        const { name, value } = event.target;
        setautoComplateField({ ...autoComplateField, [name]: value });
        if (name == 'selectDefinationType') {

            setfilterSelectVisible(value == 'Adress');

        }
    }

    const gridDigitalForm = Gridcolumns(optionClick);

    function ShowFilterRow() {
        if (filterSelectVisible) {
            return (
                <CRow>
                    <CCol sm={3}>
                        <CFormLabel htmlFor="selectFilterType"
                            className="col-sm-6 col-form-label">{t("AutoComplateDefinationFilter")}</CFormLabel>
                    </CCol>

                    <CCol sm={3}>
                        <CFormSelect id='selectFilterType' name='filterValue' onChange={e => handleChange(e)}>

                            <option value=''></option>
                            {adressDefinationTypes.map(item => {
                                return <option key={item.id} value={item.id}>{item.name}</option>
                            })}
                        </CFormSelect>
                    </CCol>
                    <CCol sm={3}>


                    </CCol>

                </CRow>
            )
        } else {
            return null;
        }
    }

    async function NewDefination() {
        try {
        
            autoComplateFieldMap.formDefinationFieldId = autoComplateField.formDefinationFieldId;

            var replectionFieldList = await GetReflectionFields(autoComplateField.complateObject);

            if (replectionFieldList.returnCode === 1) {
                setobjecReflectionFields(replectionFieldList.data);
                setautoComplateFieldVisible(true);
            } else {
                setSaveError(replectionFieldList.returnMessage);
            }

        } catch (error) {
            setSaveError(error.message);
        }
    }

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

                        <CFormSelect id='selectDefinationType' name='complateObject' onChange={e => handleChange(e)} >
                            <option value='CompanyDefination'>{t("AdressDefination")}</option>
                            <option value='ProductDefination'>{t("ProductDefination")}</option>
                            <option value='Country'>{t("CountryDefination")}</option>

                        </CFormSelect>
                    </CCol>
                    {ShowFilterRow()}
                    <CCol sm={3}>

                    </CCol>

                </CRow>
                <CRow>

                    <CCol>
                        <CButton color="primary" shape='rounded-3' onClick={() => NewDefination()} > {t("AddNewFormDefination")}</CButton>

                    </CCol>

                </CRow>


                <CRow>
                    <CCol>

                        <CCard className="mb-4">
                            <CCardHeader>{t("DefinationFieldList")}</CCardHeader>
                            <CCardBody>

                                <DataGrid columns={gridDigitalForm}
                                    rows={autoComplateFieldMaps} />

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

            <AutoComplateFieldModal visiblep={autoComplateFieldVisible}
                fieldListp={objecReflectionFields} 
                autoComplateFieldMapp={autoComplateFieldMap}
      
                formdefinationFieldp={formdefinationField} ></AutoComplateFieldModal>
        </>
    )

}

export default FormdefinationsAutoComlate;