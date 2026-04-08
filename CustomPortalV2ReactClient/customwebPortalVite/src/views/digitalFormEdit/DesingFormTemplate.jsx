
import React, { useEffect, useState } from 'react'

import {
    CCard,
    CCardBody,
    CRow,
    CCardHeader,
    CFormInput,
    CButton,
    CFormCheck,
    CFormSelect,
    CFormLabel

} from '@coreui/react'


import PropTypes, { bool, func } from 'prop-types';



import { GetFormDefinationTemplate } from "../../lib/formdef.jsx";

import BrowserAdressModal from './ModalForm/AdresModal';
import BrowserProductModal from './ModalForm/productModal';
import parse from 'html-react-parser';
import { cilMap, cilBarcode, cilUser } from '@coreui/icons'
import { DatePicker } from '@mui/x-date-pickers/DatePicker';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import 'dayjs/locale/tr';
import dayjs from 'dayjs';
import moment from 'moment';
import CIcon from '@coreui/icons-react';
import AutoCompleteField from '../../components/AutoCompleteField.jsx';

const DesingFormTemplate = ({ formdefinationTypeIdp, formgroups, onChangeData, OnCustomeValueChanged, controlValuesp }) => {

    const [formDesingTemplate, setformDesingTemplate] = useState("");

    const [SaveError, setSaveError] = useState(null);
    const [autocomplatemodalform, setautocomplatemodalform] = useState(false);
    const [autoComplateModalFormProduct, setautoComplateModalFormProduct] = useState(false);
    const [autoComplatePersonelModalForm, setautoComplatePersonelModalForm] = useState(false);
    const [formdefinationGroups, setformdefinationGroups] = useState([]);
    const [formdefinationtypeid, setformdefinationtypeid] = useState(0);

    const icons = {
        CompanyDefination: cilMap,
        ProductDefination: cilBarcode,
        FoodPerson: cilUser
    }
    /*
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
        }*/


    function handleChange(e) {
        const { name, value } = e.target;

        onChangeData(name, value);
    }

    function handleChangeChecked(e) {
        const { name, value } = e.target;

        let checkbox = (e.target.checked ? "true" : "false");

        onChangeData(value, checkbox);
    }

    function handleChangeDatetime(field, date) {

        var dateStr = moment(date.$d).format('DD-MM-YYYY');
        onChangeData(field, dateStr);
    }
    function getFieldByName(fieldName) {

        for (var i = 0; i < formgroups.length; i++) {
            var group = formgroups[i];
            var field = group.formFields.find(f => f.tagName === fieldName);
            if (field) {
                return field;
            }
        }
        //return formgroups.find(s => s.formFields.some(f => f.tagName === fieldName))
    }




    const parseOptions = {

        replace: (domNode) => {
            if (domNode.type === 'text') {
                const nodeText = domNode.data?.trim();

                if (!nodeText) {
                    return undefined;
                }

                if (nodeText.startsWith("@Group_")) {
                    const groupTag = nodeText.replace("@Group_", "").replace("@", "").trim();
                    const group = formgroups.find(s => s.groupTag === groupTag);

                    if (group != null) {
                        return (
                            <CFormLabel>
                                {group.formNumber} {group.name}
                            </CFormLabel>
                        );
                    }
                } else if (nodeText.startsWith("@Label_")) {
                    const labelTag = nodeText.replace("@Label_", "").replace("@", "").trim();

                    const field = getFieldByName(labelTag);

                    return (
                        <CFormLabel>
                            {field?.caption}
                        </CFormLabel>
                    );
                } else if (nodeText.startsWith("@input_")) {
                    const labelTag = nodeText.replace("@input_", "").replace("@", "").trim();
                    const field = getFieldByName(labelTag);

                    if (field == null) {
                        return null;
                    }

                    if (field.controlType === "hidden") {
                        return (
                            <input type="hidden"
                                name={field.tagName}
                                id={field.id}
                                onChange={(e) => handleChange(e)}
                                value={GetControlValue(field.tagName)}
                            />
                        )
                    } else if (field.controlType === "checkbox") {
                        return (
                            <CFormCheck>

                            </CFormCheck>)
                    } else if (field.controlType === "CheckBox" || field.controlType === "RadioButton") {
                        const controlType = field.controlType === "CheckBox" ? "checkbox" : "radio";

                        return (
                            <CFormCheck onChange={(e) => handleChangeChecked(e)}
                                inline
                                id={field.id}
                                name={field.tagName}
                                type={controlType}
                                label={field.caption}
                                value={GetControlValue(field.tagName)}
                                checked={GetControlCheckedValue(field.tagName)}

                            ></CFormCheck>)
                    } else if (field.controlType === "ComboBox") {
                        return (
                            <CFormSelect>
                                <options value="">Seçiniz</options>
                            </CFormSelect>)
                    } else if (field.controlType === "date") {
                        debugger;
                        return (
                            <LocalizationProvider dateAdapter={AdapterDayjs} adapterLocale="tr">
                                <DatePicker name={field.tagName}
                                    id={field.id}
                                    onChange={(e) => handleChangeDatetime(field.tagName, e)}
                                    value={GetDateValue(field.tagName)} />
                            </LocalizationProvider>
                        );
                    } else if (field.controlType === "Text") {
                        debugger;
                        if (field.autoComplate) {
                            return (
                                <AutoCompleteField
                                    tagName={field.tagName}
                                    autoComlateType={field.autoComlateType}
                                    fieldId={field.id}
                                    value={GetControlValue(field.tagName)}
                                    onValueChange={onChangeData}
                                    onLoadControlData={LoadControlData}
                                    onOpenModal={openModal}
                                />

                            )

                        }
                        else {
                            return (
                                <CFormInput
                                    id={`txt${field.tagName}`}
                                    name={field.tagName}
                                    onChange={handleChange}
                                    value={GetControlValue(field.tagName)}
                                />
                            );
                        }

                    } else {
                        return null;
                    }



                }
            }

        },
    };

    function openModal(modalType, definationTypeid) {
        setautocomplatemodalform(false);
        setautoComplateModalFormProduct(false);
        setautoComplatePersonelModalForm(false);
        setformdefinationtypeid(definationTypeid);
        const modals = { CompanyDefination: setautocomplatemodalform, ProductDefination: setautoComplateModalFormProduct, FoodPerson: setautoComplatePersonelModalForm };
        modals[modalType]?.(true);
    }

    async function getformTemplate() {

        var getTemplateReturn = await GetFormDefinationTemplate(formdefinationTypeIdp);

        if (getTemplateReturn.returnCode === 1) {
            setformDesingTemplate(getTemplateReturn.data);

        } else {
            setSaveError(getTemplateReturn.returnMessage);

            return null;
        }

    }

    function GetControlValue(fieldName) {

        var fieldValue = controlValuesp.find(s => s.fieldName === fieldName);
        return (fieldValue === undefined ? "" : fieldValue.fieldValue);
    }

    function GetControlCheckedValue(fieldName) {

        var fieldValue = controlValuesp.find(s => s.fieldName === fieldName);

        return (fieldValue === undefined ? false : fieldValue.fieldValue === 'true');
    }

    function GetDateValue(fieldName) {

        var fieldValue = controlValuesp.find(s => s.fieldName === fieldName);

        return (fieldValue === undefined ? null : dayjs(fieldValue.fieldValue));
    }
    useEffect(() => {

        getformTemplate();
        setformdefinationGroups(formgroups);

    }, [formdefinationTypeIdp, formgroups]);


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
            <div>
                {parse(formDesingTemplate, parseOptions)}
            </div>
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
    )

}

export default DesingFormTemplate;


DesingFormTemplate.propTypes = {
    formdefinationTypeIdp: PropTypes.number,
    controlValuesp: PropTypes.array,
}


