import { useState, useEffect, useContext } from 'react'

import {
    CCard,
    CCardBody,
    CRow,
    CAlert
} from '@coreui/react'


import { useNavigate } from "react-router-dom";
import { useTranslation } from "react-i18next";

import { UrlContext } from "../../lib/URLContext";

const WorkflowInbox = () => {
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




    function SetLocationAdress() {

        dispatch({ type: 'reset' })

        dispatch({
            type: 'Add',
            payload: { pathname: "#/WorkflowInbox", name: t("WorkflowInbox"), active: false }
        });
    }


    useEffect(() => {

        try {
            SetLocationAdress();

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

export default WorkflowInbox;


