
import React, { useEffect, useContext, useState } from 'react'

import { useTranslation } from "react-i18next";
import { UrlContext } from 'src/lib/URLContext.jsx';
import { GetCustomeFields, CreateCustomeField,SaveCustomeField } from 'src/lib/formdef.jsx';
import CustomeFieldsGrid from './CustomeFieldsGrid';
import DeleteModal from 'src/components/DeleteModal.jsx';
import EditModal from './editModal';
import { DataGrid } from '@mui/x-data-grid'

import {
    CButton,
    CButtonGroup,
    CCard,
    CCardBody,
    CCol,
    CRow,
    CAlert,
} from '@coreui/react'


const CustomeFields = () => {
    const { t } = useTranslation();
    const { dispatch } = useContext(UrlContext);
    const [customeField, setcustomeField] = useState(null);
    const [visibleDelete, setVisibleDelete] = useState(false);
    const [visibleEdit, setvisibleEdit] = useState(false);
    const [customeFields, setcustomeFields] = useState([]);
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
            payload: { pathname: "./customefields", name: t("CustomeFieldTitle"), active: false }
        });
    }

    async function LoadCustomeFields() {

        try {
            var customeFieldList = await GetCustomeFields();
            if (customeFieldList.returnCode === 1) {
                setcustomeFields(customeFieldList.data);
            } else {
                setsaveError(customeFieldList.ReturnMessage);
            }
        } catch (error) {
            setsaveError(error.message);

        }
    }
    useEffect(() => {

        try {
            SetLocationAdress();
            LoadCustomeFields();


        } catch (error) {
            console.log(error);
        }

    }, []);



    async function NewCustomeField() {
        try {
            setvisibleEdit(false);
            var createNewCustomeField = await CreateCustomeField();

            if (createNewCustomeField.returnCode === 1) {
                setcustomeField(createNewCustomeField.data);
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

    const gridColumns = CustomeFieldsGrid(optionClick);


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

                            <DataGrid rows={customeFields}
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
            customeFieldp={customeField} 
            setFormData={() => SaveComplated()}
            
            >


            </EditModal>

            <DeleteModal
                setClose={() => setVisibleDelete(false)}
                message={customeField?.fieldName}
                title={t("ModalDeleteProductTitle")}
                visiblep={visibleDelete}
                message2={t("AutoComplateMapDeleteMessage")}
            // OnClickOk={() => DeleteProductDB()}
            >


            </DeleteModal>
        </>
    )

}

export default CustomeFields;


