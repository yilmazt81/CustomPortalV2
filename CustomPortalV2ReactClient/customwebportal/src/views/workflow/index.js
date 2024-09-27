import { CButton } from '@coreui/react';
import React, { useEffect } from 'react'
 
import { useTranslation } from "react-i18next"; 

const WorkFlow = () => { 
  const { t } = useTranslation();
 

    return (
        <>
          <CButton>{t("Ok")}</CButton>
        </>
    )

}

export default WorkFlow;