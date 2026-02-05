
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

import  ProcessFileStatusIcon  from "../../components/ProcessFileStatusIcon.jsx";

function AttachmentProcessItem({ data, processp, OnProcessAttachment, itemCheckedp, OnSelecteAttachment }) {

    //const [itemChecked, setItemCheck] = useState(false);
  
    useEffect(() => {
  

    }, []);

    function GetControl() {

        
    }

    function CheckedChanged(e, data) {
        
        OnSelecteAttachment({ id: data.id, selected: e.target.checked, formName: data.formName });

    }

    return (
        <>
            <CRow>

                <CCol sm={10}>
                    <CFormCheck   checked={itemCheckedp} type='checkbox' label={data.formName} onChange={(e) => CheckedChanged(e, data)} ></CFormCheck >
                </CCol>
                <CCol sm={2}>

                     <ProcessFileStatusIcon 
                        formattachmentid={data.id}
                        process={processp}
                        ClickProcess={(attachmentid,versionid) => OnProcessAttachment(attachmentid,versionid)}
                      >

                     </ProcessFileStatusIcon>

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


