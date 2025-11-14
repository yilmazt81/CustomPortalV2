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
    CFormCheck,
    CFormSwitch

} from '@coreui/react'

import { SaveFormGroup } from '../../lib/formdef';
import Lottie from 'lottie-react';
import PropTypes from 'prop-types';


import ProcessAnimation from "../../content/animation/Process.json";


import { useTranslation } from "react-i18next";


const GroupEditModal = ({ visiblep, formdefinationGroupp, setFormData }) => {


    const [visible, setvisible] = useState(visiblep);
    const [formdefinationGroup, setformdefinationGroup] = useState({ ...formdefinationGroupp });

    const [saveError, setSaveError] = useState(null);

    const [saveStart, setsaveStart] = useState(false);

    const { t } = useTranslation();

    function handleChange(event) {
        const { name, value } = event.target;

        setformdefinationGroup({ ...formdefinationGroup, [name]: value });

    }
    function handleChangeSwich(event) {
        const { name, value } = event.target;
        var newValue = formdefinationGroup[name];
        newValue = !newValue;

        setformdefinationGroup({ ...formdefinationGroup, [name]: newValue });

    }

    async function ClosedClick() {
        setvisible(false);
    }



    useEffect(() => {
        setSaveError(null);
        setvisible(visiblep);
        setformdefinationGroup(formdefinationGroupp);
        //LoadBranchList();

    }, [visiblep, formdefinationGroupp])

    async function SaveData() {

        try {

            try {
                setSaveError(null);
                setsaveStart(true);
                debugger;
                var savedefinationResult = await SaveFormGroup(formdefinationGroup);

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


    return (

        <>
            <CModal
                backdrop="static"
                visible={visible}
                onClose={() => ClosedClick()}

            >
                <CModalHeader>
                    <CModalTitle>{t("FormGroupModalTitle")}</CModalTitle>
                </CModalHeader>
                <CModalBody>

                    <CRow className="mb-12">
                        <CFormLabel htmlFor="txtFormNumber" className="col-sm-3 col-form-label">{t("FormNumber")}</CFormLabel>
                        <CCol sm={9}>
                            <CFormInput type="text" id='txtFormNumber' name="formNumber"
                                onChange={e => handleChange(e)} value={formdefinationGroup?.formNumber} />
                        </CCol>
                    </CRow>
                    <CRow className="mb-12">
                        <CFormLabel htmlFor="txtname" className="col-sm-3 col-form-label">{t("GroupName")}</CFormLabel>
                        <CCol sm={9}>
                            <CFormInput type="text" id='txtname' name="name"
                                onChange={e => handleChange(e)} value={formdefinationGroup?.name} />
                        </CCol>
                    </CRow>
                    <CRow className="mb-12">
                        <CFormLabel htmlFor="txtorderNumber" className="col-sm-3 col-form-label">{t("OrderNumber")}</CFormLabel>
                        <CCol sm={9}>
                            <CFormInput type="Number" id='txtorderNumber' name="orderNumber"
                                onChange={e => handleChange(e)} value={formdefinationGroup?.orderNumber} />
                        </CCol>
                    </CRow>
                    <CRow className="mb-12">
                        <CFormLabel htmlFor="txtgroupTag" className="col-sm-3 col-form-label">{t("GroupTag")}</CFormLabel>
                        <CCol sm={9}>
                            <CFormInput type="text" id='txtgroupTag' name="groupTag"
                                onChange={e => handleChange(e)} value={formdefinationGroup?.groupTag} />
                        </CCol>
                    </CRow>

                    <CRow className="mb-12">
                        <CCol>
                            <CFormSwitch label={t("CustomerCanChange")} name='allowEditCustomer' size='lg'
                                onChange={e => handleChangeSwich(e)}
                                checked={formdefinationGroup?.allowEditCustomer} ></CFormSwitch>

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


export default GroupEditModal;


GroupEditModal.propTypes = {
    visiblep: PropTypes.bool,
    formdefinationGroupp: PropTypes.object,
};