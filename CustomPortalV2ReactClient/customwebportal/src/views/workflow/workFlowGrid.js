
import i18 from '../../translation/i18'

import * as React from 'react';
import IconButton from '@mui/material/IconButton';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
 
import dayjs from 'dayjs';

const WorkFlowGrid = (OptionClick) => {
 

    return [
        {
            field: 'workFlowName',
            headerName: i18.t('WorkFlowName'),
            width: 200,
            
        },  
        {
            field: 'createdBy',
            headerName: i18.t('CreatedBy'),
            width: 150,
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
            width: 150,
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

export default WorkFlowGrid;

