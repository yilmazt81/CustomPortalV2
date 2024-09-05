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

import { SaveUser} from '../../lib/userapi';
import { GetBranchList } from 'src/lib/companyapi'
import Lottie from 'lottie-react';
import PropTypes from 'prop-types';


import ProcessAnimation from "../../content/animation/Process.json";


import { useTranslation } from "react-i18next";
import LoadingAnimation from 'src/components/LoadingAnimation';

const EditModal = ({ visiblep, workflowp, setFormData }) => {


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
    
    useEffect(() => {
        setvisible(visiblep);
        setUser(userp);

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
                    <CModalTitle>{t("ModalWorkFlowEdit")}</CModalTitle>
                </CModalHeader>
                <CModalBody>

                    <CRow className="mb-12">
                        <CFormLabel htmlFor="txtWorkFlowName" className="col-sm-3 col-form-label">{t("UserName")}</CFormLabel>
                        <CCol sm={9}>
                            <CFormInput type="text" id='txtWorkFlowName' name="workFlowName"
                                onChange={e => handleChange(e)} value={user.userName} />
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
    workflowp: PropTypes.object,
};