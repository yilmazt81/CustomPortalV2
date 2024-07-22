import React,{useEffect,useState} from 'react'
import {
    CButton,
    CCard,
    CCardBody,
    CCol,
    CContainer,
    CForm,
    CFormInput,
    CInputGroup,
    CInputGroupText,
    CFormTextarea,
    CFormLabel,
    CFormSelect,
    CRow,
} from '@coreui/react'
import { cilMagnifyingGlass } from '@coreui/icons'
import { useTranslation } from "react-i18next";
import "../../../translation/i18";

import CIcon from '@coreui/icons-react'
import { cilLockLocked, cilUser, cilPhone, cilRoom, cilIndustry, cilMap } from '@coreui/icons'
const StepOneRegister = () => {
    const [inputValues, setInputValues] = useState ({
        CompanyName :"" ,
        email : '',
        PhoneNumber:'',
        TaxNumber:'',
        MersisNo:'',
        AuthorizedPersonName:'' 

    });
    const { t } = useTranslation();

    useEffect(() => {
        console.log(
          "Occurs ONCE, AFTER the initial render."
        );
 
      }, []);

      
      function handleChange(event) {
        const { name, value } = event.target;
         setInputValues({ ...inputValues, [name]: value });
        console.log(inputValues); 
      }
    return (

        <CRow className="justify-content-center">
            <CCol md={11} lg={9} xl={8}>
                <CForm>
                    <CInputGroup className="mb-3">
                        <CInputGroupText>
                            <CIcon icon={cilIndustry} />
                        </CInputGroupText>
                        <CFormInput placeholder={t("CompanyName")} name="CompanyName" autoComplete="CompanyName" onChange={handleChange} />
                    </CInputGroup>
                    <CInputGroup className="mb-3">
                        <CInputGroupText>@</CInputGroupText>
                        <CFormInput placeholder={t("Email")} autoComplete="email" name="email" onChange={handleChange} />
                    </CInputGroup>
                    <CInputGroup className="mb-3">
                        <CInputGroupText>
                            <CIcon icon={cilPhone} />
                        </CInputGroupText>
                        <CFormInput placeholder={t("PhoneNumber")} autoComplete="PhoneNumber" name="PhoneNumber" onChange={handleChange} />
                    </CInputGroup>
                    <CInputGroup className="mb-3">
                        <CInputGroupText>
                            <CIcon icon={cilRoom} />
                        </CInputGroupText>
                        <CFormInput placeholder={t("TaxNumber")} autoComplete="TaxNumber"   name="TaxNumber" onChange={handleChange}/>
                    </CInputGroup>
                    <CInputGroup className="mb-3">
                        <CInputGroupText>
                            <CIcon icon={cilRoom} />
                        </CInputGroupText>
                        <CFormInput placeholder={t("MersisNo")} autoComplete="MersisNo" name="MersisNo" onChange={handleChange} />
                    </CInputGroup>
                    <CInputGroup className="mb-3">
                        <CInputGroupText>
                            <CIcon icon={cilUser} />
                        </CInputGroupText>
                        <CFormInput placeholder={t("AuthorizedPersonName")} autoComplete="AuthorizedPersonName"  name="AuthorizedPersonName" onChange={handleChange}  />
                    </CInputGroup>
                  
                </CForm>


            </CCol>
        </CRow>

    )
}

export default StepOneRegister
