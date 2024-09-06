
import React, { useCallback, useState } from "react";

import {
  ReactFlow,
  Controls,
  Background,
  applyEdgeChanges,
  applyNodeChanges,
  addEdge
} from '@xyflow/react';

import '@xyflow/react/dist/style.css';

import {
  CButton,
  CButtonGroup,
  CCard,
  CCardBody,
  CCol,
  CRow,
  CFormLabel,
  CFormInput,
  CFormSelect,
  CCardHeader


} from '@coreui/react'


import TextUpdaterNode from './TextUpdaterNode.js';
import FTPNode from "./FTP/index.js";
import WhatsappNode from "./Whatsapp/index.js";
import EmailNode from "./Email/index.js";
import CustomEdge from './CustomEdge';
import OCRProcessNode from "./OCRProcess/index.js";
import BarcodeProcessNode from "./BarcodeReadProcess/index.js";
import ClassificationProcessNode from './ClassificationProcess/index.js';


import {
  FaWhatsapp,
  FaInbox,
  FaCloudDownloadAlt,
  FaCheck,
  FaExclamationCircle,
  FaTextWidth,
  FaBarcode,
  FaRandom,
  FaMailBulk,
} from "react-icons/fa";

import { useTranslation } from "react-i18next";
const initialNodes = [];

const WorkflowDefination = () => {

  const { t } = useTranslation();


  const nodeTypes = {
    textUpdater: TextUpdaterNode,
    ftpNode: FTPNode,
    whatsappNode: WhatsappNode,
    emailNode: EmailNode,
    ocrProcess: OCRProcessNode,
    barcodeReadProcess: BarcodeProcessNode,
    documentClassificationProcess: ClassificationProcessNode
  };
  const [nodes, setNodes] = useState(initialNodes);
  const [edges, setEdges] = useState([]);
  const [workFlow, setWorkFlow] = useState({ flowType: "Process" });

  const edgeTypes = {
    'custom-edge': CustomEdge,
  };
  const onNodesChange = useCallback(
    (changes) => setNodes((nds) => applyNodeChanges(changes, nds)),
    [nodes],
  );
  const onEdgesChange = useCallback(
    (changes) => setEdges((eds) => applyEdgeChanges(changes, eds)),
    [edges],
  );
  /*
  const onConnect = useCallback(
 
    (connection) => setEdges((eds) => addEdge(connection, eds)),
    [setEdges],
  );*/

  const onConnect = useCallback(
    (connection) => {
      const edge = { ...connection, type: 'custom-edge', animated: true, data: { color: "red" } };
      setEdges((eds) => addEdge(edge, eds));
    },
    [setEdges],
  );

  function AddNode(nodeType) {
    var nodeCount = nodes.length;
    var newItem = {
      id: `node-${nodeCount}`,
      type: nodeType,
      position: { x: 1, y: 2 },
      data: { value: '123b' },
    };
    nodes.push(newItem);
    setNodes([...nodes]);
  }

  function WorkFlowTypeChange(event) {
    const { name, value } = event.target;
    setWorkFlow({ ...workFlow, [name]: value });
  }


  function GetForkFlowTypeOptions() {

    if (workFlow.flowType === "Business") {

      return (
        <>
          <CButtonGroup role="group">
            <CButton color="primary" variant="outline"> <FaCheck />{t("WFAcceptUser")}</CButton>
            <CButton color="primary" variant="outline"><FaExclamationCircle /> {t("WFRejectUser")}</CButton>
            <CButton color="primary" variant="outline"><FaMailBulk /> {t("WFSendMail")}</CButton>
          </CButtonGroup>
        </>
      )

    } else {


      return (
        <>
          <CButtonGroup role="group">
            <CButton color="primary" variant="outline" onClick={() => AddNode("ftpNode")}>
              <FaCloudDownloadAlt></FaCloudDownloadAlt> {t("WFFTP")}</CButton>

            <CButton color="primary" variant="outline" onClick={() => AddNode("whatsappNode")}> <FaWhatsapp />   {t("WFWhatsapp")}</CButton>

            <CButton color="primary" variant="outline" onClick={() => AddNode("emailNode")}>
              <FaInbox />
              {t("WFMail")}</CButton>
            <CButton color="primary" variant="outline" onClick={() => AddNode('ocrProcess')}>
              <FaTextWidth />
              {t("WFOCR")}</CButton>
            <CButton color="primary" variant="outline" onClick={() => AddNode("barcodeReadProcess")}>
              <FaBarcode />
              {t("WFBarcodeReader")}</CButton>

            <CButton color="primary" variant="outline" onClick={() => AddNode("documentClassificationProcess")}>
              <FaRandom />
              {t("WFClassification")}</CButton>

            <CButton color="primary" variant="outline"> <FaCheck />{t("WFAcceptUser")}</CButton>
            <CButton color="primary" variant="outline"><FaExclamationCircle /> {t("WFRejectUser")}</CButton>
          </CButtonGroup>
        </>
      )
    }

  }



  return (

    <><CCard>
      <CCardHeader>
        {t("WFTitle")}
      </CCardHeader>
      <CCardBody>
        <CRow className="mb-12">
          <CFormLabel htmlFor="selectFlowType" className="col-sm-3 col-form-label">{t("WorkFlowType")}</CFormLabel>
          <CCol sm={3}>
            <CFormSelect id='selectFlowType' name="flowType"
              onChange={e => WorkFlowTypeChange(e)} value={workFlow?.flowType} >
              <option value="Process">{t("WFProcess")}</option>
              <option value="Business">{t("WFBusiness")}</option>
            </CFormSelect>
          </CCol>
          <CFormLabel htmlFor="txtWorkFlowName" className="col-sm-3 col-form-label">{t("WorkFlowName")}</CFormLabel>
          <CCol sm={3}>
            <CFormInput type="text" value={workFlow?.workFlowName} name="WorkFlowName"></CFormInput>
          </CCol>
        </CRow>

        <CRow>
          {GetForkFlowTypeOptions()}
        </CRow>

        <CRow>
          <div style={{ width: '70vw', height: '70vh' }}>
            <ReactFlow
              nodes={nodes}
              edges={edges}
              onNodesChange={onNodesChange}
              onEdgesChange={onEdgesChange}
              onConnect={onConnect}
              nodeTypes={nodeTypes}
              edgeTypes={edgeTypes}
              fitView
            >
              <Background />
              <Controls />

            </ReactFlow>
          </div>
        </CRow>

      </CCardBody>

    </CCard>


    </>
  )
}

export default WorkflowDefination;


