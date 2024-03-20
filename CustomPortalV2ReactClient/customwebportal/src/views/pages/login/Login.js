import React, { useState } from 'react'
import { Link } from 'react-router-dom'
import { useTranslation } from "react-i18next";
import "../../../../src/translation/i18";

import {
  CButton,
  CCard,
  CCardBody,
  CCardGroup,
  CCol,
  CContainer,
  CForm,
  CFormInput,
  CInputGroup,
  CInputGroupText,
  CRow,
} from '@coreui/react'
import CIcon from '@coreui/icons-react'
import { cilLockLocked, cilUser } from '@coreui/icons'
//import SimpleReactValidator from 'simple-react-validator';
//import {authService}  from '../../../lib/auth-service';
import {LoginUser} from '../../../lib/userapi';


const Login = () => {
  const { t } = useTranslation();
  const [userInfo] = useState({ UserName: "", password: "" });

 // const validator = new SimpleReactValidator();


  const handleSubmit = () => {

   /*
    authService
    .login(userInfo)
    .then((response) => console.log(response))
    .catch((error) => console.log(error) );
     
    */
    LoginUser(userInfo);
    //UserApi.GetAll();

  }

 

  return (

    <div className="bg-light min-vh-100 d-flex flex-row align-items-center">
      <CContainer>
        <CRow className="justify-content-center">
          <CCol md={8}>
            <CCardGroup>
              <CCard className="p-4">
                <CCardBody>
                  <CForm>
                    <h1>{t("login")}</h1>
                    <p className="text-medium-emphasis">{t("SignInAccount")}t</p>
                    <CInputGroup className="mb-3">
                      <CInputGroupText>
                        <CIcon icon={cilUser} />
                      </CInputGroupText>
                      <CFormInput placeholder={t("Username")}
                        autoComplete="username"
                        onChange={e => userInfo.UserName = e.target.value} />
                    </CInputGroup>
                    <CInputGroup className="mb-4">
                      <CInputGroupText>
                        <CIcon icon={cilLockLocked} />
                      </CInputGroupText>
                      <CFormInput
                        type="password"
                        placeholder={t("Password")}
                        autoComplete="current-password"
                        onChange={e => userInfo.password = e.target.value}
                      />
                    </CInputGroup>
                    <CRow>
                      <CCol xs={6}>
                        <CButton color="primary" className="px-4"
                          onClick={handleSubmit} >
                          {t("login")}
                        </CButton>
                      </CCol>
                      <CCol xs={6} className="text-right">
                        <CButton color="primary" className="px-0">
                          {t("Forgotpassword")}
                        </CButton>
                      </CCol>
                    </CRow>
                  </CForm>
                </CCardBody>
              </CCard>
              <CCard className="text-white bg-primary py-5" style={{ width: '44%' }}>
                <CCardBody className="text-center">
                  <div>
                    <h2>{t("Signup")}</h2>
                    <p>
                      {t("newRegisterText")}
                    </p>
                    <Link to="/register">
                    <CButton color="light"  variant="outline" className="mt-3"
                      tabIndex={-1}
                      type="submit"
                    >
                      {t("RegisterNow")}
                    </CButton>
                    </Link>
                  </div>
                </CCardBody>
              </CCard>
            </CCardGroup>
          </CCol>
        </CRow>
      </CContainer>
    </div>
  )
}

export default Login
