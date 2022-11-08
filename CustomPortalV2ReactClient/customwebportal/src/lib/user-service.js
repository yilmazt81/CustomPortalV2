import react, { useState, useEffect } from 'react';
import {LoginRequest}  from './model/auth/login';
 
import {Endpoint} from './common/endpoints-constant';


import axios from 'axios';

 
export const UserApi = { 

  GetAll: function( reuest) {
    return axiosInstance.request({
        method: "GET",
        url: Endpoint.AUTH,
        data:reuest
    });
  },

}
 