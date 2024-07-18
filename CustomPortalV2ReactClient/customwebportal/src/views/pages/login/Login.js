import React, { useState,useEffect } from 'react'
import { Link } from 'react-router-dom'
import { useTranslation } from "react-i18next";
import "../../../../src/translation/i18";
import axios from "axios"; 
import {
  CButton,
  CCard,
  CCardBody,
  CCardGroup,
  CCol,
  CContainer,
  CForm,
  CFormInput,
  CInputGroup,
  CInputGroupText,
  CRow, 
  CAlert
} from '@coreui/react'
import CIcon from '@coreui/icons-react'
import { cilLockLocked, cilUser,cilHome } from '@coreui/icons'
 
import {useNavigate } from 'react-router-dom'
//import SimpleReactValidator from 'simple-react-validator'; 
import {LoginUser} from '../../../lib/userapi';

import Lottie from "lottie-react"; 
import animationLogin from "../../../content/animation/Login.json";
import { useDispatch, useSelector } from 'react-redux';  

const Login = () => {
  const { t } = useTranslation(); 
  const navigate = useNavigate();
  const [loginStart, setloginStart] = useState(false);
  const [loginError, setLoginError] = useState(null);

  const userToken= useSelector(state=> state.userToken);   

  const dispatch = useDispatch();

  const [userInfo,setUserInfo] = useState({ UserName: "", 
                                password: "",
                                CompanyCode:"" ,
                                UserLanguage:0});
   
 function handleChange(event) { 
  const { name, value } = event.target;
  setUserInfo({ ...userInfo, [name]: value }); 

}
 async function handleSubmit(event){
   
 
  setloginStart(true);
  try {
    var loginresult= await  LoginUser(userInfo);
  
      if (loginresult.isLogin)
      {  
        dispatch({ type: 'set', userToken: loginresult.token });       
        dispatch({ type: 'set', UserName: userInfo.UserName });        
        dispatch({ type: 'set', CompanyCode: userInfo.CompanyCode })
        axios.defaults.headers.common['Authorization'] = `Bearer ${loginresult.token}`;
     
        navigate('../Dashboard');

      }else{ 
        setLoginError(t(loginresult.returnMessage));
      }
    } catch (error) { 
      setLoginError(error.message);
    }finally{
      setloginStart(false);
    }   

  }
  
  
  useEffect(() => { 
    if (userToken!=null)
      {
        axios.defaults.headers.common['Authorization'] = `Bearer ${userToken}`;
        localStorage.setItem("Token",userToken);
        navigate('../Dashboard'); 
      }else{

        setLoginError(null);
      } 

     /* setUserInfo({CompanyCode:companyCode,
      UserName:userName
     } );  
      */
    
  }, []);

  return (

    <div className="bg-light min-vh-100 d-flex flex-row align-items-center">
      <CContainer>
        <CRow className="justify-content-center">
          <CCol md={8}>
            <CCardGroup>
              <CCard className="p-4">
                <CCardBody>
                  <CForm>
                    <h1>{t("login")}</h1>
                    <p className="text-medium-emphasis">{t("SignInAccount")}t</p>
                    <CInputGroup className="mb-3">
                      <CInputGroupText>
                        <CIcon icon={cilHome} />
                      </CInputGroupText>
                      <CFormInput placeholder={t("CompanyCode")}    
                            name="CompanyCode"                   
                        onChange= {e=>handleChange(e)}   value={userInfo.CompanyCode} />
                    </CInputGroup>
                    <CInputGroup className="mb-3">
                      <CInputGroupText>
                        <CIcon icon={cilUser} />
                      </CInputGroupText>
                      <CFormInput placeholder={t("Username")}
                       name="UserName"
                        onChange={e=>handleChange(e)} value={ userInfo.UserName} />
                    </CInputGroup>
                    <CInputGroup className="mb-4">
                      <CInputGroupText>
                        <CIcon icon={cilLockLocked} />
                      </CInputGroupText>
                      <CFormInput
                        type="password"
                        name="password"  
                        placeholder={t("Password")}
                        autoComplete="current-password"
                        onChange={e=>handleChange(e)}
                      />
                    </CInputGroup>
                    <CRow>
                    <CRow> 
                      {
                      loginStart ?<Lottie animationData={animationLogin} loop={true} style={{width: "40%", height: "40%"}} ></Lottie>:"" 
                      }
                      { loginError!=null ?
                        <CAlert color="warning">{loginError}</CAlert>
                        :""
                      }
                    </CRow>
                    </CRow>
                    <CRow>
                      <CCol xs={6}>
                        <CButton color="primary" className="px-4"
                          onClick={handleSubmit} >
                             
                          {t("login")}
                          

                        </CButton>
                      </CCol>
                      <CCol xs={6} className="text-right">
                        <CButton color="primary" className="px-0">
                          {t("Forgotpassword")} 
                        </CButton>
                      </CCol>
                    </CRow>
                   
                  </CForm>
                </CCardBody>
              </CCard>
              <CCard className="text-white bg-primary py-5" style={{ width: '44%' }}>
                <CCardBody className="text-center">
                  <div>
                    <h2>{t("Signup")}</h2>
                    <p>
                      {t("newRegisterText")}
                    </p>
                    <Link to="/register">
                    <CButton color="light"  variant="outline" className="mt-3"
                      tabIndex={-1}
                      type="submit"
                    >
                      {t("RegisterNow")}
                    </CButton>
                    </Link>
                  </div>
                </CCardBody>
              </CCard>
            </CCardGroup>
          </CCol>
        </CRow>

    

      </CContainer>
    </div>
  )
}

export default Login
