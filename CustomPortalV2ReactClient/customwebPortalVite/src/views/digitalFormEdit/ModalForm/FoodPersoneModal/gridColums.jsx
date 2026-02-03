

import i18 from "../../../../translation/i18.jsx";

import * as React from 'react';
import IconButton from '@mui/material/IconButton';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';

const Gridcolumns = (OptionClick) => {
    return [


        {
            field: 'fullName',
            headerName: i18.t('FullName'),
            width: 150,
        },
        {
            field: 'title',
            headerName: i18.t('Title'),
            width: 150,
        },
        {
            field: 'registrationNumber',
            headerName: i18.t('RegistrationNumber'),
            width: 150,
        },
        {
            field: 'city',
            headerName: i18.t('City'),
            width: 150,

        },  
    ];

};

export default Gridcolumns;



