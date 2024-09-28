import React, { useEffect, useState } from 'react'
import { useSearchParams } from 'react-router-dom';
import LoadingAnimation from 'src/components/LoadingAnimation';
import { useTranslation } from "react-i18next";
import { GetFormDefination, GetFormDefinationAttachments, GetFormDefinationAttachment,GetFonts } from 'src/lib/formdef';
import GridColumsFormDefinationAttachment from './GridColumsFormDefinationAttachment';
import { DataGrid } from '@mui/x-data-grid';
import FormAttachmentModal from './FormAttachmentModal';

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


} from '@coreui/react' ;


const FormDefinationAttachments = () => {
    const { t } = useTranslation();
    const [searchParams, setSearchParams] = useSearchParams();

    const [formdefinationTypeId, setFormDefinationTypeId] = useState(0);
    const [formdefination, setformdefination] = useState(null);
    const [formAttachment,setformAttachment]=useState();
    const [formversionEdit, setFormVersionEdit] = useState(false);
    const [formAttachmentDelete,setFormAttachmentDelete]=useState(false);
    const [loading, setLoading] = useState(false);
    const [errorMessage, setErrorMessage] = useState(null);
    const [formversionList, setFormVersionList] = useState([]);
    const [fontTypes,setFontTypes]=useState([]);


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


    async function LoadFonts() {
     

        setLoading(true);
        setErrorMessage(null);
        setFormVersionEdit(false);
        try {

            var fontsReturn = await GetFonts();
            if (fontsReturn.returnCode === 1) {
           
                setFontTypes(fontsReturn.data);
            } else {

                setErrorMessage(fontsReturn.returnMessage);
            }
        } catch (error) {
            setErrorMessage(error.message);
        } finally {
            setLoading(false);
        }
    }

    async function LoadFormAttachments(id) {
        setLoading(true);
        setErrorMessage(null);
        setFormVersionEdit(false);
        debugger;
        try {

            var versionDataReturn = await GetFormDefinationAttachments(id);
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
        LoadFormAttachments(id);
        LoadFonts();

    }, []);



    const optionClick = async (option, id) => {
        if (option == 'Edit') {

            var formVersionRequest = await GetFormDefinationAttachment(id);


            if (formVersionRequest.returnCode === 1) {
                
                setformAttachment(formVersionRequest.data);
                setFormVersionEdit(true);
            } else {

                setErrorMessage(formVersionRequest.returnMessage);
            }

        }else if (option == 'Delete') {

        }else if (option==="Download")
        {
            var formVersion=formversionList.find(s=>s.id==id);
            window.open(formVersion.filePath) ;
        }
        //EditGroupDefination(option === 'Delete', id);
    }

    function CreateNewFormVersion() {
 
        setformAttachment({active:true,
                            id:0,formName:"",
                            fileName:"",
                            fontSize:20,
                            bold:false,
                            italic:false,
                            fontFamily:"Times New Roman",
                            active:true,
                        });
        setFormVersionEdit(true);

    }

    function ModalClosed(){
        debugger;
        setFormVersionEdit(false);
        
        LoadFormAttachments(formdefinationTypeId)
        
    }
    const gridColumns = GridColumsFormDefinationAttachment(optionClick);

    return (
        <>
            <CCard>
                <CCardTitle>
                    {t("FormAttachmentTitle")}
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

            <FormAttachmentModal
                visiblep={formversionEdit}
                formdefinationp={formdefination}
                formAttachmentp={formAttachment}
                fontTypesp={fontTypes}
                OnCloseModal={() => setFormVersionEdit(false)}
                setFormData={() => ModalClosed()}
            ></FormAttachmentModal>
        </>
    )
}

export default FormDefinationAttachments;