import React, { useEffect, useState } from 'react'

import {
    CButton,
    CButtonGroup,
    CCard,
    CCardBody,
    CCol,
    CRow,
    CAlert,

} from '@coreui/react'


import { useNavigate } from "react-router-dom";
import GridcolumnsGroups from './dataGridGroup'
import GridcolumnFormFields from './dataGridFormFields';


import { DataGrid } from '@mui/x-data-grid';



import { useTranslation } from "react-i18next";

import DeleteModal from '../../components/DeleteModal';
import GroupEditModal from './groupEditmodal';
import { useSearchParams } from 'react-router-dom';

import StickyBox from "react-sticky-box";

import {
    CreateNewFormDefinationGroup,
    GetFormGroups,
    GetFormGroupFields,
    GetFormDefination, 
    GetFormGroup,
    CreateNewGroupField,
    DeleteFormGroup
} from '../../lib/formdef';
import { bool } from 'prop-types';

const FormDefinationEdit = () => {
    const navigate = useNavigate();
    const { t } = useTranslation();

    const gridcolumnFormFields = GridcolumnFormFields();



    const [searchParams, setSearchParams] = useSearchParams();
 
    const [visiblemodalGroup, setvisiblemodalGroup] = useState(false);
    const [visiblemodalGroupDelete, setvisiblemodalGroupDelete] = useState(false);

    const [formdefinationEdit, setFormDefinationEdit] = useState({ id: 0, formName: "" });
    const [formdefinationGroups, setformdefinationGroups] = useState([]);
    const [formgroupFields, setformgroupFields] = useState([]);

    const [formdefinationGroup, setformdefinationGroup] = useState(null);


    const [deleteStart, setDeleteStart] = useState(false);
     
    const [saveError, setSaveError] = useState(null);
    const [deleteError, setDeleError] = useState(null);

    useEffect(() => {

        const id = searchParams.get('formdefinationId');

        GetDefination(id);
        GetGroups(id);
 

    }, []);

    async function GetDefination(id) {
        var getdefinationReturn = await GetFormDefination(id);

        if (getdefinationReturn.returnCode === 1) {
            setFormDefinationEdit(getdefinationReturn.data);
            //setVisible(true);
        } else {
            setSaveError(getdefinationReturn.returnMessage);
        }
    }



    async function GetGroups(formdefinationId) {

        var getgroupsReturn = await GetFormGroups(formdefinationId);

        if (getgroupsReturn.returnCode === 1) {
            setformdefinationGroups(getgroupsReturn.data);

        } else {
            setSaveError(getgroupsReturn.returnMessage);
        }
    } 

    async function EditGroupDefination(forDelete, id) {
        try {
            setSaveError(null);
            setvisiblemodalGroup(false);
            setvisiblemodalGroupDelete(false);
            var editformdefination = await GetFormGroup(id);

            if (editformdefination.returnCode === 1) {
                setformdefinationGroup(editformdefination.data);
                if (!forDelete) {
                    setvisiblemodalGroup(true);
                } else {
                    setvisiblemodalGroupDelete(true);
                }

            } else {
                setSaveError(editformdefination.returnMessage);
            }
        } catch (error) {
            setSaveError(error.message);
        } finally {
            // setdeleteStart(false);
        }
    }

    async function NewGroupDefination() {
        try {
            setSaveError(null);
            setvisiblemodalGroup(false);
            var editformdefination = await CreateNewFormDefinationGroup(formdefinationEdit.id);

            if (editformdefination.returnCode === 1) {
                setformdefinationGroup(editformdefination.data);
                setvisiblemodalGroup(true);
            } else {
                setSaveError(editformdefination.returnMessage);
            }
        } catch (error) {
            setSaveError(error.message);
        } finally {
            // setdeleteStart(false);
        }
    }

    async function DeleteGroupDefination() {
        

        try {
            setSaveError(null); 
            setDeleteStart(true);
            
            var editformdefination = await DeleteFormGroup(formdefinationEdit.id,formdefinationGroup.id);

            if (editformdefination.returnCode === 1) {
                setformdefinationGroup(editformdefination.data);
                setvisiblemodalGroupDelete(false);
                GetGroups(formdefinationEdit.id);
            } else {
                setDeleError(editformdefination.returnMessage);
            }
        } catch (error) {
            setDeleError(error.message);
        } finally {
              setDeleteStart(false);
        }
    }


    async function handleSelectFormGroupClick(params) {

         

        try {
            var getgroupFieldReturn = await GetFormGroupFields(params.id);

            if (getgroupFieldReturn.returnCode === 1) {
                setformgroupFields(getgroupFieldReturn.data);
                //setVisible(true);
            } else {
                setSaveError(getgroupFieldReturn.returnMessage);
            }
        } catch (error) {
            setSaveError(error.message);
        }
    };

    const optionClick = (option, id) => {
        EditGroupDefination(option === 'Delete', id);

    }

    const gridcolumnsGroups = GridcolumnsGroups(optionClick);

    return (
        <>
            <CCard className="mb-4">
                <CCardBody>
                    <CRow>
                        <CCol>
                            <CButtonGroup role="group">
                                <CButton color="primary" shape='rounded-3' onClick={() => NewGroupDefination()} > {t("AddNewFormDefination")}</CButton>
                            </CButtonGroup>
                        </CCol>
                    </CRow>
                    <CRow>
                        <CCol xs={5}>

                            <DataGrid rows={formdefinationGroups}
                                columns={gridcolumnsGroups}
                                onRowClick={handleSelectFormGroupClick}
                            />
                        </CCol>
                        <CCol xs={7}>
                            <StickyBox>

                                <CRow>
                                    <CCol>
                                        <CButtonGroup role="group">
                                            <CButton color="primary" shape='rounded-3' onClick={() => NewGroupDefination()} > {t("AddNewFormDefination")}</CButton>
                                        </CButtonGroup>
                                    </CCol>
                                </CRow>

                                <CRow>
                                    <CCol>
                                        <DataGrid columns={gridcolumnFormFields}
                                            rows={formgroupFields}

                                        ></DataGrid>
                                    </CCol>
                                </CRow>

                            </StickyBox>
                        </CCol>

                    </CRow>
                    <CRow>
                        {
                            saveError != null ?
                                <CAlert color="warning">{saveError}</CAlert>
                                : ""
                        }
                    </CRow>
                </CCardBody>
            </CCard>

            <GroupEditModal visiblep={visiblemodalGroup}
                formdefinationGroupp={formdefinationGroup}
                setFormData={() => GetGroups(formdefinationEdit.id)}

            ></GroupEditModal>


            <DeleteModal visiblep={visiblemodalGroupDelete}
                OnClickOk={(data) => DeleteGroupDefination()}
                title={t("GroupDelete")}
                message={formdefinationGroup?.name}
                message2={t("GroupDeleteMessage")}
                saveError={deleteError}
                saveStart={deleteStart}></DeleteModal>



        </>
    )
}

export default FormDefinationEdit;