

import i18 from '../../translation/i18'

import * as React from 'react';
import IconButton from '@mui/material/IconButton';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';

const Gridcolumns = (OptionClick) => {
  return [


    {
      accessorKey: 'tagName',
      header: i18.t('TagName'),
      size: 150,
    },
    {
      accessorKey: 'properyValue',
      header: i18.t('ProperyValue'),
      size: 150,
    },
    {
      accessorKey: 'properyValue2',
      header: i18.t('ProperyValue2'),
      size: 150,
    },
    {
      accessorKey: 'properyValue3',
      header: i18.t('ProperyValue3'),
      size: 150,
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
        </div>
      ),
    },
  ];

};

export default  Gridcolumns;

 