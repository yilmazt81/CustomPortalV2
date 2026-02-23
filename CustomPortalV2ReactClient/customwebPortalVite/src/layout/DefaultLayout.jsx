import React, { useEffect, useState, useRef,createContext, useReducer } from 'react'
import { AppContent, AppSidebar, AppFooter, AppHeader } from '../components/index.jsx'
import CookieConsent from '../components/CookieConsent.jsx'

import { useDispatch, useSelector } from 'react-redux';
import { useNavigate } from "react-router-dom";
import { getUserMenu } from "../lib/userapi.jsx"
  

const DefaultLayout = () => {
  const userToken = useSelector(state => state.userToken);
  const [userMenu, setUserMenu] = useState([]);
  
  const UrlContext = createContext();

  const navigate = useNavigate();
  const toaster = useRef()

  async function LoadUserMenu() {

    try {
      var appUserMenu = await getUserMenu();
      if (appUserMenu.returnCode == 1) {
        setUserMenu(appUserMenu.data);
        console.log(userMenu);
      } else {
        debugger;
      }
    } catch (error) {
      debugger;
      localStorage.removeItem("LastToken");
      navigate('../Login');
    }
  }

  useEffect(() => {
    var lastToken = localStorage.getItem("LastToken");
    if (lastToken == null) {
      navigate('../Login');
    } else {
      if (userMenu.length == 0) {
        LoadUserMenu();
      }
    }
  }, [userMenu.length, navigate]);


  return (
    <div className="d-flex" style={{ height: '100vh' }}>
      <AppSidebar menuList={userMenu} />
      <div className="wrapper d-flex flex-column flex-grow-1">
        <AppHeader />
        <div className="body flex-grow-1 px-3" style={{ overflowY: 'auto' }}>
          <AppContent />
        </div>
        <AppFooter />
      </div>
      <CookieConsent />
    </div>
  )
}

export default DefaultLayout


