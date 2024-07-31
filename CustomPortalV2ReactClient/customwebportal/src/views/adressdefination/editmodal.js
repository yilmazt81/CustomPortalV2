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


} from '@coreui/react'
 
import Lottie from 'lottie-react';
import PropTypes from 'prop-types';


import ProcessAnimation from "../../content/animation/Process.json";


import { useTranslation } from "react-i18next";

import makeAnimated from 'react-select/animated';
import Select from 'react-select' 
import {SaveDefination} from '../../lib/companyAdressDef';

const EditModal = ({ visiblep, adressdefinationp,adresDefinationTypesp,setFormData}) => {

    const [visible, setvisible] = useState(visiblep);
    const [adressfination, setadressfination] = useState({ ...adressdefinationp });
    const[selectData,setSelectData]=useState([]);
    const[defaultSelectData,setdefaultSelectData]=useState([]);

    const [saveError, setSaveError] = useState(null); 

    const [saveStart, setsaveStart] = useState(false);

    const { t } = useTranslation();

    function handleChange(event) {
    
        const { name, value } = event.target;
     
        setadressfination({ ...adressfination, [name]: value });

    }
    function handleChangeSelect(event) {
    
        var itemList= [];
        //event.map(s=> itemList.push({DefinationTypeId:s.value}) )
        var definationTypeIdList="";
        var definationNameList=""; 
        for(var i=0;i<event.length;i++)
        {
            var s=event[i];
            itemList.push({ typeid:s.value});
            definationTypeIdList+=s.value+",";
            definationNameList+=s.label+",";
        } 

        adressfination.definations = itemList;       
        adressfination.definationTypeId = definationTypeIdList;
        adressfination.definationTypeName= definationNameList;
        setadressfination(adressfination); 
    }

    async function ClosedClick(){
        setvisible(false);
    }
  

    async function SaveData()
    {
        try { 
            setSaveError(null);
            
            adressfination.definations=[];
            setadressfination(adressfination);
            setsaveStart(true); 
            var savedefinationResult = await SaveDefination(adressfination);
           
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
    }

    useEffect(() => {
        setvisible(visiblep);
        setadressfination(adressdefinationp); 
        var selecL =  [];
        setSaveError(null);

         adresDefinationTypesp.map(s=>{ selecL.push({value:s.id,label:s.name})} );
     
         setSelectData(selecL);
         setdefaultSelectData([]);
        if (adressdefinationp!=null)
        { 
           var defaultSelectData=[];
            if (adressdefinationp.definations!=null){
                for (let index = 0; index < adressdefinationp.definations.length; index++) {
                    const adressdefination =  adressdefinationp.definations[index];
                  
                     var item = selecL.find(t=>t.value==adressdefination.typeid);
            
                     defaultSelectData.push(item);   

                } 
                setdefaultSelectData(defaultSelectData);
            }else{
                setdefaultSelectData([]);
            } 
    
        }else{
            
            setdefaultSelectData([]);
        }
      
    }, [visiblep, adressdefinationp])

    const animatedComponents = makeAnimated();
    return (

        <>
            <CModal
                backdrop="static"
                visible={visible}
                onClose={() => ClosedClick()}

            >
                <CModalHeader>
                    <CModalTitle>{t("AdressDefinationModalTitle")}</CModalTitle>
                </CModalHeader>
                <CModalBody>

                    <CRow className="mb-12">
                        <CFormLabel htmlFor="txtFormName" className="col-sm-3 col-form-label">{t("CompanyName")}</CFormLabel>
                        <CCol sm={9}>
                            <CFormInput type="text" id='txtCompanyName' name="companyName"
                                onChange={e => handleChange(e)} value={adressfination?.companyName} />
                        </CCol>
                    </CRow>

                    <CRow className="mb-12">
                        <CFormLabel htmlFor="txtAdress" className="col-sm-3 col-form-label">{t("Adress")}</CFormLabel>
                        <CCol sm={9}>
                            <CFormInput type="text" id='txtAdress' name="adress"
                                onChange={e => handleChange(e)} value={adressfination?.adress} />
                        </CCol>
                    </CRow>

                    <CRow className="mb-12">
                        <CFormLabel htmlFor="txtCountry" className="col-sm-3 col-form-label">{t("Country_ISO")}</CFormLabel>
                        <CCol sm={6}>
                            <CFormInput type="text" id='txtCountry' name="country"
                                onChange={e => handleChange(e)} value={adressfination?.country} />
                        </CCol>
                        <CCol sm={3}>
                            <CFormInput type="text" id='txtIsoCode' name="isoCode"
                                onChange={e => handleChange(e)} value={adressfination?.isoCode} />
                        </CCol>
                    </CRow>


                    <CRow className="mb-12">
                        <CFormLabel htmlFor="txtCity" className="col-sm-3 col-form-label">{t("City")}</CFormLabel>
                        <CCol sm={9}>
                            <CFormInput type="text" id='txtCity' name="city"
                                onChange={e => handleChange(e)} value={adressfination?.city} />
                        </CCol>
                    </CRow>

                    <CRow className="mb-12">
                        <CFormLabel htmlFor="txtFactoryNumber" className="col-sm-3 col-form-label">{t("FactoryNumber")}</CFormLabel>
                        <CCol sm={9}>
                            <CFormInput type="text" id='txtFactoryNumber' name="factoryNumber"
                                onChange={e => handleChange(e)} value={adressfination?.factoryNumber} />
                        </CCol>
                    </CRow>

                    <CRow className="mb-12">
                        <CFormLabel htmlFor="txtEmail" className="col-sm-3 col-form-label">{t("EMail")}</CFormLabel>
                        <CCol sm={9}>
                            <CFormInput type="text" id='txtEmail' name="email"
                                onChange={e => handleChange(e)} value={adressfination?.email} />
                        </CCol>
                    </CRow>

                    <CRow className="mb-12">
                        <CFormLabel htmlFor="txtphoneNumber" className="col-sm-3 col-form-label">{t("PhoneNumber")}</CFormLabel>
                        <CCol sm={9}>
                            <CFormInput type="text" id='txtphoneNumber' name="phoneNumber"
                                onChange={e => handleChange(e)} value={adressfination?.phoneNumber} />
                        </CCol>
                    </CRow>

                    <CRow className="mb-12">
                        <CFormLabel htmlFor="txtFaxNumber" className="col-sm-3 col-form-label">{t("FaxNumber")}</CFormLabel>
                        <CCol sm={9}>
                            <CFormInput type="text" id='txtFaxNumber' name="faxNumber"
                                onChange={e => handleChange(e)} value={adressfination?.faxNumber} />
                        </CCol>
                    </CRow>


                    <CRow className="mb-12">
                        <CFormLabel htmlFor="selectdefinationTypes" className="col-sm-3 col-form-label">{t("AdressDefinationTypes")}</CFormLabel>
                        <CCol sm={9}>
                            <Select type="text" id='selectdefinationTypes' name='definationTypesList'
                             closeMenuOnSelect={false} 
                             isMulti 
                             onChange={e=>handleChangeSelect(e)}
                             defaultValue={defaultSelectData}
                             //value={adressfination?.DefinationTypesList}
                             components={animatedComponents}
                             options ={selectData}/>
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
    adressdefinationp: PropTypes.object,
    adresDefinationTypesp:PropTypes.arrayOf(PropTypes.any),

};
 