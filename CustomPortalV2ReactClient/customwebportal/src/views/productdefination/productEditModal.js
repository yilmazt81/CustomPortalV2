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
    CFormCheck


} from '@coreui/react'

import Lottie from 'lottie-react';
import PropTypes from 'prop-types';


import ProcessAnimation from "../../content/animation/Process.json";


import { useTranslation } from "react-i18next";

import { Save } from 'src/lib/customProductapi';


const ProductEditModal = ({ visiblep, productp, customSectorList, setFormData, setClose }) => {


    const [visible, setvisible] = useState(visiblep);
    const [customProduct, setcustomProduct] = useState({ ...productp });

    const [saveError, setSaveError] = useState(null);
    //const[sectorList,setSectorList] =useState([]);

    const [saveStart, setsaveStart] = useState(false);

    const { t } = useTranslation();

    function handleChange(event) {
        const { name, value } = event.target;
        setcustomProduct({ ...customProduct, [name]: value });

    }
    async function ClosedClick() {
        setvisible(false);
        setClose();
    }


    useEffect(() => {
        setSaveError(null);
        setvisible(visiblep);
        setcustomProduct(productp);
        // setSectorList(customSectorList);
        //LoadBranchList();

    }, [visiblep, productp, customSectorList])

    async function SaveData() {

        try {

            try {
                setSaveError(null);

                setsaveStart(true);
                var savedefinationResult = await Save(customProduct);

                if (savedefinationResult.returnCode === 1) {
                    setFormData(savedefinationResult.data);
                    setClose();
                } else {
                    setSaveError(savedefinationResult.returnMessage);
                }


            } catch (error) {
                setSaveError(error.message);
            } finally {

                setsaveStart(false);
            }

        } catch (error) {
            setSaveError(error.message);
        } finally {

            setsaveStart(false);
        }

    }

  
    return (

        <>
            <CModal
                backdrop="static"
                visible={visible}
                onClose={() => ClosedClick()}

            >
                <CModalHeader>
                    <CModalTitle>{t("ProductModalTitle")}</CModalTitle>
                </CModalHeader>
                <CModalBody>


                    <CRow className="mb-12">
                        <CFormLabel htmlFor="cmbCustomSector" className="col-sm-3 col-form-label">{t("CustomSectorName")}</CFormLabel>
                        <CCol sm={9}>
                            <CFormSelect type="text" id='cmbCustomSector' name="customSectorId"
                                onChange={e => handleChange(e)} value={customProduct?.customSectorId}    >

                                <option value="0">Seçiniz</option>
                                {customSectorList.map(item => {
                                    return (<option key={item.id} value={item.id}  >{item.name}</option>);
                                })}
                            </CFormSelect>

                        </CCol>
                    </CRow>

                    <CRow className="mb-12">
                        <CFormLabel htmlFor="txtProductName" className="col-sm-3 col-form-label">{t("ProductName")}</CFormLabel>
                        <CCol sm={9}>
                            <CFormInput type="text" id='txtProductName' name="productName"
                                onChange={e => handleChange(e)} value={customProduct?.productName} />
                        </CCol>
                    </CRow>

                    <CRow className="mb-12">
                        <CFormLabel htmlFor="txtProductNameTrk" className="col-sm-3 col-form-label">{t("ProductName_TRK")}</CFormLabel>
                        <CCol sm={9}>
                            <CFormInput type="text" id='txtProductNameTrk' name="productName_TRK"
                                onChange={e => handleChange(e)} value={customProduct?.productName_TRK} />
                        </CCol>
                    </CRow>

                    <CRow className="mb-12">
                        <CFormLabel htmlFor="txtproductCulture" className="col-sm-3 col-form-label">{t("ProductCulture")}</CFormLabel>
                        <CCol sm={9}>
                            <CFormInput type="text" id='txtproductCulture' name="productCulture"
                                onChange={e => handleChange(e)} value={customProduct?.productCulture} />
                        </CCol>
                    </CRow>
                    <CRow className="mb-12">
                        <CFormLabel htmlFor="txtGtipCode" className="col-sm-3 col-form-label">{t("GtipCode")}</CFormLabel>
                        <CCol sm={9}>
                            <CFormInput type="text" id='txtGtipCode' name="gtipCode"
                                onChange={e => handleChange(e)} value={customProduct?.gtipCode} />
                        </CCol>
                    </CRow>
                    <CRow className="mb-12">
                        <CFormLabel htmlFor="txtscientificName" className="col-sm-3 col-form-label">{t("ScientificName")}</CFormLabel>
                        <CCol sm={9}>
                            <CFormInput type="text" id='txtscientificName' name="scientificName"
                                onChange={e => handleChange(e)} value={customProduct?.scientificName} />
                        </CCol>
                    </CRow>

                    <CRow>

                        <CFormLabel htmlFor="txtTransfercondition" className="col-sm-3 col-form-label">{t("Transfercondition")}</CFormLabel>
                        <CCol sm={9}>
                            <CFormCheck type="radio" inline id="chkTransferCon" name='transfercondition' value="OdaSicakligi" onChange={(e)=>handleChange(e)} checked={customProduct?.transfercondition==='OdaSicakligi'} label={t("TransferCondationRoom")} />
                            <CFormCheck type="radio" inline id="chkTransferCon" name='transfercondition'  value="Sogutulmus" onChange={(e)=>handleChange(e)}  checked={customProduct?.transfercondition==='Sogutulmus'}  label={t("TransferCondationCold")} />
                            <CFormCheck type="radio" inline id="chkTransferCon" name='transfercondition'  value="Dondurulmus"  onChange={(e)=>handleChange(e)} checked={customProduct?.transfercondition==='Dondurulmus'} label={t("TransferCondationFreeze")} />
                        </CCol>

                    </CRow>

                    <CRow>
                        <CFormLabel htmlFor="txtTransferTemperature" className="col-sm-4 col-form-label">{t("TransferTemperature")}</CFormLabel>
                        <CCol sm={8}>
                            <CFormInput type="text" id='txttransferTemperature' name="transferTemperature"
                                onChange={e => handleChange(e)} value={customProduct?.transferTemperature} />
                        </CCol>
                    </CRow>

                    <CRow>
                        <CFormLabel className="col-sm-3 col-form-label">{t("IntendedUse")}</CFormLabel>
                    </CRow>
                    <CRow>

                        <CCol>

                            <CFormCheck inline name='intendedUse' id="chkTransferCon1" type='radio' checked={customProduct?.intendedUse==='InsaniTuketim'}  value="InsaniTuketim"  onChange={(e)=>handleChange(e)} label={t("IntendedUse1")} />
                            <CFormCheck inline  name='intendedUse' id="chkTransferCon2" type='radio'  checked={customProduct?.intendedUse==='Konserve'} value="Konserve"  onChange={(e)=>handleChange(e)} label={t("IntendedUse2")} />
                            <CFormCheck inline  name='intendedUse' id="chkTransferCon3" type='radio'  checked={customProduct?.intendedUse==='IlaveIslem'} value="IlaveIslem"  onChange={(e)=>handleChange(e)} label={t("IntendedUse3")} />
                            <CFormCheck inline  name='intendedUse' id="chkTransferCon4"type='radio'  checked={customProduct?.intendedUse==='CanliSuHayvan'}  value="CanliSuHayvan"  onChange={(e)=>handleChange(e)} label={t("IntendedUse4")} />

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
                    <CButton color="primary" onClick={() => SaveData()}>{t("Save")}</CButton>
                </CModalFooter>
            </CModal>

        </>
    )


}


export default ProductEditModal;


ProductEditModal.propTypes = {
    visiblep: PropTypes.bool,
    userp: PropTypes.object,
};