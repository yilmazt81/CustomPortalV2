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
    CCardText,


} from '@coreui/react'

import Lottie from 'lottie-react';
import PropTypes from 'prop-types';
import { useTranslation } from "react-i18next";

import ProcessAnimation from "../content/animation/Process.json";


const DeleteModal = ({ visiblep, title, message,message2, saveStart, deleteError,OnClickOk,OnClickCancel }) => {


    const [visible, setvisible] = useState(visiblep);

    const { t } = useTranslation();



    async function DeleteAccepted() {

        OnClickOk({return:"ok"}); 
    }

    function Cancel(){
        OnClickCancel();
    }

    useEffect(() => {
        setvisible(visiblep);

    }, [visiblep])


    return (

        <>
            <CModal
                backdrop="static"
                visible={visible}
                onClose={() => Cancel()}

            >
                <CModalHeader>
                    <CModalTitle>{title}</CModalTitle>
                </CModalHeader>
                <CModalBody>


                    <CRow>
                        <CCardText>{message}</CCardText>
                    </CRow>
                    <CRow>
                        <CCardText>{message2}</CCardText>
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
                        {deleteError != null ?
                            <CAlert color="warning">{deleteError}</CAlert>
                            : ""
                        }
                    </CRow>

                </CModalBody>

                <CModalFooter>
                    <CButton color="secondary" onClick={() => setvisible(false)}  >{t("Close")}</CButton>
                    <CButton color="primary" onClick={() => DeleteAccepted()}>{t("Ok")}</CButton>
                </CModalFooter>
            </CModal>

        </>
    )


}


export default DeleteModal;


DeleteModal.propTypes = {
    visiblep: PropTypes.bool,
    title: PropTypes.string,
    message: PropTypes.string,
    message2: PropTypes.string,
    saveStart: PropTypes.bool,
    deleteError: PropTypes.string,
    OnClickOk:PropTypes.func,

};