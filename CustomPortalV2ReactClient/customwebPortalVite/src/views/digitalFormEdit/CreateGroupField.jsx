import React, { useState, useEffect } from 'react'

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
import BrowserFoodPersonModal from './ModalForm/FoodPersoneModal';

import moment from 'moment';
import 'dayjs/locale/tr';
import dayjs from 'dayjs';


import PropTypes from 'prop-types';
import CustomeField from './CustomeField';
import AutoCompleteField from '../../components/AutoCompleteField.jsx';

const CreateGroupField = ({ fieldList, onChangeData, onchangeDataCustomeField, controlValuesp, controlValuesCustomep }) => {
    const [autocomplatemodalform, setautocomplatemodalform] = useState(false);
    const [autoComplateModalFormProduct, setautoComplateModalFormProduct] = useState(false);
    const [autoComplatePersonelmodalForm, setautoComplatePersonelModalForm] = useState(false);
    const [formdefinationtypeid, setformdefinationtypeid] = useState(0);
    const [maxRowCount, setmaxRowCount] = useState(1);


    useEffect(() => {

        let maxValue = 0;

        const values = Object.values(controlValuesCustomep);

        values.map((el) => {
            //getting the value from each object and 
            //comparing to existing value
            const valueFromObject = el.fieldOrder;
            maxValue = Math.max(maxValue, valueFromObject);
        });

        setmaxRowCount(maxValue);

    }, []);


    function openModal(modalType, definationTypeid) {
        setautocomplatemodalform(false);
        setautoComplateModalFormProduct(false);
        setautoComplatePersonelModalForm(false);
        setformdefinationtypeid(definationTypeid);
        const modals = { CompanyDefination: setautocomplatemodalform, ProductDefination: setautoComplateModalFormProduct, FoodPerson: setautoComplatePersonelModalForm };
        modals[modalType]?.(true);
    }

    const getFieldValue = (fieldName) => controlValuesp.find(s => s.fieldName === fieldName)?.fieldValue ?? undefined;
    
    const GetControlValue = (fieldName) => getFieldValue(fieldName) ?? "";
    
    const GetControlCheckedValue = (fieldName) => (getFieldValue(fieldName) === 'true');
    
    const GetDateValue = (fieldName) => {
        const val = getFieldValue(fieldName);
        return val ? dayjs(val) : null;
    };



    const handleChange = (e) => onChangeData(e.target.name, e.target.value);
    const handleChangeCustomeField = (name, value, rowCount) => onchangeDataCustomeField(name, value, rowCount);
    const handleChangeChecked = (e) => onChangeData(`${e.target.name}`, e.target.checked ? "true" : "false");
    const handleChangeDatetime = (field, date) => onChangeData(field, moment(date.$d).format('DD-MM-YYYY'));

    function CreateText(textField) {

        return (
            <CCol sm={9} ><CFormInput value={GetControlValue(textField.tagName)}
                name={textField.tagName}
                onChange={(e) => handleChange(e)}
                type="text"
                id={`txt${textField.tagName}`} /></CCol>
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
            onChangeData(item.fieldName, inputElement.value);
        }
    }

    return (
        <>
            {fieldList?.map((item) => {

                if (item.controlType == "Text") {
                    return (
                        <CRow key={item.id} className="mb-12">
                            <CFormLabel htmlFor={`txt${item.tagName}`} className="col-sm-3 col-form-label">{item.caption}</CFormLabel>

                            {
                                item.autoComplate === true ? (
                                    <AutoCompleteField
                                        tagName={item.tagName}
                                        autoComlateType={item.autoComlateType}
                                        fieldId={item.id}
                                        value={GetControlValue(item.tagName)}
                                        onValueChange={onChangeData}
                                        onLoadControlData={LoadControlData}
                                        onOpenModal={openModal}
                                    />
                                ) : CreateText(item)

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
                            <CFormSelect id={`cmb${item.tagName}`} value={GetControlValue(item.tagName)} onChange={(e) => handleChange(e)} name={item.tagName}>
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
                                        key={`${item.id}_${t}`}
                                        name={`${item.tagName}_${combo.tagName}`}

                                        checked={GetControlCheckedValue(`${item.tagName}_${combo.tagName}`)}

                                        inline id={`chk${item.tagName}_${combo.tagName}`} value={combo.tagName} label={combo.name} ></CFormCheck>
                                )
                            })}
                        </CCol>
                    </CRow>
                } else if (item.controlType == "RadioBox") {

                    return <CRow key={item.id} className="mb-12">
                        <CCol sm={9} >


                            {item.comboBoxItems.map((combo, t) => {
                                return (
                                    <CFormCheck onChange={(e) => handleChangeChecked(e)}
                                        key={`${item.id}_${t}`}
                                        name={`${item.tagName}_${combo.tagName}`} inline type='radio' id={`chk${item.tagName}_${combo.tagName}`} value={combo.tagName} label={combo.name} ></CFormCheck>
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
                        <div key={item.id}>
                            <CustomeField customeFielType={item.controlType}
                                controlValuesp={controlValuesCustomep}
                                rowCountP={maxRowCount}
                                onChangeDataCustomeField={(fieldName, value, rowNumber) => handleChangeCustomeField(fieldName, value, rowNumber)}>

                            </CustomeField>
                        </div>
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
            <BrowserFoodPersonModal
                visiblep={autoComplatePersonelmodalForm}
                formDefinationTypeIdp={formdefinationtypeid}
                setClose={() => setautoComplatePersonelModalForm(false)}
                setFormData={(e) => LoadControlData(e)}

            >
            </BrowserFoodPersonModal>
        </>


    );
}

export default CreateGroupField;


CreateGroupField.propTypes = {
    controlValuesp: PropTypes.array,
}


