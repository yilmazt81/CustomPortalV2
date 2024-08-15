
import React, { useEffect, useState } from 'react'

import {
    CCard,
    CCardBody,
    CRow,
    CCardHeader,
    CFormInput,
    CButton,
    CFormCheck


} from '@coreui/react'


import PropTypes from 'prop-types';


import { useSearchParams } from 'react-router-dom';

import { GetFormDefinationTemplate } from 'src/lib/formdef'

import BrowserAdressModal from './ModalForm/AdresModal';
import BrowserProductModal from './ModalForm/productModal';
import parse from 'html-react-parser';
import { cilMap, cilBarcode } from '@coreui/icons';
import { DatePicker } from '@mui/x-date-pickers/DatePicker';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import 'dayjs/locale/tr';
import moment from 'moment';
import CIcon from '@coreui/icons-react';

const DesingFormTemplate = ({ formdefinationTypeIdp,onChangeData }) => {

    const [formDesingTemplate, setformDesingTemplate] = useState("");

    const [SaveError, setSaveError] = useState(null);
    const [autocomplatemodalform, setautocomplatemodalform] = useState(false);
    const [autoComplateModalFormProduct, setautoComplateModalFormProduct] = useState(false);
    const [formdefinationtypeid, setformdefinationtypeid] = useState(0);


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

    const parseOptions = {

        replace: (domNode) => {

            console.log(domNode);
            if (domNode.name === 'input') {
                if (domNode.attribs.type === "checkbox" || domNode.attribs.type === "radio") {
                   
                    return (
                        <CFormCheck  onChange={(e)=>handleChangeChecked(e)}  inline id={domNode.attribs.id} name={domNode.attribs.name} type={domNode.attribs.type} value={domNode.attribs.value} label={domNode.attribs.caption} ></CFormCheck>
                    )
                } else if (domNode.attribs.type === "date") {
                    
                    return (
                        <LocalizationProvider dateAdapter={AdapterDayjs} adapterLocale="tr">
                            <DatePicker name={domNode.attribs.id} id={domNode.attribs.id} onChange={(e)=>handleChangeDatetime(domNode.attribs.id,e)} />
                        </LocalizationProvider>
                    );

                } else {
                    return (

                        <CFormInput
                            type={domNode.attribs.type}
                            name={domNode.attribs.id}
                            id={domNode.attribs.id}
                            onChange={e=>handleChange(e)}
                        />
                    )
                }



            } else if (domNode.name === "div") {
                return (
                    <CButton color="primary" onClick={() => openModal(domNode.attribs.datacontent, domNode.attribs.data)} >
                        {GetIcon(domNode.attribs.datacontent)}
                    </CButton>
                )
            } else if (domNode.name === "span") {

                return (
                    <LocalizationProvider dateAdapter={AdapterDayjs} adapterLocale="tr">
                        <DatePicker name={domNode.attribs.id} id={domNode.attribs.id} onChange={(e)=>onChangeData(e)}/>
                    </LocalizationProvider>
                );
            } else if (domNode.type === "checkbox") {
                debugger;
                console.log("dddd");
            }            
        },
    };

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

    async function getformTemplate() {

        var getTemplateReturn = await GetFormDefinationTemplate(formdefinationTypeIdp);

        if (getTemplateReturn.returnCode === 1) {
            setformDesingTemplate(getTemplateReturn.data);

        } else {
            setSaveError(getTemplateReturn.returnMessage);

            return null;
        }

    }

    useEffect(() => {

        getformTemplate();

    }, [formdefinationTypeIdp]);


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
}