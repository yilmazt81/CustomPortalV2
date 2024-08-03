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
import { DataGrid } from '@mui/x-data-grid';




import { useTranslation } from "react-i18next";

import DeleteModal from '../../components/DeleteModal';
import GroupEditModal from './groupEditmodal';
import FieldEditModal from './fieldEditModal';
import ComboBoxItemEditModal from './comboBoxItemEditModal';

import GridcolumnsGroups from './dataGridGroup'
import GridcolumnFormFields from './dataGridFormFields';
import { useSearchParams } from 'react-router-dom';

import StickyBox from "react-sticky-box";

import {
    CreateNewFormDefinationGroup,
    GetFormGroups,
    GetFormGroupFields,
    GetFormDefination,
    GetFormGroup,
    CreateNewGroupField,
    DeleteFormGroup,
    GetFieldTypes,
    GetFonts,
    GetFormDefinationField
} from '../../lib/formdef';
import { bool } from 'prop-types';

const FormDefinationEdit = () => {
    const navigate = useNavigate();
    const { t } = useTranslation();



    const [searchParams, setSearchParams] = useSearchParams();

    const [visiblemodalGroup, setvisiblemodalGroup] = useState(false);
    const [visiblemodalGroupDelete, setvisiblemodalGroupDelete] = useState(false);

    const [visiblemodalEditField, setvisiblemodalEditField] = useState(false);
    const [visibleEditComboItems, setvisibleEditComboItems] = useState(false);


    const [formdefinationEdit, setFormDefinationEdit] = useState({ id: 0, formName: "" });
    const [formdefinationGroups, setformdefinationGroups] = useState([]);
    const [formgroupFields, setformgroupFields] = useState([]);
    const [fieldTypes, setfieldTypes] = useState([]);
    const [fontTypes, setfontTypes] = useState([]);
    const [formdefinationGroup, setformdefinationGroup] = useState(null);
    const [formdefinationFieldEdit, setformdefinationFieldEdit] = useState(null);


    const [deleteStart, setDeleteStart] = useState(false);

    const [saveError, setSaveError] = useState(null);
    const [deleteError, setDeleError] = useState(null);

    useEffect(() => {

        const id = searchParams.get('formdefinationId');
        LoadFieldTypes();
        LoadFontTypes();
        GetDefination(id);
        GetGroups(id);


    }, []);

    async function LoadFieldTypes() {
        var getFieldTypesreturn = await GetFieldTypes();

        if (getFieldTypesreturn.returnCode === 1) {
            setfieldTypes(getFieldTypesreturn.data);
            //setVisible(true);
        } else {
            setSaveError(getFieldTypesreturn.returnMessage);
        }

    }
    async function LoadFontTypes() {

        var getFontTypesReturn = await GetFonts();

        if (getFontTypesReturn.returnCode === 1) {
            setfontTypes(getFontTypesReturn.data);
            //setVisible(true);
        } else {
            setSaveError(getFontTypesReturn.returnMessage);
        }

    }

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


    async function NewGroupFieldDefination() {
        try {
            setSaveError(null);
            setvisiblemodalGroup(false);
            var editfieldfination = await CreateNewGroupField(formdefinationEdit.id, formdefinationGroup.id);

            if (editfieldfination.returnCode === 1) {

                setformdefinationFieldEdit(editfieldfination.data);

                setvisiblemodalEditField(true);

            } else {
                setSaveError(editfieldfination.returnMessage);
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

            var editformdefination = await DeleteFormGroup(formdefinationEdit.id, formdefinationGroup.id);

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

    async function GetGroupFieldList(groupId) {
        try {
            var getgroupFieldReturn = await GetFormGroupFields(groupId);

            if (getgroupFieldReturn.returnCode === 1) {
                setformgroupFields(getgroupFieldReturn.data);

            } else {
                setSaveError(getgroupFieldReturn.returnMessage);
            }
        } catch (error) {
            setSaveError(error.message);
        }
    }

    async function handleSelectFormGroupClick(params) {
        setvisiblemodalGroup(false);
        GetGroupFieldList(params.id);
        //setformdefinationGroup
    };


    async function GridGroupRowChange(e) {

        var editformdefination = await GetFormGroup(e);

        if (editformdefination.returnCode === 1) {
            setformdefinationGroup(editformdefination.data);
        }
    }

    
    async function EditDefinationField(operation, id) {
        try {
            setSaveError(null);
            setvisiblemodalGroup(false);
            setvisiblemodalGroupDelete(false);
            var editformdefination = await GetFormDefinationField(id);

            if (editformdefination.returnCode === 1) {
                setformdefinationFieldEdit(editformdefination.data);
                if (operation==='Edit') {
                    setvisiblemodalEditField(true);
                } else if (operation=='AddComboItem') {
                    setvisibleEditComboItems(true);
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

    const optionClick = (option, id) => {
        EditGroupDefination(option === 'Delete', id);
    }

    const   optionClickField = (option, id) => {
        
        EditDefinationField(option , id);
    }

    const CheckItemValueChange = (option, id, checked) => {
        debugger;

    }


    const gridcolumnsGroups = GridcolumnsGroups(optionClick);


    const gridcolumnFormFields = GridcolumnFormFields(optionClickField, CheckItemValueChange);

    return (
        <>
            <CCard className="mb-4">
                <CCardBody>
                    <CRow>
                        <CCol>
                            <CButtonGroup role="group">
                                <CButton color="primary" shape='rounded-3'
                                    onClick={() => NewGroupDefination()} > {t("AddNewFormDefination")}</CButton>

                            </CButtonGroup>
                        </CCol>
                    </CRow>
                    <CRow>
                        <CCol xs={5}>

                            <DataGrid rows={formdefinationGroups}
                                columns={gridcolumnsGroups}
                                onRowClick={handleSelectFormGroupClick}
                                onRowSelectionModelChange={(e) => GridGroupRowChange(e)}
                            />
                        </CCol>
                        <CCol xs={7}>
                            <StickyBox>

                                <CRow>
                                    <CCol>
                                        <CButtonGroup role="group">
                                            <CButton color="primary" shape='rounded-3' onClick={() => NewGroupFieldDefination()} > {t("AddNewFormDefination")}</CButton>
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

            <FieldEditModal
                formDefinationFieldp={formdefinationFieldEdit}
                visiblep={visiblemodalEditField}
                setFormData={() => GetGroupFieldList(formdefinationGroup.id)}
                fieldTypesp={fieldTypes}
                formGroupp={formdefinationGroup}
                fontTypesp={fontTypes}
            ></FieldEditModal>

            <ComboBoxItemEditModal 
                visiblep={visibleEditComboItems}
                formDefinationFieldp={formdefinationFieldEdit}
            
            ></ComboBoxItemEditModal>

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