 

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


import ProcessAnimation from "src/content/animation/Process.json";
import Gridcolumns from './gridColumns';


import { useTranslation } from "react-i18next";

import { DataGrid } from '@mui/x-data-grid';
const FormActionModal = () => {

    return (
        <>
            <CModal
                backdrop="static"
                visible={visible}
                size="xl"
                onClose={() => ClosedClick()}

            >
                <CModalHeader>
                    <CModalTitle>{t("EditUserName")}</CModalTitle>
                </CModalHeader>
                <CModalBody>

                </CModalBody>
            </CModal>
        </>
    )
}

export default FormActionModal;