
import i18 from '../../translation/i18'

import * as React from 'react';
 



const datagridFormGroupCheckBox = (OptionClick) => {
    return [    
        {
            field: 'formNumber',
            headerName: i18.t('GroupFormNumber'),
            width: 100,
        },
        {
            field: 'name',
            headerName: i18.t('Name'),
            width: 200,
        }, 

        {
            field: 'actions',
            headerName: 'Actions',
            width: 150,
            renderCell: (params) => (
                <div>
                   
                </div>
            ),
        },
    ];


};

export default datagridFormGroupCheckBox;

