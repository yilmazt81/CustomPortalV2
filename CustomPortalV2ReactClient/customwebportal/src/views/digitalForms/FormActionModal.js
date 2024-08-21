

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


} from '@coreui/react'

import Lottie from 'lottie-react';
import PropTypes from 'prop-types';


import { useTranslation } from "react-i18next";

import { DataGrid } from '@mui/x-data-grid';
import FileCreateProcess from '../digitalFormEdit/FileCreateProcess';

import LoadingAnimation from 'src/components/LoadingAnimation';


const FormActionModal = ({ visiblep, formidp }) => {

    function ClosedClick() {

    }

    const [visible, setvisible] = useState(false);
    const [processLoading,setprocessLoading]=useState(false);
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
                    <CModalTitle>{t("FormModalProcessTitle")}</CModalTitle>
                </CModalHeader>
                <CModalBody>

                    <CRow>

                        <FileCreateProcess formidp={formidp} loading={(e)=>setprocessLoading(e)}></FileCreateProcess>
                    </CRow>

                    <CRow>
                        <LoadingAnimation loading={processLoading} size={"%30"}></LoadingAnimation>
                    </CRow>
                </CModalBody>
            </CModal>
        </>
    )
}

export default FormActionModal;