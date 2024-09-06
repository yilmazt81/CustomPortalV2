

import React, { useEffect, useState } from 'react'

import {
    CButton,
    CAlert,
    CRow,
    CModal,
    CModalHeader,
    CModalTitle,
    CModalFooter,
    CModalBody,


} from '@coreui/react'
 

import { Link } from 'react-router-dom';

import { useTranslation } from "react-i18next";
 
import FileCreateProcess from '../digitalFormEdit/FileCreateProcess';

import LoadingAnimation from 'src/components/LoadingAnimation';


const FormActionModal = ({ visiblep, formidp, OnClose, foreditForm }) => {

    function ClosedClick() {
        OnClose(true);
        setvisible(false);
    }

    const [visible, setvisible] = useState(false);
    const [processLoading, setprocessLoading] = useState(false);
    const { t } = useTranslation();

    useEffect(() => {


        setvisible(visiblep);

    }, [visiblep]);
    return (
        <>
            <CModal
                backdrop="static"
                visible={visible}
                size="lg"
                onClose={() => ClosedClick()}

            >
                <CModalHeader>
                    <CModalTitle> { foreditForm? t("SaveFormProcessTitle"): t("FormModalProcessTitle") }</CModalTitle>
                </CModalHeader>
                <CModalBody>
                    <CRow>
                        {foreditForm ? <CAlert color="success" >{t("SaveFormMessage")}</CAlert> : ""}

                    </CRow>
                    <CRow>

                        <FileCreateProcess formidp={formidp} loading={(e) => setprocessLoading(e)}></FileCreateProcess>
                    </CRow>

                    <CRow>
                        <LoadingAnimation loading={processLoading} size={"%30"}></LoadingAnimation>
                    </CRow>
                </CModalBody>
                <CModalFooter>
                    <CButton color="success"  >{t("SendMail")}</CButton>
                    {foreditForm ? <Link to={{pathname:"/digitalForms"}}> <CButton color="secondary">{t("ReturnDigitalForms")} </CButton></Link> : ""}
                    <CButton color="secondary" onClick={() => ClosedClick()}  >{t("Close")}</CButton>

                </CModalFooter>
            </CModal>
        </>
    )
}

export default FormActionModal;