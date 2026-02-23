import { useEffect, useState } from 'react'
import { CButton } from '@coreui/react'
import { useTranslation } from 'react-i18next'

const STORAGE_KEY = 'cookieConsent'

const normalizeConsent = (value) => {
  if (!value) return null

  if (value === 'accepted') {
    return { necessary: true, analytics: true, marketing: true, source: 'legacy' }
  }

  if (value === 'rejected') {
    return { necessary: true, analytics: false, marketing: false, source: 'legacy' }
  }

  try {
    const parsed = JSON.parse(value)
    if (parsed && typeof parsed === 'object') {
      return {
        necessary: true,
        analytics: Boolean(parsed.analytics),
        marketing: Boolean(parsed.marketing),
        source: parsed.source || 'stored',
      }
    }
  } catch (error) {
    return null
  }

  return null
}

const CookieConsent = () => {
  const [visible, setVisible] = useState(false)
  const { t } = useTranslation()

  useEffect(() => {
    const stored = localStorage.getItem(STORAGE_KEY)
    const consent = normalizeConsent(stored)
    if (!consent) {
      setVisible(true)
      return
    }

    if (consent.source === 'legacy') {
      localStorage.setItem(
        STORAGE_KEY,
        JSON.stringify({
          necessary: true,
          analytics: consent.analytics,
          marketing: consent.marketing,
          updatedAt: new Date().toISOString(),
          source: 'migrated',
        })
      )
    }
  }, [])

  const handleChoice = (value) => {
    const accepted = value === 'accepted'
    localStorage.setItem(
      STORAGE_KEY,
      JSON.stringify({
        necessary: true,
        analytics: accepted,
        marketing: accepted,
        updatedAt: new Date().toISOString(),
        source: 'banner',
      })
    )
    setVisible(false)
  }

  if (!visible) {
    return null
  }

  return (
    <div
      style={{
        position: 'fixed',
        bottom: 0,
        left: 0,
        width: '100%',
        zIndex: 2000,
        background: '#0f172a',
        color: '#ffffff',
        boxShadow: '0 -10px 30px rgba(15, 23, 42, 0.25)',
      }}
    >
      <div
        style={{
          display: 'flex',
          gap: '12px',
          alignItems: 'center',
          justifyContent: 'space-between',
          padding: '14px 20px',
          flexWrap: 'wrap',
        }}
      >
        <div style={{ fontSize: '14px', lineHeight: 1.5 }}>
          {t('CookieConsentMessage')}
          <span style={{ marginLeft: '6px' }}>{t('CookieConsentNecessaryNote')}</span>
          <a
            href={t('CookieConsentDetailsUrl')}
            target="_blank"
            rel="noreferrer"
            style={{
              marginLeft: '10px',
              color: '#f8fafc',
              textDecoration: 'underline',
              fontWeight: 600,
            }}
          >
            {t('CookieConsentDetailsLink')}
          </a>
        </div>
        <div style={{ display: 'flex', gap: '8px' }}>
          <CButton color="light" size="sm" onClick={() => handleChoice('accepted')}>
            {t('CookieConsentAccept')}
          </CButton>
          <CButton color="secondary" size="sm" onClick={() => handleChoice('rejected')}>
            {t('CookieConsentReject')}
          </CButton>
        </div>
      </div>
    </div>
  )
}

export default CookieConsent
