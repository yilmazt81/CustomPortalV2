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

import { SaveFormDefination } from '../../lib/formdef';
import Lottie from 'lottie-react';
import PropTypes, { func } from 'prop-types';


import ProcessAnimation from "../../content/animation/Process.json";


import { useTranslation } from "react-i18next";


const EditModal = ({ visiblep, formdefinationp, customSectorList, setFormData }) => {


    const [visible, setvisible] = useState(visiblep);
    const [formdefination, setFormdefination] = useState({ ...formdefinationp });

    const [saveError, setSaveError] = useState(null);
    const [sectorList, setSectorList] = useState([]);

    const [saveStart, setsaveStart] = useState(false);
    const [templateFile, settemplateFile] = useState(null);

    const { t } = useTranslation();

    function handleChange(event) {
        const { name, value } = event.target;

        setFormdefination({ ...formdefination, [name]: value });

    }
    function handleFileChange(event) {
        const { files } = event.target;

        if (files) {

            settemplateFile(files[0]);
        }
    };

    async function ClosedClick() {
        setvisible(false);
    }


    useEffect(() => {
        setSaveError(null);
        setvisible(visiblep);
        settemplateFile(null);
        setFormdefination(formdefinationp);
        setSectorList(customSectorList);
        //LoadBranchList();

    }, [visiblep, formdefinationp, customSectorList])

    async function SaveData() {

        try {

            try {
                setSaveError(null);

                const formData = new FormData();
                formData.append("Id", formdefination.id);
                formData.append("FormName", formdefination.formName);
                formData.append("CustomSectorId", formdefination.customSectorId);
                formData.append("Deployed", formdefination.deployed);
                formData.append("Templatefile", templateFile);
                formData.append("MainCompanyId",formdefination.mainCompanyId);
                formData.append("DesingTemplate",formdefination.desingTemplate);
                 
                setsaveStart(true);
                var savedefinationResult = await SaveFormDefination(formData);

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

        var newValue = formdefination[name];
        newValue = !newValue;

        setFormdefination({ ...formdefination, [name]: newValue });

    }

    return (

        <>
            <CModal
                backdrop="static"
                visible={visible}
                onClose={() => ClosedClick()}

            >
                <CModalHeader>
                    <CModalTitle>{t("FormDefinationModalTitle")}</CModalTitle>
                </CModalHeader>
                <CModalBody>
                    <CForm>


                        <CRow className="mb-12">
                            <CFormLabel htmlFor="txtFormName" className="col-sm-3 col-form-label">{t("FormName")}</CFormLabel>
                            <CCol sm={9}>
                                <CFormInput type="text" id='txtFormName' name="formName"
                                    onChange={e => handleChange(e)} value={formdefination.formName} />
                            </CCol>
                        </CRow>


                        <CRow className="mb-12">
                            <CFormLabel htmlFor="cmbCustomSector" className="col-sm-3 col-form-label">{t("CustomSectorName")}</CFormLabel>
                            <CCol sm={9}>
                                <CFormSelect type="text" id='cmbCustomSector' name="customSectorId"
                                    onChange={e => handleChange(e)} value={formdefination?.customSectorId}    >

                                    <option value="0">Seçiniz</option>
                                    {sectorList.map(item => {
                                        return (<option key={item.id} value={item.id}  >{item.name}</option>);
                                    })}
                                </CFormSelect>

                            </CCol>
                        </CRow>

                        <CRow className="mb-12">
                            <CFormLabel htmlFor="frmTemplatePath" className="col-sm-3 col-form-label">{t("TemplatePath")}</CFormLabel>
                            <CCol sm={9}>
                                <CFormInput type="file" id="frmTemplatePath" onChange={(e) => handleFileChange(e)} />
                            </CCol>
                        </CRow>

                        <CRow>

                            <CCol sm={6}>
                                <CFormSwitch label={t("UseTemplate")} name='desingTemplate' size='lg'
                                    onChange={e => handleChangeSwich(e)}
                                    checked={formdefination?.desingTemplate} ></CFormSwitch>

                            </CCol>

                            <CCol sm={6}>
                                <CFormSwitch label={t("Deployed")} name='deployed' size='lg'
                                    onChange={e => handleChangeSwich(e)}
                                    checked={formdefination?.deployed} ></CFormSwitch>

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


export default EditModal;


EditModal.propTypes = {
    visiblep: PropTypes.bool,
    userp: PropTypes.object,
};