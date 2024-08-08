import { CButton, CCol, CRow  } from '@coreui/react';
import React, { useState, useEffect } from 'react';
import { useTranslation } from "react-i18next";
import { cilSave,cilClearAll,cilPin } from '@coreui/icons';

import { CIcon } from '@coreui/icons-react';

function MenuButtons({ onClick }) {
    const { t } = useTranslation();

    return (
        <>
            <CRow>


                <CCol sm={3}>
                </CCol>
                <CCol sm={3}>
                    <CButton color="secondary"   ><CIcon icon={cilClearAll}/> {t("DigitalFormClear")}</CButton>
                </CCol>

                <CCol sm={3}>
                    <CButton color="info"   ><CIcon icon={cilPin}/>   {t("DigitalFormSaveDefault")}</CButton>
                </CCol>

                <CCol sm={3}>
                    <CButton color="primary" ><CIcon icon={cilSave}/> {t("DigitalFormSave")}</CButton>
                </CCol>

            </CRow>


        </>
    );
}

export default MenuButtons; 