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

import { CreateCustomeField,SaveCustomeField } from '../../lib/formdef';
import Lottie from 'lottie-react';
import PropTypes from 'prop-types';


import ProcessAnimation from "../../content/animation/Process.json";


import { useTranslation } from "react-i18next";


const EditModal = ({ visiblep, customeFielditemp, setFormData }) => {


    const [visible, setvisible] = useState(visiblep);
    const [customeFielditem, setcustomeFielditem] = useState({ ...customeFielditemp });

    const [saveError, setSaveError] = useState(null);

    const [saveStart, setsaveStart] = useState(false);

    const { t } = useTranslation();

    function handleChange(event) {
        const { name, value } = event.target;

        setcustomeFielditem({ ...customeFielditem, [name]: value });

    }


    async function ClosedClick() {
        setvisible(false);
    }


    useEffect(() => {
        setSaveError(null);
        setvisible(visiblep);
        setcustomeFielditem(customeFielditemp);
        //LoadBranchList();

    }, [visiblep, customeFielditemp])

    async function SaveData() {

        try {
            var customeFieldReturn = await SaveCustomeField(customeFielditem);
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
                            <CFormLabel htmlFor="txtfieldCaption" className="col-sm-3 col-form-label">{t("FieldCaption")}</CFormLabel>
                            <CCol sm={9}>
                                <CFormInput type="text" id='txtfieldCaption' name="fieldCaption"
                                    onChange={e => handleChange(e)} value={customeFielditem?.fieldCaption} />
                            </CCol>
                        </CRow>

                        <CRow className="mb-12">
                            <CFormLabel htmlFor="txtTagName" className="col-sm-3 col-form-label">{t("TagName")}</CFormLabel>
                            <CCol sm={9}>
                                <CFormInput type="text" id='txtTagName' name="tagName"
                                    onChange={e => handleChange(e)} value={customeFielditem?.tagName} />
                            </CCol>
                        </CRow>

                        <CRow className="mb-12">
                            <CFormLabel htmlFor="txtorderNumber" className="col-sm-3 col-form-label">{t("OrderNumber")}</CFormLabel>
                            <CCol sm={9}>
                                <CFormInput type="number" id='txtorderNumber' name="orderNumber"
                                    onChange={e => handleChange(e)} value={customeFielditem?.orderNumber} />
                            </CCol>
                        </CRow>

                        <CRow className="mb-12">
                            <CFormLabel htmlFor="txttableWith" className="col-sm-3 col-form-label">{t("TableWith")}</CFormLabel>
                            <CCol sm={9}>
                                <CFormInput type="number" id='txttableWith' name="tableWith"
                                    onChange={e => handleChange(e)} value={customeFielditem?.tableWith} />
                            </CCol>
                        </CRow>

                        <CRow className="mb-12">
                            <CFormLabel htmlFor="selectControlType" className="col-sm-3 col-form-label">{t("ControlType")}</CFormLabel>
                            <CCol sm={9}>
                                <CFormSelect id='selectControlType' name="controlType" required
                                    onChange={e => handleChange(e)} value={customeFielditem?.controlType}>
                                    <option value="Text">{t("Text")}</option>
                                    <option value="DateTime">{t("DateTime")}</option>
                                    <option value="CheckBox">{t("CheckBox")}</option>
                                    <option value="RadioBox">{t("RadioBox")}</option>
                                    <option value="ComboBox">{t("ComboBox")}</option>
                                </CFormSelect>
                                
                            </CCol>

                        </CRow>


                        

                        <CRow className="mb-12">
                            <CFormLabel htmlFor="selectHeaderHeightRule" className="col-sm-3 col-form-label">{t("CustomeFieldItemHeaderWidthRuleValue")}</CFormLabel>
                            <CCol sm={4}>

                                <CFormSelect id='selectHeaderHeightRule' name="headerWidthRuleValue"
                                    onChange={e => handleChange(e)} value={customeFielditem?.headerWidthRuleValue}>
                                    <option value="3">{t("CustomeFieldItemOto")} </option>
                                    <option value="1">{t("CustomeFieldItemPer")} </option>
                                    <option value="2">{t("CustomeFieldHeaderInc")} </option>
                                    <option value="10">{t("CustomeFieldItemLen")} </option>

                                </CFormSelect>

                            </CCol>
                            <CCol sm={4}>
                                <CFormInput type="text" id='txtheaderWidth' name="headerWidth"
                                    onChange={e => handleChange(e)}
                                    placeholder={t("CustomeFieldItemColomnWith")}
                                    value={customeFielditem?.headerWidth} />
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
    customeFielditemp: PropTypes.object,
};