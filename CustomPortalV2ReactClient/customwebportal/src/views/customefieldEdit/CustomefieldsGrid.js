
import i18 from '../../translation/i18'

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

            valueFormatter: (params) => (params === null ? "" : dayjs(params.value).format('DD/MM/YYYY HH:mm'))

        },
        {
            field: 'actions',
            headerName: 'Actions',
            width: 150,
            renderCell: (params) => (
                <div>

                    <IconButton>



                        <Link to={{
                            pathname: '/formdefinationsAComp',
                            search: `?formfieldid=${params.row.id}`,
                        }}> <Dehaze /></Link>;
                    </IconButton>

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

