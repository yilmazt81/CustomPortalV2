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

import { SaveFormDefination } from '../../lib/formdef'; 
import Lottie from 'lottie-react';
import PropTypes from 'prop-types';


import ProcessAnimation from "../../content/animation/Process.json";


import { useTranslation } from "react-i18next";


const EditModal = ({ visiblep, formdefinationp,customSectorList, setFormData }) => {


    const [visible, setvisible] = useState(visiblep);
    const [formdefination, setFormdefination] = useState({ ...formdefinationp });

    const [saveError, setSaveError] = useState(null);
    const[sectorList,setSectorList] =useState([]);

    const [saveStart, setsaveStart] = useState(false);

    const { t } = useTranslation();

    function handleChange(event) {
        const { name, value } = event.target;
        setFormdefination({ ...formdefination, [name]: value });

    }
    async function ClosedClick(){
        setvisible(false);
    }
  

    useEffect(() => {
        setSaveError(null);
        setvisible(visiblep);
        setFormdefination(formdefinationp);
        setSectorList(customSectorList);
       //LoadBranchList();

    }, [visiblep, formdefinationp,customSectorList])

    async function SaveData() {

        try {
            
            try { 
                setSaveError(null);
                  debugger;
                setsaveStart(true); 
                var savedefinationResult = await SaveFormDefination(formdefination);
               
                if (savedefinationResult.returnCode === 1) {  
                    setFormData(savedefinationResult.data);
                    setvisible(false);
                } else {
                  setSaveError(savedefinationResult.returnMessage);
                }
                 
              
            } catch (error) {
                setSaveError(error.message);
            }finally{
                
                setsaveStart(false);
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
                    <CModalTitle>{t("FormDefinationModalTitle")}</CModalTitle>
                </CModalHeader>
                <CModalBody>

                    <CRow className="mb-12">
                        <CFormLabel htmlFor="txtFormName" className="col-sm-3 col-form-label">{t("FormName")}</CFormLabel>
                        <CCol sm={9}>
                            <CFormInput type="text" id='txtFormName' name="formName"
                                onChange={e => handleChange(e)} value={formdefination.formName} />
                        </CCol>
                    </CRow>

               
                    <CRow className="mb-12">
                        <CFormLabel htmlFor="cmbCustomSector" className="col-sm-3 col-form-label">{t("CustomSectorName")}</CFormLabel>
                        <CCol sm={9}>
                            <CFormSelect type="text" id='cmbCustomSector' name="customSectorId"
                                onChange={e => handleChange(e)} value={formdefination?.customSectorId}    >

                                <option value="0">Seçiniz</option>
                                {sectorList.map(item => {
                                    return (<option key={item.id} value={item.id}  >{item.name}</option>);
                                })}
                            </CFormSelect>

                        </CCol>
                    </CRow>

                    <CRow className="mb-12">
                        <CFormLabel htmlFor="frmTemplatePath" className="col-sm-3 col-form-label">{t("TemplatePath")}</CFormLabel>
                        <CCol sm={9}>
                                    <CFormInput type="file" id="frmTemplatePath"/>

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