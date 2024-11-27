
import React, { useState, useEffect } from 'react';
import { useTranslation } from "react-i18next";
import { cilSave, cilClearAll, cilPin, cilNoteAdd } from '@coreui/icons';
import { GetCustomeFieldByName } from 'src/lib/formdef';
import { CIcon } from '@coreui/icons-react';
import {
    CCol,
    CRow,
    CFormLabel,
    CFormInput,
    CFormSelect,
    CFormCheck,
    CButton
} from '@coreui/react'

import moment from 'moment';
import 'dayjs/locale/tr';
import dayjs from 'dayjs';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';

import { DatePicker } from '@mui/x-date-pickers/DatePicker';

import { cilMap, cilBarcode, cilPlus, cilDelete } from '@coreui/icons';

function CustomeField({ customeFielType, rowCountP, onChangeDataCustomeField, controlValuesp }) {
    const { t } = useTranslation();
    const [customeFielditems, setcustomeFielditems] = useState([]);
    const [saveError, setSaveError] = useState(null);
    const [Loading, setLoading] = useState(false);
    const [rowCount, setRowCount] = useState(1);

    useEffect(() => {
        setRowCount(rowCountP);
        getFieldItems();

    }, [customeFielType, rowCountP]);

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

    function GetDateValue(fieldName,lineNumber) {
 
        var fieldValue = controlValuesp.find(s => s.fieldName === fieldName && s.fieldOrder===lineNumber);

        return (fieldValue === undefined ? null : dayjs(fieldValue.fieldValue));
    }
    function handleChangeChecked(e,lineNumber) {
        const { name, value } = e.target;

        let checkbox = (e.target.checked ? "true" : "false");

        onChangeDataCustomeField(name, checkbox,lineNumber);
    }

    function CreateAutoComplateText(textField,lineNumber) {


        return (
            <>

                <CCol sm={10} ><CFormInput key={textField.id}
                    type="text"
                    name={textField.tagName}
                    id={`txt${textField.tagName}`}
                    onChange={(e) => handleChange(e,lineNumber)} value={GetControlValue(textField.tagName,lineNumber)} /></CCol>


                <CCol sm={2}><CButton color="primary" onClick={() => openModal(textField.autoComlateType, textField.id)}>
                    {GetIcon(textField.autoComlateType)}
                </CButton> </CCol>


            </>
        )
    }


    function openModal(modalType, definationTypeid) {

        /*
        setautocomplatemodalform(false);
        setautoComplateModalFormProduct(false);
        setformdefinationtypeid(definationTypeid);
        if (modalType == 'CompanyDefination') {
            setautocomplatemodalform(true);
        } else if (modalType == "ProductDefination") {
            setautoComplateModalFormProduct(true);
        } else {

        }*/
    }

    function handleChange(e,lineNumber) {

         const { name, value } = e.target;
 
        onChangeDataCustomeField(name,value,lineNumber);
    }

    function GetControlValue(fieldName,lineNumber) {

        var fieldValue = controlValuesp.find(s => s.fieldName === fieldName && s.fieldOrder===lineNumber);
        return (fieldValue === undefined ? "" : fieldValue.fieldValue);
    }

    function GetControlCheckedValue(fieldName,lineNumber ) {

        var fieldValue = controlValuesp.find(s => s.fieldName === fieldName & s.fieldOrder===lineNumber);

        return (fieldValue === undefined ? false : fieldValue.fieldValue === 'true');
    }

    async function getFieldItems() {

        try {
            setLoading(true);

            setSaveError(null);
            var fcustomeFieldTypeItems = await GetCustomeFieldByName(customeFielType);
            if (fcustomeFieldTypeItems.returnCode === 1) {
                setcustomeFielditems(fcustomeFieldTypeItems.data);
            } else {
                setSaveError(fcustomeFieldTypeItems.returnMessage);
            }
        } catch (error) {
            setSaveError(error.message);

        } finally {
            setLoading(false);
        }
    }
    function handleChangeDatetime(field, date,lineNumber) {

        var dateStr = moment(date.$d).format('DD-MM-YYYY');
        onChangeDataCustomeField(field, dateStr,lineNumber);
    }

    function CreateText(textField, fieldName,lineNumber) {

        return (
            <CFormInput value={GetControlValue(textField.tagName,lineNumber)} 
            key={textField.id} 
            name={textField.tagName} 
            onChange={(e) => handleChange(e,lineNumber)} 
            type="text" 
            id={`txt${fieldName}`} />
        )
    }

    function getFieldControl(item, linenumber) {
        var fieldName = customeFielType + '_' + item.tagName + '_' + linenumber;

        if (item.controlType == "Text") {
            return (
                <CCol key={item.id}>

                    {
                        item.autoComplate === true ? CreateAutoComplateText(item, fieldName,linenumber) : CreateText(item, fieldName,linenumber)

                    }


                </CCol>)
        } else if (item.controlType == "DateTime") {
            return (
                <CCol key={item.id}>
                    <LocalizationProvider dateAdapter={AdapterDayjs} adapterLocale="tr">
                        <DatePicker key={item.id} id={`txt${fieldName}`}

                            name={item.tagName}
                            value={GetDateValue(item.tagName,linenumber)}
                            onChange={date => handleChangeDatetime(item.tagName, date,linenumber)} />
                    </LocalizationProvider>

                </CCol>)
        } else if (item.controlType == "ComboBox") {

            return <CCol key={item.id}>

                <CFormSelect id={`cmb${fieldName}`}
                 value={GetControlValue(item.tagName,linenumber)} onChange={(e) => handleChange(e,linenumber)} name={item.tagName}>
                    <option value="" ></option>
                    {item.comboBoxItems.map((combo, t) => {
                        return (
                            <option key={t} value={combo.TagName}>{combo.name}</option>
                        )
                    })}</CFormSelect>

            </CCol>
        } else if (item.controlType == "CheckBox") {

            return <CCol key={item.id}>

                {item.comboBoxItems.map((combo, t) => {

                    return (
                        <CFormCheck onChange={(e) => handleChangeChecked(e,linenumber)}
                            name={`${item.tagName}_${combo.tagName}`}
                            checked={GetControlCheckedValue(`${item.tagName}_${combo.tagName}`,linenumber)}
                            inline key={t} id={`chk${fieldName}_${combo.tagName}`} value={combo.tagName} label={combo.name} ></CFormCheck>
                    )
                })}

            </CCol>
        } else if (item.controlType == "RadioBox") {

            return <CCol key={item.id}>



                {item.comboBoxItems.map((combo, t) => {
                    return (
                        <CFormCheck 
                        onChange={(e) => handleChangeChecked(e,linenumber)} 
                        name={`${item.tagName}_${combo.tagName}`} 
                        inline key={t} type='radio'
                         id={`chk${item.tagName}_${combo.tagName}`} 
                         value={combo.tagName} label={combo.name} ></CFormCheck>
                    )
                })}

            </CCol>
        } else if (item.controlType == "Hidden") {
            return (
                <CFormInput key={item.id} 
                onChange={(e) => handleChange(e,linenumber)} 
                name={item.tagName} 
                type="Hidden" 
                id={`hdn${fieldName}`} 
                value={GetDateValue(item.tagName,linenumber)}
                />
            )
        }

    }

    function CreateFormHeader() {

        return (
            <CRow className="mb-12" >
                {customeFielditems.map((item, i) => {

                    return (
                        <>
                            <CCol key={item.id}>
                                <CFormLabel key={i} className="col-form-label">{item.fieldCaption}</CFormLabel>
                            </CCol>
                        </>
                    )

                })}

                <CCol>
                </CCol>
            </CRow>
        )
    }

    function AddRowCount(a) {
        var newRow = 0;
        if (a===false) {

            newRow = rowCount - 1;
        } else {
            newRow = rowCount + 1;
        }


        setRowCount(newRow);
        CreateFormByLine();

    }

    function CreateOneLine(index) {
        return (
            <CRow className="mb-12" >
                {customeFielditems.map((item, i) => {

                    return getFieldControl(item, index)

                })}
                <CCol>

                    <CButton color="primary" onClick={() => AddRowCount((rowCount) === index)}  >
                        <CIcon icon={(rowCount) === index ? cilPlus : cilDelete} ></CIcon>
                    </CButton>
                </CCol>
            </CRow>
        )
    }
    function CreateFormByLine() {

        var items = [];

        for (let index = 1; index <= rowCount; index++) {

            items.push(CreateOneLine(index));

        }

        return items;

    }

    return (
        <>
            {CreateFormHeader()}
            {CreateFormByLine()}

        </>
    );
}

export default CustomeField; 