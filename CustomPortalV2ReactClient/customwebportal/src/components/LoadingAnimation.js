
import React from "react";
import * as Icons from "react-icons/fa";
import PropTypes from 'prop-types'
import Lottie from "lottie-react";

import ProcessAnimation from "src/content/animation/Process.json";


const LoadingAnimation = ({ loading,size }) => {



    return (
        <>
            {loading ? <Lottie animationData={ProcessAnimation} loop={true} style={{ width:size, height: size }} ></Lottie> : ""}
        </>
    )
};

export default LoadingAnimation;

LoadingAnimation.propTypes = {
    loading: PropTypes.bool,
}




