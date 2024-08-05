import React, { useEffect, useState } from 'react'

import {
    CCard,
    CCardBody,
    CRow,
    CCardTitle,
    CCardText,
    CFormLabel,
    CCol,
    CFormInput,
    CCardHeader

} from '@coreui/react'





import PropTypes from 'prop-types';


import { GetFormGroupFields } from 'src/lib/formdef'
import { useSearchParams } from 'react-router-dom';
import CreateTextField from './TextControl';
import CreateGroupField from './CreateGroupField';
import { Padding } from '@mui/icons-material';

const DynamicForm = ({ formdefinationTypeIdp, formgroups }) => {


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
 
                <CCard style={{Padding:10}} >
                    <CCardHeader>{item.name}</CCardHeader>
                    <CCardBody>

                        <CreateGroupField fieldList={item.formFields}></CreateGroupField>
                    </CCardBody>
                </CCard>

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
}