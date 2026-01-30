
import React, { useEffect, useContext, useState } from 'react'

import { useTranslation } from "react-i18next";
import { UrlContext } from '../../lib/URLContext.jsx';
import { GetBranchWorkFlows } from '../../lib/workflowApi.jsx';
import WorkFlowGrid from './workFlowGrid';
import DeleteModal from '../../components/DeleteModal.jsx';
import { DataGrid } from '@mui/x-data-grid'

import {
  CButton,
  CButtonGroup,
  CCard,
  CCardBody,
  CCol,
  CRow,
  CAlert, 
} from '@coreui/react'


import { Link, useNavigate } from "react-router-dom";

const WorkFlow = () => {
  const { t } = useTranslation();
  const { dispatch } = useContext(UrlContext);
  const [workflow, setworkflow] = useState(null);
  const [visibleDelete, setVisibleDelete] = useState(false);
  const [workflowList, setworkflowList] = useState([]);
  const [saveError, setsaveError] = useState(null);


  const optionClick = (option, id) => {
    // EditGroupDefination(option === 'Delete', id);
  }

  function SetLocationAdress() {

    dispatch({ type: 'reset' })

    dispatch({
      type: 'Add',
      payload: { pathname: "./workflow", name: t("Workflows"), active: false }
    });
  }

  async function LoadWorkFlowList() {

    try {
      var workFlowListReturn = await GetBranchWorkFlows();
      if (workFlowListReturn.returnCode === 1) {
        setworkflowList(workFlowListReturn.data);
      } else {
        setsaveError(workFlowListReturn.ReturnMessage);
      }
    } catch (error) {
      setsaveError(error.message);
    }
  }
  useEffect(() => {

    try {
      SetLocationAdress();
      LoadWorkFlowList();


    } catch (error) {
      console.log(error);
    }

  }, []);

 
  const gridColumns = WorkFlowGrid(optionClick);
 

  return (
    <>
      <CCard className="mb-4">
        <CCardBody>
          <CRow>
            <CCol>
              <CButtonGroup role="group"> 
              <Link to={{
                  pathname: '/workflowDefination',
                  search: '?id=0',

                }}> 

                <CButton color="primary" shape='rounded-3'   > {t("AddNewFormDefination")}</CButton> 
                </Link>
              </CButtonGroup>
            </CCol>
          </CRow>

          <CRow>

            <CCol>

              <DataGrid rows={workflowList}
                columns={gridColumns}
                slotProps={{
                  toolbar: {
                    showQuickFilter: true,
                  },
                }}
              />

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




      <DeleteModal
        setClose={() => setVisibleDelete(false)}
        message={workflow?.workFlowName}
        title={t("ModalDeleteProductTitle")}
        visiblep={visibleDelete}
        message2={t("AutoComplateMapDeleteMessage")}
      // OnClickOk={() => DeleteProductDB()}
      >


      </DeleteModal>
    </>
  )

}

export default WorkFlow;


