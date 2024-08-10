

import * as React from 'react';
import i18 from '../../translation/i18'

import dayjs from 'dayjs';
import IconButton from '@mui/material/IconButton';
import Settings from '@mui/icons-material/Settings';

import { Link } from 'react-router-dom';
import EditIcon from '@mui/icons-material/Edit';
import AttachFileSharp from '@mui/icons-material/AttachFileSharp';
import FileCopy from '@mui/icons-material/FileCopy';
import Rule from '@mui/icons-material/Rule';

const Gridcolumns = (OptionClick) => {
  return [
    {
      field: 'id', //access nested data with dot notation
      headerName: 'Id',
      width: 50,
    },
    {
      field: 'formName',
      headerName: i18.t('FormName'),
      width: 300,
    },
    {
      field: 'createdBy',
      headerName: i18.t('CreatedBy'),
      width: 150,
    },
    {
      field: 'createdDate',
      headerName: i18.t('CreatedDate'),
      type: 'date',
      valueFormatter: (params) => (params === null ? "" : dayjs(params.value).format('DD/MM/YYYY HH:mm')),
      width: 150,
    },
    {
      field: 'customSectorName',
      headerName: i18.t('CustomSectorName'),
      width: 150,
    },
    {
      field: 'actions',
      headerName: 'Actions',
      width: 200,
      renderCell: (params) => (
        <div>
          <IconButton
            onClick={() => OptionClick('Edit', params.row.id)}
            aria-label="edit"
            color="primary"
          >
            <EditIcon />
          </IconButton>
          <IconButton>
            <Link to={{
              pathname: '/FormDefinationTypeEdit',
              search: '?formdefinationId=' + params.row.id,
            }} >   <Settings></Settings></Link>

          </IconButton>
          <IconButton>
            <Link to={{
              pathname: '/FormDefinationVersion',
              search: '?formdefinationId=' + params.row.id,
            }} >

              <FileCopy /> </Link>


          </IconButton>

          <IconButton ><Link to={{
            pathname: '/definationattachments',
            search: '?formdefinationId=' + params.row.id,
          }} >
            <AttachFileSharp /></Link>
          </IconButton>

          <IconButton
            onClick={() => OptionClick('Roles', params.row.id)}
            aria-label="edit"
            color="primary"
          >
            <Rule />
          </IconButton>


        </div>
      ),
    },
  ];


};

export default Gridcolumns;