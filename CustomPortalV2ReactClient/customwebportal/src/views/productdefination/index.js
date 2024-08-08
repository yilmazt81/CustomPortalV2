import React, { useEffect, useState } from 'react'

import {
  CButton,
  CButtonGroup,
  CCard,
  CCardBody,
  CCol,
  CRow,
  CAlert
} from '@coreui/react'

 
import { useNavigate } from "react-router-dom";
import { DataGrid } from '@mui/x-data-grid'

import { useTranslation } from "react-i18next";

import GridColumns from './GridColumns';
import ProductEditModal from './productEditModal'
import {GetSector} from 'src/lib/formdef';
import {CreateProduct,GetCompanyProducts} from 'src/lib/customProductapi';

const ProductDefination = () => {
  const navigate = useNavigate();
  const { t } = useTranslation();
  const [productList,setProductList] = useState([]);
  const [saveError,setsaveError]=useState(null);
  const [visibleEdit,SetVisibleEdit]=useState(false);
  const [customSectors,setCustomSectors]=useState([]);
  
  
  const optionClick = (option, id) => {
    //    EditGroupDefination(option === 'Delete', id);
}

function CreateNewProduct(){
  newProduct();
  SetVisibleEdit(true);
}

async function LoadCustomSectors() {

  try {
      var fSectorService = await GetSector();
      if (fSectorService.returnCode === 1) {
        setCustomSectors(fSectorService.data);
      } else {
        setsaveError(fSectorService.returnMessage);
      }
  } catch (error) {
    setsaveError(error.message);
  }
}
async function newProduct() {

  try {
      var fcreateReturn = await CreateProduct();
      if (fcreateReturn.returnCode === 1) {
        setProductList(fcreateReturn.data);
      } else {
        setsaveError(fcreateReturn.returnMessage);
      }
  } catch (error) {
    setsaveError(error.message);
  }
}
async function getProductList() {

  try {
      var fcreateReturn = await GetCompanyProducts();
      if (fcreateReturn.returnCode === 1) {
        setProductList(fcreateReturn.data);
      } else {
        setsaveError(fcreateReturn.returnMessage);
      }
  } catch (error) {
    setsaveError(error.message);
  }
}
useEffect(() => {
 
  LoadCustomSectors();
  getProductList();


}, []);

  const gridColumns=GridColumns(optionClick);

  return (

    <> <CCard className="mb-4">
      <CCardBody>
        <CRow>
          <CCol>
            <CButtonGroup role="group">

           
                <CButton color="primary" shape='rounded-3'  onClick={()=>CreateNewProduct()}  > {t("AddNewFormDefination")}</CButton>

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

          <ProductEditModal visiblep={visibleEdit} 
          customSectorList={customSectors} setClose={()=>SetVisibleEdit(false)} setFormData={()=>getProductList()}></ProductEditModal>
    </>
  )
}

export default ProductDefination;