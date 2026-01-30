

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


import LoadingAnimation from '../../components/LoadingAnimation.jsx';

import { CloneForm } from "../../lib/formMetaDataApi.jsx";
import { ThreeDot } from 'react-loading-indicators';
import {
    FaFileWord
} from "react-icons/fa";


import { cilCopy, cibOpenAccess } from '@coreui/icons';

import { CIcon } from '@coreui/icons-react';

 

const FormCopyModal = ({ visiblep, formidp, OnClose }) => {

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
            debugger;
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

  

    function GetCloneControls() {

        if (newFormmetadata != null) {
            return (

                <Link to={{
                    pathname: '/digitalFormEdit',
                    search: '?id=' + newFormmetadata.id,
                }} >
                    <CButton color='primary'><FaFileWord />{t("OpenCloneDocument")}</CButton>
                </Link>
            )
        } else if (processLoading) {
            return (
                <ThreeDot color="#1919e3" size="medium" text="" textColor="" />
            )
        } else {
            return (
                <></>
            )
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
                    <CModalTitle> {t("CopyDocumentModalTitle")}</CModalTitle>
                </CModalHeader>
                <CModalBody>

                    <CRow>
                        <CFormLabel>{t("CopyDocumentLabelDescription")}</CFormLabel>
                    </CRow>

                    <CRow>
                        {
                            GetCloneControls()
                        }


                    </CRow>

                    <CRow>
                        {
                            saveError != null ?
                                <CAlert color="warning">{saveError}</CAlert>
                                : ""
                        }
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

export default FormCopyModal;


