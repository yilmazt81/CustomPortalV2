

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
    },
    {
      field: 'propertyValue1',
      headerName: i18.t('PropertyValue1'),
      width: 150, 
    },
    {
      field: 'propertyValue2',
      headerName: i18.t('PropertyValue2'),
      width: 150, 
    },
    {
      field:  'propertyValue3' ,
      headerName: i18.t('PropertyValue3'),
      width: 150, 

    },
    {
      
      field: 'actions',
      headerName: 'Actions',
      width: 100, 
      renderCell: (params) => (
        <div>
          <IconButton
            onClick={() => OptionClick('Delete', params.row.id)}
            aria-label="delete"
            color="secondary"
          >
            <DeleteIcon />
          </IconButton>
          <IconButton
            onClick={() => OptionClick('Edit', params.row.id)}
            aria-label="delete"
            color="secondary"
          >
            <EditIcon />
          </IconButton>
        </div>
      ),
    },
  ];

};

export default  Gridcolumns;

 