import React from 'react'
import { CFooter } from '@coreui/react'

const AppFooter = () => {
  return (
    <CFooter>
      <div>
        <a href="https://istanbulyazilimofisi.com" target="_blank" rel="noopener noreferrer">
          The Office
        </a>
        <span className="ms-1">&copy; 2024 Istanbul Yazilim Ofisi.</span>
      </div>
      <div className="ms-auto">
        
       
      </div>
    </CFooter>
  )
}

export default React.memo(AppFooter)


