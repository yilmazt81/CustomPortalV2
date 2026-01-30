

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

import LoadingAnimation from "../../components/LoadingAnimation.jsx";
import FormSendMailModal from './FormSendMailModal';


const FormActionModal = ({ visiblep, formidp, OnClose, foreditForm }) => {

    function ClosedClick() {
        OnClose(true);
        setvisible(false);
    }

    const [visible, setvisible] = useState(false);
    const [visibleSendMail, setvisibleSendMail] = useState(false)
    const [processLoading, setprocessLoading] = useState(false);

    const [attachmentList, setAttachmentList] = useState([]);
    const [attachmentListSuccess, setattachmentListSuccess] = useState([]);

    const { t } = useTranslation();

    useEffect(() => {


        setvisible(visiblep);

    }, [visiblep]);

    function ShowMailForm(show) {
        setvisibleSendMail(show);
        setvisible(!show);
    }

    function AddSuggesctionList(data) {

        for (let index = 0; index < data.length; index++) {
            const element = data[index];

            var item = attachmentListSuccess.find(s => s.id === element.id.toString())
            if (item === undefined) {
                attachmentListSuccess.push({ id: element.id.toString(), text: element.formName,className:'' })
            }
        }
        setattachmentListSuccess(attachmentListSuccess);
    }

    function onSelectedAttachment(data) {
 
        var item = attachmentList.find(s => s.id === data.id.toString());
        if (item == null) {
            attachmentList.push({ id: data.id.toString(), selected: data.selected, text: data.formName,className:'' });
            setAttachmentList([...attachmentList]);
        } else {

            item.selected = data.selected;
          
            setAttachmentList([...attachmentList]);
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
                    <CModalTitle> {foreditForm ? t("SaveFormProcessTitle") : t("FormModalProcessTitle")}</CModalTitle>
                </CModalHeader>
                <CModalBody>
                    <CRow>
                        {foreditForm ? <CAlert color="success" >{t("SaveFormMessage")}</CAlert> : ""}

                    </CRow>
                    <CRow>
                        <FileCreateProcess listAttachmentSelectionp={attachmentList} formidp={formidp} onSelecteAttachment={(data) => onSelectedAttachment(data)} OnLoadFormAttachment={(alist) => AddSuggesctionList(alist)} loading={(e) => setprocessLoading(e)}></FileCreateProcess>
                    </CRow>

                    <CRow>
                        <LoadingAnimation loading={processLoading} size={"%30"}></LoadingAnimation>
                    </CRow>
                </CModalBody>
                <CModalFooter>
                    <CButton color="success" onClick={() => ShowMailForm(true)}  >{t("SendMail")}</CButton>
                    {foreditForm ? <Link to={{ pathname: "/digitalForms" }}> <CButton color="secondary">{t("ReturnDigitalForms")} </CButton></Link> : ""}
                    <CButton color="secondary" onClick={() => ClosedClick()}  >{t("Close")}</CButton>

                </CModalFooter>
            </CModal>

            <FormSendMailModal visiblep={visibleSendMail} suggestions={attachmentListSuccess} OnClose={() => ShowMailForm(false)} formidp={formidp} attachmentlistp={attachmentList} ></FormSendMailModal>
        </>
    )
}

export default FormActionModal;


