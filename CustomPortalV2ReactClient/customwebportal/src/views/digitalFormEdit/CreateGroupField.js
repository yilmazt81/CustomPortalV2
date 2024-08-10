import React, { useState } from 'react'

import {
    CCol,
    CRow,
    CFormLabel,
    CFormInput,
    CFormSelect,
    CFormCheck,
    CButton
} from '@coreui/react'
import { DatePicker } from '@mui/x-date-pickers/DatePicker';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import 'dayjs/locale/tr';
import BrowserAdressModal from './ModalForm/AdresModal';
import BrowserProductModal from './ModalForm/productModal';

import { cilMap, cilBarcode } from '@coreui/icons';

import CIcon from '@coreui/icons-react';



const CreateGroupField = ({ fieldList }) => {
    const [autocomplatemodalform, setautocomplatemodalform] = useState(false);
    const [autoComplateModalFormProduct, setautoComplateModalFormProduct] = useState(false);
    const [formdefinationtypeid, setformdefinationtypeid] = useState(0);

    function openModal(modalType, definationTypeid) {

        setautocomplatemodalform(false);
        setautoComplateModalFormProduct(false);
        setformdefinationtypeid(definationTypeid);
        if (modalType == 'CompanyDefination') {
            setautocomplatemodalform(true);
        } else if (modalType == "ProductDefination") {
            setautoComplateModalFormProduct(true);
        } else {

        }
    }

    function GetIcon(modalType) {
        if (modalType == 'CompanyDefination') {
            {
                return (
                    <CIcon icon={cilMap}></CIcon>
                )
            }
        } else if (modalType == "ProductDefination") {
            return (
                <CIcon icon={cilBarcode}></CIcon>
            )

        } else {
            return (
                <></>
            )
        }
    }

    function CreateAutoComplateText(textField) {


        return (
            <>

                <CCol sm={6} ><CFormInput key={textField.id} type="text" id={`txt${textField.tagName}`} /></CCol>


                <CCol sm={3}><CButton color="primary" onClick={() => openModal(textField.autoComlateType, textField.id)}>
                    {GetIcon(textField.autoComlateType)}
                </CButton> </CCol>


            </>
        )
    }
    function CreateText(textField) {

        return (
            <CCol sm={9} ><CFormInput key={textField.id} type="text" id={`txt${textField.tagName}`} /></CCol>
        )
    }

    function LoadControlData(controlDataList) {
        if (controlDataList == null)
            return;
        for (var i = 0; i < controlDataList.length; i++) {
            var item = controlDataList[i];

            const inputElement = document.getElementById(item.controlName);
            if (inputElement == null)
                continue;

            inputElement.value = item.controlValue;

        }
    }

    return (
        <>
            {fieldList?.map((item, i) => {

                if (item.controlType == "Text") {
                    return (
                        <CRow key={item.id} className="mb-12">
                            <CFormLabel htmlFor={`txt${item.tagName}`} className="col-sm-3 col-form-label">{item.caption}</CFormLabel>

                            {
                                item.autoComplate === true ? CreateAutoComplateText(item) : CreateText(item)

                            }


                        </CRow>)
                } else if (item.controlType == "DateTime") {
                    return (
                        <CRow key={item.id} className="mb-12">
                            <CFormLabel htmlFor={`txt${item.tagName}`} className="col-sm-3 col-form-label">{item.caption}</CFormLabel>
                            <CCol sm={9} >
                                <LocalizationProvider dateAdapter={AdapterDayjs} adapterLocale="tr">
                                    <DatePicker key={item.id} id={`txt${item.tagName}`} />
                                </LocalizationProvider>
                            </CCol>
                        </CRow>)
                } else if (item.controlType == "ComboBox") {

                    return <CRow key={item.id} className="mb-12">
                        <CFormLabel htmlFor={`cmb${item.tagName}`} className="col-sm-3 col-form-label">{item.caption}</CFormLabel>
                        <CCol sm={9} >
                            <CFormSelect key={item.id} id={`cmb${item.tagName}`}>
                                <option value="" ></option>
                                {item.comboBoxItems.map((combo, t) => {
                                    return (
                                        <option key={t} value={combo.TagName}>{combo.name}</option>
                                    )
                                })}</CFormSelect>
                        </CCol>
                    </CRow>
                } else if (item.controlType == "CheckBox") {

                    return <CRow key={item.id} className="mb-12">
                        <CCol sm={9} >

                            {item.comboBoxItems.map((combo, t) => {

                                return (
                                    <CFormCheck inline key={t} id={`chk${item.tagName}_${combo.tagName}`} value={combo.tagName} label={combo.name} ></CFormCheck>
                                )
                            })}
                        </CCol>
                    </CRow>
                } else if (item.controlType == "RadioBox") {

                    return <CRow key={item.id} className="mb-12">
                        <CCol sm={9} >


                            {item.comboBoxItems.map((combo, t) => {
                                return (
                                    <CFormCheck inline key={t} type='radio' id={`chk${item.tagName}_${combo.tagName}`} value={combo.tagName} label={combo.name} ></CFormCheck>
                                )
                            })}
                        </CCol>
                    </CRow>
                } else if (item.controlType == "Hidden") {
                    return (
                        <CFormInput key={item.id} type="Hidden" id={`hdn${item.tagName}`} />
                    )
                } else {
                    return (
                        <></>
                    )
                }
            })}

            <BrowserAdressModal visiblep={autocomplatemodalform}
                setClose={() => setautocomplatemodalform(false)}
                setFormData={(e) => LoadControlData(e)}
                formDefinationTypeIdp={formdefinationtypeid} ></BrowserAdressModal>

            <BrowserProductModal visiblep={autoComplateModalFormProduct}
                setFormData={(e) => LoadControlData(e)}
                formDefinationTypeIdp={formdefinationtypeid}
                setClose={() => setautoComplateModalFormProduct(false)}
            >
            </BrowserProductModal>
        </>


    );
}

export default CreateGroupField;