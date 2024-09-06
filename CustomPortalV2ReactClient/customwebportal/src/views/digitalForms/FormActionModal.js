

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
import FormSendMailModal from './FormSendMailModal';


const FormActionModal = ({ visiblep, formidp, OnClose, foreditForm }) => {

    function ClosedClick() {
        OnClose(true);
        setvisible(false);
    }

    const [visible, setvisible] = useState(false);
    const [visibleSendMail, setvisibleSendMail] = useState(false)
    const [processLoading, setprocessLoading] = useState(false);
    const { t } = useTranslation();

    useEffect(() => {


        setvisible(visiblep);

    }, [visiblep]);

    function ShowMailForm(show){
        setvisibleSendMail(show);
        setvisible(!show);
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
                    <CButton color="success" onClick={()=>ShowMailForm(true)}  >{t("SendMail")}</CButton>
                    {foreditForm ? <Link to={{pathname:"/digitalForms"}}> <CButton color="secondary">{t("ReturnDigitalForms")} </CButton></Link> : ""}
                    <CButton color="secondary" onClick={() => ClosedClick()}  >{t("Close")}</CButton>

                </CModalFooter>
            </CModal>

            <FormSendMailModal visiblep={visibleSendMail} OnClose={()=>ShowMailForm(false)} formidp={formidp} ></FormSendMailModal>
        </>
    )
}

export default FormActionModal;