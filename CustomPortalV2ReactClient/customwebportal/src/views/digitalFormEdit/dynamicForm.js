import React, { useEffect, useState } from 'react'

import {
    CCard,
    CCardBody,
    CRow,
    CCardHeader

} from '@coreui/react'





import PropTypes from 'prop-types';


import { useSearchParams } from 'react-router-dom'; 
import CreateGroupField from './CreateGroupField';

const DynamicForm = ({ formdefinationTypeIdp, formgroups,OnValueChanged,controlValues }) => {


    const [formgroup, setformGroup] = useState(null);
    const [searchParams, setSearchParams] = useSearchParams();
    const [formdefinationId, setformdefinationId] = useState(null);
    const [formdefinationGroups, setformdefinationGroups] = useState([]);
    const [saveError, setSaveError] = useState(null);





    useEffect(() => {

        setformdefinationId(formdefinationTypeIdp);


        setformdefinationGroups(formgroups);


    }, [formdefinationTypeIdp, formgroups]);




    function CreateGroupCard(item) {

        return (
            <>
                <CRow xs={{ gutterX: 1,gutterY: 1 }}>
                    <CCard >
                        <CCardHeader>{item.formNumber} {item.name}</CCardHeader>
                        <CCardBody>

                            <CreateGroupField onChangeData={(fieldName,value)=>OnValueChanged(fieldName,value)} fieldList={item.formFields}></CreateGroupField>
                        </CCardBody>
                    </CCard>
                </CRow>
            </>
        )
    }

    return (

        <>
            {formdefinationGroups?.map(item => {

                return CreateGroupCard(item);
            })}
        </>
    )

}

export default DynamicForm;

DynamicForm.propTypes = {
    formdefinationTypeIdp: PropTypes.number,
    formgroups: PropTypes.array,
    controlValues:PropTypes.array,
}