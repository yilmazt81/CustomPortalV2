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
    CFormSelect,
    CPopover
} from '@coreui/react'
import { CIcon } from '@coreui/icons-react';
import PropTypes from 'prop-types';
import { ThreeDot } from 'react-loading-indicators';

import { cilChevronCircleRightAlt, cilCloudDownload, cilX, cilReload } from '@coreui/icons';


function ProcessFileStatusIcon({ formattachmentid, formversionid, processp, ClickProcess }) {
    function DownloadFile(fileUrl) {
        console.log("download", fileUrl);
        window.open(fileUrl);
    }

    if (!processp.process) {
        if (processp.error != '') {
            return (

                <>
                    <CPopover
                        title="Error"
                        content={processp.error}
                        placement="right"
                    >
                        <CButton color="danger" variant="outline" shape="rounded-pill"  >
                            <CIcon icon={cilX} />
                        </CButton>
                    </CPopover>

                    <CButton color="primary" variant="outline" shape="rounded-pill"  >

                        <CIcon icon={cilReload} onClick={() => ClickProcess(formattachmentid, formversionid)} />

                    </CButton>
                </>
            )

        } else if (processp.downloadUrl === '') {

            return (
                <CButton color="primary" variant="outline" shape="rounded-pill"  >

                    <CIcon icon={cilChevronCircleRightAlt} onClick={() => ClickProcess(formattachmentid, formversionid)} />

                </CButton>
            )
        } else if (processp.downloadUrl != '') {
            return (
                <CButton color="success" variant="outline" shape="rounded-pill" onClick={() => DownloadFile(processp.downloadUrl)} >
                    <CIcon icon={cilCloudDownload} ></CIcon>
                </CButton>
            )
        } else {
            return (
                <>

                </>
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

export default ProcessFileStatusIcon;


