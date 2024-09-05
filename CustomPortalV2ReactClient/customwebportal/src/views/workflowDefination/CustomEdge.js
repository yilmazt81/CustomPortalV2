
import React, { useCallback, useState } from "react";
import {
  BaseEdge,
  EdgeLabelRenderer,
  getStraightPath,
  useReactFlow,
} from '@xyflow/react';

import {
  CButton,
  CButtonGroup

} from '@coreui/react'

import {
  FaCog, FaMinus
} from "react-icons/fa";

export default function CustomEdge({ id, sourceX, sourceY, targetX, targetY, data }) {
  const { setEdges } = useReactFlow();
  const [edgePath, labelX, labelY] = getStraightPath({
    sourceX,
    sourceY,
    targetX,
    targetY,
  });

  return (
    <>
      <BaseEdge id={id} path={edgePath} />
      <EdgeLabelRenderer>
        <CButton color="danger" variant="ghost" size="sm" shape="rounded-pill" style={{
          position: 'absolute',
          transform: `translate(-50%, -50%) translate(${labelX}px,${labelY}px)`,
          pointerEvents: 'all',
        }} onClick={() => {
          setEdges((es) => es.filter((e) => e.id !== id));
        }}>
          <FaMinus />
        </CButton>

      </EdgeLabelRenderer>
    </>
  );
}