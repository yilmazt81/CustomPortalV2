import React, { useEffect, useState ,useContext} from 'react'

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
import {CreateProduct,GetCompanyProducts,GetCompanyProduct,DeleteProduct} from 'src/lib/customProductapi';
import DeleteModal from 'src/components/DeleteModal';

import { UrlContext } from 'src/lib/URLContext';

const ProductDefination = () => { 
  const { t } = useTranslation();
  const [productList,setProductList] = useState([]);
  const [product,setProduct]=useState(null);
  const [saveError,setsaveError]=useState(null);
  const [visibleEdit,setVisibleEdit]=useState(false);
  const [visibleDelete,setVisibleDelete]=useState(false);
  const [customSectors,setCustomSectors]=useState([]);
  const { dispatch } = useContext(UrlContext);
  
const optionClick = (option, id) => {
      EditGroupDefination(option === 'Delete', id);
}

function CreateNewProduct(){
  newProduct();
  setVisibleEdit(true);
}


async function EditGroupDefination(forDelete, id) {
  try {
      setsaveError(null);
      setVisibleDelete(false);
      setVisibleEdit(false);
      var editCompanyProduct = await GetCompanyProduct(id);
 
      if (editCompanyProduct.returnCode === 1) {
          setProduct(editCompanyProduct.data);
          if (forDelete) {
            setVisibleDelete(true);
          } else {
            setVisibleEdit(true);
          }

      } else {
        setsaveError(editCompanyProduct.returnMessage);
      }
  } catch (error) {
    setsaveError(error.message);
  } finally {
      // setdeleteStart(false);
  }
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
        setProduct(fcreateReturn.data);
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

async function DeleteProductDB() {
  try {
    var fcreateReturn = await DeleteProduct(product.id);
   
    if (fcreateReturn.returnCode === 1) {
      setVisibleDelete(false);
      getProductList();
    } else {
      setsaveError(fcreateReturn.returnMessage);
    }
} catch (error) {
  setsaveError(error.message);
} 
}

useEffect(() => {
 
  dispatch({type:'reset'})
  
  dispatch({
    type: 'Add',
    payload: { pathname: "#/productdefination", name: t("ProductDefination"), active: false }
});



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
          customSectorList={customSectors} 
          setClose={()=>setVisibleEdit(false)}
          productp={product}
          setFormData={()=>getProductList()}></ProductEditModal>

          
          <DeleteModal 
          setClose={()=>setVisibleDelete(false)}
          message={product?.productName}
          title={t("ModalDeleteProductTitle")}
          visiblep={visibleDelete}
          message2={t("AutoComplateMapDeleteMessage")}
          OnClickOk={()=>DeleteProductDB()}
          >


          </DeleteModal>
    </>
  )
}

export default ProductDefination;