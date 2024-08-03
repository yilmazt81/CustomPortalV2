
import i18 from '../../translation/i18'

import * as React from 'react';
import IconButton from '@mui/material/IconButton';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import AddCircleIcon from '@mui/icons-material/AddCircle';
import AutoModeIcon from '@mui/icons-material/AutoMode'; 

import {
  CButton,
  CCol,
  CAlert,
  CRow,
  CModal,
  CModalHeader,
  CModalTitle,
  CModalFooter,
  CModalBody,
  CFormLabel,
  CFormInput,
  CFormCheck,
  CFormSwitch

} from '@coreui/react'



const OptionClick = (option, id) => {
  console.log(option);
}
const CheckItemValueChange = (option, id, checked) => {

}

const GridcolumnFormFields = (OptionClick, CheckItemValueChange) => {
  return [
    {
      field: 'orderNumber',
      headerName: i18.t('OrderNumber'),
      width: 70,
    },
    {
      field: 'fieldCaption',
      headerName: i18.t('FieldCaption'),
      width: 100,
    },
    {
      field: 'tagName',
      headerName: i18.t('TagName'),
      width: 100,
    },
    {
      field: 'controlType',
      headerName: i18.t('ControlType'),
      width: 100,
      renderCell: (params) => (
        <CFormLabel>{i18.t(params.row.controlType)}</CFormLabel>
      )
    },
    {
      field: 'mandatory',
      headerName: i18.t('Mandatory'),
      width: 70,
      renderCell: (params) => (
        <div>
          <CFormSwitch size='xl' checked={params.row.mandatory} onChange={() => CheckItemValueChange('mandatory', params.row.id, params.row.mandatory)}></CFormSwitch>
        </div>
      )
    },
    {
      field: 'deleted',
      headerName: i18.t('Deleted'),
      width: 70,
      renderCell: (params) => (
        <div>
          <CFormSwitch size='xl' checked={params.row.deleted} onChange={() => CheckItemValueChange('deleted', params.row.id, params.row.deleted)}></CFormSwitch>
        </div>
      )
    },
    {
      headerName: i18.t('Actions'),
      width: 100,
      renderCell: (params) => {
        let statusIcon;
        if (params.row.controlType == 'ComboBox' || params.row.controlType=='CheckBox' || params.row.controlType=='RadioBox')  {
          statusIcon = <IconButton color="secondary" onClick={() => OptionClick('AddComboItem', params.row.id)} > <AddCircleIcon /></IconButton>;
        } else if (params.row.autoComplate === true) {
          statusIcon = <AutoModeIcon />;
        }
        return (
          <div>
            <IconButton
              onClick={() => OptionClick('Edit', params.row.id)}
              aria-label="delete"
              color="secondary"
            >
              <EditIcon />
            </IconButton>

            {statusIcon}


          </div>
        )
      }
    },
  ];
};

export default GridcolumnFormFields;
