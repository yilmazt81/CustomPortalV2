import React, { useEffect, useState } from 'react'

import {
    CCard,
    CCardBody,
    CRow,
    CCardHeader

} from '@coreui/react'





import PropTypes from 'prop-types';


import { useSearchParams } from 'react-router-dom';
import CreateGroupField from './CreateGroupField.jsx';
import 'dayjs/locale/tr';
import dayjs from 'dayjs';

const DynamicForm = ({ formdefinationTypeIdp, formgroups, OnValueChanged, OnCustomeValueChanged, controlValues, controlValuesCustome }) => {


    const [formgroup, setformGroup] = useState(null);
    const [searchParams, setSearchParams] = useSearchParams();
    const [formdefinationId, setformdefinationId] = useState(null);
    const [formdefinationGroups, setformdefinationGroups] = useState([]);
    const [saveError, setSaveError] = useState(null);




    useEffect(() => {

        setformdefinationId(formdefinationTypeIdp);


        setformdefinationGroups(formgroups);


    }, [formdefinationTypeIdp, formgroups]);


    function customeValueChanged(fieldName, value, orderNumber) {

        OnCustomeValueChanged(fieldName, value, orderNumber);
    }

    return (

        <>
            {formdefinationGroups?.map(item => {

                return (
                    <div key={item.id}>
                        <CRow xs={{ gutterX: 1, gutterY: 1 }}>
                            <CCard>
                                <CCardHeader>{item.formNumber} {item.name}</CCardHeader>
                                <CCardBody>

                                    <CreateGroupField
                                        onChangeData={(fieldName, value) => OnValueChanged(fieldName, value)}
                                        onchangeDataCustomeField={(fieldName, value, orderNumber) => customeValueChanged(fieldName, value, orderNumber)}
                                        controlValuesp={controlValues}
                                        controlValuesCustomep={controlValuesCustome}
                                        fieldList={item.formFields}></CreateGroupField>
                                </CCardBody>
                            </CCard>
                        </CRow>
                    </div>
                );
            })}
        </>
    )

}

export default DynamicForm;

DynamicForm.propTypes = {
    formdefinationTypeIdp: PropTypes.number,
    formgroups: PropTypes.array,
    controlValues: PropTypes.array,
}


