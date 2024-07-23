import React, { useEffect, useState,setstate } from 'react'

import {
    CAvatar,
    CButton,
    CButtonGroup,
    CCard,
    CCardBody,
    CCardFooter,
    CCardHeader,
    CCol,
    CAlert,
    CProgress,
    CRow,
    CTable,
    CTableBody,
    CTableDataCell,
    CTableHead,
    CTableHeaderCell,
    CTableRow,
    CModal,
    CModalHeader,
    CModalTitle,
    CModalFooter,
    CModalBody,
    CFormLabel,
    CFormInput,
    CFormSelect,
    CCardText,


} from '@coreui/react'
import { CChartLine } from '@coreui/react-chartjs'
import { getStyle, hexToRgba } from '@coreui/utils'
import CIcon from '@coreui/icons-react'

import { useDispatch, useSelector } from 'react-redux';
import { useNavigate } from "react-router-dom";
import { GetUserList } from '../../lib/userapi';
import Lottie from 'lottie-react';
import PropTypes from 'prop-types';


import ProcessAnimation from "../../content/animation/Process.json";


import { useTranslation } from "react-i18next";


const EditModal = ( props ) => {
    /*const [editUser, visible] =  props;
     
    */

    const [visible, setvisible] = useState({...props.visible});  
    const [user, setUser] = useState({...props.user});

    const [saveError, setSaveError] = useState(null);

    const [saveStart, setsaveStart] = useState(false);

    const { t } = useTranslation();

    function handleChange(event) {
        const { name, value } = event.target;
        setUser({ ...user, [name]: value });

    }
    useEffect(() => {
        setvisible(props.visible);
        setUser(props.user);
    }, [props.visible])

    async function SaveData() {

        try {
            /* var saveBranchReturn = await SaveBranch(editBranch);
             if (saveBranchReturn.returnCode === 1) {
               setVisible(!visible);
               LoadBranchList();
             } else {
               setSaveError(saveBranchReturn.ReturnMessage);
             }
             */
        } catch (error) {
            setSaveError(error.message);
        }

    }
  
    return (
     
        <>
            <CModal
             backdrop="static"
             visible={visible} 
             onClose={() => setvisible( false)}

            >
                <CModalHeader>
                    <CModalTitle>{t("EditUserName")}</CModalTitle>
                </CModalHeader>
                <CModalBody>

                    <CRow className="mb-12">
                        <CFormLabel htmlFor="txtUserName" className="col-sm-3 col-form-label">{t("UserName")}</CFormLabel>
                        <CCol sm={9}>
                            <CFormInput type="text" id='txtUserName' name="userName"
                                onChange={e => handleChange(e)} value={user.userName} />
                        </CCol>
                    </CRow>

                    <CRow className="mb-12">
                        <CFormLabel htmlFor="txtEMail" className="col-sm-3 col-form-label">{t("Email")}</CFormLabel>
                        <CCol sm={9}>
                            <CFormInput type="text" id='txtEMail' name="email"
                                onChange={e => handleChange(e)} value={user.email} />
                        </CCol>
                    </CRow>

                    <CRow className="mb-12">
                        <CFormLabel htmlFor="txtPassword" className="col-sm-3 col-form-label">{t("Password")}</CFormLabel>
                        <CCol sm={9}>
                            <CFormInput type="Password" id='txtPassword' name="password"
                                onChange={e => handleChange(e)} />
                        </CCol>
                    </CRow>

                    <CRow className="mb-12">
                        <CFormLabel htmlFor="txtFullName" className="col-sm-3 col-form-label">{t("FullName")}</CFormLabel>
                        <CCol sm={9}>
                            <CFormInput type="text" id='txtFullName' name="fullName"
                                onChange={e => handleChange(e)} value={user.fullName} />
                        </CCol>
                    </CRow>

                    <CRow className="mb-12">
                        <CFormLabel htmlFor="txtPhoneNumber" className="col-sm-3 col-form-label">{t("PhoneNumber")}</CFormLabel>
                        <CCol sm={9}>
                            <CFormInput type="text" id='txtPhoneNumber' name="phoneNumber"
                                onChange={e => handleChange(e)} value={user.phoneNumber} />
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
                    <CButton color="secondary" onClick={() => setvisible( false)}  >{t("Close")}</CButton>
                    <CButton color="primary" onClick={() => SaveData()}>{t("Save")}</CButton>
                </CModalFooter>
            </CModal>

        </>
    )


}


export default EditModal;

EditModal.propTypes = {
    user: PropTypes.object,
    visible: PropTypes.bool,
}