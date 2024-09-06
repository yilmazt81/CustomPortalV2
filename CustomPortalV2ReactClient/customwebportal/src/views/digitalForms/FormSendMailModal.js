

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


} from '@coreui/react'

import PropTypes from 'prop-types';

import { Link } from 'react-router-dom';

import { useTranslation } from "react-i18next";


import LoadingAnimation from 'src/components/LoadingAnimation';

import { CloneForm } from 'src/lib/formMetaDataApi';
import {
    FaFileWord
} from "react-icons/fa";

const FormSendMailModal = ({ visiblep, formidp, OnClose }) => {

    function ClosedClick() {
        OnClose(true);
        setvisible(false);
    }

    const [visible, setvisible] = useState(false);
    const [processLoading, setprocessLoading] = useState(false);
    const [saveError, setSaveError] = useState(null);
    const [newFormmetadata, setnewFormmetadata] = useState(null);
    const { t } = useTranslation();

    useEffect(() => {

        setvisible(visiblep);
        setnewFormmetadata(null);

    }, [visiblep]);


    async function CloneDocument() {

        try {
            setSaveError(null);
            setprocessLoading(true);

            var formmetaDataReturn = await CloneForm(formidp);
            if (formmetaDataReturn.returnCode === 1) {
                setnewFormmetadata(formmetaDataReturn.data);
            } else {
                setSaveError(formmetaDataReturn.returnMessage);
            }
        } catch (error) {
            setSaveError(error.message);
        } finally {
            setprocessLoading(false);
        }
    }


    return (
        <>
            <CModal
                backdrop="static"
                visible={visible}
                size="lg"
                onClose={() => ClosedClick()}

            >
                <CModalHeader>
                    <CModalTitle> {t("SendMailModalTitle")}</CModalTitle>
                </CModalHeader>
                <CModalBody>

                    <CRow>
                        <CFormLabel>{t("SendMailDescription")}</CFormLabel>
                    </CRow>
                    <CRow>
                        <CFormLabel htmlFor="txtToMailAdres" className="col-sm-3 col-form-label">{t("ToMail")}</CFormLabel>
                        <CCol sm={9}>
                            <CFormInput type="text" id='txtToMailAdres' name="ToMail"
                                onChange={e => handleChange(e)}  />
                        </CCol>

                    </CRow>
                    <CRow>
                        <CFormLabel htmlFor="txtToMailAdresCC" className="col-sm-3 col-form-label">{t("CCMail")}</CFormLabel>
                        <CCol sm={9}>
                            <CFormInput type="text" id='txtToMailAdresCC' name="CCMail"
                                onChange={e => handleChange(e)}  />
                        </CCol>

                    </CRow>
                    <CRow>
                        <CFormLabel htmlFor="txtToMailSubject" className="col-sm-3 col-form-label">{t("MailSubject")}</CFormLabel>
                        <CCol sm={9}>
                            <CFormInput type="text" id='txtToMailAdresCC' name="CCMail"
                                onChange={e => handleChange(e)}  />
                        </CCol>

                    </CRow>

                    <CRow>
                        <CFormLabel htmlFor="txtAttachmentList" className="col-sm-3 col-form-label">{t("MailAttachment")}</CFormLabel>
                        <CCol sm={9}>
                            <CFormInput type="text" id='txtToMailAdresCC' name="CCMail"
                                onChange={e => handleChange(e)}  />
                        </CCol>

                    </CRow>

                    <CRow>
                        <CFormLabel htmlFor="txtMailBody" className="col-sm-3 col-form-label">{t("MailBody")}</CFormLabel>
                        <CCol sm={9}>
                            <CFormInput type="text" id='txtMailBody' name="CCMail"
                                onChange={e => handleChange(e)}  />
                        </CCol>

                    </CRow>
                </CModalBody>
                <CModalFooter>
                    <CButton color='primary' disabled={newFormmetadata != null} onClick={() => CloneDocument()}>{t("Ok")}</CButton>
                    <CButton color="secondary" onClick={() => ClosedClick()}  >{t("Close")}</CButton>

                </CModalFooter>
            </CModal>
        </>
    )
}

export default FormSendMailModal;