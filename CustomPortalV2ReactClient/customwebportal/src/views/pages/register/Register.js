import React,{useEffect,useState} from 'react'
import {
  CContainer,
  CRow,
  CCardBody,
  CCard,
  CCardGroup

} from '@coreui/react'
import MultiStep from 'react-multistep'
import "../../../../src/translation/i18";

import StepOne from "../../companyregister/stepone";
import StepTwo from "../../companyregister/steptwo";

import StepThree from "../../companyregister/stepthree";

//import {GetCountry} from '../../../lib/countryapi';

import { useTranslation } from "react-i18next";
import "../../../translation/i18";

const Register = () => {
  const { t } = useTranslation();


  return (
    <div className="bg-light min-vh-100 d-flex flex-row align-items-center">
      <CContainer>

        <CRow className="justify-content-center">

          <CCardGroup>
            <CCard className="p-4">
              <CCardBody className="p-4">
                <h1>{t("Register")}</h1>
                <p className="text-medium-emphasis">{t("Createyouraccount")}</p>
                <MultiStep activeStep={0} prevButton={{ title: 'prev step'}}>
                  <StepOne title={t("CompanyInfo")} />
                  <StepTwo title={t("Adress")}  />
                  <StepThree title= {t("password")}  />
                </MultiStep>

              </CCardBody>
            </CCard>
          </CCardGroup>
        </CRow>
      </CContainer>

    </div>
  )
}

export default Register
