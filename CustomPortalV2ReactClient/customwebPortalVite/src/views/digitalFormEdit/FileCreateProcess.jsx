

import React, { useEffect, useState } from 'react'

import {
    CButton,
    CCol,
    CRow,
    CFormSelect,
    CFormLabel


} from '@coreui/react'

import Tabs from '@mui/material/Tabs';
import Tab from '@mui/material/Tab';
import Box from '@mui/material/Box';

import AttachmentProcessItem from './AttachmentProcessItem';

import PropTypes from 'prop-types';



import { useTranslation } from "react-i18next";


import { GetConvertFileList } from "../../lib/formMetaDataApi.jsx";

import { cilChevronCircleRightAlt } from '@coreui/icons';

import { CIcon } from '@coreui/icons-react';

import { CreateFormAttachment, CreateFormVersion } from "../../lib/CreateFileApi.jsx";

import ProcessFileStatusIcon from "../../components/ProcessFileStatusIcon.jsx";
function a11yProps(index) {
    return {
        id: `simple-tab-${index}`,
        'aria-controls': `simple-tabpanel-${index}`,
    };
}

function CustomTabPanel(props) {
    const { children, value, index, ...other } = props;

    return (
        <div
            role="tabpanel"
            hidden={value !== index}
            id={`simple-tabpanel-${index}`}
            aria-labelledby={`simple-tab-${index}`}
            {...other}
        >
            {value === index && <Box sx={{ p: 3 }}>{children}</Box>}
        </div>
    );
}

CustomTabPanel.propTypes = {
    children: PropTypes.node,
    index: PropTypes.number.isRequired,
    value: PropTypes.number.isRequired,
};



