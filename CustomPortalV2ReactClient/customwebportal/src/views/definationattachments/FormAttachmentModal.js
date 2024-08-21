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
    CFormSwitch,
    CForm,

} from '@coreui/react'

import { SaveFormAttachment } from '../../lib/formdef';
import Lottie from 'lottie-react';
import PropTypes, { func } from 'prop-types';


import ProcessAnimation from "../../content/animation/Process.json";


import { useTranslation } from "react-i18next";


const FormAttachmentModal = ({ visiblep, formdefinationp, formAttachmentp, fontTypesp,setFormData, OnCloseModal }) => {


    const [visible, setvisible] = useState(visiblep);
    const [formdefinationAttachment, setformdefinationAttachment] = useState({ ...formAttachmentp });

    const [saveError, setSaveError] = useState(null);

    const [saveStart, setsaveStart] = useState(false);
    const [templateFile, settemplateFile] = useState(null);

    const { t } = useTranslation();

    function handleChange(event) {
        const { name, value } = event.target;

        setformdefinationAttachment({ ...formdefinationAttachment, [name]: value });

    }
    function handleFileChange(event) {
        const { files } = event.target;
 
        if (files) {

            settemplateFile(files[0]);
        }
    };

    async function ClosedClick() {
        OnCloseModal(false); 
    }


    useEffect(() => {
        setSaveError(null);
        setvisible(visiblep);
        settemplateFile(null);
        setformdefinationAttachment(formAttachmentp);


    }, [visiblep, formdefinationp, formAttachmentp, fontTypesp])

    async function SaveData() {

        try {

            try {
                setSaveError(null);

                const formData = new FormData();
                formData.append("Id", formdefinationAttachment.id);
                formData.append("FormName", formdefinationAttachment.formName);
                formData.append("FontSize", formdefinationAttachment.fontSize);
                formData.append("Templatefile", templateFile);
                formData.append("FormDefinationId", formdefinationp.id);
                formData.append("Bold", formdefinationAttachment.bold);
                formData.append("Italic", formdefinationAttachment.italic);
                formData.append("FontFamily", formdefinationAttachment.fontFamily);
                formData.append("Active", formdefinationAttachment.active);
 
                setsaveStart(true);

                var savedefinationResult = await SaveFormAttachment(formData);
                debugger;
                if (savedefinationResult.returnCode === 1) {
                    setFormData(savedefinationResult.data);
                    OnCloseModal();
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

    function handleChangeSwich(event) {
        const { name, value } = event.target;

        var newValue = formdefinationAttachment[name];
        newValue = !newValue;

        setformdefinationAttachment({ ...formdefinationAttachment, [name]: newValue });

    }

    return (

        <>
            <CModal
                backdrop="static"
                visible={visible}
                onClose={() => ClosedClick()}

            >
                <CModalHeader>
                    <CModalTitle>{t("FormDefinationAttachmentModalTitle")}</CModalTitle>
                </CModalHeader>
                <CModalBody>
                    <CForm>

                        <CRow className="mb-12">
                            <CFormLabel htmlFor="txtFormName" className="col-sm-3 col-form-label">{t("FormName")}</CFormLabel>
                            <CCol sm={9}>
                                <CFormInput type="text" id='txtFormName' readOnly={true} name="formName" value={formdefinationp?.formName} />
                            </CCol>
                        </CRow>
                        <CRow className="mb-12">
                            <CFormLabel htmlFor="txtformName" className="col-sm-3 col-form-label">{t("FormName")}</CFormLabel>
                            <CCol sm={9}>
                                <CFormInput type="text" id='txtformName' name="formName"
                                    onChange={e => handleChange(e)} value={formdefinationAttachment?.formName} />
                            </CCol>
                        </CRow>

                        <CRow className="mb-12">
                            <CFormLabel htmlFor="frmTemplatePath" className="col-sm-3 col-form-label">{t("TemplatePath")}</CFormLabel>
                            <CCol sm={9}>
                                <CFormInput type="file" id="frmTemplatePath" accept='.docx,.xlsx,.pdf,.xml' onChange={(e) => handleFileChange(e)} />
                            </CCol>
                        </CRow>
                        <CRow className="mb-12">
                            <CFormLabel htmlFor="txtfontSize" className="col-sm-3 col-form-label">{t("FontSize")}</CFormLabel>
                            <CCol sm={9}>
                                <CFormInput type="number" id='txtfontSize' name="fontSize"
                                    onChange={e => handleChange(e)} value={formdefinationAttachment?.fontSize} />
                            </CCol>
                        </CRow>

                        <CRow className="mb-12">
                            <CFormLabel htmlFor="selectFont" className="col-sm-3 col-form-label">{t("FontFamily")}</CFormLabel>
                            <CCol sm={9}>
                                <CFormSelect id='selectFont' name="fontFamily"
                                    onChange={e => handleChange(e)} value={formdefinationAttachment?.fontFamily}>
                                    <option value=""></option>
                                    {fontTypesp.map(item => {
                                        return (<option key={item.id} value={item.fontName} >{item.fontName}</option>);
                                    })}
                                </CFormSelect>
                            </CCol>
                        </CRow>

                        <CRow>

                            <CCol sm={4}>
                                <CFormSwitch label={t("Bold")} name='bold' size='lg'
                                    onChange={e => handleChangeSwich(e)}
                                    checked={formdefinationAttachment?.bold} ></CFormSwitch>

                            </CCol>
                            <CCol sm={4}>
                                <CFormSwitch label={t("Italic")} name='italic' size='lg'
                                    onChange={e => handleChangeSwich(e)}
                                    checked={formdefinationAttachment?.italic} ></CFormSwitch>

                            </CCol>
                            <CCol sm={4}>
                                <CFormSwitch label={t("Active")} name='active' size='lg'
                                    onChange={e => handleChangeSwich(e)}
                                    checked={formdefinationAttachment?.active} ></CFormSwitch>

                            </CCol>
                        </CRow>

                    </CForm>
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


export default FormAttachmentModal;


FormAttachmentModal.propTypes = {
    visiblep: PropTypes.bool,
    formAttachmentp: PropTypes.object,
    formdefinationp: PropTypes.object,
    fontTypesp:PropTypes.array,
};