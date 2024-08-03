import React, { useEffect, useMemo, useState } from 'react'

import {
  CAvatar,
  CButton,
  CButtonGroup,
  CCard,
  CCardBody,
  CCardFooter,
  CCardHeader,
  CCol,
  CAlert,
  CProgress,
  CRow,
  CTable,
  CTableBody,
  CTableDataCell,
  CTableHead,
  CTableHeaderCell,
  CTableRow,
  CModal,
  CModalHeader,
  CModalTitle,
  CModalFooter,
  CModalBody,
  CFormLabel,
  CFormInput,
  CFormSelect,
  CCardText
  

} from '@coreui/react'
import { CChartLine } from '@coreui/react-chartjs'
import { getStyle, hexToRgba } from '@coreui/utils'
import CIcon from '@coreui/icons-react'
import { cilNoteAdd } from '@coreui/icons'
import Lottie from 'lottie-react';

import ProcessAnimation from "../../content/animation/Process.json";

import { useDispatch, useSelector } from 'react-redux';
import { useNavigate } from "react-router-dom";

import {
  MaterialReactTable,
  useMaterialReactTable,
  MRT_ColumnDef,
  MRT_GlobalFilterTextField,
  MRT_ToggleFiltersButton,


} from 'material-react-table';

import {
  Box,
  Button,
  ListItemIcon,
  MenuItem,
  Typography,
  lighten,
  IconButton


} from '@mui/material';
import {
  Edit as EditIcon,
  Delete as DeleteIcon,
  Email as EmailIcon,
  AlignHorizontalCenter,
} from '@mui/icons-material';


import { useTranslation } from "react-i18next";

 


const DigitalFormEdit = () => { 


    return(
        <>
        </>
    )
 
}

export default DigitalFormEdit;