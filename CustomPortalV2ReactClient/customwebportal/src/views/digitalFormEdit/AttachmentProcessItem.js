
import React, { useState, useEffect } from 'react';
import { useTranslation } from "react-i18next";
import { cilSave, cilClearAll, cilPin } from '@coreui/icons';
import {
    CButton,
    CButtonGroup,
    CCard,
    CCardBody,
    CAlert,
    CFormLabel,
    CCol,
    CRow,
    CFormInput,
    CFormCheck,
    CFormSelect
} from '@coreui/react'
import { CIcon } from '@coreui/icons-react';
import PropTypes, { bool, func } from 'prop-types';

import { ThreeDot } from 'react-loading-indicators';


import { cilChevronCircleRightAlt, cilCloudDownload } from '@coreui/icons';


function AttachmentProcessItem({ data, processp, downloadFilePathp, OnProcessAttachment, OnSelecteAttachment }) {

    function GetControl() {

        if (!processp) {
            if (downloadFilePathp === '') {

                return (
                    <CButton color="primary" variant="outline" shape="rounded-pill"  >

                        <CIcon icon={cilChevronCircleRightAlt} onClick={() => OnProcessAttachment(data.id)} />

                    </CButton>
                )
            } else {
                return (
                    <CButton color="success" variant="outline" shape="rounded-pill" >
                        <CIcon icon={cilCloudDownload} ></CIcon>
                    </CButton>
                )
            }
        } else {
            return (
                <CRow>

                    <ThreeDot color="#1919e3" size="medium" text="" textColor="" />
                </CRow>
            )
        }
    }

    return (
        <>
            <CRow>

                <CCol sm={10}>
                    <CFormCheck type='CheckBox' label={data.formName} ></CFormCheck >
                </CCol>
                <CCol sm={2}>

                    {GetControl()}

                </CCol>
            </CRow>
        </>
    );
}

export default AttachmentProcessItem;

AttachmentProcessItem.propTypes = {
    processp: PropTypes.bool,
    data: PropTypes.object,
    downloadFilePathp: PropTypes.string,
}