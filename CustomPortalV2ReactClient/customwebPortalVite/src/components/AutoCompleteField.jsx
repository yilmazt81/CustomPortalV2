import React, { useState } from 'react'
import PropTypes from 'prop-types'

import { Autocomplete, TextField } from '@mui/material'
import { CButton, CCol } from '@coreui/react'
import { cilMap, cilBarcode, cilUser } from '@coreui/icons'
import CIcon from '@coreui/icons-react'

import { FilterProduct, GetAutoComplateProduct } from '../lib/customProductapi.jsx'
import { FilterCompanyDefination, GetAutoComplateAdress } from '../lib/companyAdressDef.jsx'
import { FilterPersonel, GetAutoComplatePersonel } from '../lib/foodPersonelApi.jsx'

const filterConfig = {
    CompanyDefination: { api: FilterCompanyDefination, labelKey: 'companyName' },
    ProductDefination: { api: FilterProduct, labelKey: 'productName' },
    FoodPerson: { api: FilterPersonel, labelKey: 'fullName' }
}

const icons = {
    CompanyDefination: cilMap,
    ProductDefination: cilBarcode,
    FoodPerson: cilUser
}

const AutoCompleteField = ({
    tagName,
    autoComlateType,
    fieldId,
    value,
    onValueChange,
    onLoadControlData,
    onOpenModal,
    applyLabelOnSelect,
    placeholder
}) => {
    const [options, setOptions] = useState([])
    const [loading, setLoading] = useState(false)

    const handleInputChange = async (event, inputValue) => {
        if (onValueChange) {
            onValueChange(tagName, inputValue)
        }

        if (inputValue && inputValue.length > 2) {
            setLoading(true)
            try {
                const config = filterConfig[autoComlateType]
                const result = await config?.api({
                    formDefinationFieldId: fieldId,
                    filterValue: inputValue
                })
                if (result?.returnCode === 1) {
                    setOptions(
                        result.data.map((item) => ({
                            label: item[config.labelKey],
                            value: item.id
                        }))
                    )
                }
            } catch (error) {
                console.error('Autocomplete error:', error)
            } finally {
                setLoading(false)
            }
        }
    }

    const handleSelect = async (event, selected) => {
       
        if (!selected) {
            return
        }

        if (applyLabelOnSelect && onValueChange) {
         
           // onValueChange(tagName, selected.label)
        }

        if (autoComlateType === 'CompanyDefination') {
            const response = await GetAutoComplateAdress(fieldId, selected.value)
            if (response?.returnCode === 1) {
                onLoadControlData?.(response.data)
            }
        } else if (autoComlateType === 'ProductDefination') {
            const response = await GetAutoComplateProduct(fieldId, selected.value)
            if (response?.returnCode === 1) {
                onLoadControlData?.(response.data)
            }
        } else if (autoComlateType === 'FoodPerson') {
            const response = await GetAutoComplatePersonel(fieldId, selected.value)
            if (response?.returnCode === 1) {
                onLoadControlData?.(response.data)
            }
        }
    }

    return (
        <>
            <CCol sm={6}> 
                <Autocomplete
                    freeSolo
                    options={options}
                    loading={loading}
                    inputValue={value}
                    onInputChange={handleInputChange}
                    onChange={handleSelect}
                    getOptionLabel={(option) => option.label || option}
                    renderInput={(params) => (
                        <TextField
                            {...params}
                            name={tagName}
                            id={`txt${tagName}`}
                            inputProps={{
                                ...params.inputProps,
                                id: `txt${tagName}`
                            }}
                            placeholder={placeholder}
                            size="small"
                        />
                    )}
                />
            </CCol>
            <CCol sm={3}>
                <CButton
                    color="primary"
                    onClick={() => onOpenModal?.(autoComlateType, fieldId)}
                >
                    {icons[autoComlateType] ? <CIcon icon={icons[autoComlateType]} /> : null}
                </CButton>
            </CCol>
        </>
    )
}

AutoCompleteField.propTypes = {
    tagName: PropTypes.string.isRequired,
    autoComlateType: PropTypes.string.isRequired,
    fieldId: PropTypes.number.isRequired,
    value: PropTypes.string,
    onValueChange: PropTypes.func,
    onLoadControlData: PropTypes.func,
    onOpenModal: PropTypes.func,
    applyLabelOnSelect: PropTypes.bool,
    placeholder: PropTypes.string
}

AutoCompleteField.defaultProps = {
    value: '',
    onValueChange: null,
    onLoadControlData: null,
    onOpenModal: null,
    applyLabelOnSelect: true,
    placeholder: 'Yazin...'
}

export default AutoCompleteField
