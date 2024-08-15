import React, { useEffect, useState } from 'react'

import {
    CCard,
    CCardBody,
    CCol,
    CAlert,
    CRow,
    CFormLabel,
    CFormSelect,
    CForm


} from '@coreui/react'








import { useTranslation } from "react-i18next";

import { GetSector, GetFormDefinationBySector, GetFormGroupFormApp } from 'src/lib/formdef'
import DesingFormTemplate from './DesingFormTemplate'
import { GetFormMetaDataById, SaveMetaData } from 'src/lib/formMetaDataApi'
import { useSearchParams } from 'react-router-dom';
import DynamicForm from './dynamicForm';
import MenuButtons from './MenuButtons'
import LoadingAnimation from 'src/components/LoadingAnimation';


const DigitalFormEdit = () => {

    const { t } = useTranslation();

    const [searchParams, setSearchParams] = useSearchParams();
    const [saveError, setSaveError] = useState(null);
    const [customSectors, setCustomSectors] = useState([]);
    const [formMetaData, setformMetaData] = useState(null);
    const [formDefinationTypes, setFormDefinationTypes] = useState([]);
    const [formdefinationGroups, setFormDefinationGroups] = useState([]);
    const [useTemplate, setuseTemplate] = useState(false);
    const [loading, setLoading] = useState(false);
    const [formdefinationType, setformdefinationType] = useState(0);
    const [controlValues, setControlValues] = useState([{ fieldName: '', fieldValue: '' }]);

    async function LoadCustomSectors() {


        try {
            setLoading(true);
            setSaveError(null);
            var fSectorService = await GetSector();
            if (fSectorService.returnCode === 1) {
                setCustomSectors(fSectorService.data);
            } else {
                setSaveError(fSectorService.returnMessage);
            }
        } catch (error) {
            setSaveError(error.message);
            console.log(error);

        } finally {
            setLoading(false);
        }
    }


    async function GetformDefinationTypes(sectorid) {

        try {
            setLoading(true);

            setSaveError(null);
            var fSectorService = await GetFormDefinationBySector(sectorid);
            if (fSectorService.returnCode === 1) {
                setFormDefinationTypes(fSectorService.data);
            } else {
                setSaveError(fSectorService.returnMessage);
            }
        } catch (error) {
            setSaveError(error.message);
            console.log(error);

        } finally {
            setLoading(false);
        }
    }


    async function GetFormMetaData(id) {
        if (id === null) {
            return;
        }
        setLoading(true);
        setSaveError(null);
        try {
            var fSectorService = await GetFormMetaDataById(id);
            if (fSectorService.returnCode === 1) {
                setformMetaData(fSectorService.data);
                if (fSectorService.data.id != 0) {
                    await LoadCustomSectors();
                    await GetformDefinationTypes(fSectorService.data.customSectorId);

                    var formDef = formDefinationTypes.find(s => s.id === fSectorService.data.formDefinationId);

                    setformdefinationType(fSectorService.data.formDefinationId);
                   // setuseTemplate(formDef.desingTemplate)
                    GetGroupList(fSectorService.data.formDefinationId);

                }
            } else {
                setSaveError(fSectorService.returnMessage);
            }
        } catch (error) {
            setSaveError(error.message);
            console.log(error);

        } finally {
            setLoading(false);
        }
    }


    useEffect(() => {

        const id = searchParams.get('id');


        LoadCustomSectors();

        GetFormMetaData(id)



    }, []);
    async function GetGroupList(id) {

        if (id == 0) {
            return null;
        }
        try {
            setLoading(true);

            setSaveError(null);
            var getgroupsReturn = await GetFormGroupFormApp(id);

            if (getgroupsReturn.returnCode === 1) {
                setFormDefinationGroups(getgroupsReturn.data);

            } else {
                setSaveError(getgroupsReturn.returnMessage);

                return null;
            }
        } catch (error) {
            setSaveError(error.message);
        } finally {
            setLoading(false);
        }
    }

    function handleChange(event) {
        const { name, value } = event.target;
        setformMetaData({ ...formMetaData, [name]: value });

        if (name == 'customSectorId') {
            GetformDefinationTypes(value);
            formMetaData.customSectorId = value;
            setformMetaData(formMetaData);

        }

        if (name == 'formDefinationTypeId') {

            if (value != 0) {
                formMetaData.formDefinationId = value;
                setformMetaData(formMetaData);


                var formDef = formDefinationTypes.find(s => s.id == value);
                setLoading(true);
                setformdefinationType(formDef.id);
                setuseTemplate(formDef.desingTemplate)
                GetGroupList(formDef.id);
                setLoading(false);
            } else {
                setFormDefinationGroups([]);
            }
        }
    }

    function changeDynamicControlValues(name, value) {

        var findField = controlValues.find(s => s.fieldName === name);

        if (findField === undefined) {
            controlValues.push({ fieldName: name, fieldValue: value })
        } else {
            findField.fieldValue = value;
        }
        setControlValues(controlValues);
    }


    async function ClickMenuIcon(value) {

        setSaveError(null);
        if (value === "Save") {
            try {
                setLoading(true);

                var fsaveReturn = await SaveMetaData((formMetaData == null ? 0 : formMetaData.id), formdefinationType, controlValues);
                if (fsaveReturn.returnCode === 1) {
                    setformMetaData(fsaveReturn.data);
                } else {
                    setSaveError(fsaveReturn.returnMessage);
                }
            } catch (error) {
                setSaveError(error.message);
                console.log(error);

            } finally {
                setLoading(false);
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
                        <CFormSelect type="text" id='cmbCustomSector' value={formMetaData?.customSectorId} name="customSectorId"
                            onChange={e => handleChange(e)}     >

                            <option value="0">Seçiniz</option>
                            {customSectors.map(item => {
                                return (<option key={item.id} value={item.id}   >{item.name}</option>);
                            })}
                        </CFormSelect>
                    </CCol>

                </CRow>
                <CRow>
                    <CCol sm={3}>
                        <CFormLabel htmlFor="cmbFormDefinationType" className="form-label">{t("FormDefinationName")}</CFormLabel>
                    </CCol>
                    <CCol sm={3}>
                        <CFormSelect type="text" id='cmbFormDefinationType' value={formMetaData?.formDefinationId} name="formDefinationTypeId"
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
                    <LoadingAnimation loading={loading} size={"%40"}></LoadingAnimation>
                </CRow>
                <CForm>
                    <CRow>
                        <CCol>
                            {
                                formdefinationType != 0 ? <MenuButtons onButtonClick={(e) => ClickMenuIcon(e)}></MenuButtons> : ""

                            }
                        </CCol>
                    </CRow>

                    <CRow>
                        {

                            useTemplate ? <DesingFormTemplate onChangeData={(fieldname, e) => changeDynamicControlValues(fieldname, e)} formdefinationTypeIdp={formdefinationType}></DesingFormTemplate> :
                                <DynamicForm OnValueChanged={(fieldname, e) => changeDynamicControlValues(fieldname, e)} formdefinationTypeIdp={formdefinationType} formgroups={formdefinationGroups}></DynamicForm>

                        }

                    </CRow>

                    <CRow>
                        {
                            saveError != null ?
                                <CAlert color="warning">{saveError}</CAlert>
                                : ""
                        }
                    </CRow>

                    <CRow>
                        <CCol>
                            {
                                formdefinationType != 0 ? <MenuButtons onButtonClick={(e) => ClickMenuIcon(e)}></MenuButtons> : ""

                            }

                        </CCol>
                    </CRow>
                    <CRow>
                        <LoadingAnimation loading={loading} size={"%50"}></LoadingAnimation>
                    </CRow>
                </CForm>
            </CCardBody>
        </CCard>


        </>
    )

}

export default DigitalFormEdit;