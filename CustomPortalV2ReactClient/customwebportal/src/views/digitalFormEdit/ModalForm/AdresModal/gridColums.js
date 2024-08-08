

import i18 from 'src/translation/i18'

import * as React from 'react';
import IconButton from '@mui/material/IconButton';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';

const Gridcolumns = (OptionClick) => {
    return [


        {
            field: 'companyName',
            headerName: i18.t('CompanyName'),
            width: 150,
        },
        {
            field: 'adress',
            headerName: i18.t('Adress'),
            width: 150,
        },
        {
            field: 'country',
            headerName: i18.t('Country'),
            width: 150,
        },
        {
            field: 'city',
            headerName: i18.t('City'),
            width: 150,

        }, 
        {
            field: 'factoryNumber',
            headerName: i18.t('FactoryNumber'),
            width: 150,

        }
    ];

};

export default Gridcolumns;

