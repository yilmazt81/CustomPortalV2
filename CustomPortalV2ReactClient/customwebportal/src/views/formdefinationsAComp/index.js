import React, { useEffect, useState } from 'react'

import {
    CButton,
    CCard,
    CCardBody,
    CCardHeader,
    CCol,
    CAlert,
    CRow,
    CFormLabel,
    CFormSelect,


} from '@coreui/react'


import { DataGrid } from '@mui/x-data-grid';

import { useSearchParams } from 'react-router-dom';




import { useTranslation } from "react-i18next";

import { GetAutoComlateField, GetAutoComlateFieldMaps, GetReflectionFields,GetFormDefinationField,DeleteAutoComplateFieldMap } from 'src/lib/formdef'
import { CreateDefinationTypes } from 'src/lib/companyAdressDef';

import DeleteModal from 'src/components/DeleteModal';

import Gridcolumns from './DataGrid';
import AutoComplateFieldModal from './autoComplateFieldModal'

const FormdefinationsAutoComlate = () => {
    const { t } = useTranslation();

    const [saveError, setSaveError] = useState(null);
    const [searchParams, setSearchParams] = useSearchParams();
    const [autoComplateFieldMaps, setautoComplateFieldMaps] = useState([]);
    const [autoComplateFieldMap, setautoComplateFieldMap] = useState({ id: 0,fieldCaption:'', formDefinationFieldId: 0, tagName: '', propertyValue1: '', propertyValue2: '', propertyValue3: '' });
    const [autoComplateField, setautoComplateField] = useState({ formDefinationFieldId: 0, id: 0, fieldName: '', complateObject: 'CompanyDefination', filterValue: '', relationalFieldName: '' });
    const [filterSelectVisible, setfilterSelectVisible] = useState(true);
    const [deletemodalVisible,setDeleteModalVisible]=useState(false);
    const [autoComplateFieldVisible, setautoComplateFieldVisible] = useState(false);
    const [adressDefinationTypes, setadressDefinationTypes] = useState([]);
    const [objecReflectionFields, setobjecReflectionFields] = useState([]);
    const [formdefinationField,setformdefinationField]=useState(null);
    const [formdefinationTypeId,setFormDefinationTypeId]=useState(0);

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
        setFormDefinationTypeId(id);
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
        setDeleteModalVisible(false);
        setautoComplateFieldVisible(false);
            
        var selectedD= autoComplateFieldMaps.find(s=>s.id==id);
        setautoComplateFieldMap(selectedD);
        if (option==='Delete'){
            setDeleteModalVisible(true);

        }else{
            setautoComplateFieldVisible(true);
        }
        // EditGroupDefination(option === 'Delete', id);
    }

    function handleChange(event) {
        const { name, value } = event.target;
        debugger;
        setautoComplateField({ ...autoComplateField, [name]: value });
        if (name == 'complateObject') {
         
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
                        <CFormSelect id='selectFilterType' name='filterValue' value={autoComplateField?.filterValue} onChange={e => handleChange(e)}>

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
        
            setDeleteModalVisible(false);
            autoComplateFieldMap.formDefinationFieldId = autoComplateField.formDefinationFieldId;

            var replectionFieldList = await GetReflectionFields(autoComplateField.complateObject);
               
            if (replectionFieldList.returnCode === 1) {
                setobjecReflectionFields(replectionFieldList.data);
                setautoComplateFieldMap({ id: 0,fieldCaption:'', formDefinationFieldId: autoComplateFieldMap.formDefinationFieldId , tagName: '', propertyValue1: '', propertyValue2: '', propertyValue3: '' });
                setautoComplateFieldVisible(true);
            } else {
                setSaveError(replectionFieldList.returnMessage);
            }

        } catch (error) {
            setSaveError(error.message);
        }
    }


    async function DeleteAutoComplateMaps() {

        

        try {
        
       
            
            var replectionFieldList = await DeleteAutoComplateFieldMap(formdefinationField.id,autoComplateFieldMap.id);
               
            if (replectionFieldList.returnCode === 1) {
             
                setDeleteModalVisible(false);
                getAutoComplateDefinationMaps(formdefinationTypeId);
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
                autoComplateFieldp={autoComplateField}
                formdefinationFieldp={formdefinationField} 
                setFormData ={()=> getAutoComplateDefinationMaps(formdefinationTypeId)}></AutoComplateFieldModal>


        <DeleteModal deleteError={saveError} visiblep={deletemodalVisible}
         message={autoComplateFieldMap.fieldCaption}
         message2={t("AutoComplateMapDeleteMessage")}
         OnClickOk={()=> DeleteAutoComplateMaps()}
        ></DeleteModal>
        </>
    )

}

export default FormdefinationsAutoComlate;