const FileCreateProcess = ({ formidp, loading, onError, onSelecteAttachment, OnLoadFormAttachment, listAttachmentSelectionp }) => {

    const { t } = useTranslation();

    const [formVersionList, setFormVersionList] = useState([]);
    const [formAttachmentPrivate, setformAttachmentPrivate] = useState([]);
    const [formAttachmentNotPrivate, setformAttachmentNotPrivate] = useState([]);

    const [formdefinationVersion, setformdefinationVersion] = useState(0);
   
    const [formdefinationName, setformdefinationName] = useState("");

    const [processAttachmentList, setprocessAttachmentList] = useState([{ attachmentid: 0, process: false, downloadUrl: '', error: '' }]);
    const [processVersionstatus, setprocessVersionstatus] = useState({ process: false, downloadUrl: '', error: '' });

    const [value, setValue] = useState(0);

    const handleChangeTab = (event, newValue) => {
        setValue(newValue);
    };


    const handleChange = (event) => {
        setformdefinationVersion(event.target.value); 
    };

    const LoadFormConvertTypes = async () => {
        try {
            const { data, returnCode, returnMessage } = await GetConvertFileList(formidp);
            if (returnCode === 1) {
                setformdefinationName(data.formDefinationTypeName);
                setFormVersionList(data.formVersions);
                setformAttachmentPrivate(data.attachmentPrivateForForm);
                setformAttachmentNotPrivate(data.attachmentNotPrivateForForm);
                OnLoadFormAttachment(data.attachmentPrivateForForm);
                OnLoadFormAttachment(data.attachmentNotPrivateForForm);
                if (data.attachmentPrivateForForm.length === 0) setValue(1);
                if (data.formVersions.length > 0) setformdefinationVersion(data.formVersions[0].id);
            } else {
                onError(returnMessage);
            }
        } catch (error) {
            onError(error.message);
        } finally {
            loading(false);
        }
    };

    useEffect(() => {

        LoadFormConvertTypes();


    }, []);


    async function CreateVersionFile() {
        //CreateFormVersion
        
        setprocessVersionstatus({ process: true, downloadUrl: '', error: '' });
        const { data, returnCode, returnMessage } = await CreateFormVersion(formidp, formdefinationVersion);
        if (returnCode === 1) { 
            setprocessVersionstatus({ process: false, downloadUrl: data, error: '' });
        } else {
            setprocessVersionstatus({ process: false, downloadUrl: '', error: returnMessage });
        }
    }

    const CreateAttacment = async (versionid,attachmentid) => {
        let item = processAttachmentList.find(s => s.attachmentid === attachmentid);
        if (!item) {
            processAttachmentList.push({ attachmentid, process: true, downloadUrl: '', error: '' });
            item = processAttachmentList[processAttachmentList.length - 1];
        }
        item.process = true;
        setprocessAttachmentList([...processAttachmentList]);

        try {
            const { data, returnCode, returnMessage } = await CreateFormAttachment(formidp, attachmentid);
            item = processAttachmentList.find(s => s.attachmentid === attachmentid);
            if (returnCode === 1) {
                item.downloadUrl = data;
                item.error = '';
            } else {
                item.error = returnMessage;
            }
        } catch (error) {
            item = processAttachmentList.find(s => s.attachmentid === attachmentid);
            item.error = error.message;
        } finally {
            item = processAttachmentList.find(s => s.attachmentid === attachmentid);
            item.process = false;
            setprocessAttachmentList([...processAttachmentList]);
        }
    };

    const GetProcessStatus = (attachmentid) =>
        processAttachmentList.find(s => s.attachmentid === attachmentid) ||
        { attachmentid, process: false, downloadUrl: '', error: '' };

    const GetProcessVersionStatus = () => {

        return processVersionstatus;
    }
    const getAttachmentCheckedValue = (id) =>
        listAttachmentSelectionp.find(s => s.id === id.toString())?.selected ?? false;

    return (
        <>
            <CRow>
                <CFormLabel>{t("FormProcessActionDescription")}</CFormLabel>
            </CRow>
            <CRow>
                <CCol sm={4}>

                    <CFormLabel className="form-label">{formdefinationName}</CFormLabel>
                </CCol>
                <CCol sm={6}>
                    <CFormSelect
                        id="selectFormVersion"
                        name="formVersion"
                        value={formdefinationVersion}
                        onChange={handleChange}
                    >
                        {formVersionList.map(item => (
                            <option key={item.id} value={item.id}>{item.formLanguage}</option>
                        ))}
                    </CFormSelect>

                </CCol>
                <CCol sm={2}>

                   
                    <ProcessFileStatusIcon
                        formversionid={formdefinationVersion}
                        processp={GetProcessVersionStatus()}
                        ClickProcess={(attachmentid,versionid) => CreateVersionFile(versionid)} >

                    </ProcessFileStatusIcon>

                </CCol>
            </CRow>
            <CCol>
                <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
                    <Tabs value={value} onChange={handleChangeTab} aria-label="basic tabs example">
                        <Tab label={t("PrivateAttachment")} {...a11yProps(0)} />
                        <Tab label={t("NotPrivateAttachment")} {...a11yProps(1)} />
                    </Tabs>
                </Box>
                <CustomTabPanel value={value} index={0}>
                    <CRow>
                        <CCol sm={12}>
                            {formAttachmentPrivate.map(item => (
                                <AttachmentProcessItem
                                    key={item.id}
                                    data={item}
                                    itemCheckedp={getAttachmentCheckedValue(item.id)}
                                    processp={GetProcessStatus(item.id)}
                                    ClickProcess={(attachmentid,versionid) => CreateAttacment(attachmentid,versionid)}
                                    OnSelecteAttachment={onSelecteAttachment}
                                />
                            ))}
                        </CCol>
                    </CRow>

                </CustomTabPanel>
                <CustomTabPanel value={value} index={1}>
                    <CRow>
                        <CCol sm={12}>


                            {formAttachmentNotPrivate.map(item => (
                                <AttachmentProcessItem
                                    key={item.id}
                                    data={item}
                                    itemCheckedp={getAttachmentCheckedValue(item.id)}
                                    processp={GetProcessStatus(item.id)}
                                    OnProcessAttachment={() => CreateAttacment(item.id)}
                                    OnSelecteAttachment={onSelecteAttachment}
                                />
                            ))}

                        </CCol>
                    </CRow>
                </CustomTabPanel>



            </CCol>
        </>
    )
}


export default FileCreateProcess;


