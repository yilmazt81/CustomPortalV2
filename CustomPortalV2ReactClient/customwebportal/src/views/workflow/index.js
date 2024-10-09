
import React, { useEffect, useContext, useState } from 'react'

import { useTranslation } from "react-i18next";
import { UrlContext } from 'src/lib/URLContext';
import { GetBranchWorkFlows } from 'src/lib/workflowApi';
import WorkFlowGrid from './workFlowGrid';
import DeleteModal from 'src/components/DeleteModal';
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



  const gridColumns = WorkFlowGrid(optionClick);





  return (
    <>
      <CCard className="mb-4">
        <CCardBody>
          <CRow>
            <CCol>
              <CButtonGroup role="group">


                <CButton color="primary" shape='rounded-3'   > {t("AddNewFormDefination")}</CButton>

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