import React, { useEffect, useState, useCallback } from 'react'

import {
    CButton,
    CButtonGroup,
    CCard,
    CCardBody,
    CCol,
    CAlert,
    CRow,
    CImage,
    CCardImage,

} from '@coreui/react'


import { useTranslation } from "react-i18next";

import EmailSetting from './EmailSetting';
import DeleteModal from 'src/components/DeleteModal'
import { Handle, Position } from '@xyflow/react';
import EMailImage from 'src/content/image/email.png';

import {
    FaCog, FaMinus
} from "react-icons/fa";
const handleStyle = { left: 30 };
function EmailNode({ data, isConnectable }) {
    const [settingModal, setSettingModal] = useState(false);
    const { t } = useTranslation();
    const onChange = useCallback((evt) => {
        console.log(evt.target.value);
    }, []);



    return (
        <div>

            <div>
            
                <CCard style={{ width: '5rem', height:"8rem" }}>
                    <CCardImage orientation="top" src={EMailImage} />
                    <CCardBody>
                        <CButtonGroup role="group">
                            <CButton color="primary" variant="ghost" size="sm" shape="rounded-pill" onClick={() => setSettingModal(true)}><FaCog /> </CButton>
                            <CButton color="danger" variant="ghost" size="sm" shape="rounded-pill"><FaMinus /></CButton>
                        </CButtonGroup>
                    </CCardBody>
                </CCard>
            
            </div>
        
            <Handle
                type="source"
                position={Position.Bottom}
                id="b"
                isConnectable={isConnectable}
            />
            <EmailSetting visiblep={settingModal} OnClose={() => setSettingModal(false)}></EmailSetting>
        </div>

    );
}

export default EmailNode;
