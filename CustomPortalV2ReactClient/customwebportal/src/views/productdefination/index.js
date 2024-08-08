import React, { useEffect, useState } from 'react'

import {
  CAvatar,
  CButton,
  CButtonGroup,
  CCard,
  CCardBody,
  CCardFooter,
  CCardHeader,
  CCol,
  CProgress,
  CRow,
  CTable,
  CTableBody,
  CTableDataCell,
  CTableHead,
  CTableHeaderCell,
  CTableRow,
  CAlert
} from '@coreui/react'
import { CChartLine } from '@coreui/react-chartjs'
import { getStyle, hexToRgba } from '@coreui/utils'
import CIcon from '@coreui/icons-react'
import {
  cibCcAmex,
  cibCcApplePay,
  cibCcMastercard,
  cibCcPaypal,
  cibCcStripe,
  cibCcVisa,
  cibGoogle,
  cibFacebook,
  cibLinkedin,
  cifBr,
  cifEs,
  cifFr,
  cifIn,
  cifPl,
  cifUs,
  cibTwitter,
  cilCloudDownload,
  cilPeople,
  cilUser,
  cilUserFemale,
} from '@coreui/icons'

 
import { useNavigate } from "react-router-dom";
import { DataGrid } from '@mui/x-data-grid'

import { useTranslation } from "react-i18next";

import GridColumns from './GridColumns';


const ProductDefination = () => {
  const navigate = useNavigate();
  const { t } = useTranslation();
  const [productList,setProductList] = useState([]);
  const [saveError,setsaveError]=useState(null);
  
  const optionClick = (option, id) => {
    //    EditGroupDefination(option === 'Delete', id);
}

  const gridColumns=GridColumns(optionClick);

  return (

    <> <CCard className="mb-4">
      <CCardBody>
        <CRow>
          <CCol>
            <CButtonGroup role="group">

           
                <CButton color="primary" shape='rounded-3'   > {t("AddNewFormDefination")}</CButton>

            </CButtonGroup>
          </CCol>
        </CRow>
     
        <CRow>

          <CCol>

            <DataGrid rows={productList}
              columns={gridColumns}
              slotProps={{
                toolbar: {
                  showQuickFilter: true,
                },
              }}
            // onRowClick={handleSelectFormGroupClick}
            // onRowSelectionModelChange={(e) => GridGroupRowChange(e)}
            />

          </CCol>
        </CRow>
        <CRow>
          {
            saveError != null ?
              <CAlert color="warning">{saveError}</CAlert>
              : ""
          }
        </CRow>
      </CCardBody>
    </CCard>

    </>
  )
}

export default ProductDefination;