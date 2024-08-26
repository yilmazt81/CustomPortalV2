
import React, { useCallback,useState } from "react";

import {
    ReactFlow,
    Controls,
    Background,
    applyEdgeChanges,
    applyNodeChanges,
    Handle, Position,
    addEdge
} from '@xyflow/react';

import '@xyflow/react/dist/style.css';

import {
    CButton,
    CButtonGroup,
    CCard,
    CCardBody,
    CCol,
    CAlert,
    CRow,

} from '@coreui/react'
import TextUpdaterNode from './TextUpdaterNode.js';
import CustomEdge from './CustomEdge';

import './text-updater-node.css';
const initialNodes = [
    {
        id: 'node-0',
        type: 'textUpdater',
        position: { x: 0, y: 0 },
        data: { value: '123a' }, 
      },
]; 

const WorkflowDefination = () => {

 

    const nodeTypes = { textUpdater: TextUpdaterNode };
    const [nodes, setNodes] = useState(initialNodes);
    const [edges, setEdges] = useState([]);

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
          const edge = { ...connection, type: 'custom-edge' ,animated:true,data:{color:"red"}};
          setEdges((eds) => addEdge(edge, eds));
        },
        [setEdges],
      );

      function AddNode(){ 
        var nodeCount=nodes.length;
        var newItem = {
            id: `node-${nodeCount}`,
            type: 'textUpdater',
            position: { x: 1, y: 2 },
            data: { value: '123b' }, 
          };
        nodes.push(newItem);
        setNodes([...nodes]);
      }
    return (

        <>
            <CButton  onClick={()=>AddNode()}>Add</CButton>
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

        </>
    )
}

export default WorkflowDefination;


