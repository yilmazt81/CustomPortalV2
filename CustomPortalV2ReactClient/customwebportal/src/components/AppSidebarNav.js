import React,{useEffect} from 'react'
import { NavLink, useLocation } from 'react-router-dom'
import PropTypes from 'prop-types' 
import { CBadge ,CNavItem,CNavGroup } from '@coreui/react' 
import CIcon from '@coreui/icons-react'

import DynamicFaIcon  from './DynamicFaIcon';


export const AppSidebarNav = ({ items }) => {
  const location = useLocation()
  const navLink = (name, icon, badge) => {
    return (
      
      <>
        <DynamicFaIcon name={icon}></DynamicFaIcon>
        {name && name}
        {badge && (
          <CBadge color={badge.color} className="ms-auto">
               {badge.text}
          </CBadge>
        )}
      </>
    )
  }



 

  const navItem = (item, index) => {
    const {  name, badge, icon, ...rest } = item
  
    return (
      <CNavItem
        {...(rest.to &&
          !rest.items && {
            component: NavLink,
          })}
        key={index}
        {...rest}
      >
        {navLink(name, icon, badge)}
      </CNavItem>
    )
  }
  const navGroup = (item, index) => {
    const { name, icon, to, ...rest } = item
    
    return ( 
      <CNavGroup
        idx={String(index)}
        key={index}
        toggler={navLink(name, icon)}
        visible={location.pathname.startsWith(to)}
        {...rest}
      >
        {item.items?.map((item, index) =>
          item.items ? navGroup(item, index) : navItem(item, index),
        )}
      </CNavGroup>
    )
  }

  return (
    <React.Fragment>
      {items &&
        items.map((item, index) => (item.items ? navGroup(item, index) : navItem(item, index)))}
    </React.Fragment>
  )
}

AppSidebarNav.propTypes = {
  items: PropTypes.arrayOf(PropTypes.any),
}
