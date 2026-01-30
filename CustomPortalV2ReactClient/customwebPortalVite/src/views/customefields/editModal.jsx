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

import { CreateCustomeField,SaveCustomeField } from '../../lib/formdef.jsx';
import Lottie from 'lottie-react';
import PropTypes from 'prop-types';


import ProcessAnimation from "../../content/animation/Process.json";


import { useTranslation } from "react-i18next";


const EditModal = ({ visiblep, customeFieldp, setFormData }) => {


    const [visible, setvisible] = useState(visiblep);
    const [customeField, setcustomeField] = useState({ ...customeFieldp });

    const [saveError, setSaveError] = useState(null);

    const [saveStart, setsaveStart] = useState(false);

    const { t } = useTranslation();

    function handleChange(event) {
        const { name, value } = event.target;

        setcustomeField({ ...customeField, [name]: value });

    }


    async function ClosedClick() {
        setvisible(false);
    }


    useEffect(() => {
        setSaveError(null);
        setvisible(visiblep);
        setcustomeField(customeFieldp);
        //LoadBranchList();

    }, [visiblep, customeFieldp])

    async function SaveData() {

        try {
            var customeFieldReturn = await SaveCustomeField(customeField);
            if (customeFieldReturn.returnCode === 1) {
                setFormData();
            } else {
                setSaveError(customeFieldReturn.ReturnMessage);
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
                    <CModalTitle>{t("CustomeFieldModalTitle")}</CModalTitle>
                </CModalHeader>
                <CModalBody>
                    <CForm>


                        <CRow className="mb-12">
                            <CFormLabel htmlFor="txtfieldName" className="col-sm-3 col-form-label">{t("CustomeFieldName")}</CFormLabel>
                            <CCol sm={9}>
                                <CFormInput type="text" id='txtfieldName' name="fieldName"
                                    onChange={e => handleChange(e)} value={customeField?.fieldName} />
                            </CCol>
                        </CRow>

                        <CRow className="mb-12">
                            <CFormLabel htmlFor="txtfieldTagName" className="col-sm-3 col-form-label">{t("CustomeFieldTag")}</CFormLabel>
                            <CCol sm={9}>
                                <CFormInput type="text" id='txtfieldTagName' name="fieldTagName"
                                    onChange={e => handleChange(e)} value={customeField?.fieldTagName} />
                            </CCol>
                        </CRow>

                        <CRow className="mb-12">
                            <CFormLabel htmlFor="selectViewType" className="col-sm-3 col-form-label">{t("CustomeFieldViewType")}</CFormLabel>
                            <CCol sm={9}>

                                <CFormSelect id='selectViewType' name="elementType"
                                    onChange={e => handleChange(e)} value={customeField?.elementType}>
                                    <option value="Tablo">{t("CustomeFieldTable")} </option>
                                    <option value="Text">{t("CustomeFieldText")} </option>
                                    <option value="TableText">{t("CustomeFieldTableText")} </option>

                                </CFormSelect>

                            </CCol>
                        </CRow>

                        <CRow className="mb-12">
                            <CFormLabel htmlFor="selectHeaderHeightRule" className="col-sm-3 col-form-label">{t("CustomeFieldHeaderHeight")}</CFormLabel>
                            <CCol sm={4}>

                                <CFormSelect id='selectHeaderHeightRule' name="headerHeightRuleValue"
                                    onChange={e => handleChange(e)} value={customeField?.headerHeightRuleValue}>
                                    <option value="0">{t("CustomeFieldHeaderOto")} </option>
                                    <option value="1">{t("CustomeFieldHeaderInc")} </option>
                                    <option value="2">{t("CustomeFieldHeaderEnazInc")} </option>

                                </CFormSelect>

                            </CCol>
                            <CCol sm={4}>
                                <CFormInput type="text" id='txtHeaderHeight' name="headerHeight"
                                    onChange={e => handleChange(e)}
                                    placeholder={t("CustomeFieldHeaderHeight")}
                                    value={customeField?.headerHeight} />

                            </CCol>

                        </CRow>

                        <CRow className="mb-12">
                            <CFormLabel htmlFor="selectRowHeightRule"
                                className="col-sm-3 col-form-label">{t("CustomeFieldRowHeightRule")}</CFormLabel>
                            <CCol sm={4}>

                                <CFormSelect id='selectRowHeightRule' name="rowHeightRuleValue"
                                    onChange={e => handleChange(e)} value={customeField?.rowHeightRuleValue}>
                                    <option value="0">{t("CustomeFieldHeaderOto")} </option>
                                    <option value="1">{t("CustomeFieldHeaderInc")} </option>
                                    <option value="2">{t("CustomeFieldHeaderEnazInc")} </option>

                                </CFormSelect>

                            </CCol>


                            <CCol sm={4}>
                                <CFormInput type="text" id='txtHeaderRowHeightRule' name="rowHeight"
                                    onChange={e => handleChange(e)} value={customeField?.rowHeight}
                                    placeholder={t("CustomeFieldRowHeightRule")}
                                />

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
    customeFieldp: PropTypes.object,
};


