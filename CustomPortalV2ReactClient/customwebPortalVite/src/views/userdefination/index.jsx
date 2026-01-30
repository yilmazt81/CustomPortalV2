import React, { useEffect, useState, useContext } from 'react'

import {
  CButton,
  CButtonGroup,
  CCard,
  CCardBody,
  CCol,
  CAlert,
  CRow,

} from '@coreui/react'

import { GetUserList, GetUser, CreateNewUser, DeleteUser } from '../../lib/userapi.jsx';

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
import { Gridcolumns } from './DataGrid';

import { useTranslation } from "react-i18next";

import EditModal from './editmodal';
import DeleteModal from "../../components/DeleteModal.jsx";
import { UrlContext } from "../../lib/URLContext.jsx";

const UserDefination = () => {
  const { t } = useTranslation();
  const { dispatch } = useContext(UrlContext);

  //Bu sekilde redux tan okunacak 

  const [useredit, setuseredit] = useState({ id: 0 });
  const [users, setUsers] = useState([]);
  const [error, setError] = useState(null);
  const [saveError, setSaveError] = useState(null);
  const [deleteStart, setDeleteStart] = useState(null);

  const [visible, setVisible] = useState(false);
  const [visibleDelete, setVisibleDelete] = useState(false);
  async function LoadUserList() {

    try {
      var userList = await GetUserList();
      if (userList.returnCode === 1) {
        setUsers(userList.data);
      } else {
        setError(userList.ReturnMessage);
      }
    } catch (error) {
      setError(error.message);
    }
  }

  async function NewUser() {
    try {
      setVisible(false);
      var editUser = await CreateNewUser();

      if (editUser.returnCode === 1) {
        setuseredit(editUser.data);
        setVisible(true);
      } else {
        setSaveError(editUser.returnMessage);
      }
    } catch (error) {
      setSaveError(error.message);
    } finally {
      // setdeleteStart(false);
    }
  }

  async function EditData(row) {
    var id = row.original["id"];

    try {
      setDeleteStart(false);
      setVisible(false);
      var getuserReturn = await GetUser(id);

      if (getuserReturn.returnCode === 1) {
        setuseredit(getuserReturn.data);
        setVisible(true);
      } else {
        setSaveError(getuserReturn.returnMessage);
      }
    } catch (error) {
      setSaveError(error.message);
    } finally {
      // setdeleteStart(false);
    }

  }

  async function DeleteData(row) {
    var id = row.original["id"];

    try {
      setVisibleDelete(false);
      setVisible(false);
      var editUser = await GetUser(id);

      if (editUser.returnCode === 1) {
        setuseredit(editUser.data);
        setVisibleDelete(true);
      } else {
        setSaveError(editUser.returnMessage);
      }
    } catch (error) {
      setVisibleDelete(error.message);
    } finally {
      setDeleteStart(false);
    }

  }

  async function DeleteAccepted(data) {
    setDeleteStart(true);

    try {
      if (data == null)
        return;
      var deleteUserReturn = await DeleteUser(useredit.id);

      if (deleteUserReturn.returnCode === 1) {
        LoadUserList();
        setVisibleDelete(false);
      } else {
        setSaveError(deleteUserReturn.returnMessage);
      }
    } catch (error) {
      setVisibleDelete(error.message);
    } finally {
      setDeleteStart(false);
    }

    setDeleteStart(false);
  }

  function SetLocationAdress() {

    dispatch({ type: 'reset' })

    dispatch({
      type: 'Add',
      payload: { pathname: "./userdefination", name: t("Userdefination"), active: false }
    });
  }


  useEffect(() => {

    try {
      SetLocationAdress();
      LoadUserList();


    } catch (error) {
      console.log(error);
    }

  }, []);


  const table = useMaterialReactTable({
    columns: Gridcolumns,
    data: users,
    enableRowActions: true,
    layoutMode: "grid",
    positionActionsColumn: 'last',
    renderRowActions: ({ row }) => (
      <Box sx={{ display: 'flex', flexWrap: 'nowrap', gap: '3px' }}>
        <IconButton onClick={() => EditData(row)}>
          <EditIcon />
        </IconButton>
        <IconButton onClick={() => DeleteData(row)}>
          <DeleteIcon />
        </IconButton>

      </Box>
    ),

  });

  return (
    <>

      <CCard className="mb-4">
        <CCardBody>
          <CRow>
            <CCol>
              <CButtonGroup role="group">
                <CButton color="primary" shape='rounded-3' onClick={() => NewUser()} > {t("AddNew")}</CButton>
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

      <EditModal userp={useredit} visiblep={visible} setFormData={() => LoadUserList()}></EditModal>
      <DeleteModal visiblep={visibleDelete}
        OnClickOk={(data) => DeleteAccepted(data)}
        title={t("UserDelete")}
        message={useredit.fullName}
        message2={t("UserDeleteMessage")} saveError={error} saveStart={deleteStart}></DeleteModal>
    </>
  )
}

export default UserDefination;




