
import i18 from '../../translation/i18'

import * as React from 'react';
import IconButton from '@mui/material/IconButton';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';

import dayjs from 'dayjs';

const GridColumsDigitalForm = (OptionClick) => {
    return [
        {
            field: 'id',
            headerName: 'Id',
            width: 70,
        },
        {
            field: 'formDefinationName',
            headerName: i18.t('FormDefinationName'),
            width: 100,
        },
        {
            field: 'brancName',
            headerName: i18.t('BrancName'),
            width: 100,
        },

        {
            field: 'senderCompanyName',
            headerName: i18.t('SenderCompanyName'),
            width: 200,
        },
        
        {
            field: 'recrivedCompanyName',
            headerName: i18.t('RecrivedCompanyName'),
            width: 200,
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
            valueFormatter: (params) => (params===null ?"" : dayjs(params.value).format('DD/MM/YYYY HH:mm'))
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

export default GridColumsDigitalForm;

