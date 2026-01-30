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


} from '@coreui/react'

import { SaveUser} from '../../lib/userapi.jsx';
import { GetBranchList } from "../../lib/companyapi.jsx";
import Lottie from 'lottie-react';
import PropTypes from 'prop-types';


import ProcessAnimation from "../../content/animation/Process.json";


import { useTranslation } from "react-i18next";


const EditModal = ({ visiblep, userp, setFormData }) => {


    const [visible, setvisible] = useState(visiblep);
    const [user, setUser] = useState({ ...userp });

    const [saveError, setSaveError] = useState(null);
    const[branchList,setbranchList] =useState([]);

    const [saveStart, setsaveStart] = useState(false);

    const { t } = useTranslation();

    function handleChange(event) {
        const { name, value } = event.target;
        setUser({ ...user, [name]: value });

    }
    async function ClosedClick(){
        setvisible(false);
    }
    async function LoadBranchList() {
        try {
    
          var companyBranchList = await GetBranchList();
    
          if (companyBranchList.returnCode === 1) {
            setbranchList(companyBranchList.data);
          } else {
            setSaveError(companyBranchList.returnMessage);
          }
    
        } catch (error) {
          setSaveError(error.message);
        }
    
      }

    useEffect(() => {
        setvisible(visiblep);
        setUser(userp);

       LoadBranchList();

    }, [visiblep, userp])

    async function SaveData() {

        try {
            
            setsaveStart(true);
            var saveUserResult = await SaveUser(user);
           
            if (saveUserResult.returnCode === 1) {  
                setFormData(user);
                setvisible(false);
            } else {
              setSaveError(saveUserResult.returnMessage);
            }
             
          
        } catch (error) {
            setSaveError(error.message);
        }finally{
            
            setsaveStart(false);
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

                    <CRow className="mb-12">
                        <CFormLabel htmlFor="cmbBranchId" className="col-sm-3 col-form-label">{t("BranchName")}</CFormLabel>
                        <CCol sm={9}>
                            <CFormSelect type="text" id='cmbBranchId' name="companyBranchId"
                                onChange={e => handleChange(e)} value={user?.companyBranchId}    >

                                <option value="0">Seçiniz</option>
                                {branchList.map(item => {
                                    return (<option key={item.id} value={item.id}  >{item.name}</option>);
                                })}
                            </CFormSelect>

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
                    <CButton color="secondary" onClick={() =>ClosedClick()}  >{t("Close")}</CButton>
                    <CButton color="primary" onClick={() => SaveData()}>{t("Save")}</CButton>
                </CModalFooter>
            </CModal>

        </>
    )


}


export default EditModal;


EditModal.propTypes = {
    visiblep: PropTypes.bool,
    userp: PropTypes.object,
};


