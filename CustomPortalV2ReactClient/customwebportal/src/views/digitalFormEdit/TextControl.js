import React, {  } from 'react'

import {
    CCol,
    CRow,
    CFormLabel,
    CFormInput

} from '@coreui/react'

export default function CreateTextField({fieldInfo}) {
  return (
    <CRow className="mb-12">
      <CFormLabel htmlFor={`txt=${fieldInfo.tagName}`} className="col-sm-3 col-form-label">{fieldInfo.fieldCaption}</CFormLabel>
      <CCol sm={9}>
        <CFormInput type="text" id={`txt=${fieldInfo.tagName}`} />
      </CCol>
    </CRow>
  );
}