import React, { useEffect, useState } from 'react'

import {
    CButton,
    CCol,
    CAlert,
    CRow,
    CModal,
    CModalHeader,
    CModalTitle,
    CModalFooter,
    CModalBody,
    CFormLabel,
    CFormInput,
    CFormSelect,


} from '@coreui/react'

import Lottie from 'lottie-react';
import PropTypes from 'prop-types';


import ProcessAnimation from "src/content/animation/Process.json";
import Gridcolumns from './gridColums';


import { useTranslation } from "react-i18next";

import { DataGrid } from '@mui/x-data-grid';

import {FilterCompanyDefination} from 'src/lib/companyAdressDef';

const BrowserAdressModal = ({ visiblep,formDefinationTypeIdp, setFormData }) => {


    const [visible, setvisible] = useState(visiblep);
    const [filterAdressList,setFilterAdressList]=useState([]);
    const [filterCompany,setFilterCompany]=useState({formDefinationFieldId:formDefinationTypeIdp,filterValue:''});
    
    //const [user, setUser] = useState({ ...userp });

    const [saveError, setSaveError] = useState(null);
    // const[branchList,setbranchList] =useState([]);

    const [saveStart, setsaveStart] = useState(false);

    const { t } = useTranslation();

    function handleChange(event) {
        const { name, value } = event.target;
        // setUser({ ...user, [name]: value });

    }

    
    async function GetCompanyList() {

        try {
            setsaveStart(true);
            debugger;
            var filterServiceReturn = await FilterCompanyDefination(filterCompany);
            if (filterServiceReturn.returnCode === 1) {
                setFilterAdressList(filterServiceReturn.data);
            } else {
                setSaveError(filterServiceReturn.returnMessage);
            }
        } catch (error) {
            setSaveError(error.message);
            console.log(error);

        }

        
        setsaveStart(false);
    }
    async function ClosedClick() {
        setvisible(false);
        setFormData();
    }

    useEffect(() => {
        setvisible(visiblep);
        if (visiblep){
            filterCompany.formDefinationFieldId=formDefinationTypeIdp;
            setFilterCompany(filterCompany);
            //  setUser(userp);//set if you change value inside
            GetCompanyList();
        }
        

    }, [visiblep])

  
    const optionClick = (option, id) => {
        //    EditGroupDefination(option === 'Delete', id);
    }

    const SelectedRowChanged=(e)=>{
        console.log(e);
        debugger;

    }

    const gridColumns = Gridcolumns(optionClick);


    return (

        <>
            <CModal
                backdrop="static"
                visible={visible}
                 size="xl"
                onClose={() => ClosedClick()}

            >
                <CModalHeader>
                    <CModalTitle>{t("AdressModalTitle")}</CModalTitle>
                </CModalHeader>
                <CModalBody>

                  
                    <CRow>

                        <CCol>
                            <DataGrid rows={filterAdressList}
                                columns={gridColumns}
                                onRowSelectionModelChange={(e)=>SelectedRowChanged(e)}
                                slotProps={{
                                    toolbar: {
                                        showQuickFilter: true,
                                    },
                                }} />
                        </CCol>
                    </CRow>

                    <CRow xs={{ cols: 4 }}>
                        <CCol> </CCol>
                        <CCol>
                            {
                                saveStart ? <Lottie animationData={ProcessAnimation} loop={true} style={{ width: "80%", height: "80%" }} ></Lottie> : ""
                            }
                        </CCol>
                        <CCol> </CCol>
                        <CCol> </CCol>


                    </CRow>
                    <CRow>
                        {saveError != null ?
                            <CAlert color="warning">{saveError}</CAlert>
                            : ""
                        }
                    </CRow>

                </CModalBody>

                <CModalFooter>
                    <CButton color="secondary" onClick={() => ClosedClick()}  >{t("Close")}</CButton>
                    <CButton color="primary" onClick={()=>setFormData()} >{t("Ok")}</CButton>
                </CModalFooter>
            </CModal>

        </>
    )


}


export default BrowserAdressModal;


BrowserAdressModal.propTypes = {
    visiblep: PropTypes.bool,
    formDefinationTypeIdp:PropTypes.number,
};