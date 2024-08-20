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

import { SaveFormVersion } from '../../lib/formdef';
import Lottie from 'lottie-react';
import PropTypes, { func } from 'prop-types';


import ProcessAnimation from "../../content/animation/Process.json";


import { useTranslation } from "react-i18next";


const FormVersionModal = ({ visiblep, formdefinationp,formdefinationVersionp, setFormData,OnCloseModal }) => {


    const [visible, setvisible] = useState(visiblep);
    const [formdefinationVersion, setformdefinationVersion] = useState({ ...formdefinationVersionp });

    const [saveError, setSaveError] = useState(null); 

    const [saveStart, setsaveStart] = useState(false);
    const [templateFile, settemplateFile] = useState(null);

    const { t } = useTranslation();

    function handleChange(event) {
        const { name, value } = event.target;

        setformdefinationVersion({ ...formdefinationVersion, [name]: value });

    }
    function handleFileChange(event) {
        const { files } = event.target;

        if (files) {

            settemplateFile(files[0]);
        }
    };

    async function ClosedClick() {
        setvisible(false);
        OnCloseModal();
    }


    useEffect(() => {
        setSaveError(null);
        setvisible(visiblep);
        settemplateFile(null); 
        setformdefinationVersion(formdefinationVersionp);
     

    }, [visiblep,  formdefinationVersionp])

    async function SaveData() {
       
        try {

            try {
                setSaveError(null);
           
                const formData = new FormData();
                formData.append("Id", formdefinationVersion.id);
                formData.append("FormLanguage", formdefinationVersion.formLanguage);
                formData.append("Active", formdefinationVersion.active); 
                formData.append("Templatefile", templateFile);
                formData.append("FormDefinationId", formdefinationp.id);
                
              
                setsaveStart(true);
                
                var savedefinationResult = await SaveFormVersion(formData);
         
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

    function handleChangeSwich(event) {
        const { name, value } = event.target;

        var newValue = formdefinationVersion[name];
        newValue = !newValue;

        setformdefinationVersion({ ...formdefinationVersion, [name]: newValue });

    }

    return (

        <>
            <CModal
                backdrop="static"
                visible={visible}
                onClose={() => ClosedClick()}

            >
                <CModalHeader>
                    <CModalTitle>{t("FormDefinationVersionModalTitle")}</CModalTitle>
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
                            <CFormLabel htmlFor="txtLanguage" className="col-sm-3 col-form-label">{t("FormLanguage")}</CFormLabel>
                            <CCol sm={9}>
                                <CFormInput type="text" id='txtLanguage' name="formLanguage"
                                    onChange={e => handleChange(e)} value={formdefinationVersion?.formLanguage} />
                            </CCol>
                        </CRow>
                         
                        <CRow className="mb-12">
                            <CFormLabel htmlFor="frmTemplatePath" className="col-sm-3 col-form-label">{t("TemplatePath")}</CFormLabel>
                            <CCol sm={9}>
                                <CFormInput type="file" id="frmTemplatePath"  accept='.docx,.xlsx,.pdf,.xml' onChange={(e) => handleFileChange(e)} />
                            </CCol>
                        </CRow>

                        <CRow>

                            <CCol sm={9}>
                                <CFormSwitch label={t("Active")} name='active' size='lg'
                                    onChange={e => handleChangeSwich(e)}
                                    checked={formdefinationVersion?.active} ></CFormSwitch>

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


export default FormVersionModal;


FormVersionModal.propTypes = {
    visiblep: PropTypes.bool,
    formdefinationVersionp: PropTypes.object,
    formdefinationp: PropTypes.object
};