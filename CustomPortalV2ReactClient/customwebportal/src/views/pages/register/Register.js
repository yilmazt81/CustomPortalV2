import React from 'react'
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
import CIcon from '@coreui/icons-react'
import { cilLockLocked, cilUser,cilPhone,cilRoom,cilIndustry,cilMap } from '@coreui/icons'

import { useTranslation } from "react-i18next";
import "../../../../src/translation/i18";


const Register = () => {
  const { t } = useTranslation();


  return (
    <div className="bg-light min-vh-100 d-flex flex-row align-items-center">
      <CContainer>
        <CRow className="justify-content-center">
          <CCol md={9} lg={7} xl={6}>
            <CCard className="mx-4">
              <CCardBody className="p-4">
                <CForm>
                  <h1>{t("Register")}</h1>
                  <p className="text-medium-emphasis">{t("Createyouraccount")}</p>
                  <CInputGroup className="mb-3">
                    <CInputGroupText>
                      <CIcon icon={cilIndustry} />
                    </CInputGroupText>
                    <CFormInput placeholder={t("CompanyName")} autoComplete="CompanyName" />
                  </CInputGroup>
                  <CInputGroup className="mb-3">
                    <CInputGroupText>@</CInputGroupText>
                    <CFormInput placeholder={t("Email")} autoComplete="email" />
                  </CInputGroup>
                  <CInputGroup className="mb-3">
                    <CInputGroupText>
                      <CIcon icon={cilPhone} />
                    </CInputGroupText>
                    <CFormInput placeholder={t("PhoneNumber")} autoComplete="PhoneNumber" />
                  </CInputGroup>
                  <CInputGroup className="mb-3">
                    <CInputGroupText>
                      <CIcon icon={cilRoom} />
                    </CInputGroupText>
                    <CFormInput placeholder={t("TaxNumber")} autoComplete="TaxNumber" />
                  </CInputGroup>
                  <CInputGroup className="mb-3">
                    <CInputGroupText>
                      <CIcon icon={cilRoom} />
                    </CInputGroupText>
                    <CFormInput placeholder={t("MersisNo")} autoComplete="MersisNo" />
                  </CInputGroup>
                  <CInputGroup className="mb-3">
                    <CInputGroupText>
                      <CIcon icon={cilUser} />
                    </CInputGroupText>
                    <CFormInput placeholder={t("AuthorizedPersonName")} autoComplete="AuthorizedPersonName" />
                  </CInputGroup>
                  <CInputGroup className="mb-3">
                    <CInputGroupText>
                      <CIcon icon={cilMap} />
                    </CInputGroupText> 
                    <CFormSelect size='sm' aria-label={t("Country")}>
                      <option>Open this select menu</option>
                      <option value="1">One</option>
                      <option value="2">Two</option>
                      <option value="3">Three</option>
                    </CFormSelect>
                  </CInputGroup>
                  <CInputGroup className="mb-3">
                    <CInputGroupText>
                      <CIcon icon={cilMap} />
                    </CInputGroupText> 
                    <CFormSelect size='sm' aria-label={t("city")}>
                      <option>Open this select menu</option>
                      <option value="1">One</option>
                      <option value="2">Two</option>
                      <option value="3">Three</option>
                    </CFormSelect>
                  </CInputGroup>
                  <CInputGroup className="mb-3">
                  <CFormLabel htmlFor="textAdress">{t("Adress")}</CFormLabel>
                    
                  </CInputGroup>
                  <CInputGroup className="mb-3">
                     
                    <CFormTextarea id="textAdress" rows="5"></CFormTextarea>
                     
                  </CInputGroup>
                  <CInputGroup className="mb-3">
                    <CInputGroupText>
                      <CIcon icon={cilLockLocked} />
                    </CInputGroupText>
                    <CFormInput
                      type="password"
                      placeholder=   {t("password")}
                      autoComplete="new-password"
                    />
                  </CInputGroup>
                  <CInputGroup className="mb-4">
                    <CInputGroupText>
                      <CIcon icon={cilLockLocked} />
                    </CInputGroupText>
                    <CFormInput
                      type="password"
                      placeholder={t("Repeatpassword")}
                      autoComplete="new-password"
                    />
                  </CInputGroup>
                  <div className="d-grid">
                    <CButton color="success">{t("CreateAccount")}</CButton>
                  </div>
                </CForm>
              </CCardBody>
            </CCard>
          </CCol>
        </CRow>
      </CContainer>
    </div>
  )
}

export default Register
