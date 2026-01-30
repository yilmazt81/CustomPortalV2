import React, { useEffect, useState, useContext } from 'react'

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

import DeleteModal from '../../components/DeleteModal.jsx';
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
} from '../../lib/formdef.jsx';
import { UrlContext } from "../../lib/URLContext.jsx";
import CloneFormGroupModal from './cloneFormGroupModal';


const FormDefinationEdit = () => {
    const navigate = useNavigate();
    const { t } = useTranslation();


    const { dispatch } = useContext(UrlContext);
    const [searchParams, setSearchParams] = useSearchParams();

    const [visiblemodalGroup, setvisiblemodalGroup] = useState(false);
    const [visiblemodalGroupDelete, setvisiblemodalGroupDelete] = useState(false);

    const [visiblemodalEditField, setvisiblemodalEditField] = useState(false);
    const [visibleEditComboItems, setvisibleEditComboItems] = useState(false);
    const [visibleCloneModal, setvisibleCloneModal] = useState(false);


    const [formdefinationEdit, setFormDefinationEdit] = useState({ id: 0, formName: "" });
    const [formdefinationGroups, setformdefinationGroups] = useState([]);
    const [formgroupFields, setformgroupFields] = useState([]);
    const [fieldTypes, setfieldTypes] = useState([]);
    const [fontTypes, setfontTypes] = useState([]);
    const [formdefinationGroup, setformdefinationGroup] = useState(null);
    const [formdefinationFieldEdit, setformdefinationFieldEdit] = useState(null);
    const [currentFormdefinationType, setcurrentFormdefinationType] = useState(null);

    const [deleteStart, setDeleteStart] = useState(false);

    const [saveError, setSaveError] = useState(null);
    const [deleteError, setDeleError] = useState(null);
    /*
        useEffect(async () => {
    
            const id = searchParams.get('formdefinationId');
    
            LoadFieldTypes();
            LoadFontTypes();
            GetDefination(id);
            GetGroups(id); 
    
            return () => {
                console.log("unmount");
            }
    
        }, []);
        */
    useEffect(() => {
        const controller = new AbortController();
        const { signal } = controller;

        const load = async () => {
            try {
                const id = searchParams.get("formdefinationId");

                // Paralel yükleme (daha hızlı)
                await Promise.all([
                    LoadFieldTypes({ signal }),
                    LoadFontTypes({ signal }),
                    GetDefination(id, { signal }),
                    GetGroups(id, { signal })
                ]);

            } catch (err) {
                if (err.name !== "AbortError") {
                    console.error("Hata:", err);
                }
            }
        };

        load();

        return () => {
            console.log("Unmount → fetch iptal edildi");
            controller.abort();   // cleanup
        };
    }, []);

    function SetLocationAdress(formdefination) {

        dispatch({ type: 'reset' })

        dispatch({
            type: 'Add',
            payload: { pathname: "#/FormDefinationType", name: t("FormDefinations"), active: true }
        });
        dispatch({
            type: 'Add',
            payload: { pathname: "/FormDefinationTypeEdit/formdefinationId=", name: formdefination?.formName, active: false }
        });
    }

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
        try {

            var getdefinationReturn = await GetFormDefination(id);

            if (getdefinationReturn.returnCode === 1) {
                setFormDefinationEdit(getdefinationReturn.data);

                setcurrentFormdefinationType(getdefinationReturn.data);
                SetLocationAdress(getdefinationReturn.data);

                // return getdefinationReturn.data;
                //setVisible(true);
            } else {
                setSaveError(getdefinationReturn.returnMessage);
            }
        } catch (error) {
            setSaveError(error.message);

        } finally {

        }
    }



    async function GetGroups(formdefinationId) {

        try {

            var getgroupsReturn = await GetFormGroups(formdefinationId);

            if (getgroupsReturn.returnCode === 1) {
                setformdefinationGroups(getgroupsReturn.data);

            } else {
                setSaveError(getgroupsReturn.returnMessage);
            }
        } catch (error) {
            setSaveError(error.message);
        } finally {

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


    async function CloneGroupDefination() {
        setvisibleCloneModal(true);
    }

    async function NewGroupFieldDefination() {
        try {
            setSaveError(null);
            setvisiblemodalGroup(false);
            setvisibleEditComboItems(false);
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
        } finally {

        }
    }

    async function handleSelectFormGroupClick(params) {
        setvisiblemodalGroup(false);
        GetGroupFieldList(params.id);
        //setformdefinationGroup
    };


    async function GridGroupRowChange(e) {

        if (e.length > 0) {
            try {
                var editformdefination = await GetFormGroup(e);

                if (editformdefination.returnCode === 1) {
                    setformdefinationGroup(editformdefination.data);
                }

            } catch (error) {
                debugger;
                setSaveError(error.message);

            }
        }

    }


    async function EditDefinationField(operation, id) {
        try {
            setSaveError(null);
            setvisiblemodalGroup(false);
            setvisiblemodalGroupDelete(false);
            setvisiblemodalEditField(false);
            var editformdefination = await GetFormDefinationField(id);

            if (editformdefination.returnCode === 1) {
                setformdefinationFieldEdit(editformdefination.data);
                if (operation === 'Edit') {
                    setvisiblemodalEditField(true);
                } else if (operation == 'AddComboItem') {
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

    const optionClickField = (option, id) => {

        EditDefinationField(option, id);
    }

    const CheckItemValueChange = (option, id, checked) => {
        var field = formgroupFields.find(f => f.id === id);
        debugger;
        if (option === 'mandatory') {
            field.mandatory= !checked;
        } else if (option === 'deleted') {
             field.deleted= !checked;
        }
    }


    const gridcolumnsGroups = GridcolumnsGroups(optionClick);


    const gridcolumnFormFields = GridcolumnFormFields(optionClickField, CheckItemValueChange);

    const CloseCloneModal = (data) => {
        setvisibleCloneModal(false);
        setformdefinationGroups(data);
    }

    return (
        <>
            <CCard className="mb-4">
                <CCardBody>
                    <CRow>
                        <CCol>
                            <CButtonGroup role="group">
                                <CButton color="primary" shape='rounded-3'
                                    onClick={() => NewGroupDefination()} > {t("AddNewFormGroup")}</CButton>
                                <CButton color="primary" shape='rounded-3'
                                    onClick={() => CloneGroupDefination()} > {t("CloneNewFormGroup")}</CButton>

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
                OnClickOk={() => DeleteGroupDefination()}
                OnClickCancel={() => setvisiblemodalGroupDelete(false)}
                title={t("GroupDelete")}
                message={formdefinationGroup?.name}
                message2={t("GroupDeleteMessage")}
                saveError={deleteError}
                saveStart={deleteStart}></DeleteModal>

            <CloneFormGroupModal
                visiblep={visibleCloneModal}
                formdefinationP={currentFormdefinationType}
                setClosed={() => setvisibleCloneModal(false)}
                setFormData={(data) => CloseCloneModal(data)}

            ></CloneFormGroupModal>

        </>
    )
}

export default FormDefinationEdit;


