import React, { useEffect, useContext, useState } from 'react'

import {
  CAvatar,
  CButton,
  CButtonGroup,
  CCard,
  CCardBody,
  CCardFooter,
  CCardHeader,
  CCol,
  CProgress,
  CRow,
  CTable,
  CTableBody,
  CTableDataCell,
  CTableHead,
  CTableHeaderCell,
  CTableRow,
} from '@coreui/react'
import { CChartLine } from '@coreui/react-chartjs'
import { getStyle, hexToRgba } from '@coreui/utils'
import CIcon from '@coreui/icons-react'
import IconButton from '@mui/material/IconButton';
import EditIcon from '@mui/icons-material/Edit';
import Download from '@mui/icons-material/Download';
import { Link } from 'react-router-dom';

import {
  cibCcAmex,
  cibCcApplePay,
  cibCcMastercard,
  cibCcPaypal,
  cibCcStripe,
  cibCcVisa,
  cibGoogle,
  cibFacebook,
  cibLinkedin,
  cifBr,
  cifEs,
  cifFr,
  cifIn,
  cifPl,
  cifUs,
  cibTwitter,
  cilCloudDownload,
  cilPeople,
  cilUser,
  cilUserFemale,
} from '@coreui/icons'

import avatar1 from "../../assets/images/avatars/1.jpg"
import avatar2 from '../../assets/images/avatars/2.jpg'
import avatar3 from '../../assets/images/avatars/3.jpg'
import avatar4 from '../../assets/images/avatars/4.jpg'
import avatar5 from '../../assets/images/avatars/5.jpg'
import avatar6 from '../../assets/images/avatars/6.jpg'
import { GetDashBoard, GetLastForms } from '../../lib/dashboardapi.jsx'
import GridColumsDigitalForm from "../digitalForms/GridColumsDigitalForm.jsx";
import { DataGrid } from '@mui/x-data-grid';

import AiChatPanel from './AiChatPanel.jsx'

import FormActionModal from "../digitalForms/FormActionModal.jsx";
import { useNavigate } from "react-router-dom";
import { UrlContext } from '../../lib/URLContext.jsx';

