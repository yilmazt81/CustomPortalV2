

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
    CFormTextarea,
    CFormInput

} from '@coreui/react'

import PropTypes, { func } from 'prop-types';

import { Link } from 'react-router-dom';

import { useTranslation } from "react-i18next";


import LoadingAnimation from 'src/components/LoadingAnimation';


import { WithContext as ReactTags, SEPARATORS } from 'react-tag-input';


import {
    FaFileWord
} from "react-icons/fa";

import { ReactMultiEmail, isEmail } from 'react-multi-email';
import 'react-multi-email/dist/style.css';

import './reactTags.scss';

import { GetCompanyMailList } from 'src/lib/mailApi';

const KeyCodes = {
    comma: 188,
    enter: [10, 13],
};


const FormSendMailModal = ({ visiblep, attachmentlistp, formidp, OnClose, suggestions }) => {

    function ClosedClick() {
        OnClose(true);
        setvisible(false);
    }

    const [visible, setvisible] = useState(false);
    const [processLoading, setprocessLoading] = useState(false);
    const [saveError, setSaveError] = useState(null);
    const [newFormmetadata, setnewFormmetadata] = useState(null);
    const [toMailList, setToMailList] = useState([]);
    const [ccMailList, setCCMailList] = useState([]);
    const [inputValue, setInputValue] = useState('');
    const [filteredSuggestions, setFilteredSuggestions] = useState([]);
    const [mailsuggestions, setmailsuggestions] = useState([]);

    const [attachmentTaglist, setAttachmentTaglist] = useState([]);

    const [focused, setFocused] = useState(false);

    const { t } = useTranslation();

    useEffect(() => {

        setvisible(visiblep);
        setnewFormmetadata(null);

        setAttachmentTaglist(attachmentlistp);
        LoadAutoComplateMailList();

    }, [visiblep, attachmentlistp]);


    async function LoadAutoComplateMailList() {

        try {
            setSaveError(null);
            setprocessLoading(true);
            var fcompanyMailList = await GetCompanyMailList();
            if (fcompanyMailList.returnCode === 1) {
                setmailsuggestions(fcompanyMailList.data);
            } else {
                setSaveError(fcompanyMailList.returnMessage);
            }
        } catch (error) {
            setSaveError(error.message);
        } finally {

            setprocessLoading(false);
        }
    }

    function handleChange(event) {
        const { name, value } = event.target;

        //var newValue = formdefination[name];
        // newValue = !newValue;

        // setFormdefination({ ...formdefination, [name]: newValue });

    }


    function handleInputChangeMail(value) {

        setInputValue(value);

        // Girilen değere göre önerileri filtrele
        const filtered = mailsuggestions.filter((email) =>
            email.toLowerCase().includes(value.toLowerCase())
        );
        if (value === '') {
            setFilteredSuggestions([]);
        } else {
            setFilteredSuggestions(filtered);
        }

    };

    const handleSuggestionClick = (suggestion) => {
        setToMailList([...toMailList, suggestion]);
        setInputValue(''); // Seçildikten sonra inputu temizle
        setFilteredSuggestions([]); // Seçim yapıldıktan sonra öneri listesini temizle
    };

    function handleChangeAttachment(e) {
        setAttachmentTaglist(e);
    }

    const onClearAll = () => {
        setAttachmentTaglist([]);
    };

    const handleDelete = (index) => {
        setAttachmentTaglist(attachmentTaglist.filter((_, i) => i !== index));
    };

    const handleAddition = (tag) => {
        setAttachmentTaglist((prevTags) => {
            return [...prevTags, tag];
        });
    };

    return (
        <>
            <CModal
                backdrop="static"
                visible={visible}
                size="lg"
                onClose={() => ClosedClick()}

            >
                <CModalHeader>
                    <CModalTitle> {t("SendMailModalTitle")}</CModalTitle>
                </CModalHeader>
                <CModalBody>

                    <CRow>
                        <CFormLabel>{t("SendMailDescription")}</CFormLabel>
                    </CRow>
                    <CRow>
                        <CFormLabel htmlFor="txtToMailAdres" className="col-sm-3 col-form-label">{t("ToMail")}</CFormLabel>
                        <CCol sm={9}>
                            <ReactMultiEmail
                                emails={toMailList}
                                delimiter={'[ ,;]'}
                                onChange={(_emails) => {
                                    setToMailList(_emails)
                                }}
                                validateEmail={email => isEmail(email)}

                                getLabel={(email, index, removeEmail) => {
                                    return (
                                        <div data-tag key={index}>
                                            <div data-tag-item>{email}</div>
                                            <span data-tag-handle onClick={() => removeEmail(index)}>
                                                ×
                                            </span>
                                        </div>
                                    );
                                }}
                                onChangeInput={(e) => handleInputChangeMail(e)}
                                value={inputValue}
                            />
                            {filteredSuggestions.length > 0 && (
                                <ul>
                                    {filteredSuggestions.map((suggestion, index) => (
                                        <li key={index} onClick={() => handleSuggestionClick(suggestion)}>
                                            {suggestion}
                                        </li>
                                    ))}
                                </ul>
                            )}
                        </CCol>

                    </CRow>
                    <CRow>
                        <CFormLabel htmlFor="txtToMailAdresCC" className="col-sm-3 col-form-label">{t("CCMail")}</CFormLabel>
                        <CCol sm={9}>


                            <ReactMultiEmail
                                emails={ccMailList}
                                delimiter={'[ ,;]'}
                                onChange={(_emails) => {
                                    setCCMailList(_emails)
                                }}
                                autoFocus={true}
                                onFocus={() => setFocused(true)}
                                onBlur={() => setFocused(false)}
                                getLabel={(email, index, removeEmail) => {
                                    return (
                                        <div data-tag key={index}>
                                            <div data-tag-item>{email}</div>
                                            <span data-tag-handle onClick={() => removeEmail(index)}>
                                                ×
                                            </span>
                                        </div>
                                    );
                                }}

                            />
                        </CCol>

                    </CRow>
                    <CRow>
                        <CFormLabel htmlFor="txtToMailSubject" className="col-sm-3 col-form-label">{t("MailSubject")}</CFormLabel>
                        <CCol sm={9}>
                            <CFormInput type="text" id='txtToMailAdresCC' name="CCMail"
                                onChange={e => handleChange(e)} />

                        </CCol>

                    </CRow>

                    <CRow>
                        <CFormLabel htmlFor="txtAttachmentList" className="col-sm-3 col-form-label">{t("MailAttachment")}</CFormLabel>
                        <CCol sm={9}>
                            <ReactTags id='txtAttachmentList'
                                suggestions={suggestions}
                                tags={attachmentTaglist}
                                separators={[SEPARATORS.ENTER, SEPARATORS.COMMA]}
                                inputFieldPosition="bottom"
                                allowAdditionFromPaste
                                editable
                                clearAll
                                onClearAll={onClearAll}
                                handleDelete={handleDelete}
                                handleAddition={handleAddition}
                                //value={attachmentTaglist}
                                onChange={(e) => handleChangeAttachment(e)}

                            />
                        </CCol>

                    </CRow>

                    <CRow>
                        <CFormLabel htmlFor="txtMailBody" className="col-sm-3 col-form-label">{t("MailBody")}</CFormLabel>
                        <CCol sm={9}>
                            <CFormTextarea type="text" id='txtMailBody' name="MailBody"
                                onChange={e => handleChange(e)} />
                        </CCol>

                    </CRow>
                </CModalBody>
                <CModalFooter>
                    <CButton color='primary'  >{t("Ok")}</CButton>
                    <CButton color="secondary" onClick={() => ClosedClick()}  >{t("Close")}</CButton>

                </CModalFooter>
            </CModal>
        </>
    )
}

export default FormSendMailModal;