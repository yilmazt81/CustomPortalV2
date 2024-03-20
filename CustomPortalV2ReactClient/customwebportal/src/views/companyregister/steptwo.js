import React ,{useEffect,useState} from 'react'
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
import {GetCountryForSale,GetCountryCity} from '../../lib/countryapi';

import CIcon from '@coreui/icons-react'
import { cilLockLocked, cilUser, cilPhone, cilRoom, cilIndustry, cilMap } from '@coreui/icons'
const StepTwoRegister = (props) => {
  const { t } = useTranslation();
  const [countrys, setcountrys] = useState([]);
  const [citys, setCitys] = useState([]);
  
  
 const [usersLoading, setUsersLoading] = useState(false);
  
  
  
  const [inputValues, setInputValues] = useState ({
    Country :"" ,
    city : '',
    Adress:"",

  });
 
  useEffect(() => {
    if (countrys.length==0)
    {

      loadData();
    } 
  }); 



  const loadData = async () => {
    
    setUsersLoading(true);
    debugger;
  console.log( process.env.REACT_APP_APIURL);

    var list= await GetCountryForSale();
    setcountrys(list.data);
    setUsersLoading(false);

  };

function handleChange(event) {
    const { name, value } = event.target;
 
    setInputValues({ ...inputValues, [name]: value });
   
    
  } 

  async   function countryChanges(event){
 
    const { name, value } = event.target;
 
    setInputValues({ ...inputValues, [name]: value });
    setUsersLoading(true);
    debugger;
    
    var list= await GetCountryCity(value);
    
    setCitys(list.data);
    setUsersLoading(false);

  }

  return (
      <CRow className="justify-content-center">
        <CCol md={11} lg={9} xl={8}>
          <CForm>
            <CInputGroup className="mb-3">
              <CInputGroupText>
                <CIcon icon={cilMap} />
              </CInputGroupText>
              <CFormSelect size='sm' aria-label={t("Country")} name="Country" onChange={countryChanges}>
                <option value="0">Seçiniz</option>
                {countrys.map(item => {               
                  
                  return (<option key={item.id} value={item.id}>{item.name}</option>);
                })}
              </CFormSelect>
            </CInputGroup>
            <CInputGroup className="mb-3">
              <CInputGroupText>
                <CIcon icon={cilMap} />
              </CInputGroupText>
              <CFormSelect size='sm' aria-label={t("city")} name="city" onChange={handleChange}>
                <option>Open this select menu</option>
                {citys.map(item => {               
                  
                  return (<option key={item.id} value={item.id}>{item.name}</option>);
                })}
              </CFormSelect>
            </CInputGroup>
            <CInputGroup className="mb-3">
              <CFormLabel htmlFor="textAdress">{t("Adress")}</CFormLabel>

            </CInputGroup>
            <CInputGroup className="mb-3">

              <CFormTextarea id="textAdress" onChange={handleChange} rows="5"></CFormTextarea>

            </CInputGroup>
          </CForm>
        </CCol>
      </CRow>

  )
}

export default StepTwoRegister
