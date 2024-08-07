import React, { useEffect, useState } from 'react'

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
    CFormSelect,


} from '@coreui/react'

import { GetFormGroupFields,GetFormGroups } from '../../lib/formdef';
import Lottie from 'lottie-react';
import PropTypes from 'prop-types';


import ProcessAnimation from "../../content/animation/Process.json";


import { useTranslation } from "react-i18next";


const AutoComplateFieldModal = ({ visiblep, fieldListp, autoComplateFieldMapp,formdefinationFieldp, setFormData }) => {


    const [visible, setvisible] = useState(visiblep);
    const [formGroups, setformGroups] = useState([]);
    const [fieldList, setFieldList] = useState([]);
    const [formGroupFields,setFormGroupFields]=useState([]);

    const [autoComplateFieldMap, setautoComplateFieldMap] = useState({ ...autoComplateFieldMapp });

    const [saveError, setSaveError] = useState(null);


    const [saveStart, setsaveStart] = useState(false);

    const { t } = useTranslation();

    function handleChange(event) {
        const { name, value } = event.target;
        setautoComplateFieldMap({ ...autoComplateFieldMap, [name]: value });
        if (name=='formGroupId'){
            loadGroupFields(value);
        }

    }
    async function ClosedClick() {
        setvisible(false);
    }


    useEffect(() => {
        setSaveError(null);
        setvisible(visiblep);
        setautoComplateFieldMap(autoComplateFieldMapp);
    
        setFieldList(fieldListp);
        loadGroup();
        //LoadBranchList();

    }, [visiblep, autoComplateFieldMapp, fieldListp])

   
    async function loadGroup() {
        if (formdefinationFieldp==null)
            return;
      
        var formGroupReturn = await GetFormGroups(formdefinationFieldp?.formDefinationId);
       
        if (formGroupReturn.returnCode === 1) {
            setformGroups(formGroupReturn.data);
        } else {
            setSaveError(formGroupReturn.returnMessage);
        }
    }

    async function loadGroupFields(  groupId) {
        if (formdefinationFieldp==null)
            return;
      
        var formGroupReturn = await GetFormGroupFields(groupId);
       
        if (formGroupReturn.returnCode === 1) {
            setFormGroupFields(formGroupReturn.data);
        } else {
            setSaveError(formGroupReturn.returnMessage);
        }
    }

    

    async function SaveData() {

        try {

            try {
                setSaveError(null);
                debugger;
                setsaveStart(true);
               /* var savedefinationResult = await SaveFormDefination(formdefination);

                if (savedefinationResult.returnCode === 1) {
                    setFormData(savedefinationResult.data);
                    setvisible(false);
                } else {
                    setSaveError(savedefinationResult.returnMessage);
                }
*/

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


    return (

        <>
            <CModal
                backdrop="static"
                visible={visible}
                onClose={() => ClosedClick()}

            >
                <CModalHeader>
                    <CModalTitle>{t("AutoComplateFielDefinationTitle")}</CModalTitle>
                </CModalHeader>
                <CModalBody>

                    <CRow className="mb-12">
                        <CFormLabel htmlFor="selectGroupName" className="col-sm-4 col-form-label">{t("GroupName")}</CFormLabel>
                        <CCol sm={8}>
                            <CFormSelect   id='selectGroupName' name="formGroupId"
                                onChange={e => handleChange(e)} value={autoComplateFieldMap?.formGroupId}    >
 
                                {formGroups.map(item => {
                                    return (<option key={item.id} value={item.id}  >{item.name}</option>);
                                })}
                            </CFormSelect>
                        </CCol>
                    </CRow>


                    <CRow className="mb-12">
                        <CFormLabel htmlFor="cmbFieldName" className="col-sm-4 col-form-label">{t("TagName")}</CFormLabel>
                        <CCol sm={8}>
                            <CFormSelect type="text" id='cmbFieldName' name="tagName"
                                onChange={e => handleChange(e)} value={autoComplateFieldMap?.tagName}    > 
                                {formGroupFields.map(item => {
                                    return (<option key={item.id} value={item.tagName}  >{item.fieldCaption}</option>);
                                })}
                            </CFormSelect>

                        </CCol>
                    </CRow>

                
                    <CRow className="mb-12">
                        <CFormLabel htmlFor="cmbPropertyName1" className="col-sm-4 col-form-label">{t("ProperyValue")}</CFormLabel>
                        <CCol sm={8}>
                            <CFormSelect type="text" id='cmbPropertyName1' name="properyValue"
                                onChange={e => handleChange(e)} value={autoComplateFieldMap?.properyValue}    >
 
                                {fieldListp.map(item => {
                                     return (<option key={item.name} value={item.name}  >{item.caption}</option>);
                                })}
                            </CFormSelect>

                        </CCol>
                    </CRow>

                    <CRow className="mb-12">
                        <CFormLabel htmlFor="cmbPropertyName2" className="col-sm-4 col-form-label">{t("ProperyValue2")}</CFormLabel>
                        <CCol sm={8}>
                            <CFormSelect type="text" id='cmbPropertyName2' name="properyValue2"
                                onChange={e => handleChange(e)} value={autoComplateFieldMap?.properyValue2}    >

                                <option value=""></option>
                                {fieldListp.map(item => {
                                        return (<option key={item.name} value={item.name}  >{item.caption}</option>);
                                })}
                            </CFormSelect>

                        </CCol>
                    </CRow>

                    <CRow className="mb-12">
                        <CFormLabel htmlFor="cmbPropertyName3" className="col-sm-4 col-form-label">{t("ProperyValue3")}</CFormLabel>
                        <CCol sm={8}>
                            <CFormSelect type="text" id='cmbPropertyName3' name="properyValue3"
                                onChange={e => handleChange(e)} value={autoComplateFieldMap?.properyValue3}    >

                                <option value=""></option>
                                {fieldListp.map(item => {
                                    return (<option key={item.name} value={item.name}  >{item.caption}</option>);
                                })}
                            </CFormSelect>

                        </CCol>
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

                </CModalBody>

                <CModalFooter>
                    <CButton color="secondary" onClick={() => ClosedClick()}  >{t("Close")}</CButton>
                    <CButton color="primary" onClick={() => SaveData()}>{t("Save")}</CButton>
                </CModalFooter>
            </CModal>

        </>
    )


}


export default AutoComplateFieldModal;


AutoComplateFieldModal.propTypes = {
    visiblep: PropTypes.bool, 
    fieldListp: PropTypes.array,
    autoComplateFieldMapp: PropTypes.object,
    formdefinationFieldp:PropTypes.array,
};