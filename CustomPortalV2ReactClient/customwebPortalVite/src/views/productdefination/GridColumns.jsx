
import i18 from '../../translation/i18.jsx'

import * as React from 'react';
import IconButton from '@mui/material/IconButton';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
 
import dayjs from 'dayjs';

const GridColumns = (OptionClick) => {

 

    return [
        {
            field: 'customSectorName',
            headerName: i18.t('CustomSectorName'),
            width: 100,
        },
        {
            field: 'productName',
            headerName: i18.t('ProductName'),
            width: 100,
        },
        {
            field: 'productName_TRK',
            headerName: i18.t('ProductName_TRK'),
            width: 100,
        },

        {
            field: 'productCulture',
            headerName: i18.t('ProductCulture'),
            width: 200,
        },

        {
            field: 'gtipCode',
            headerName: i18.t('GtipCode'),
            width: 100,
        },

        {
            field: 'createdBy',
            headerName: i18.t('CreatedBy'),
            width: 100,
        },

        {
            field: 'createdDate',
            headerName: i18.t('CreatedDate'),
            width: 100, 
            valueFormatter: (params) => dayjs(params.value).format('DD/MM/YYYY HH:mm')
        },

        {
            field: 'editedBy',
            headerName: i18.t('EditedBy'),
            width: 100,
        },

        {
            field: 'editedDate',
            headerName: i18.t('EditedDate'),
            width: 100,
             
            valueFormatter: (params) => (params===null ?"" : dayjs(params.value).format('DD/MM/YYYY HH:mm'))

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

export default GridColumns;



