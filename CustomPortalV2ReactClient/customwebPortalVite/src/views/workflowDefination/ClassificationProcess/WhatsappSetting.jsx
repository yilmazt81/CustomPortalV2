import React, { useEffect, useState } from 'react'

import {
    CButton,
    CButtonGroup,
    CCard,
    CCardBody,
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

} from '@coreui/react'

 


import { useTranslation } from "react-i18next";


const WhatsappSetting = ({ visiblep, OnClose }) => {

    const { t } = useTranslation();
    const [visible, setvisible] = useState(visiblep);
    const [saveError, setsaveError] = useState(null);
    const [whatsAppSetting, setwhatsAppSetting] = useState(null);

    async function ClosedClick() {
        setvisible(false);
        OnClose(true);
    }

    useEffect(() => {
        setvisible(visiblep);

    }, [visiblep])

    function SaveData() {

    }

    function handleChange(event) {
        const { name, value } = event.target;
        setwhatsAppSetting({ ...whatsAppSetting, [name]: value });

    }

    return (
        <>
            <CModal
                backdrop="static"
                visible={visible}
                onClose={() => ClosedClick()}

            >
                <CModalHeader>
                    <CModalTitle>{t("WhatsAppSettingTitle")}</CModalTitle>
                </CModalHeader>
                <CModalBody>

                    <CRow className="mb-12">
                        <CFormLabel htmlFor="txtDeveloperKey" className="col-sm-3 col-form-label">{t("DeveloperKey")}</CFormLabel>
                        <CCol sm={9}>
                            <CFormInput type="text" id='txtDeveloperKey' name="DeveloperKey"
                                onChange={e => handleChange(e)} value={whatsAppSetting?.DeveloperKey} />
                        </CCol>
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

export default WhatsappSetting;


