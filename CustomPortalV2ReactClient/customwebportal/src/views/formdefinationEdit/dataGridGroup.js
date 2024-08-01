
import i18 from '../../translation/i18'

import * as React from 'react';
import IconButton from '@mui/material/IconButton';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';

 

const GridcolumnsGroups = (OptionClick) => {
    return [
        {
            field: 'orderNumber',
            headerName: i18.t('OrderNumber'),
            width: 70,
          },
        {
            field: 'name',
            headerName: i18.t('Name'),
            width: 200,
        },

        {
          field: 'actions',
          headerName: 'Actions',
          width: 150,
          renderCell: (params) => (
            <div>
              <IconButton
                onClick={() => OptionClick('Edit',params.row.id)}
                aria-label="edit"
                color="primary"
              >
                <EditIcon />
              </IconButton>
              <IconButton
                onClick={() => OptionClick('Delete',params.row.id)}
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

export default GridcolumnsGroups;

