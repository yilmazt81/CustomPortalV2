
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

import { cibCmake } from '@coreui/icons';


function AttachmentProcessItem({ data, OnProcessAttachment, OnSelecteAttachment }) {


    return (
        <>
            <CRow>



                <CCol sm={10}>
                    <CFormCheck type='CheckBox' label={data.formName} ></CFormCheck >
                </CCol>
                <CCol sm={2}>

                    <CButton color="primary"  >
                        <CIcon icon={cibCmake} onClick={() => OnProcessAttachment(data.id)} />
                    </CButton>
                </CCol>
            </CRow>
        </>
    );
}

export default AttachmentProcessItem; 