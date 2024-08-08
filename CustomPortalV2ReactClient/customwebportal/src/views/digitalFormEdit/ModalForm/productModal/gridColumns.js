

import i18 from 'src/translation/i18'

import * as React from 'react';
import IconButton from '@mui/material/IconButton';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';

const Gridcolumns = (OptionClick) => {
    return [


        {
            field: 'productName',
            headerName: i18.t('ProductName'),
            width: 150,
        },
        {
            field: 'productName_TRK',
            headerName: i18.t('ProductName_TRK'),
            width: 150,
        },
        {
            field: 'gtipCode',
            headerName: i18.t('GtipCode'),
            width: 150,
        },
        {
            field: 'productCulture',
            headerName: i18.t('ProductCulture'),
            width: 150,

        },
        {
            field: 'isoCode',
            headerName: i18.t('IsoCode'),
            width: 150,

        }
    ];

};

export default Gridcolumns;

