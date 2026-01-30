import React, { useEffect, useState } from 'react'
import {
  CContainer,
  CRow,
  CCardBody,
  CCard,
  CCardGroup,
  CCol,
  CForm,
  CFormInput,
  CInputGroup,
  CInputGroupText,
  CFormTextarea,
  CFormLabel,
  CFormSelect,
  CButton,CSpinner
} from '@coreui/react'
import Lottie from "lottie-react";
import process_color from "../../../content/animation/Process_Color.json";

import CIcon from '@coreui/icons-react'

import { cilLockLocked, cilUser, cilPhone, cilRoom, cilIndustry, cilMap } from '@coreui/icons' 
import "../../../../src/translation/i18";
import { GetCountryForSale, GetCountryCity } from '../../../lib/countryapi.jsx';
import {CreateCompany} from '../../../lib/companyapi.jsx';

import styles  from './styles.css';
 

import { useTranslation } from "react-i18next";
import "../../../translation/i18";

const Register = () => {
  const { t } = useTranslation();

  const [usersLoading, setUsersLoading] = useState(false);
  const [countrys, setcountrys] = useState([]);
  const [citys, setCitys] = useState([]);
  const [inputValues, setInputValues] = useState({
    CompanyName: "",
    email: '',
    PhoneNumber: '',
    TaxNumber: '',
    MersisNo: '',
    AuthorizedPersonName: '',
    Country:  0,
    City: 0,
    Adress: "",
    password:"",
    PasswordRetry:""
  });

  function handleChange(event) {
    const { name, value } = event.target;
    setInputValues({ ...inputValues, [name]: value });
 
  }

  async function countryChanges(event) {

    const { name, value } = event.target;

    setInputValues({ ...inputValues, [name]: value });
    setUsersLoading(true);

    var list = await GetCountryCity(value);

    setCitys(list.data);
    setUsersLoading(false);

  }

  useEffect(() => {
    if (countrys.length == 0) {

      loadData();
    }
  });



  const loadData = async () => {

    setUsersLoading(true);
    var list = await GetCountryForSale();
    setcountrys(list.data);
    setUsersLoading(false);

  };


   async function SaveCompanyInfo(){
    setUsersLoading(true);
    var returnObj=await CreateCompany(inputValues)
    setUsersLoading(false);

  }
  
  return (
    <div className="bg-light min-vh-100 d-flex flex-row align-items-center">
      <CContainer>
        <CRow className="justify-content-center">
          <CCardGroup>
            <CCard className="p-4">
              <CCardBody className="p-4">
                <h1>{t("Register")}</h1>
                <p className="text-medium-emphasis">{t("Createyouraccount")}</p>
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
                        <CFormInput placeholder={t("TaxNumber")} autoComplete="TaxNumber" name="TaxNumber" onChange={handleChange} />
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
                        <CFormInput placeholder={t("AuthorizedPersonName")} autoComplete="AuthorizedPersonName" name="AuthorizedPersonName" onChange={handleChange} />
                      </CInputGroup>
                      <CInputGroup className="mb-3">
                        <CInputGroupText>
                          <CIcon icon={cilMap} />
                        </CInputGroupText>
                        <CFormSelect size='sm' aria-label={t("Country")} name="Country" onChange={countryChanges}>
                          <option value="0">Seçiniz</option>
                          {countrys.map(item => {

                            return (<option key={item.id} value={item.id}>{item.name}</option>);
                          })}
                        </CFormSelect>
                      </CInputGroup>
                      <CInputGroup className="mb-3">
                        <CInputGroupText>
                          <CIcon icon={cilMap} />
                        </CInputGroupText>
                        <CFormSelect size='sm' aria-label={t("city")} name="city" onChange={handleChange}>
                          <option>Open this select menu</option>
                          {citys.map(item => {

                            return (<option key={item.id} value={item.id}>{item.name}</option>);
                          })}
                        </CFormSelect>
                      </CInputGroup>
                      <CInputGroup className="mb-3">
                        <CFormLabel htmlFor="textAdress"  >{t("Adress")}</CFormLabel>

                      </CInputGroup>
                      <CInputGroup className="mb-3">

                        <CFormTextarea id="textAdress" onChange={handleChange} rows="5"  name="Adress"></CFormTextarea>

                      </CInputGroup>

                      <CInputGroup className="mb-3">
                        <CInputGroupText>
                          <CIcon icon={cilLockLocked} />
                        </CInputGroupText>
                        <CFormInput
                          type="password"
                          name="password"
                          placeholder={t("password")}
                          autoComplete="new-password"
                          onChange={handleChange}
                        />
                      </CInputGroup>
                      <CInputGroup className="mb-4">
                        <CInputGroupText>
                          <CIcon icon={cilLockLocked} />
                        </CInputGroupText>
                        <CFormInput
                          type="password"
                          
                          name="PasswordRetry"
                          placeholder={t("Repeatpassword")}
                          autoComplete="new-password"
                          onChange={handleChange}
                        />
                      </CInputGroup>
                      <div className="d-grid">
                        <CButton color="success" onClick={()=>SaveCompanyInfo()}>{t("CreateAccount")}

                      
                        </CButton>
                      </div>
                    </CForm> 
                  </CCol>
                </CRow>
                <CRow>
                  <Lottie animationData={process_color} loop={true} style={{width: "20%", height: "20%"}}></Lottie>
                </CRow>
              </CCardBody>
            </CCard>
          </CCardGroup>
        </CRow>
      </CContainer>

    </div>
  )
}

export default Register


