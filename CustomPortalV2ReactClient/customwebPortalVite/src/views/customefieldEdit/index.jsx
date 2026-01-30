
import React, { useEffect, useContext, useState } from 'react'

import { useTranslation } from "react-i18next";
import { UrlContext } from 'src/lib/URLContext.jsx';
import { SaveCustomeFieldItem, GetCustomeFieldItems, CreateCustomeFieldItem } from 'src/lib/formdef.jsx';
import CustomefieldsGrid from './CustomefieldsGrid';
import DeleteModal from 'src/components/DeleteModal.jsx';
import EditModal from './editModal';
import { DataGrid } from '@mui/x-data-grid'


import { useSearchParams } from 'react-router-dom';

import {
    CButton,
    CButtonGroup,
    CCard,
    CCardBody,
    CCol,
    CRow,
    CAlert,
} from '@coreui/react'


const CustomefieldEdit = () => {

    const { t } = useTranslation();
    const { dispatch } = useContext(UrlContext);
    const [searchParams, setSearchParams] = useSearchParams();

    const [customeFieldId, setcustomeFieldId] = useState(0);
    const [customeField, setcustomeField] = useState(null);
    const [customeFieldItem, setcustomeFieldItem] = useState(null);
    const [visibleDelete, setVisibleDelete] = useState(false);
    const [visibleEdit, setvisibleEdit] = useState(false);
    const [customeFieldItems, setcustomeFieldItems] = useState([]);
    const [saveError, setsaveError] = useState(null);


    const optionClick = (option, id) => {
        EditCustomeFieldItemDefination(option === 'Delete', id);
    }


    async function EditCustomeFieldItemDefination(forDelete, id) {


        var tmpCustomeField = customeFieldItems.find(s => s.id === id);
        setcustomeFieldItem(tmpCustomeField);
        if (!forDelete) {
            setvisibleEdit(true);
        } else {
            setVisibleDelete(true);
        }

    }

    function SetLocationAdress() {

        dispatch({ type: 'reset' })
        dispatch({
            type: 'Add',
            payload: { pathname: "../#/FormDefinationType", name: t("FormDefinations"), active: true }
        });
        dispatch({
            type: 'Add',
            payload: { pathname: "../#/customefields", name: t("CustomeFieldTitle"), active: true }
        });

        dispatch({
            type: 'Add',
            payload: { pathname: "./customefieldEdit", name: t("CustomeFieldTitleItems"), active: false }
        });
    }

    async function LoadCustomeFields() {

        try {
            const id = searchParams.get('customeFieldid');
            var customeFieldList = await GetCustomeFieldItems(id);
            if (customeFieldList.returnCode === 1) {
                setcustomeFieldItems(customeFieldList.data);
            } else {
                setsaveError(customeFieldList.ReturnMessage);
            }
        } catch (error) {
            setsaveError(error.message);

        }
    }
    useEffect(() => {

        try {

            const id = searchParams.get('customeFieldid');

            setcustomeFieldId(id);
            SetLocationAdress();
            LoadCustomeFields();


        } catch (error) {
            console.log(error);
        }

    }, []);



    async function NewCustomeField() {
        try {
            setvisibleEdit(false);
            debugger;
            var createNewCustomeField = await CreateCustomeFieldItem(customeFieldId);

            if (createNewCustomeField.returnCode === 1) {
                setcustomeFieldItem(createNewCustomeField.data);
                setvisibleEdit(true);
            } else {
                setsaveError(createNewCustomeField.returnMessage);
            }
        } catch (error) {
            setsaveError(error.message);
        } finally {
            // setdeleteStart(false);
        }
    }

    async function SaveComplated() {
        setvisibleEdit(false);
        LoadCustomeFields();

    }


    async function DeleteCustomeItemData() {

        try {
            debugger;
            customeFieldItem.deleted=true;
            var customeFieldReturn = await SaveCustomeFieldItem(customeFieldItem);
            if (customeFieldReturn.returnCode === 1) {
                setVisibleDelete(false);
                LoadCustomeFields();
            } else {
                setsaveError(customeFieldReturn.ReturnMessage);
            }

        } catch (error) {
            setsaveError(error.message);
        } finally {

            //setsaveStart(false);
        }

    }

    const gridColumns = CustomefieldsGrid(optionClick);


    return (
        <>
            <CCard className="mb-4">
                <CCardBody>
                    <CRow>
                        <CCol>
                            <CButtonGroup role="group">
                                <CButton color="primary" shape='rounded-3' onClick={() => NewCustomeField()}  > {t("AddNewFormDefination")}</CButton>
                            </CButtonGroup>
                        </CCol>
                    </CRow>

                    <CRow>

                        <CCol>

                            <DataGrid rows={customeFieldItems}
                                columns={gridColumns}
                                slotProps={{
                                    toolbar: {
                                        showQuickFilter: true,
                                    },
                                }}
                            />

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


            <EditModal visiblep={visibleEdit}
                setFormData={() => SaveComplated()}
                customeFielditemp={customeFieldItem}
                visibleEdit={visibleEdit}


            >


            </EditModal>

            <DeleteModal
                message={customeFieldItem?.fieldCaption}
                title={t("ModalDeleteCustomeFieldTitle")}
                visiblep={visibleDelete}
                message2={t("AutoComplateCustomeFieldDeleteMessage")}
                OnClickCancel={() => setVisibleDelete(false)}
                OnClickOk={() =>DeleteCustomeItemData()}
            >


            </DeleteModal>
        </>
    )

}

export default CustomefieldEdit;


