
import i18 from '../../translation/i18'

import * as React from 'react';
import IconButton from '@mui/material/IconButton';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import Download from '@mui/icons-material/Download';
import CopyAll from '@mui/icons-material/CopyAll';
import SendToMobile from '@mui/icons-material/SendToMobile';
import dayjs from 'dayjs';


import { Link } from 'react-router-dom';

const GridColumsFormDefinationAttachment = (OptionClick) => {
    return [      
        {
            field: 'formName',
            headerName: i18.t('FormName'),
            width: 100,
        },
        {
            field: 'fileName',
            headerName: i18.t('FileName'),
            width: 200,
        },

        {
            field: 'createdBy',
            headerName: i18.t('CreatedBy'),
            width: 200,
        },

        {
            field: 'createdDate',
            headerName: i18.t('CreatedDate'),
            width: 200,
            valueFormatter: (params) => (params === null ? "" : dayjs(params.value).format('DD/MM/YYYY HH:mm'))
        },

        {
            field: 'editedBy',
            headerName: i18.t('EditedDate'),
            width: 100,
            valueFormatter: (params) => (params === null ? "" : dayjs(params.value).format('DD/MM/YYYY HH:mm'))
        },
 
  
        {
            field: 'actions',
            headerName: 'Actions',
            width: 200,
            renderCell: (params) => (
                <div>
                    <IconButton
                        aria-label="edit"
                        color="primary"
                        onClick={() => OptionClick('Edit', params.row.id)}
                    >

                        
                            <EditIcon /> 
                    </IconButton>

                    <IconButton
                        onClick={() => OptionClick('Download', params.row.id)}
                        aria-label="Download"
                        color="secondary"
                    >
                        <Download />
                    </IconButton>
                       

                </div>
            ),
        },
    ];


};

export default GridColumsFormDefinationAttachment;

