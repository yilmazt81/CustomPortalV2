import { useState, useEffect, useContext } from 'react'

import {
    CCard,
    CCardBody,
    CRow,
    CAlert,
    CButton,
    CButtonGroup,
    CCol,
    CBadge,
    CCardTitle,
    CCardText
} from '@coreui/react'


import { useNavigate } from "react-router-dom";
import { useTranslation } from "react-i18next";

import { UrlContext } from "../../lib/URLContext";

import EditModal from './editModal';

const CustomWorks = () => {
    const navigate = useNavigate();
    //Bu sekilde redux tan okunacak 
    const { t } = useTranslation();
    const { dispatch } = useContext(UrlContext);

    const [saveError, setSaveError] = useState(null);
    const [filter, setFilter] = useState('all');
    const [editWork, setEditWork] = useState(null);
    const [editModalVisible, setEditModalVisible] = useState(false);

    const data = [
        {
            id: 1,
            title: 'E-Ticaret Paneli',
            status: 'biten',
            desc: 'Yayinlandi ve teslim edildi.',
            updatedAt: '09.02.2026',
            count: 18,
        },
        {
            id: 2,
            title: 'SaaS Dashboard',
            status: 'devam-eden',
            desc: 'Arayuz ve raporlar hazirlaniyor.',
            updatedAt: '09.02.2026',
            count: 7,
        },
        {
            id: 3,
            title: 'Mobil Uygulama',
            status: 'iptal',
            desc: 'Kapsam degisikligi nedeniyle durduruldu.',
            updatedAt: '02.02.2026',
            count: 3,
        },
        {
            id: 4,
            title: 'Musteri Portal',
            status: 'devam-eden',
            desc: 'Yetkilendirme ve bildirim akislari suruyor.',
            updatedAt: '09.02.2026',
            count: 12,
        },
    ];

    const statusConfig = {
        'devam-eden': { label: 'Devam Eden', color: 'warning' },
        'biten': { label: 'Biten', color: 'success' },
        'iptal': { label: 'Iptal', color: 'danger' },
    };

    const filteredData = filter === 'all' ? data : data.filter((p) => p.status === filter);
    function SetLocationAdress() {

        dispatch({ type: 'reset' })

        dispatch({
            type: 'Add',
            payload: { pathname: "#/CustomWorks", name: t("CustomWorks"), active: false }
        });
    }


    useEffect(() => {

        try {
            SetLocationAdress();

        } catch (error) {
            console.log(error);
        }

    }, []);



    return (
        <>
            <CCard className="mb-4">
                <CCardBody>

                    <CRow className="mb-3">
                        <CCol>
                            <CButtonGroup>
                                <CButton color="primary" onClick={() => {
                                    setEditWork(null);
                                    setEditModalVisible(true);
                                }}>{t("NewWork")}</CButton>
                                <CButton
                                    color={filter === 'all' ? 'dark' : 'light'}
                                    onClick={() => setFilter('all')}
                                >
                                    Tum Isler
                                </CButton>
                                <CButton
                                    color={filter === 'devam-eden' ? 'warning' : 'light'}
                                    onClick={() => setFilter('devam-eden')}
                                >
                                    Devam Eden
                                </CButton>
                                <CButton
                                    color={filter === 'biten' ? 'success' : 'light'}
                                    onClick={() => setFilter('biten')}
                                >
                                    Biten
                                </CButton>
                                <CButton
                                    color={filter === 'iptal' ? 'danger' : 'light'}
                                    onClick={() => setFilter('iptal')}
                                >
                                    Iptal
                                </CButton>
                            </CButtonGroup>
                        </CCol>
                    </CRow>

                    <CRow>
                        {
                            saveError != null ?
                                <CAlert color="warning">{saveError}</CAlert>
                                : ""
                        }
                    </CRow>

                    <CRow className="g-4">
                        {filteredData.map((item) => (
                            <CCol key={item.id} xs={12} sm={6} lg={4}>
                                <CCard
                                    className="h-100"
                                    style={{
                                        border: '1px solid #e7e9ed',
                                        borderRadius: '12px',
                                        background: 'linear-gradient(160deg, #ffffff 0%, #f8f9fb 100%)',
                                        boxShadow: '0 8px 24px rgba(17, 24, 39, 0.06)'
                                    }}
                                >
                                    <CCardBody>
                                        <div
                                            style={{
                                                display: 'flex',
                                                alignItems: 'center',
                                                gap: '12px',
                                                marginBottom: '12px'
                                            }}
                                        >
                                            <div
                                                style={{
                                                    width: '48px',
                                                    height: '36px',
                                                    borderRadius: '10px',
                                                    background: '#ffcc66',
                                                    position: 'relative',
                                                    boxShadow: 'inset 0 0 0 1px rgba(0,0,0,0.05)'
                                                }}
                                            >
                                                <div
                                                    style={{
                                                        position: 'absolute',
                                                        top: '-6px',
                                                        left: '6px',
                                                        width: '20px',
                                                        height: '12px',
                                                        borderRadius: '6px 6px 0 0',
                                                        background: '#ffd88a',
                                                        boxShadow: 'inset 0 0 0 1px rgba(0,0,0,0.04)'
                                                    }}
                                                />
                                            </div>
                                            <div>
                                                <CCardTitle className="mb-1">{item.title}</CCardTitle>
                                                <div style={{ display: 'flex', gap: '8px', alignItems: 'center' }}>
                                                    <CBadge color={statusConfig[item.status].color}>
                                                        {statusConfig[item.status].label}
                                                    </CBadge>
                                                    <span style={{ fontSize: '12px', color: '#6b7280' }}>
                                                        Son guncelleme: {item.updatedAt}
                                                    </span>
                                                </div>
                                            </div>
                                        </div>

                                        <CCardText style={{ color: '#4b5563' }}>{item.desc}</CCardText>

                                        <div
                                            style={{
                                                display: 'flex',
                                                alignItems: 'center',
                                                justifyContent: 'space-between',
                                                marginTop: '16px'
                                            }}
                                        >
                                            <span style={{ fontSize: '12px', color: '#6b7280' }}>
                                                {item.count} is kaydi
                                            </span>
                                            <CButton color="primary" size="sm">
                                                Guncelle
                                            </CButton>
                                        </div>
                                    </CCardBody>
                                </CCard>
                            </CCol>
                        ))}
                    </CRow>
                </CCardBody>
            </CCard>
            <EditModal visiblep={editModalVisible}
                editWorkp={editWork}
                OnCloseModal={() => setEditModalVisible(false)}
                setFormData={() => { }} ></EditModal>
        </>
    )
}

export default CustomWorks;


