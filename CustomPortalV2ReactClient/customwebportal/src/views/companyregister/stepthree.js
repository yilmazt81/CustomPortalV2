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
import { cilMagnifyingGlass } from '@coreui/icons'
import { useTranslation } from "react-i18next";
import "../../translation/i18";

import CIcon from '@coreui/icons-react'
import { cilLockLocked, cilUser, cilPhone, cilRoom, cilIndustry, cilMap } from '@coreui/icons'
const StepThreeRegister = () => {
  const { t } = useTranslation();

  return (
    <CRow className="justify-content-center">
      <CCol md={11} lg={9} xl={8}>
        <CForm>    
          <CInputGroup className="mb-3">
            <CInputGroupText>
              <CIcon icon={cilLockLocked} />
            </CInputGroupText>
            <CFormInput
              type="password"
              placeholder={t("password")}
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


      </CCol>
    </CRow>

  )
}

export default StepThreeRegister
