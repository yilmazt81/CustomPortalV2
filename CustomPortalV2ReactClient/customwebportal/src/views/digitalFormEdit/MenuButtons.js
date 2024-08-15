import { CButton, CCol, CRow  } from '@coreui/react';
import React, { useState, useEffect } from 'react';
import { useTranslation } from "react-i18next";
import { cilSave,cilClearAll,cilPin } from '@coreui/icons';

import { CIcon } from '@coreui/icons-react';

function MenuButtons({ onButtonClick }) {
    const { t } = useTranslation();

    return (
        <>
            <CRow>


                <CCol sm={3}>
                </CCol>
                <CCol sm={3}>
                    <CButton color="secondary"  type='reset'  onClick={()=>onButtonClick('Reset')} ><CIcon icon={cilClearAll}/> {t("DigitalFormClear")}</CButton>
                </CCol>

                <CCol sm={3}>
                    <CButton color="info" onClick={()=>onButtonClick('SaveDefault')}  ><CIcon icon={cilPin}/>   {t("DigitalFormSaveDefault")}</CButton>
                </CCol>

                <CCol sm={3}>
                    <CButton color="primary"  onClick={()=>onButtonClick('Save')}  ><CIcon icon={cilSave}/> {t("DigitalFormSave")}</CButton>
                </CCol>

            </CRow>


        </>
    );
}

export default MenuButtons; 