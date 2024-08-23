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
import moment from 'moment';
import 'dayjs/locale/tr';
import dayjs from 'dayjs';


import PropTypes from 'prop-types';

const CreateGroupField = ({ fieldList, onChangeData ,controlValuesp}) => {
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

    function GetControlValue(fieldName) {

        var fieldValue = controlValuesp.find(s => s.fieldName === fieldName);
        return (fieldValue === undefined ? "" : fieldValue.fieldValue);
    }

    function GetControlCheckedValue(fieldName) {

        var fieldValue = controlValuesp.find(s => s.fieldName === fieldName);
    
        return (fieldValue === undefined ? false :  fieldValue.fieldValue==='true');
    }

    function GetDateValue(fieldName) {

        var fieldValue = controlValuesp.find(s => s.fieldName === fieldName);

        return (fieldValue === undefined ? null : dayjs(fieldValue.fieldValue));
    }
    


    function handleChange(e) {
        const { name, value } = e.target;
    
        onChangeData(name, value);
    }

    function handleChangeChecked(e) {
        const { name, value } = e.target;
        
        let checkbox = (e.target.checked?"true":"false");

        onChangeData(name, checkbox);
    }

    function handleChangeDatetime(field, date) {
        
        var dateStr = moment(date.$d).format('DD-MM-YYYY');
        onChangeData(field, dateStr);
    }

    function CreateAutoComplateText(textField) {


        return (
            <>

                <CCol sm={6} ><CFormInput key={textField.id}
                    type="text"
                    name={textField.tagName}
                    id={`txt${textField.tagName}`}
                    onChange={(e) => handleChange(e)}  value={GetControlValue(textField.tagName)} /></CCol>


                <CCol sm={3}><CButton color="primary" onClick={() => openModal(textField.autoComlateType, textField.id)}>
                    {GetIcon(textField.autoComlateType)}
                </CButton> </CCol>


            </>
        )
    }
    function CreateText(textField) {

        return (
            <CCol sm={9} ><CFormInput  value={GetControlValue(textField.tagName)} key={textField.id} name={textField.tagName} onChange={(e) => handleChange(e)} type="text" id={`txt${textField.tagName}`} /></CCol>
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
            onChangeData(item.fieldName,  inputElement.value);
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
                                    <DatePicker key={item.id} id={`txt${item.tagName}`}
                                    
                                    name={item.tagName}
                                    value={GetDateValue(item.tagName)}
                                     onChange={date => handleChangeDatetime(item.tagName, date)} />
                                </LocalizationProvider>
                            </CCol>
                        </CRow>)
                } else if (item.controlType == "ComboBox") {

                    return <CRow key={item.id} className="mb-12">
                        <CFormLabel htmlFor={`cmb${item.tagName}`} className="col-sm-3 col-form-label">{item.caption}</CFormLabel>
                        <CCol sm={9} >
                            <CFormSelect   id={`cmb${item.tagName}`} value={GetControlValue(item.tagName)} onChange={(e)=>handleChange(e)} name={item.tagName}>
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
                                    <CFormCheck onChange={(e) => handleChangeChecked(e)}
                                     name={`${item.tagName}_${combo.tagName}`}

                                     checked={GetControlCheckedValue(`${item.tagName}_${combo.tagName}`)}
                                     
                                     inline key={t} id={`chk${item.tagName}_${combo.tagName}`} value={combo.tagName} label={combo.name} ></CFormCheck>
                                )
                            })}
                        </CCol>
                    </CRow>
                } else if (item.controlType == "RadioBox") {

                    return <CRow key={item.id} className="mb-12">
                        <CCol sm={9} >


                            {item.comboBoxItems.map((combo, t) => {
                                return (
                                    <CFormCheck   onChange={(e) => handleChangeChecked(e)} name={`${item.tagName}_${combo.tagName}`} inline key={t} type='radio' id={`chk${item.tagName}_${combo.tagName}`} value={combo.tagName} label={combo.name} ></CFormCheck>
                                )
                            })}
                        </CCol>
                    </CRow>
                } else if (item.controlType == "Hidden") {
                    return (
                        <CFormInput key={item.id} onChange={(e) => handleChange(e)} name={item.tagName} type="Hidden" id={`hdn${item.tagName}`} />
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


CreateGroupField.propTypes = { 
    controlValuesp: PropTypes.array,
}