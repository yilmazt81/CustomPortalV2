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
    CForm

} from '@coreui/react'

import { Save } from '../../lib/customWorkapi.jsx';
import { GetUserList } from '../../lib/userapi.jsx';
import { GetSector } from '../../lib/formdef.jsx';
import Lottie from 'lottie-react';
import PropTypes from 'prop-types';


import './reactTags.scss';
import ProcessAnimation from "../../content/animation/Process.json";

import { WithContext as ReactTags, SEPARATORS } from 'react-tag-input';

import { useTranslation } from "react-i18next";
import BrowserAdressModal from '../digitalFormEdit/ModalForm/AdresModal/index.jsx';
import AutoCompleteField from '../../components/AutoCompleteField.jsx';

const EditModal = ({ visiblep, customeWorkP, setFormData }) => {


    const [visible, setvisible] = useState(visiblep);
    const [autoComplateModalFormSender, setautoComplateModalFormSender] = useState(false);
    const [autoComplateModalFormRecrived, setautoComplateModalFormRecrived] = useState(false);

    const [formdefinationtypeid, setformdefinationtypeid] = useState(0);
    const [customeWork, setcustomeWork] = useState({ ...customeWorkP });
    const [saveError, setSaveError] = useState(null);

    const [saveStart, setsaveStart] = useState(false);
    const [customSectors, setCustomSectors] = useState([]);
    const [userSuggestions, setUserSuggestions] = useState([]);
    const [userTags, setUserTags] = useState([]);

    const { t } = useTranslation();



    function handleChange(event) {
        const { name, value } = event.target;
 
        setcustomeWork({ ...customeWork, [name]: value });

    }


    async function ClosedClick() {
        if (typeof document !== "undefined") {
            document.activeElement?.blur?.();
        }
        setvisible(false);
    }

    function normalizeUserIds(value) {
        if (!value) {
            return [];
        }

        if (Array.isArray(value)) {
            return value
                .map((id) => Number(id))
                .filter((id) => Number.isFinite(id));
        }

        if (typeof value === 'string') {
            return value
                .split(',')
                .map((id) => Number(id.trim()))
                .filter((id) => Number.isFinite(id));
        }

        return [];
    }

    function buildUserTag(user) {
        const label = user.fullName || user.userName || user.email || String(user.id);
        return {
            id: String(user.id),
            text: label
        };
    }

    function GetControlValue(fieldName) {
        return customeWork?.[fieldName] ?? "";
    }



    useEffect(() => {
        setSaveError(null);
        setvisible(visiblep);
        LoadCustomSectors();
        LoadUsers();
        setcustomeWork(customeWorkP);
        //LoadBranchList();

    }, [visiblep, customeWorkP])

    useEffect(() => {
        const userIds = normalizeUserIds(customeWorkP?.userIds);
        if (userIds.length === 0) {
            setUserTags([]);
            return;
        }

        const tags = userIds.map((userId) => {
            const match = userSuggestions.find((user) => String(user.id) === String(userId));
            return match ? buildUserTag(match) : { id: String(userId), text: String(userId) };
        });

        setUserTags(tags);
    }, [customeWorkP, userSuggestions]);

    useEffect(() => {
        console.log("customeWork updated", customeWork);
    }, [customeWork]);

    async function SaveData() {

        try {
            var customeWorkReturn = await Save(customeWork);
            if (customeWorkReturn.returnCode === 1) {
                setFormData();
            } else {
                setSaveError(customeWorkReturn.ReturnMessage);
            }

        } catch (error) {
            setSaveError(error.message);
        } finally {

            setsaveStart(false);
        }

    }

    async function LoadCustomSectors() {


        try {
            setSaveError(null);
            var fSectorService = await GetSector();
            if (fSectorService.returnCode === 1) {
                setCustomSectors(fSectorService.data);
            } else {
                setSaveError(fSectorService.returnMessage);
            }
        } catch (error) {
            setSaveError(error.message);
            console.log(error);

        } finally {
        }
    }

    async function LoadUsers() {
        try {
            setSaveError(null);
            var userService = await GetUserList();
            if (userService.returnCode === 1) {
                setUserSuggestions(userService.data);
            } else {
                setSaveError(userService.returnMessage);
            }
        } catch (error) {
            setSaveError(error.message);
        }
    }

    const handleUserDelete = (index) => {
        setUserTags((prevTags) => {
            const nextTags = prevTags.filter((_, i) => i !== index);
            setcustomeWork((prevWork) => ({
                ...prevWork,
                userIds: nextTags.map((tag) => Number(tag.id))
            }));
            return nextTags;
        });
    };

    function openModal(modalType, definationTypeid) {
      
        if (typeof document !== "undefined") {
            document.activeElement?.blur?.();
        }
        setautoComplateModalFormSender(false);
        setautoComplateModalFormRecrived(false);
        setformdefinationtypeid(definationTypeid);
        setvisible(false);
        const modals = {
            SenderCompanyName: setautoComplateModalFormSender,
            RecrivedCompanyName: setautoComplateModalFormRecrived
        };
        modals[modalType]?.(true);
        /*setautocomplatemodalform(false);
        setautoComplateModalFormProduct(false);
        setautoComplatePersonelModalForm(false);
        setformdefinationtypeid(definationTypeid);
        const modals = { CompanyDefination: setautocomplatemodalform, ProductDefination: setautoComplateModalFormProduct, FoodPerson: setautoComplatePersonelModalForm };
        modals[modalType]?.(true);
        */
    }

    const SetValuesToWork = (data) => {
        const fieldMap = {
            GonderenAdi: "SenderCompanyName",
            SenderCompanyId: "SenderCompanyId",
            AliciAdi: "RecrivedCompanyName",
            RecrivedCompanyId: "RecrivedCompanyId"
        }; 
        const updates = (Array.isArray(data) ? data : []).reduce((acc, item) => {
            const key = fieldMap[item.fieldName];
            if (key) {
                acc[key] = item.controlValue ?? item.value ?? "";
               customeWork[key] = item.controlValue ?? item.value ?? "";
            }
            return acc;
        }, {});

        if (Object.keys(updates).length > 0) {
            setcustomeWork((prev) => ({ ...prev, ...updates }));
        }
        console.log("Updated customeWork with modal data", customeWork);
    }

    const handleUserAddition = (tag) => {
        const match = userSuggestions.find(
            (user) =>
                (user.fullName || user.userName || user.email || String(user.id)).toLowerCase() ===
                tag.text.toLowerCase()
        );

        if (!match) {
            return;
        }

        const nextTag = buildUserTag(match);

        setUserTags((prevTags) => {
            if (prevTags.some((item) => item.id === nextTag.id)) {
                return prevTags;
            }

            const nextTags = [...prevTags, nextTag];
            setcustomeWork((prevWork) => ({
                ...prevWork,
                userIds: nextTags.map((item) => Number(item.id))
            }));
            return nextTags;
        });
    };


    return (

        <>
            <CModal
                backdrop="static"
                visible={visible}
                onClose={() => ClosedClick()}
                size="xl"

            >
                <CModalHeader>
                    <CModalTitle>{t("CustomeWorkModalTitle")}</CModalTitle>
                </CModalHeader>
                <CModalBody>
                    <CForm>
                        <CRow className="mb-12">
                            <CFormLabel htmlFor="cmbCustomSector" className="col-sm-3 col-form-label">{t("CustomSectorName")}</CFormLabel>
                            <CCol sm={9}>
                                <CFormSelect type="text" id='cmbCustomSector' value={customeWork?.customSectorId} name="customSectorId"
                                    onChange={e => handleChange(e)}     >

                                    <option value="0">Seçiniz</option>
                                    {customSectors.map(item => {
                                        return (<option key={item.id} value={item.id}   >{item.name}</option>);
                                    })}
                                </CFormSelect>
                            </CCol>
                        </CRow>

                        <CRow className="mb-12">
                            <CFormLabel htmlFor="txtWorkName" className="col-sm-3 col-form-label">{t("CustomWorkName")}</CFormLabel>
                            <CCol sm={9}>
                                <CFormInput type="text" id='txtWorkName' name="workName"
                                    onChange={e => handleChange(e)} value={customeWork?.workName} />
                            </CCol>

                        </CRow>

                        <CRow className="mb-12">
                            <CFormLabel htmlFor="txtSenderCompanyName" className="col-sm-3 col-form-label">{t("SenderCompanyName")}</CFormLabel>
                            <AutoCompleteField
                                tagName="SenderCompanyName"
                                autoComlateType="CompanyDefination"
                                fieldId={1}
                                value={customeWork?.SenderCompanyName}
                                onValueChange={(name, value) =>
                                    setcustomeWork((prev) => ({ ...prev, [name]: value }))
                                }
                                onLoadControlData={SetValuesToWork}
                                onOpenModal={openModal}
                                applyLabelOnSelect={false}
                            />
                        </CRow>

                        <CRow className="mb-12">
                            <CFormLabel htmlFor="txtRecrivedCompanyName" className="col-sm-3 col-form-label">{t("RecrivedCompanyName")}</CFormLabel>
                            <AutoCompleteField
                                tagName="RecrivedCompanyName"
                                autoComlateType="CompanyDefination"
                                fieldId={12}
                                value={customeWork?.RecrivedCompanyName}
                                onValueChange={(name, value) =>
                                    setcustomeWork((prev) => ({ ...prev, [name]: value }))
                                }
                                onLoadControlData={SetValuesToWork}
                                onOpenModal={openModal}
                                applyLabelOnSelect={false}
                            />
                        </CRow>
                        <CRow className="mb-12">
                            <CFormLabel htmlFor="txtUserList" className="col-sm-3 col-form-label">{t("Users")}</CFormLabel>
                            <CCol sm={9}>
                                <ReactTags
                                    id="txtUserList"
                                    suggestions={userSuggestions.map(buildUserTag)}
                                    tags={userTags}
                                    separators={[SEPARATORS.ENTER, SEPARATORS.COMMA]}
                                    inputFieldPosition="bottom"
                                    handleDelete={handleUserDelete}
                                    handleAddition={handleUserAddition}
                                />
                            </CCol>
                        </CRow>

                    </CForm>
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
            <BrowserAdressModal visiblep={autoComplateModalFormSender}
                ClosedClick={() => setautoComplateModalFormSender(false)}
                formDefinationTypeIdp={formdefinationtypeid}
                setFormData={(data) => {
                    setautoComplateModalFormSender(false);
                    setvisible(true);
                    SetValuesToWork(data);
                }} />
            <BrowserAdressModal visiblep={autoComplateModalFormRecrived}
                formDefinationTypeIdp={formdefinationtypeid}
                ClosedClick={() => setautoComplateModalFormRecrived(false)} setFormData={(data) => {
                    setautoComplateModalFormRecrived(false);
                    setvisible(true);
                    SetValuesToWork(data);
                }} />
        </>
    )


}


export default EditModal;


EditModal.propTypes = {
    visiblep: PropTypes.bool,
    customeFieldp: PropTypes.object,
};


