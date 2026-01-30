
import i18 from '../../translation/i18.jsx'

import * as React from 'react';
import IconButton from '@mui/material/IconButton';
import DeleteIcon from '@mui/icons-material/Delete'; 



const DataGridComboItems = (OptionClick) => {
    return [
        {
            field: 'name',
            headerName: i18.t('ComboBoxName'),
            width: 100,
        },
        {
            field: 'tagName',
            headerName: i18.t('ComboBoxTag'),
            width: 100,
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

export default DataGridComboItems;



