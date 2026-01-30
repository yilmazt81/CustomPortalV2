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

import WhatsappSetting from './WhatsappSetting';
import DeleteModal from '../../../components/DeleteModal.jsx'
import { Handle, Position } from '@xyflow/react';
import ClassificationImage from '../../../content/image/DocumentClassification.png';

import {
    FaCog, FaMinus
} from "react-icons/fa";
const handleStyle = { left: 30 };
function ClassificationProcessNode({ data, isConnectable }) {
    const [settingModal, setSettingModal] = useState(false);
    const { t } = useTranslation();
    const onChange = useCallback((evt) => {
        console.log(evt.target.value);
    }, []);



    return (
        <div>

            <div>
                <Handle
                    type="target"
                    position={Position.Top}

                    isConnectable={isConnectable}
                />
                <CCard style={{ width: '5rem', height: "8rem" }}>
                    <CCardImage orientation="top" src={ClassificationImage} />
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
            <WhatsappSetting visiblep={settingModal} OnClose={() => setSettingModal(false)}></WhatsappSetting>
        </div>

    );
}

export default ClassificationProcessNode;


