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

import {
    MaterialReactTable,
    useMaterialReactTable,


} from 'material-react-table';


import { useTranslation } from "react-i18next";


const EmailSetting = ({ visiblep, OnClose }) => {

    const { t } = useTranslation();
    const [visible, setvisible] = useState(visiblep);
    const [saveError, setsaveError] = useState(null);
    const [ftpSetting, setftpSetting] = useState({ fTPPort: 21 });

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
        setftpSetting({ ...ftpSetting, [name]: value });

    }

    return (
        <>
            <CModal
                backdrop="static"
                visible={visible}
                onClose={() => ClosedClick()}

            >
                <CModalHeader>
                    <CModalTitle>{t("FTPModalSettingTitle")}</CModalTitle>
                </CModalHeader>
                <CModalBody>

                    <CRow className="mb-12">
                        <CFormLabel htmlFor="txtFTPPort" className="col-sm-3 col-form-label">{t("FTPPort")}</CFormLabel>
                        <CCol sm={9}>
                            <CFormInput type="number" id='txtFTPPort' name="FTPPort"
                                onChange={e => handleChange(e)} value={ftpSetting.fTPPort} />
                        </CCol>
                    </CRow>
                    <CRow className="mb-12">
                        <CFormLabel htmlFor="txtFTPAdress" className="col-sm-3 col-form-label">{t("FTPAdress")}</CFormLabel>
                        <CCol sm={9}>
                            <CFormInput type="text" id='txtFTPAdress' name="FTPAdress"
                                onChange={e => handleChange(e)} value={ftpSetting?.fTPAdress} />
                        </CCol>
                    </CRow>
                    <CRow className="mb-12">
                        <CFormLabel htmlFor="txtFTPUserName" className="col-sm-3 col-form-label">{t("FTPUserName")}</CFormLabel>
                        <CCol sm={9}>
                            <CFormInput type="text" id='txtFTPUserName' name="FTPUserName"
                                onChange={e => handleChange(e)} value={ftpSetting?.fTPUserName} />
                        </CCol>
                    </CRow>

                    <CRow className="mb-12">
                        <CFormLabel htmlFor="txtFTPPassword" className="col-sm-3 col-form-label">{t("FTPPassword")}</CFormLabel>
                        <CCol sm={9}>
                            <CFormInput type="password" id='txtFTPPassword' name="FTPPassword"
                                onChange={e => handleChange(e)} value={ftpSetting?.fTPPassword} />
                        </CCol>
                    </CRow>

                    <CRow className="mb-12">
                        <CFormLabel htmlFor="txtListenFolder" className="col-sm-3 col-form-label">{t("FTPListenFolder")}</CFormLabel>
                        <CCol sm={9}>
                            <CFormInput type="text" id='txtListenFolder' name="FTPListenFolder"
                                onChange={e => handleChange(e)} value={ftpSetting?.fTPListenFolder} />
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
                    <CButton color='info'>{t("FTPTest")}</CButton>
                    <CButton color="secondary" onClick={() => ClosedClick()}  >{t("Close")}</CButton>
                    <CButton color="primary" onClick={() => SaveData()}>{t("Save")}</CButton>
                </CModalFooter>
            </CModal>
        </>
    )

}

export default EmailSetting;
