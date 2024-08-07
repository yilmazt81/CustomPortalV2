

import i18 from '../../translation/i18'

import * as React from 'react';
import IconButton from '@mui/material/IconButton';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';

const Gridcolumns = (OptionClick) => {
  return [


    { 
      field: 'fieldCaption',
      headerName: i18.t('TagName'),
      width: 150,
      height:20,
    },
    {
      field: 'properyValue',
      headerName: i18.t('ProperyValue'),
      width: 150,
      height:20,
    },
    {
      field: 'properyValue2',
      headerName: i18.t('ProperyValue2'),
      width: 150,
      height:20,
    },
    {
      field: 'properyValue3',
      headerName: i18.t('ProperyValue3'),
      width: 150,
      height:20,

    },
    {
      
      field: 'actions',
      headerName: 'Actions',
      width: 100,
      height:20,
      renderCell: (params) => (
        <div>
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

export default  Gridcolumns;

 