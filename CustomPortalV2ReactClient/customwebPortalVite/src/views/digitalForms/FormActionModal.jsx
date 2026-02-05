

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

import FileCreateProcess from '../digitalFormEdit/FileCreateProcess.jsx';

import LoadingAnimation from "../../components/LoadingAnimation.jsx";
import FormSendMailModal from './FormSendMailModal';


const FormActionModal = ({ visiblep, formidp, OnClose, foreditForm }) => {

    const [visible, setvisible] = useState(false);

    const ClosedClick = () => {
        OnClose(true);
        setvisible(false);
    };
    const [visibleSendMail, setvisibleSendMail] = useState(false)
    const [processLoading, setprocessLoading] = useState(false);

    const [attachmentList, setAttachmentList] = useState([]);
    const [attachmentListSuccess, setattachmentListSuccess] = useState([]);

    const { t } = useTranslation();

    useEffect(() => {
 
        setvisible(visiblep);

    }, [visiblep]);

    const ShowMailForm = (show) => {
        setvisibleSendMail(show);
        setvisible(!show);
    };

    const AddSuggesctionList = (data) => {
        data.forEach(element => {
            if (!attachmentListSuccess.find(s => s.id === element.id.toString())) {
                attachmentListSuccess.push({ id: element.id.toString(), text: element.formName, className: '' });
            }
        });
        setattachmentListSuccess(attachmentListSuccess);
    };

    const onSelectedAttachment = (data) => {
        const item = attachmentList.find(s => s.id === data.id.toString());
        if (!item) {
            attachmentList.push({ id: data.id.toString(), selected: data.selected, text: data.formName, className: '' });
        } else {
            item.selected = data.selected;
        }
        setAttachmentList([...attachmentList]);
    };



    return (
        <>
            <CModal
                backdrop="static"
                visible={visible}
                size="lg"
                onClose={() => ClosedClick()}
            >
                <CModalHeader>
                    <CModalTitle>{foreditForm ? t("SaveFormProcessTitle") : t("FormModalProcessTitle")}</CModalTitle>
                </CModalHeader>
                <CModalBody>
                    <CRow>
                        {foreditForm ? <CAlert color="success">{t("SaveFormMessage")}</CAlert> : ""}

                    </CRow>
                    <CRow>
                        <FileCreateProcess
                            listAttachmentSelectionp={attachmentList}
                            formidp={formidp}
                            onSelecteAttachment={onSelectedAttachment}
                            OnLoadFormAttachment={AddSuggesctionList}
                            loading={setprocessLoading}
                        />
                    </CRow>

                    <CRow>
                        <LoadingAnimation loading={processLoading} size={"%30"} />
                    </CRow>
                </CModalBody>
                <CModalFooter>
                    <CButton color="success" onClick={() => ShowMailForm(true)}>{t("SendMail")}</CButton>
                    {foreditForm ? <Link to={{ pathname: "/digitalForms" }}><CButton color="secondary">{t("ReturnDigitalForms")}</CButton></Link> : ""}
                    <CButton color="secondary" onClick={() => ClosedClick()}>{t("Close")}</CButton>
                </CModalFooter>
            </CModal>

            <FormSendMailModal
                visiblep={visibleSendMail}
                suggestions={attachmentListSuccess}
                OnClose={() => ShowMailForm(false)}
                formidp={formidp}
                attachmentlistp={attachmentList}
            />
        </>
    )
}

export default FormActionModal;


