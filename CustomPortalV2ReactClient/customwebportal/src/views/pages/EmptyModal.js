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


const EmptyModal = ({ visiblep, setFormData }) => {


    const [visible, setvisible] = useState(visiblep);
    //const [user, setUser] = useState({ ...userp });

    const [saveError, setSaveError] = useState(null);
   // const[branchList,setbranchList] =useState([]);

    const [saveStart, setsaveStart] = useState(false);

    const { t } = useTranslation();

    function handleChange(event) {
        const { name, value } = event.target;
       // setUser({ ...user, [name]: value });

    }
    async function ClosedClick(){
        setvisible(false);
    }
     
    useEffect(() => {
        setvisible(visiblep);
      //  setUser(userp);//set if you change value inside
 

    }, [visiblep, userp])

    async function SaveData() {

        try {
            
            setsaveStart(true);
            /*
            var saveUserResult = await SaveUser(user);
           
            if (saveUserResult.returnCode === 1) {  
                setFormData(user);
                setvisible(false);
            } else {
              setSaveError(saveUserResult.returnMessage);
            }
             */
          
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
                                onChange={e => handleChange(e)} /* value={user.userName}*/ />
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


export default EmptyModal;


EmptyModal.propTypes = {
    visiblep: PropTypes.bool 
};