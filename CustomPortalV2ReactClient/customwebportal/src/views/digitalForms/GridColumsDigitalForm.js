
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

const GridColumsDigitalForm = (OptionClick) => {
    return [ 
        {
            field: 'formDefinationName',
            headerName: i18.t('FormDefinationName'),
            width: 100,
        },
        {
            field: 'brancName',
            headerName: i18.t('BranchName'),
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
            valueFormatter: (params) => (params === null ? "" : dayjs(params.value).format('DD/MM/YYYY HH:mm'))
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
                    >

                        <Link to={{
                            pathname: '/digitalFormEdit',
                            search: '?id='+params.row.id,
                        }} >
                            <EditIcon /></Link>
                    </IconButton>

                    <IconButton
                        onClick={() => OptionClick('Download', params.row.id)}
                        aria-label="Download"
                        color="secondary"
                    >
                        <Download />
                    </IconButton>
                    <IconButton
                        onClick={() => OptionClick('Copy', params.row.id)}
                        aria-label="Copy"
                        color="secondary"
                    >
                        <CopyAll />
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

