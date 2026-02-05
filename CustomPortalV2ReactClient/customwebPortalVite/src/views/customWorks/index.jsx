import React, { useState, useEffect, useContext } from 'react'

import {
    CButton,
    CButtonGroup,
    CCard,
    CCardBody,
    CCol,
    CRow,
    CAlert
} from '@coreui/react'


import { useSelector } from 'react-redux';
import { useNavigate } from "react-router-dom";
import { useTranslation } from "react-i18next";
import { Gridcolumns } from './DataGrid'
import {
    MaterialReactTable,
    useMaterialReactTable
} from 'material-react-table';
import {
    Box,
    IconButton,
    MenuItem


} from '@mui/material';
import {
    Edit as EditIcon,
    Delete as DeleteIcon,
    AccessibilityNew,
    BorderAll,
} from '@mui/icons-material';
import {
    CreateDefinationTypes,
    NewCompanyDefination,
    GetUserCompanyDefinations,
    GetCompanyDefination,
    DeleteCompanyDefination
} from '../../lib/companyAdressDef.jsx';
import EditModal from './editmodal';
import DeleteModal from "../../components/DeleteModal.jsx";
import LoadingAnimation from "../../components/LoadingAnimation.jsx";
import { UrlContext } from "../../lib/URLContext";

const CustomWorks = () => {
    const navigate = useNavigate();
    //Bu sekilde redux tan okunacak 
    const { t } = useTranslation();
    const { dispatch } = useContext(UrlContext);

    const [visibleDelete, setVisibleDelete] = useState(false);
    const [visibleEdit, setVisibleEdit] = useState(false);

    const [saveError, setSaveError] = useState(null);

    const [adressDefinationList, setadressDefinationList] = useState([]);
    const [adressDefinationTypes, setadressDefinationTypes] = useState([]);
    const [updateAdressDefination, setupdateAdressDefination] = useState(null);
    const [deleteStart, setDeleteStart] = useState(false);
    const [loading, setLoading] = useState(false);


    async function NewDefination() {
        try {
            setSaveError(null);
            setLoading(true);
            LoadDefinationTypes();
            var adresDefinationService = await NewCompanyDefination();
            if (adresDefinationService.returnCode === 1) {

                setupdateAdressDefination(adresDefinationService.data);
                setVisibleEdit(true);
            } else {
                setSaveError(adresDefinationService.returnMessage);
            }
        } catch (error) {
            setSaveError(error.message);
        } finally {
            setLoading(false);
        }
    }


    function SetLocationAdress() {

        dispatch({ type: 'reset' })

        dispatch({
            type: 'Add',
            payload: { pathname: "#/CustomWorks", name: t("CustomWorks"), active: false }
        });
    }


    useEffect(() => {

        try {


        } catch (error) {
            console.log(error);
        }

    }, []);



    return (
        <>
            <CCard className="mb-4">
                <CCardBody>

                    <CRow>
                        {
                            saveError != null ?
                                <CAlert color="warning">{saveError}</CAlert>
                                : ""
                        }
                    </CRow>
                </CCardBody>
            </CCard>

        </>
    )
}

export default CustomWorks;


