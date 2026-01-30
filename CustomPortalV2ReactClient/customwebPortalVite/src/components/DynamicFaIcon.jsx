import React from "react"; 
import * as Icons from "react-icons/fa";
import PropTypes from 'prop-types' 

const DynamicFaIcon = ({ name }) => {
    const IconComponent = Icons[name];
  
    if (!IconComponent) { // Return a default one
      return <Icons.FaBeer />;
    }
  
    return <IconComponent size={16} />;
  };

  export default DynamicFaIcon;

  DynamicFaIcon.propTypes = {
    name: PropTypes.string,
  }
  


