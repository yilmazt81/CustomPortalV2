
import i18 from '../../translation/i18'

import * as React from 'react';
import IconButton from '@mui/material/IconButton';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';

const OptionClick = (option, id) => {
  console.log(option);
}

const GridcolumnFormFields = () => {
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
    },
    {
      field: 'mandatory',
      headerName: i18.t('Mandatory'),
      width: 100,
    },
    {
      field: 'deleted',
      headerName: i18.t('Deleted'),
      width: 100,
    },
    {
      field: 'actions',
      headerName: 'Actions',
      width: 150,
      renderCell: (params) => (
        <div>
          <IconButton
            onClick={() => OptionClick('Edit', params.row.id)}
            aria-label="edit"
            color="primary"
          >
            <EditIcon />
          </IconButton>
          <IconButton
            onClick={() => OptionClick('Delete', params.row.id)}
            aria-label="delete"
            color="secondary"
          >
            <DeleteIcon />
          </IconButton>
        </div>
      ),
    },
  ];
};

export default GridcolumnFormFields;
