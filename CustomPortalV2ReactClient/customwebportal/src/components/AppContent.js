import React, { Suspense } from 'react'
import { Navigate, Route, Routes } from 'react-router-dom'
import { CContainer, CSpinner } from '@coreui/react'

import { useTranslation } from "react-i18next";


// routes config
import routes from '../routes'

const AppContent = () => {

  const { t } = useTranslation(); 

  return (
    <CContainer lg>
      <Suspense fallback={<CSpinner color="primary" />}>
        <Routes>
          {routes.map((route, idx) => {
          
            return (
              route.element && (
          
                <Route
                  key={idx}
                  path={route.path}
                  exact={route.exact}
                  name={t(route.name)}
                  element={<route.element />}
                />
              )
            )
          })}
          <Route path="/" element={<Navigate to="dashboard" replace />} />
        </Routes>
      </Suspense>
    </CContainer>
  )
}

export default React.memo(AppContent)
