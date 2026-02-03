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
    CFormLabel,
    CFormInput,
    CFormSelect,
    CFormSwitch,
    CForm,

} from '@coreui/react'


import { useTranslation } from "react-i18next";

import { GetBranchList, GetBranch, SaveBranch, DeleteBranch, CreateNewBranch } from '../../lib/companyapi.jsx';

import LoadingAnimation from '../../components/LoadingAnimation.jsx';
import { getUserRoles, GetBranchpackages } from '../../lib/userapi.jsx';

import PropTypes  from 'prop-types';

const BranchEditModal = ({ visiblep, editBranchp, setFormData, OnCloseModal }) => {



    const [userRoles, setUserRoles] = useState([]);
    const [branchPackages, setBranchPackages] = useState([]);

    const [editBranch, setEditBranch] = useState(null);
    const [visible, setvisible] = useState(false);

    const [saveError, setSaveError] = useState(null);

    const [saveStart, setsaveStart] = useState(false);

    const { t } = useTranslation();

    function handleChange(event) {
        const { name, value } = event.target;

        setEditBranch({ ...editBranch, [name]: value });

    }

    async function ClosedClick() {
        setvisible(false);
        OnCloseModal();
    }


    useEffect(() => {
        setvisible(visiblep);
        setEditBranch(editBranchp);

        LoadUserRoles();

        LoadBrachPackages();


    }, [visiblep, editBranchp])

    async function LoadUserRoles() {

        try {
            var userRolesList = await getUserRoles();
            if (userRolesList.returnCode === 1) {
                setUserRoles(userRolesList.data);
            } else {
                setSaveError(userRolesList.ReturnMessage);
            }
        } catch (error) {
            setSaveError(error.message);
        }

    }


    async function LoadBrachPackages() {

        try {
            var branchPackagesList = await GetBranchpackages();
            if (branchPackagesList.returnCode === 1) {
                setBranchPackages(branchPackagesList.data);
            } else {
                setSaveError(branchPackagesList.ReturnMessage);
            }
        } catch (error) {
            setSaveError(error.message);
        }

    }

    async function SaveData() {


        try {
            setSaveError(null);
            var saveBranchReturn = await SaveBranch(editBranch);
            if (saveBranchReturn.returnCode === 1) {

                setFormData();
                ClosedClick();
            } else {
                setSaveError(saveBranchReturn.ReturnMessage);
            }
        } catch (error) {
            setSaveError(error.message);
        }

    }


    return (

        <>
            <CModal
                backdrop="static"
                visible={visible}
                onClose={() => ClosedClick()}

            >
                <CModalHeader>
                    <CModalTitle>{t("BranchEdit")}</CModalTitle>
                </CModalHeader>
                <CModalBody>

                    <CRow className="mb-12">
                        <CFormLabel htmlFor="txtBranchName" className="col-sm-3 col-form-label">{t("BranchName")}</CFormLabel>
                        <CCol sm={9}>
                            <CFormInput type="text" id='txtBranchName' name="name"
                                onChange={e => handleChange(e)} value={editBranch?.name} />
                        </CCol>
                    </CRow>

                    <CRow className="mb-12">
                        <CFormLabel htmlFor="txtEMail" className="col-sm-3 col-form-label">{t("Email")}</CFormLabel>
                        <CCol sm={9}>
                            <CFormInput type="text" id='txtEMail' name="email"
                                onChange={e => handleChange(e)} value={editBranch?.email} />
                        </CCol>
                    </CRow>

                    <CRow className="mb-12">
                        <CFormLabel htmlFor="txtEMailPassword" className="col-sm-3 col-form-label">{t("EMailPassword")}</CFormLabel>
                        <CCol sm={9}>
                            <CFormInput type="Password" id='txtEMailPassword' name="eMailPassword"
                                onChange={e => handleChange(e)} />
                        </CCol>
                    </CRow>

                    <CRow className="mb-12">
                        <CFormLabel htmlFor="txtPhoneNumber" className="col-sm-3 col-form-label">{t("PhoneNumber")}</CFormLabel>
                        <CCol sm={9}>
                            <CFormInput type="text" id='txtPhoneNumber' name="phoneNumber"
                                onChange={e => handleChange(e)} value={editBranch?.phoneNumber} />
                        </CCol>
                    </CRow>

                    <CRow className="mb-12">
                        <CFormLabel htmlFor="cmbPackageName" className="col-sm-3 col-form-label">{t("PackedName")}</CFormLabel>
                        <CCol sm={9}>
                            <CFormSelect type="text" id='cmbPackageName' name="branchPackageId"
                                onChange={e => handleChange(e)} value={editBranch?.branchPackageId}    >

                                <option value="0">Seçiniz</option>
                                {branchPackages.map(item => {
                                    return (<option key={item.id} value={item.id}  >{item.name}</option>);
                                })}
                            </CFormSelect>

                        </CCol>
                    </CRow>

                    <CRow className="mb-12">
                        <CFormLabel htmlFor="cmbUserRuleName" className="col-sm-3 col-form-label">{t("UserRuleName")}</CFormLabel>
                        <CCol sm={9}>
                            <CFormSelect id='cmbUserRuleName' name="userRuleId" value={editBranch?.userRuleId}
                                onChange={e => handleChange(e)}    >

                                <option value="0">Seçiniz</option>
                                {userRoles.map(item => {
                                    return (<option key={item.id} value={item.id}  >{item.name}</option>);
                                })}
                            </CFormSelect>
                        </CCol>
                    </CRow>

                    <CRow xs={{ cols: 4 }}>
                        <CCol> </CCol>
                        <CCol>


                            <LoadingAnimation loading={saveStart} size={"%40"}></LoadingAnimation>
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
                    <CButton color="secondary" onClick={() =>ClosedClick()}>{t("Close")}</CButton>
                    <CButton color="primary" onClick={() => SaveData()}>{t("Save")}</CButton>
                </CModalFooter>
            </CModal>

        </>
    )


}


export default BranchEditModal;


BranchEditModal.propTypes = {
    visiblep: PropTypes.bool,
    editBranchp: PropTypes.object
};


