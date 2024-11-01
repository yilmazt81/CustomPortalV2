
import React, { useState, useEffect } from 'react';
import { useTranslation } from "react-i18next";
import { cilSave, cilClearAll, cilPin } from '@coreui/icons';
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

import { cilMap, cilBarcode } from '@coreui/icons';

function CustomeField({ customeFielType, onButtonClick, rowCountP, onChangeData, controlValuesp }) {
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

    function GetDateValue(fieldName) {

        var fieldValue = controlValuesp.find(s => s.fieldName === fieldName);

        return (fieldValue === undefined ? null : dayjs(fieldValue.fieldValue));
    }
    function handleChangeChecked(e) {
        const { name, value } = e.target;

        let checkbox = (e.target.checked ? "true" : "false");

        onChangeData(name, checkbox);
    }

    function CreateAutoComplateText(textField) {


        return (
            <>

                <CCol sm={10} ><CFormInput key={textField.id}
                    type="text"
                    name={textField.tagName}
                    id={`txt${textField.tagName}`}
                    onChange={(e) => handleChange(e)} value={GetControlValue(textField.tagName)} /></CCol>


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

    function handleChange(e) {
     
        //const { name, value } = e.target;


        onChangeData(e);
    }

    function GetControlValue(fieldName) {

        var fieldValue = controlValuesp.find(s => s.fieldName === fieldName);
        return (fieldValue === undefined ? "" : fieldValue.fieldValue);
    }

    function GetControlCheckedValue(fieldName) {

        var fieldValue = controlValuesp.find(s => s.fieldName === fieldName);

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
    function handleChangeDatetime(field, date) {

        var dateStr = moment(date.$d).format('DD-MM-YYYY');
        onChangeData(field, dateStr);
    }

    function CreateText(textField, fieldName) {

        return (
            <CFormInput value={GetControlValue(fieldName)} key={textField.id} name={fieldName} onChange={(e) => handleChange(e)} type="text" id={`txt${fieldName}`} />
        )
    }

    function getFieldControl(item, linenumber) {
        var fieldName = customeFielType +'_'+item.tagName+ '_' + linenumber;
        
        if (item.controlType == "Text") {
            return (
                <CCol key={item.id}>

                    {
                        item.autoComplate === true ? CreateAutoComplateText(item, fieldName) : CreateText(item, fieldName)

                    }


                </CCol>)
        } else if (item.controlType == "DateTime") {
            return (
                <CCol key={item.id}>
                    <LocalizationProvider dateAdapter={AdapterDayjs} adapterLocale="tr">
                        <DatePicker key={item.id} id={`txt${fieldName}`}

                            name={item.tagName}
                            value={GetDateValue(item.tagName)}
                            onChange={date => handleChangeDatetime(item.tagName, date)} />
                    </LocalizationProvider>

                </CCol>)
        } else if (item.controlType == "ComboBox") {

            return <CCol key={item.id}>

                <CFormSelect id={`cmb${fieldName}`} value={GetControlValue(fieldName)} onChange={(e) => handleChange(e)} name={fieldName}>
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
                        <CFormCheck onChange={(e) => handleChangeChecked(e)}
                            name={`${fieldName}_${combo.tagName}`}

                            checked={GetControlCheckedValue(`${item.tagName}_${combo.tagName}`)}

                            inline key={t} id={`chk${fieldName}_${combo.tagName}`} value={combo.tagName} label={combo.name} ></CFormCheck>
                    )
                })}

            </CCol>
        } else if (item.controlType == "RadioBox") {

            return <CCol key={item.id}>



                {item.comboBoxItems.map((combo, t) => {
                    return (
                        <CFormCheck onChange={(e) => handleChangeChecked(e)} name={`${fieldName}_${combo.tagName}`} inline key={t} type='radio' id={`chk${item.tagName}_${combo.tagName}`} value={combo.tagName} label={combo.name} ></CFormCheck>
                    )
                })}

            </CCol>
        } else if (item.controlType == "Hidden") {
            return (
                <CFormInput key={item.id} onChange={(e) => handleChange(e)} name={fieldName} type="Hidden" id={`hdn${fieldName}`} />
            )
        }

    }

    function CreateFormHeader() {
 
        return (
            <CRow className="mb-12" >
                {customeFielditems.map((item, i) => {

                    return (
                        <CCol key={item.id}>
                            <CFormLabel key={i} className="col-form-label">{item.fieldCaption}</CFormLabel>

                        </CCol>
                    )

                })}
            </CRow>
        )
    }
    function CreateFormByLine() {



        for (let index = 0; index < rowCount; index++) {
            return (
                <CRow className="mb-12" >
                    {customeFielditems.map((item, i) => {

                        return getFieldControl(item, index)

                    })}
                </CRow>
            )

        }

    }

    return (
        <>
            {CreateFormHeader()}
            {CreateFormByLine()}

        </>
    );
}

export default CustomeField; 