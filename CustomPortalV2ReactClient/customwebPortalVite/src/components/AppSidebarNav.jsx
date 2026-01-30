import React from 'react'
import { useLocation } from 'react-router-dom'
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
    const {  name, badge, icon, to, items, ...rest } = item
 
    if (to && !items) {
      // This is a link item - use regular href for hash routing
      return (
        <CNavItem
          key={index}
          href={`#${to}`}
          {...rest}
        >
          {navLink(name, icon, badge)}
        </CNavItem>
      )
    }
    
    // Regular item without link
    return (
      <CNavItem
        key={index}
        {...rest}
      >
        {navLink(name, icon, badge)}
      </CNavItem>
    )
  }
  
  const navGroup = (item, index) => {
    const { name, icon, to, items, ...rest } = item
    
    return ( 
      <CNavGroup
        idx={String(index)}
        key={index}
        toggler={navLink(name, icon)}
        visible={to && location.pathname.startsWith(to)}
        {...rest}
      >
        {items?.map((item, index) =>
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


