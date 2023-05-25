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
const StepOneRegister = () => {
    const { t } = useTranslation();



    return (

        <CRow className="justify-content-center">
            <CCol md={11} lg={9} xl={8}>
                <CForm>
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
                  
                </CForm>


            </CCol>
        </CRow>

    )
}

export default StepOneRegister