import { useTranslation } from "react-i18next";
const Dashboard = () => {

  const [dasboardDocumentsCount, setdasboardDocumentsCount] = useState(null);
  const [lastDocumentsList, setlastDocumentsList] = useState([]);

  const [formActionModal, setFormActionModal] = useState(false);
  const [formCopyModal, setformCopyModal] = useState(false);
  const [formDeleteModal, setformDeleteModal] = useState(false);

  const [selectedFormId, setSelectedFormId] = useState(null);

  const navigate = useNavigate();
  const { t } = useTranslation();

  //Bu sekilde redux tan okunacak
  //const userToken= useSelector(state=> state.userToken);

  const { dispatch } = useContext(UrlContext);

  function SetLocationAdress() {

    dispatch({ type: 'reset' })
    /*
        dispatch({
          type: 'Add',
          payload: { pathname: "#/dashboard", name: t("Home"), active: true }
        });
        */
  }

  async function LoadDasBoardData() {

    try {
      var dasBoardReturn = await GetDashBoard();

      if (dasBoardReturn.returnCode === 1) {
        setdasboardDocumentsCount(dasBoardReturn.data);
      } else {
        setsaveError(dasBoardReturn.returnMessage);
      }
    } catch (error) {
      setsaveError(error.message);

    }
  }

  async function LoadLastForms() {
    try {
      var lastFormsReturn = await GetLastForms();
      if (lastFormsReturn.returnCode === 1) {
        setlastDocumentsList(lastFormsReturn.data);
      } else {
        setsaveError(lastFormsReturn.returnMessage);
      }
    } catch (error) {
      setsaveError(error.message);
    }
  }

  useEffect(() => {
    SetLocationAdress();
    LoadDasBoardData();
    LoadLastForms();
  }, []);

  const optionClick = (option, id) => {
    setSelectedFormId(id);
    if (option === "Download") {
      setFormActionModal(true);
    } else if (option === "Copy") {
      setformCopyModal(true);
    } else if (option === "Delete") {
      setformDeleteModal(true);
    }
  }

  return (
    <>

      <CRow>
        <CCol xs>
          <CCard className="mb-4">
            <CCardHeader>{t("DasBoardDocumentCounts")}</CCardHeader>
            <CCardBody>
              <CRow>


                <CCol sm={3}>
                  <div className="border-start border-start-4 border-start-info py-1 px-3">
                    <div className="text-medium-emphasis small">{t("DayDocumentCount")}</div>
                    <div className="fs-5 fw-semibold">{dasboardDocumentsCount?.dayCount}</div>
                  </div>
                </CCol>
                <CCol sm={3}>
                  <div className="border-start border-start-4 border-start-danger py-1 px-3 mb-3">
                    <div className="text-medium-emphasis small">{t("WeekDocumentCount")}</div>
                    <div className="fs-5 fw-semibold">{dasboardDocumentsCount?.weekCount}</div>
                  </div>
                </CCol>

                <CCol sm={3}>
                  <div className="border-start border-start-4 border-start-warning py-1 px-3">
                    <div className="text-medium-emphasis small">{t("MonthDocumentCount")}</div>
                    <div className="fs-5 fw-semibold">{dasboardDocumentsCount?.mountCount}</div>
                  </div>
                </CCol>
                <CCol sm={3}>
                  <div className="border-start border-start-4 border-start-success py-1 px-3 mb-3">
                    <div className="text-medium-emphasis small">{t("YearDocumentCount")}</div>
                    <div className="fs-5 fw-semibold">{dasboardDocumentsCount?.yearCount}</div>
                  </div>
                </CCol>



              </CRow>

              <br />



              <CTable align="middle" className="mb-0 border" hover responsive>
                <CTableHead color="light">
                  <CTableRow>

                    <CTableHeaderCell className='text-left'>{t("User")}</CTableHeaderCell>
                    <CTableHeaderCell className='text-left'>{t("BranchName")}</CTableHeaderCell>
                    <CTableHeaderCell className="text-left">{t("FormDefinationType")}</CTableHeaderCell>
                    <CTableHeaderCell className="text-left">{t("SenderCompany")}</CTableHeaderCell>
                    <CTableHeaderCell className="text-left">{t("ReceiverCompany")}</CTableHeaderCell>
                    <CTableHeaderCell className="text-left">{t("Actions")}</CTableHeaderCell>
                  </CTableRow>
                </CTableHead>
                <CTableBody>
                  {lastDocumentsList.map((item, index) => (
                    <CTableRow v-for="item in tableItems" key={index}>
                      <CTableDataCell>
                        <div>{item.createdBy}</div>
                      </CTableDataCell>

                      <CTableDataCell>
                        <div>{item.brancName}</div>
                      </CTableDataCell>
                      <CTableDataCell className="text-left">
                        <div>{item.formDefinationName}</div>
                      </CTableDataCell>
                      <CTableDataCell>
                        <div>{item.senderCompanyName}</div>
                      </CTableDataCell>
                      <CTableDataCell className="text-left">
                        <div>{item.recrivedCompanyName}</div>
                      </CTableDataCell>
                      <CTableDataCell className="text-left">
                        <div>

                          <IconButton
                            aria-label="edit"
                            color="primary"
                          >

                            <Link to={{
                              pathname: '/digitalFormEdit',
                              search: '?id=' + item.id,
                            }} >
                              <EditIcon /></Link>
                          </IconButton>

                          <IconButton
                            onClick={() => optionClick('Download', item.id)}
                            aria-label="Download"
                            color="secondary"
                          >
                            <Download />
                          </IconButton>
                        </div>
                      </CTableDataCell>
                    </CTableRow>
                  ))}
                </CTableBody>
              </CTable>
            </CCardBody>
          </CCard>
        </CCol>
      </CRow>

      <FormActionModal foreditForm={false} visiblep={formActionModal} OnClose={() => setFormActionModal(false)} formidp={selectedFormId} ></FormActionModal>

    </>
  )
}

export default Dashboard


