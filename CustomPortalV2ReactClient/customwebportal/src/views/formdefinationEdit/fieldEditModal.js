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

import { SaveFormDefinationField, GetUniqueFieldNames } from '../../lib/formdef';
import Lottie from 'lottie-react';
import PropTypes from 'prop-types';


import ProcessAnimation from "../../content/animation/Process.json";
import CoreUiAutoComplete from '../../components/CoreUiAutoComplete';


import { useTranslation } from "react-i18next";

const FieldEditModal = ({ visiblep, formGroupp, formDefinationFieldp, setFormData, fieldTypesp, fontTypesp }) => {

    const formRef = useRef(null); // Form referansını oluşturun

    const [visible, setvisible] = useState(visiblep);
    const [formdefinationField, setformdefinationField] = useState({ ...formDefinationFieldp });

    const [saveError, setSaveError] = useState(null);
    const [fieldTypes, setfieldTypes] = useState([]);
    const [uniqueTagNames, setuniqueTagNames] = useState([]);
    const [fontTypes, setfontTypes] = useState([]);

    const [formgroup, setformGroup] = useState(null);
    const [saveStart, setsaveStart] = useState(false);
    const [validated, setValidated] = useState(false)
    const [canSaveForm, setCanSaveForm] = useState(false);

    const { t } = useTranslation();

    function handleChange(event) {
        const { name, value } = event.target;

        setValidated(false);
        setformdefinationField({ ...formdefinationField, [name]: value });

    }
    function handleChangeSwich(event) {
        const { name, value } = event.target;

        var newValue = formdefinationField[name];
        newValue = !newValue;

        setformdefinationField({ ...formdefinationField, [name]: newValue });
    }

    async function LoaduniqueFormName() {
        var uniqueNamesResult = await GetUniqueFieldNames();
        if (uniqueNamesResult.returnCode === 1) {
            setuniqueTagNames(uniqueNamesResult.data);
        }
    }

    async function ClosedClick() {
        setvisible(false);
    }

    useEffect(() => {
        setSaveError(null);
        setvisible(visiblep);
        LoaduniqueFormName();
        setformdefinationField(formDefinationFieldp);
        setfieldTypes(fieldTypesp);
        setformGroup(formGroupp);
        setfontTypes(fontTypesp);
        console.log(formdefinationField);

        //LoadBranchList();

    }, [visiblep, formDefinationFieldp, fieldTypesp])

    const handleSubmit = (event) => {
        const form = event.currentTarget
        setCanSaveForm(form.checkValidity())
        if (canSaveForm === false) {

            event.preventDefault()
            event.stopPropagation()
        }
        setValidated(true)
    }

    async function SaveData() {

        try {

            try {
                setSaveError(null);
                setsaveStart(true);
                debugger;
                var savedefinationResult = await SaveFormDefinationField(formdefinationField);

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
                    <CModalTitle>{t("FormFieldModalTitle")}</CModalTitle>
                </CModalHeader>
                <CModalBody>
                    <CForm
                        className="row g-3 needs-validation"
                        validated={validated}
                        onSubmit={handleSubmit}
                        ref={formRef}
                    >
                        <CRow className="mb-12">
                            <CFormLabel htmlFor="txtGroupName" className="col-sm-3 col-form-label">{t("GroupName")}</CFormLabel>
                            <CCol sm={9}>
                                <CFormInput type="text" id='txtGroupName' value={formgroup?.name} readOnly={true} />
                            </CCol>
                        </CRow>
                        <CRow className="mb-12">
                            <CFormLabel htmlFor="txtFieldCaption" className="col-sm-3 col-form-label">{t("FieldCaption")}</CFormLabel>
                            <CCol sm={9}>
                                <CFormInput type="text"
                                    id='txtFieldCaption'
                                    name="fieldCaption"
                                    required
                                    onChange={e => handleChange(e)} value={formdefinationField?.fieldCaption} />
                            </CCol>
                        </CRow>
                        <CRow className="mb-12">
                            <CFormLabel htmlFor="txtTagName" className="col-sm-3 col-form-label">{t("TagName")}</CFormLabel>
                            <CCol sm={9}>
                                <CoreUiAutoComplete
                                    type="text"
                                    id='txtTagName'
                                    name="tagName"
                                    suggestions={uniqueTagNames}
                                    required
                                    onChange={handleChange} 
                                    value={formdefinationField?.tagName} />
                            </CCol>
                        </CRow>

                        <CRow className="mb-12">
                            <CFormLabel htmlFor="selectControlType" className="col-sm-3 col-form-label">{t("ControlType")}</CFormLabel>
                            <CCol sm={9}>
                                <CFormSelect id='selectControlType' name="controlType" required
                                    onChange={e => handleChange(e)} value={formdefinationField?.controlType}>
                                    <option value=""></option>
                                    {fieldTypes.map(item => {
                                        return (<option key={item.id} value={item.controlType} >{item.name}</option>);
                                    })}
                                </CFormSelect>

                            </CCol>

                        </CRow>

                        <CRow className="mb-12">
                            <CFormLabel htmlFor="txtorderNumber" className="col-sm-3 col-form-label">{t("OrderNumber")}</CFormLabel>
                            <CCol sm={9}>
                                <CFormInput type="number" id='txtorderNumber' name="orderNumber"
                                    onChange={e => handleChange(e)} value={formdefinationField?.orderNumber} />
                            </CCol>
                        </CRow>

                        <CRow className="mb-12">
                            <CFormLabel htmlFor="txtfontSize" className="col-sm-3 col-form-label">{t("FontSize")}</CFormLabel>
                            <CCol sm={9}>
                                <CFormInput type="number" id='txtfontSize' name="fontSize"
                                    onChange={e => handleChange(e)} value={formdefinationField?.fontSize} />
                            </CCol>
                        </CRow>

                        <CRow className="mb-12">
                            <CFormLabel htmlFor="selectFont" className="col-sm-3 col-form-label">{t("FontFamily")}</CFormLabel>
                            <CCol sm={9}>
                                <CFormSelect id='selectFont' name="fontFamily"
                                    onChange={e => handleChange(e)} value={formdefinationField?.fontFamily}>
                                    <option value=""></option>
                                    {fontTypesp.map(item => {
                                        return (<option key={item.id} value={item.fontName} >{item.fontName}</option>);
                                    })}
                                </CFormSelect>
                            </CCol>
                        </CRow>

                        <CRow className="mb-12">
                            <CCol sm={3}>
                                <CFormSwitch label={t("Bold")} name='bold' size='lg'
                                    onChange={e => handleChangeSwich(e)}
                                    checked={formdefinationField?.bold} ></CFormSwitch>

                            </CCol>
                            <CCol sm={3}>
                                <CFormSwitch label={t("Italic")} name='italic' size='lg'
                                    onChange={e => handleChangeSwich(e)}
                                    checked={formdefinationField?.italic} ></CFormSwitch>
                            </CCol>
                            <CCol sm={3}>
                                <CFormSwitch label={t("Mandatory")} name='mandatory' size='lg'
                                    onChange={e => handleChangeSwich(e)}
                                    checked={formdefinationField?.mandatory} ></CFormSwitch>
                            </CCol>

                        </CRow>
                        <CRow className='mb-12'>
                            <CCol sm={6}>
                                <CFormSwitch label={t("AutoComplate")} name='autoComplate' size='lg'
                                    onChange={e => handleChangeSwich(e)}

                                    checked={formdefinationField?.autoComplate} ></CFormSwitch>
                            </CCol>
                        </CRow>

                        <CRow className="mb-12">
                            <CFormLabel htmlFor="txttranslateLanguage" className="col-sm-3 col-form-label">{t("TranslateLanguage")}</CFormLabel>
                            <CCol sm={9}>
                                <CFormSelect id='txttranslateLanguage' name="translateLanguage"
                                    onChange={e => handleChange(e)} value={formdefinationField?.translateLanguage}>
                                    <option value=""></option>
                                    <option value="Turkce">Turkce</option>
                                    <option value="Ingilizce">Ingilizce</option>
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
                    </CForm >
                </CModalBody>

                <CModalFooter>
                    <CButton color="secondary" onClick={() => ClosedClick()}  >{t("Close")}</CButton>
                    <CButton color="primary" type='button' onClick={() => handleExternalSubmit()}    >{t("Save")} </CButton>
                </CModalFooter>

            </CModal>

        </>
    )


}


export default FieldEditModal;


FieldEditModal.propTypes = {
    visiblep: PropTypes.bool,
    formDefinationFieldp: PropTypes.object,
    fieldTypesp: PropTypes.array,
    formGroupp: PropTypes.object,
    fontTypesp: PropTypes.array,
};