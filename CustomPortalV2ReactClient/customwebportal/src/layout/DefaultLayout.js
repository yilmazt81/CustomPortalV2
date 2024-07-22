import React,{useEffect,useState,useRef} from 'react'
import { AppContent, AppSidebar, AppFooter, AppHeader } from '../components/index'

import { useDispatch, useSelector } from 'react-redux'; 
import { useNavigate } from "react-router-dom";
import {getUserMenu} from 'src/lib/userapi';
import {CToast,CToastHeader,CToastBody  }  from '@coreui/react'


const DefaultLayout = () => {
  const userToken= useSelector(state=> state.userToken);  
  const [userMenu,setUserMenu] = useState([]);
  const [toast, addToast] = useState(0);
  const navigate = useNavigate();
  const toaster = useRef()

  async function LoadUserMenu(){
    var appUserMenu =  await  getUserMenu();
    if (appUserMenu.ReturnCode!=1)
    {
      setUserMenu(appUserMenu.data);     
      console.log(userMenu);
    }
  }

   useEffect( () => {
 
    var lastToken=localStorage.getItem("LastToken");
    if (lastToken==null)
    {
        navigate('../Login'); 
    }else{
       
     if (userMenu.length==0)
     {
      LoadUserMenu();
     }
    } 
  
  }); 

  return (
    <div>
      <AppSidebar  menuList={userMenu}/>
      <div className="wrapper d-flex flex-column min-vh-100 bg-light">
        <AppHeader />
        <div className="body flex-grow-1 px-3">
          <AppContent />
        </div>
        <AppFooter />
      </div>
    </div>
  )
}

export default DefaultLayout
