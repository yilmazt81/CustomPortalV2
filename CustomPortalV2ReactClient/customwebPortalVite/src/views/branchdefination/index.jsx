import { useEffect, useState, useContext } from 'react'

import {
  CButton,
  CButtonGroup,
  CCard,
  CCardBody,
  CCol,
  CAlert,
  CRow,
} from '@coreui/react'

import {
  MaterialReactTable,
  useMaterialReactTable,
} from 'material-react-table';

import {
  Box,
  IconButton


} from '@mui/material';
import {
  Edit as EditIcon,
  Delete as DeleteIcon,
} from '@mui/icons-material';


import { useTranslation } from "react-i18next";

import { Gridcolumns } from './DataGrid';
import { GetBranchList, GetBranch, DeleteBranch, CreateNewBranch } from '../../lib/companyapi.jsx';

import DeleteModal from '../../components/DeleteModal.jsx'

import BranchEditModal from './branchEditModal'


import { UrlContext } from '../../lib/URLContext.jsx';


const BranchDefination = () => {

  const { t } = useTranslation();
  const [branchList, setBranchList] = useState([]);

  const [editBranch, setEditBranch] = useState(null);

  const [visible, setVisible] = useState(false)
  const [visibleDelete, setVisibleDelete] = useState(false)

  const { dispatch } = useContext(UrlContext); 

  const [saveError, setSaveError] = useState(null);

  const loadBranchList = async () => {
    try {
      setVisible(false);
      const companyBranchList = await GetBranchList();

      if (companyBranchList.returnCode === 1) {
        setBranchList(companyBranchList.data);
      } else {
        setSaveError(companyBranchList.returnMessage);
      }

    } catch (error) {
      setSaveError(error.message);
    }

  };


  const setLocationAdress = () => {
    dispatch({ type: 'reset' })
    dispatch({
      type: 'Add',
      payload: { pathname: "./BranchDefination", name: t("BranchDefination"), active: false }
    });
  };


  useEffect(() => {

    try {
      setLocationAdress();
      loadBranchList(); 

    } catch (error) {
      console.log(error);
    }

  }, []);

  const editData = async (row) => {
    const id = row.original["id"];
    try {
      const editbranch = await GetBranch(id);
      if (editbranch.returnCode === 1) {
        setEditBranch(editbranch.data);
        setVisible(!visible);
      } else {
        setSaveError(editbranch.ReturnMessage);
      }
    } catch (error) {
      setSaveError(error.message);
    }
  };

  const deleteData = async (row) => {
    const id = row.original["id"];
    setSaveError(null);
    try {
      const editbranch = await GetBranch(id);
      if (editbranch.returnCode === 1) {
        setEditBranch(editbranch.data); 
        setVisibleDelete(true);
      } else {
        setSaveError(editbranch.ReturnMessage);
      }
    } catch (error) {
      setSaveError(error.message);
    }
  };

 
  

  const deleteDataServer = async () => {
    try {
      const deleteBranchReturn = await DeleteBranch(editBranch.id);
      if (deleteBranchReturn.returnCode === 1) {
        setVisibleDelete(!visibleDelete);
        loadBranchList();
      } else {
        setSaveError(deleteBranchReturn.returnMessage);
      }
    } catch (error) {
      setSaveError(error.message);
    }
  };

  const newBranch = async () => {
    setSaveError(null);

    try {
      const createBranchReturn = await CreateNewBranch();
      if (createBranchReturn.returnCode === 1) {

        setEditBranch(createBranchReturn.data);
      } else {
        setSaveError(createBranchReturn.ReturnMessage);
      }
    } catch (error) {
      setSaveError(error.message);
    }
    setVisible(!visible);
  };

  const table = useMaterialReactTable({
    columns: Gridcolumns,
    data: branchList,
    enableRowActions: true,
    layoutMode: "grid",
    positionActionsColumn: 'last',
    renderRowActions: ({ row }) => (
      <Box sx={{ display: 'flex', flexWrap: 'nowrap', gap: '3px' }}>
        <IconButton onClick={() => editData(row)}>
          <EditIcon />
        </IconButton>
        <IconButton onClick={() => deleteData(row)}>
          <DeleteIcon />
        </IconButton>

      </Box>
    ),

  });

  return (<>




    <DeleteModal title={t("BranchDelete")}
      message={t("DeleteMessage1")}
      message2={t("DeleteMessage2")}
      visiblep={visibleDelete}
      OnClickOk={() => deleteDataServer()}
      OnClickCancel={() => setVisibleDelete(!visibleDelete)}
    ></DeleteModal>

    <BranchEditModal visiblep={visible} editBranchp={editBranch}  OnCloseModal={() => setVisible(false)} setFormData={() => loadBranchList()} ></BranchEditModal>

   

    <CCard className="mb-4">
      <CCardBody>
        <CRow>
          <CCol>
            <CButtonGroup role="group">
              <CButton color="primary" shape='rounded-3' onClick={() => newBranch()} > {t("AddNew")}</CButton>
            </CButtonGroup>
          </CCol>
        </CRow>
        <CRow>
          <CCol>

            <MaterialReactTable table={table} />
          </CCol>
        </CRow>
        <CRow>
          {
            saveError != null ?
              <CAlert color="warning">{saveError}</CAlert>
              : ""
          }
        </CRow>
      </CCardBody>
    </CCard>

  </>
  )
}

export default BranchDefination;


