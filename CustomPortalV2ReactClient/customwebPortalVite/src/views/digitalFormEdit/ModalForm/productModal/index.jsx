import React, { useEffect, useState } from 'react'

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


} from '@coreui/react'

import Lottie from 'lottie-react';
import PropTypes from 'prop-types';


import ProcessAnimation from "../../../../content/animation/Process.json";
import Gridcolumns from './gridColumns';


import { useTranslation } from "react-i18next";

import { DataGrid } from '@mui/x-data-grid';

import { FilterProduct,GetAutoComplateProduct } from "../../../../lib/customProductapi.jsx";

const BrowserProductModal = ({ visiblep, formDefinationTypeIdp, setFormData, setClose }) => {


    const [visible, setvisible] = useState(visiblep);
    const [filterproductList, setfilterproductList] = useState([]);
    const [filterCompany, setFilterCompany] = useState({ formDefinationFieldId: formDefinationTypeIdp, filterValue: '' });

    const [rowSelectionModel, setRowSelectionModel] = useState([]);

    const [saveError, setSaveError] = useState(null);
    // const[branchList,setbranchList] =useState([]);

    const [saveStart, setsaveStart] = useState(false);

    const { t } = useTranslation();

    function handleChange(event) {
        const { name, value } = event.target;
        // setUser({ ...user, [name]: value });

    }
    async function ClosedClick() {
        setvisible(false);
        setClose();

    }


    async function GetCompanyProductList() {

        try {
            setsaveStart(true);
            var filterServiceReturn = await FilterProduct(filterCompany);
            if (filterServiceReturn.returnCode === 1) {
                setfilterproductList(filterServiceReturn.data);
            } else {
                setSaveError(filterServiceReturn.returnMessage);
            }
        } catch (error) {
            setSaveError(error.message);
            console.log(error);

        }


        setsaveStart(false);
    }

    useEffect(() => {
        setvisible(visiblep);

        //  setUser(userp);//set if you change value inside
        setSaveError(null);
        setRowSelectionModel([]);
      
        if (visiblep) {
            filterCompany.formDefinationFieldId = formDefinationTypeIdp;
            setFilterCompany(filterCompany);
            //  setUser(userp);//set if you change value inside
            GetCompanyProductList();
        }


    }, [visiblep])

    const SelectedRowChanged = async (param) => {
        console.log(param);

        // await GetAdressControlList(param.id);

    }

    const optionClick = (option, id) => {
        //    EditGroupDefination(option === 'Delete', id);
    }
    const gridColumns = Gridcolumns(optionClick);


    async function LoadSelectedItemsToForm() {
        var selecteidlist=rowSelectionModel.join(',');
        debugger;
        try {
            setsaveStart(true);

            var filterServiceReturn = await GetAutoComplateProduct(filterCompany.formDefinationFieldId, selecteidlist);

            if (filterServiceReturn.returnCode === 1) {
                //setadressDefinationControlList(filterServiceReturn.data);
                setFormData(filterServiceReturn.data);
                setClose();

            } else {
                setSaveError(filterServiceReturn.returnMessage);
            }
        } catch (error) {
            setSaveError(error.message);
            console.log(error);

        }
        setsaveStart(false);
        
    }

    return (

        <>
            <CModal
                backdrop="static"
                visible={visible}
                size="xl"
                onClose={() => ClosedClick()}

            >
                <CModalHeader>
                    <CModalTitle>{t("EditUserName")}</CModalTitle>
                </CModalHeader>
                <CModalBody>


                    <CRow>

                        <CCol>
                            <div style={{ height: 300, width: '100%' }}>
                                <DataGrid rows={filterproductList}
                                    columns={gridColumns}
                                    checkboxSelection
                                    onRowSelectionModelChange={(newRowSelectionModel) => {
                                        setRowSelectionModel(newRowSelectionModel);
                                    }}
                                    rowSelectionModel={rowSelectionModel}
                                    slotProps={{
                                        toolbar: {
                                            showQuickFilter: true,
                                        },
                                    }} /></div>
                        </CCol>
                    </CRow>

                    <CRow xs={{ cols: 4 }}>
                        <CCol> </CCol>
                        <CCol>
                            {
                                saveStart ? <Lottie animationData={ProcessAnimation} loop={true} style={{ width: "40%", height: "40%" }} ></Lottie> : ""
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
                    <CButton color="primary" onClick={() => LoadSelectedItemsToForm()} >{t("Save")}</CButton>
                </CModalFooter>
            </CModal>

        </>
    )


}


export default BrowserProductModal;


BrowserProductModal.propTypes = {
    visiblep: PropTypes.bool,
    formDefinationTypeIdp: PropTypes.number,
};


