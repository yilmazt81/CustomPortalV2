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
const StepTwoRegister = () => {
  const { t } = useTranslation();

  return (
    <CRow className="justify-content-center">
      <CCol md={11} lg={9} xl={8}>
        <CForm>
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
        </CForm>
      </CCol>
    </CRow>

  )
}

export default StepTwoRegister
