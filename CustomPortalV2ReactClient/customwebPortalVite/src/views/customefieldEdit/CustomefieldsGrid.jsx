
import i18 from '../../translation/i18.jsx'

import * as React from 'react';
import IconButton from '@mui/material/IconButton';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import Dehaze from '@mui/icons-material/Dehaze';


import { Link } from 'react-router-dom';



import dayjs from 'dayjs';

const CustomefieldsGrid = (OptionClick) => {


    return [
        {
            field: 'fieldCaption',
            headerName: i18.t('FieldCaption'),
            width: 200,

        },
        {
            field: 'controlType',
            headerName: i18.t('ControlType'),
            width: 200,

        },
        {
            field: 'tagName',
            headerName: i18.t('TagName'),
            width: 200,

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
                    <IconButton>

                    </IconButton>
                </div>
            ),
        },
    ];


};

export default CustomefieldsGrid;



