import React, { useEffect, useState } from 'react'
import { useSearchParams } from 'react-router-dom';
import LoadingAnimation from 'src/components/LoadingAnimation';
import { useTranslation } from "react-i18next";
import { GetFormDefination, GetFormDefinationVersions, GetFormDefinationVersion } from 'src/lib/formdef';
import GridColumsFormDefinationVersion from './GridColumsFormDefinationVersion';
import { DataGrid } from '@mui/x-data-grid';
import FormVersionModal from './FormVersionModal';

import {
    CCard,
    CCardBody,
    CCol,
    CAlert,
    CRow,
    CFormLabel,
    CFormSelect,
    CForm,
    CCardTitle,
    CButtonGroup,
    CButton


} from '@coreui/react'



const FormDefinationVersion = () => {
    const { t } = useTranslation();
    const [searchParams, setSearchParams] = useSearchParams();

    const [formdefinationTypeId, setFormDefinationTypeId] = useState(0);
    const [formdefination, setformdefination] = useState(null);
    const [formVersion,setFormVersion]=useState({active:true,id:0,formLanguage:"",fileName:""});
    const [formversionEdit, setFormVersionEdit] = useState(false);
    const [loading, setLoading] = useState(false);
    const [errorMessage, setErrorMessage] = useState(null);
    const [formversionList, setFormVersionList] = useState([]);


    async function GetDefination(id) {

        setLoading(true);
        setErrorMessage(null);
        try {
            var versionDataReturn = await GetFormDefination(id);

            if (versionDataReturn.returnCode === 1) {

                setformdefination(versionDataReturn.data);
            } else {

                setErrorMessage(versionDataReturn.returnMessage);
            }
        } catch (error) {
            setErrorMessage(error.message);
        } finally {
            setLoading(false);
        }

    }

    async function LoadFormVersions(id) {
        setLoading(true);
        setErrorMessage(null);
        setFormVersionEdit(false);
        try {

            var versionDataReturn = await GetFormDefinationVersions(id);
            if (versionDataReturn.returnCode === 1) {
           
                setFormVersionList(versionDataReturn.data);
            } else {

                setErrorMessage(versionDataReturn.returnMessage);
            }
        } catch (error) {
            setErrorMessage(error.message);
        } finally {
            setLoading(false);
        }

    }


    useEffect(() => {

        const id = searchParams.get('formdefinationId');
        setFormDefinationTypeId(id);
        GetDefination(id);
        LoadFormVersions(id);

    }, []);



    const optionClick = async (option, id) => {
        if (option == 'Edit') {

            var formVersionRequest = await GetFormDefinationVersion(id);


            if (formVersionRequest.returnCode === 1) {
                
                setFormVersion(formVersionRequest.data);
                setFormVersionEdit(true);
            } else {

                setErrorMessage(formVersionRequest.returnMessage);
            }

        } else if (option == 'Delete') {

        }else if (option==="Download")
        {
            var formVersion=formversionList.find(s=>s.id==id);
            window.open(formVersion.filePath) ;
        }
        //EditGroupDefination(option === 'Delete', id);
    }

    function CreateNewFormVersion() {

        setFormVersion({active:true,id:0,formLanguage:"",fileName:""});
        setFormVersionEdit(true);

    }

    const gridColumns = GridColumsFormDefinationVersion(optionClick);

    return (
        <>
            <CCard>
                <CCardTitle>
                    {t("FormVersionTitle")}
                </CCardTitle>
                <CCardBody>
                    <CRow>

                        <CCol>

                            <CButtonGroup role="group">
                                <CButton color="primary" shape='rounded-3' onClick={() => CreateNewFormVersion()} > {t("AddNewFormDefination")}</CButton>
                            </CButtonGroup>

                        </CCol>
                    </CRow>

                    <CRow>

                        <CCol>
                            <DataGrid
                                columns={gridColumns}
                                rows={formversionList}
                            >

                            </DataGrid>
                        </CCol>
                    </CRow>
                    <CRow>
                        <LoadingAnimation loading={loading} size={"%10"}></LoadingAnimation>
                    </CRow>

                </CCardBody>
            </CCard>

            <FormVersionModal
                visiblep={formversionEdit}
                formdefinationp={formdefination}
                formdefinationVersionp={formVersion}
                OnCloseModal={() => setFormVersionEdit(false)}
                setFormData={() => LoadFormVersions(formdefinationTypeId)}
            ></FormVersionModal>
        </>
    )
}

export default FormDefinationVersion;