
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


function AttachmentProcessItem({ data, processp, OnProcessAttachment, itemCheckedp, OnSelecteAttachment }) {

    //const [itemChecked, setItemCheck] = useState(false);
    function DownloadFile(fileUrl) {
        window.open(fileUrl);
    }

    useEffect(() => {
  

    }, []);

    function GetControl() {

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

                            <CIcon icon={cilReload} onClick={() => OnProcessAttachment(data.id)} />

                        </CButton>
                    </>
                )

            } else if (processp.downloadUrl === '') {

                return (
                    <CButton color="primary" variant="outline" shape="rounded-pill"  >

                        <CIcon icon={cilChevronCircleRightAlt} onClick={() => OnProcessAttachment(data.id)} />

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

    function CheckedChanged(e, data) {
       
        //setItemCheck(!e.target.checked);

        OnSelecteAttachment({ id: data.id, selected: e.target.checked, formName: data.formName });

    }

    return (
        <>
            <CRow>

                <CCol sm={10}>
                    <CFormCheck   checked={itemCheckedp} type='checkbox' label={data.formName} onChange={(e) => CheckedChanged(e, data)} ></CFormCheck >
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
    processp: PropTypes.object,
    data: PropTypes.object,
    itemCheckedp:PropTypes.bool,
}