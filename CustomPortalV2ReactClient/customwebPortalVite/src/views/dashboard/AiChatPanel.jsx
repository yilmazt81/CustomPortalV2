import React, { useMemo, useState } from 'react'
import axios from 'axios'
import {
  CAlert,
  CButton,
  CCard,
  CCardBody,
  CCardHeader,
  CFormInput,
  CListGroup,
  CListGroupItem,
  CSpinner,
} from '@coreui/react'
import { useTranslation } from 'react-i18next'

const AiChatPanel = () => {
  const { t } = useTranslation()
  const [prompt, setPrompt] = useState('')
  const [isSending, setIsSending] = useState(false)
  const [errorMessage, setErrorMessage] = useState(null)
  const [messages, setMessages] = useState([
    {
      role: 'assistant',
      content: t('AiChatWelcomeMessage'),
    },
  ])

  const apiPath = useMemo(() => import.meta.env.VITE_APIURL || '/api/ChatIA/Chat', [])

  const getAssistantText = (data) => {
    if (!data) {
      return ''
    }

    if (typeof data === 'string') {
      return data
    }

    if (data?.returnCode && data.returnCode !== 1) {
      return ''
    }

    return (
      data?.message ||
      data?.answer ||
      data?.content ||
      data?.data?.message ||
      data?.data?.answer ||
      data?.data?.content ||
      ''
    )
  }

  const sendMessage = async () => {
    const trimmedPrompt = prompt.trim()
    if (!trimmedPrompt || isSending) {
      return
    }

    const updatedMessages = [...messages, { role: 'user', content: trimmedPrompt }]
    setMessages(updatedMessages)
    setPrompt('')
    setErrorMessage(null)
    setIsSending(true)

    try {
      const payload = {
        message: trimmedPrompt,
        history: updatedMessages.map((item) => ({ role: item.role, content: item.content })),
      }

      const { data } = await axios.post(`${import.meta.env.VITE_APIURL}${apiPath}`, payload)
      const assistantText = getAssistantText(data)

      setMessages((prev) => [
        ...prev,
        {
          role: 'assistant',
          content: assistantText || t('AiChatFallbackMessage'),
        },
      ])
    } catch (error) {
      setErrorMessage(error?.message || t('AiChatRequestError'))
    } finally {
      setIsSending(false)
    }
  }

  const onInputKeyDown = (event) => {
    if (event.key === 'Enter' && !event.shiftKey) {
      event.preventDefault()
      sendMessage()
    }
  }

  return (
    <CCard className="mb-4">
      <CCardHeader>{t('AiChatTitle')}</CCardHeader>
      <CCardBody>
        <p className="text-medium-emphasis">{t('AiChatDescription')}</p>

        <CListGroup className="mb-3" style={{ maxHeight: '280px', overflowY: 'auto' }}>
          {messages.map((item, index) => (
            <CListGroupItem key={`${item.role}-${index}`} className={item.role === 'user' ? 'text-end' : ''}>
              <strong>{item.role === 'user' ? t('AiChatUserLabel') : t('AiChatAssistantLabel')}:</strong>{' '}
              {item.content}
            </CListGroupItem>
          ))}
        </CListGroup>

        {errorMessage ? <CAlert color="warning">{errorMessage}</CAlert> : null}

        <div className="d-flex gap-2">
          <CFormInput
            value={prompt}
            placeholder={t('AiChatInputPlaceholder')}
            onChange={(event) => setPrompt(event.target.value)}
            onKeyDown={onInputKeyDown}
            disabled={isSending}
          />
          <CButton color="primary" onClick={sendMessage} disabled={isSending || !prompt.trim()}>
            {isSending ? <CSpinner size="sm" /> : t('AiChatSend')}
          </CButton>
        </div>
      </CCardBody>
    </CCard>
  )
}

export default AiChatPanel
