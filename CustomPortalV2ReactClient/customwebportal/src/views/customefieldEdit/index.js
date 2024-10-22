
import React, { useEffect, useContext, useState } from 'react'

import { useTranslation } from "react-i18next";
import { UrlContext } from 'src/lib/URLContext';
import { CreateCustomeField, GetCustomeFieldItems, CreateCustomeFieldItem } from 'src/lib/formdef';
import CustomefieldsGrid from './CustomefieldsGrid';
import DeleteModal from 'src/components/DeleteModal';
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
        // EditGroupDefination(option === 'Delete', id);
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
            var customeFieldList = await GetCustomeFieldItems();
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
            //LoadCustomeFields();


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
                setClose={() => setVisibleDelete(false)}
                message={customeFieldItem?.fieldCaption}
                title={t("ModalDeleteProductTitle")}
                visiblep={visibleDelete}
                message2={t("AutoComplateMapDeleteMessage")}
            // OnClickOk={() => DeleteProductDB()}
            >


            </DeleteModal>
        </>
    )

}

export default CustomefieldEdit;