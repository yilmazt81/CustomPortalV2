import React, { useEffect, useState, useRef } from 'react'

import {
    CButton,
    CCol,
    CAlert,
    CRow,
    CModal,
    CModalHeader,
    CModalTitle,
    CModalFooter,
    CModalBody,
    CFormLabel,
    CFormInput,
    CFormCheck,
    CFormSwitch,
    CForm,

} from '@coreui/react'

import { GetComboBoxItems, CreateComboBoxItem, SaveComboBoxItem } from '../../lib/formdef.jsx';
import Lottie from 'lottie-react';
import PropTypes from 'prop-types';
import AddCircleOutlineIcon from '@mui/icons-material/AddCircleOutline';
import ProcessAnimation from "../../content/animation/Process.json";
import DataGridComboItems from './dataGridComboItems';

import { DataGrid } from '@mui/x-data-grid';
import IconButton from '@mui/material/IconButton';

import { useTranslation } from "react-i18next";


const ComboBoxItemEditModal = ({ visiblep, formDefinationFieldp, setFormData }) => {


    const [visible, setvisible] = useState(visiblep);
    const [validated, setvalidated] = useState(false);
    const [formDefinationField, setformDefinationField] = useState({ ...formDefinationFieldp });
    const [comboboxItems, setComboboxItems] = useState([]);
    const [comboboxItem, setComboboxItem] = useState(null);

    const [saveError, setSaveError] = useState(null);

    const [saveStart, setsaveStart] = useState(false);

    const { t } = useTranslation();
    const formRef = useRef(null); 


    function handleChange(event) {
        const { name, value } = event.target;

        setComboboxItem({ ...comboboxItem, [name]: value });

    }

    async function ClosedClick() {
        setvisible(false);
    }

    useEffect(() => {
        setSaveError(null);
     
        setvisible(visiblep);
     
        setformDefinationField(formDefinationFieldp);
        //LoadBranchList();
        LoadComboboxItems();
        NewComboBoxItem();
    }, [visiblep, formDefinationFieldp])


    async function NewComboBoxItem() {


        try {
            setsaveStart(true); 
            var comboboxItemReturn = await CreateComboBoxItem(formDefinationField.tagName);

            if (comboboxItemReturn.returnCode === 1) {
                setComboboxItem(comboboxItemReturn.data);

            } else {
                setSaveError(comboboxItemReturn.returnMessage);
            }

        } catch (error) {
            setSaveError(error.message);
        } finally {
            setsaveStart(false);
        }

    }

    async function LoadComboboxItems() {

        try {
            setsaveStart(true);
     
            var comboboxItemsReturn = await GetComboBoxItems(formDefinationField.tagName);

            if (comboboxItemsReturn.returnCode === 1) {
                setComboboxItems(comboboxItemsReturn.data);

            } else {
                setSaveError(comboboxItemsReturn.returnMessage);
            }

        } catch (error) {
            setSaveError(error.message);
        } finally {
            setsaveStart(false);
        }
    }

    async function SaveData() {

        try {

            try {
                setSaveError(null);
                setsaveStart(true);
           
                var savedefinationResult = await SaveComboBoxItem(comboboxItem);

                if (savedefinationResult.returnCode === 1) {
                    LoadComboboxItems();
                    NewComboBoxItem();
                    setvisible(true);
                } else {
                    setSaveError(savedefinationResult.returnMessage);
                }

            } catch (error) {
                setSaveError(error.message);
            } finally {

                setsaveStart(false);
            }

        } catch (error) {
            setSaveError(error.message);
        } finally {

            setsaveStart(false);
        }
        setvisible(true);

    }
    const optionClick = (option, id) => {
        //EditGroupDefination(option === 'Delete', id);
    }


    const gridcolumnsGroups = DataGridComboItems(optionClick);

    return (

        <>
            <CModal
                backdrop="static"
                visible={visible}
                onClose={() => ClosedClick()}

            >
                <CModalHeader>
                    <CModalTitle>{t("FormComboFieldModalTitle")}</CModalTitle>
                </CModalHeader>
                <CModalBody>

           
                        <CRow className="mb-12">
                            <CFormLabel htmlFor="txtComboText" className="col-sm-3 col-form-label">{t("ComboBoxName")}</CFormLabel>
                            <CCol sm={9}>
                                <CFormInput type="text" id='txtname' name="name"  
                                    onChange={e => handleChange(e)}value={comboboxItem?.name} />
                            </CCol>
                        </CRow>
                        <CRow className="mb-12">
                            <CFormLabel htmlFor="txttagName" className="col-sm-3 col-form-label">{t("ComboBoxTag")}</CFormLabel>
                            <CCol sm={7}>
                                <CFormInput type="text" id='txttagName' name="tagName"
                                    onChange={e => handleChange(e)} value={comboboxItem?.tagName} />
                            </CCol>
                            <CCol>


                                <IconButton
                                    aria-label="delete"
                                    color="secondary"
                                    onClick={() => SaveData()}  >
                                    <AddCircleOutlineIcon />
                                </IconButton>

                            </CCol>
                        </CRow>
                   
                    <CRow>
                        <CCol>
                            <DataGrid columns={gridcolumnsGroups}
                                rows={comboboxItems}
                                disableColumnFilter
                                disableColumnMenu
                                disableMultipleRowSelection

                            ></DataGrid>
                        </CCol>
                    </CRow>

                    <CRow xs={{ cols: 4 }}>
                        <CCol> </CCol>
                        <CCol>
                            {
                                saveStart ? <Lottie animationData={ProcessAnimation} loop={true} style={{ width: "80%", height: "80%" }} ></Lottie> : ""
                            }
                        </CCol>
                        <CCol> </CCol>
                        <CCol> </CCol>


                    </CRow>
                    <CRow>
                        {saveError != null ?
                            <CAlert color="warning">{saveError}</CAlert>
                            : ""
                        }
                    </CRow>

                </CModalBody>

                <CModalFooter>
                    <CButton color="secondary" onClick={() => ClosedClick()}  >{t("Close")}</CButton>
                   
                </CModalFooter>
            </CModal>

        </>
    )


}


export default ComboBoxItemEditModal;


ComboBoxItemEditModal.propTypes = {
    visiblep: PropTypes.bool,
    formDefinationFieldp: PropTypes.object,
};


