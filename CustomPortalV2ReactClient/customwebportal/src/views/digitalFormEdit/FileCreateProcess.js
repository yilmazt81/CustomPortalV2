

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


import { GetConvertFileList } from 'src/lib/formMetaDataApi';

import { cilChevronCircleRightAlt } from '@coreui/icons';

import { CIcon } from '@coreui/icons-react';

import { CreateFormAttachment } from 'src/lib/CreateFileApi';

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



const FileCreateProcess = ({ formidp, loading, onError }) => {

    const { t } = useTranslation();

    const [formVersionList, setFormVersionList] = useState([]);
    const [formAttachmentPrivate, setformAttachmentPrivate] = useState([]);
    const [formAttachmentNotPrivate, setformAttachmentNotPrivate] = useState([]);

    const [formdefinationVersion, setformdefinationVersion] = useState(0);
    const [versionConvertFileUrl, setversionConvertFileUrl] = useState(null);
    const [formdefinationName, setformdefinationName] = useState("");

    const [processAttachmentList, setprocessAttachmentList] = useState([{ attachmentid: 0, process: false, downloadUrl: '' }]);


    const [value, setValue] = useState(0);

    const handleChangeTab = (event, newValue) => {
        setValue(newValue);
    };


    function handleChange(event) {
        const { name, value } = event.target;

        setformdefinationVersion(value);
        setversionConvertFileUrl(null);

    }

    async function LoadFormConvertTypes() {

        //loading(true);

        try {
            var convertTypeReturn = await GetConvertFileList(formidp);

            if (convertTypeReturn.returnCode === 1) {

                setformdefinationName(convertTypeReturn.data.formDefinationTypeName);
                setFormVersionList(convertTypeReturn.data.formVersions)
                setformAttachmentPrivate(convertTypeReturn.data.attachmentPrivateForForm);
                setformAttachmentNotPrivate(convertTypeReturn.data.attachmentNotPrivateForForm);
                if (convertTypeReturn.data.attachmentPrivateForForm.length === 0) {
                    setValue(1);
                }
                if (convertTypeReturn.data.formVersions.length != 0) {
                    setformdefinationVersion(convertTypeReturn.data.formVersions[0].id);
                }

            } else {
                onError(convertTypeReturn.returnMessage)
            }
        } catch (error) {
            onError(error.message);
        }

        loading(false);

    }

    useEffect(() => {

        LoadFormConvertTypes();


    }, []);


    async function CreateVersionFile() {

    }

    async function CreateAttacment(attachmentid) {

        var itemold = processAttachmentList.find(s => s.attachmentid === attachmentid);
        if (itemold === undefined) {
            processAttachmentList.push({ attachmentid: attachmentid, process: true, downloadUrl: '' });
        } else {
            itemold.process = true;
        }

        setprocessAttachmentList([...processAttachmentList]);
        var createFileReturn = await CreateFormAttachment(formidp, attachmentid);

        var itemold1 = processAttachmentList.find(s => s.attachmentid === attachmentid);

        itemold1.process = false;
        itemold1.downloadUrl="ttttt";
        setprocessAttachmentList([...processAttachmentList]);

    }

    function GetProcessStatus(attachmentid) {

        var item = processAttachmentList.find(s => s.attachmentid === attachmentid);

        if (item === undefined)
            return false;
        else
            return item.process;

    }
    function GetFilePath(attachmentid) {
        var item = processAttachmentList.find(s => s.attachmentid === attachmentid);

        if (item === undefined)
            return '';
        else
            return item.downloadUrl;
    }

    return (
        <>
            <CRow>
                <CCol sm={4}>

                    <CFormLabel className="form-label">{formdefinationName}</CFormLabel>
                </CCol>
                <CCol sm={6}>
                    <CFormSelect id='selectFormVersion' name="formVersion" value={formdefinationVersion}
                        onChange={e => handleChange(e)}>
                        {formVersionList.map(item => {

                            return (<option key={item.id} value={item.id} >{item.formLanguage}</option>);
                        })}
                    </CFormSelect>

                </CCol>
                <CCol sm={2}>

                    <CButton color="primary" variant="outline" shape="rounded-pill">
                        <CIcon icon={cilChevronCircleRightAlt} onClick={() => CreateVersionFile()} />
                    </CButton>

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
                            {formAttachmentPrivate.map(item => {
                                return (<AttachmentProcessItem
                                    OnProcessAttachment={() => CreateAttacment(item.id)}
                                    key={item.id} data={item}
                                    processp={GetProcessStatus(item.id)}

                                    downloadFilePathp={GetFilePath(item.id)}
                                ></AttachmentProcessItem>);
                            })}
                        </CCol>
                    </CRow>

                </CustomTabPanel>
                <CustomTabPanel value={value} index={1}>
                    <CRow>
                        <CCol sm={12}>


                            {formAttachmentNotPrivate.map(item => {
                                return (<AttachmentProcessItem
                                    key={item.id}
                                    data={item}
                                    OnProcessAttachment={() => CreateAttacment(item.id)}
                                    processp={GetProcessStatus(item.id)}
                                    downloadFilePathp={GetFilePath(item.id)}
                                ></AttachmentProcessItem>);
                            })}

                        </CCol>
                    </CRow>
                </CustomTabPanel>



            </CCol>
            <CRow>


            </CRow>
        </>
    )
}


export default FileCreateProcess;