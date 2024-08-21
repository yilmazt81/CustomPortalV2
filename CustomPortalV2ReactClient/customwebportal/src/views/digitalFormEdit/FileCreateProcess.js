

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

import { cibCmake } from '@coreui/icons';

import { CIcon } from '@coreui/icons-react';

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
            debugger;
            if (convertTypeReturn.returnCode === 1) {
                debugger;
                setformdefinationName(convertTypeReturn.data.formDefinationTypeName);
                setFormVersionList(convertTypeReturn.data.formVersions)
                setformAttachmentPrivate(convertTypeReturn.data.attachmentPrivateForForm);
                setformAttachmentNotPrivate(convertTypeReturn.data.attachmentNotPrivateForForm);
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

    return (
        <>
            <CRow>
                <CCol sm={4}>

                    <CFormLabel className="form-label">{formdefinationName}</CFormLabel>
                </CCol>
                <CCol sm={6}>
                    <CFormSelect id='selectFormVersion' name="formVersion"
                        onChange={e => handleChange(e)}>
                        <option value={0}></option>
                        {formVersionList.map(item => {
                            return (<option key={item.id} value={item.id} >{item.formLanguage}</option>);
                        })}
                    </CFormSelect>

                </CCol>
                <CCol sm={2}>

                    <CButton color="primary"  >
                        <CIcon icon={cibCmake} onClick={() => CreateVersionFile()} />
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
                                return (<AttachmentProcessItem key={item.id} data={item}></AttachmentProcessItem>);
                            })}
                        </CCol>
                    </CRow>

                </CustomTabPanel>
                <CustomTabPanel value={value} index={1}>
                    <CRow>
                        <CCol sm={12}>


                            {formAttachmentNotPrivate.map(item => {
                                return (<AttachmentProcessItem key={item.id} data={item}></AttachmentProcessItem>);
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