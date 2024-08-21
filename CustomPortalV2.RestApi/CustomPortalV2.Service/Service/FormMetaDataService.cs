using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Business.Helper;
using CustomPortalV2.Core.Model.App;
using CustomPortalV2.Core.Model.Company;
using CustomPortalV2.Core.Model.Definations;
using CustomPortalV2.Core.Model.DTO;
using CustomPortalV2.Core.Model.FDefination;
using CustomPortalV2.Core.Model.Form;
using CustomPortalV2.DataAccessLayer.Concrete;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Business.Service
{
    public class FormMetaDataService : IFormMetaDataService
    {
        IFormMetaDataRepository _formMetaDataRepository;
        IBranchRepository _branchRepository;
        IFormDefinationRepository _formDefinationRepository;
        public FormMetaDataService(IFormMetaDataRepository formMetaDataRepository,
                    IBranchRepository branchRepository, IFormDefinationRepository formDefinationRepository)
        {
            _formMetaDataRepository = formMetaDataRepository;
            _branchRepository = branchRepository;
            _formDefinationRepository = formDefinationRepository;
        }
        public DefaultReturn<List<FormMetaData>> FilterForms(FormMetaDataFilterPost formMetaData)
        {
            throw new NotImplementedException();
        }

        public DefaultReturn<List<FormMetaData>> GetBranchFormMetaData(int companyId, int brachId)
        {
            DefaultReturn<List<FormMetaData>> defaultReturn = new DefaultReturn<List<FormMetaData>>();
            var branch = _branchRepository.Get(s => s.Id == brachId);

            if (branch.CompanyAdmin)
            {
                defaultReturn.Data = _formMetaDataRepository.Get(s => s.MainCompanyId == companyId && !s.Deleted, 50).ToList().OrderBy(s => s.Id).ToList();
            }
            else
            {
                defaultReturn.Data = _formMetaDataRepository.Get(s => s.MainCompanyId == companyId && s.CompanyBranchId == brachId && !s.Deleted, 50).ToList().OrderBy(s => s.Id).ToList();
            }


            return defaultReturn;

        }

        public DefaultReturn<FormMetaData> GetCompanyFormMetaData(int companyId, int branchId, int id)
        {

            DefaultReturn<FormMetaData> defaultReturn = new DefaultReturn<FormMetaData>();
            try
            {

                var branch = _branchRepository.Get(s => s.Id == branchId);
                var formMetaData = _formMetaDataRepository.Get(id);
                if (formMetaData == null)
                {
                    throw new Exception("FormNotFound");
                }
                if (formMetaData.MainCompanyId != companyId)
                {
                    throw new Exception("DocumentISNotInYourCompany");
                }
                if (!branch.CompanyAdmin)
                {
                    if (branch.Id != formMetaData.CompanyBranchId)
                    {
                        throw new Exception("DocumentISNotBelongToyou");
                    }
                }

                defaultReturn.Data = formMetaData;

            }
            catch (Exception ex)
            {
                defaultReturn.SetException(ex);
            }

            return defaultReturn;
        }

        public DefaultReturn<FormMetaData> GetFormMetaData(User user, int id)
        {
            throw new NotImplementedException();
        }
        private bool ChechPostValues(List<FormGroup> formGroups, List<FormDefinationField> documentTypeFields, FormMetaDataDTO formfield, ref string messages)
        {
            bool returnType = false;

            string mandatoryReturnText = string.Empty;

            foreach (var formGroup in formGroups)
            {

                string groupEmpy = string.Empty;

                foreach (var formElement in documentTypeFields.Where(s => s.FormGroupId == formGroup.Id && !s.Deleted && s.Mandatory))
                {
                    if (formElement.ControlType == "Text" || formElement.ControlType == "DateTime")
                    {

                        var formValue = formfield.fieldValues.FirstOrDefault(s => s.fieldName == formElement.TagName);
                        if (formValue == null)
                        {
                            groupEmpy += $"<b> {formElement.FieldCaption}</b>,";
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(formValue.fieldValue.Replace('_', ' ').Replace('.', ' ').Trim()))
                            {
                                groupEmpy += $"<b> {formElement.FieldCaption}</b>,";
                            }
                        }
                    }
                    else if (formElement.ControlType == "CheckBox" || formElement.ControlType == "RadioBox")
                    {
                        if (!formfield.fieldValues.Any(s => s.fieldName.StartsWith(formElement.TagName) && s.fieldValue == "true"))
                        {
                            groupEmpy += $"<b> {formElement.FieldCaption}</b>,";
                        }
                    }
                }

                if (!string.IsNullOrEmpty(groupEmpy))
                {
                    mandatoryReturnText += $" Group Adı {formGroup.Name} [ {groupEmpy} ]";
                }
            }

            if (!string.IsNullOrEmpty(mandatoryReturnText))
            {
                messages = $"{mandatoryReturnText}<br> Alanları boş geçilemez";

                return false;
            }

            foreach (var formElement in documentTypeFields)
            {
                var controlName = formElement.GetControlName();

                var formValue = formfield.fieldValues.FirstOrDefault(s => s.fieldName == controlName);
                if (formValue != null && formValue.fieldValue != null && formValue.fieldValue.IsSaveSqlInjection())
                {
                    mandatoryReturnText += $"<b> {formElement.FieldCaption}</b>,";
                }
            }

            if (!string.IsNullOrEmpty(mandatoryReturnText))
            {
                messages = $"{mandatoryReturnText}<br> Zararlı giriş tespit edildi";
                return false;
            }


            return true;
        }

        private Dictionary<string, string> GetFieldValue(FormDefinationField formDefinationField, FormMetaDataDTO formMetaDataDTO)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            if (formDefinationField.ControlType == "Text" || formDefinationField.ControlType == "DateTime" || formDefinationField.ControlType == "Hidden" || formDefinationField.ControlType == "ComboBox")
            {
                var controlValue = formMetaDataDTO.fieldValues.FirstOrDefault(s => s.fieldName == formDefinationField.TagName);

                keyValuePairs.Add(formDefinationField.TagName, controlValue == null ? string.Empty : controlValue.fieldValue);
            }
            else if (formDefinationField.ControlType == "CheckBox" || formDefinationField.ControlType == "RadioBox")
            {
                var comboboxItems = _formDefinationRepository.GetComboBoxItems(formMetaDataDTO.CompanyId, formDefinationField.TagName);

                foreach (var comboBoxItem in comboboxItems)
                {
                    string chechControlOptionName = formDefinationField.TagName + "_" + comboBoxItem.TagName;
                    var checkBoxValue = formMetaDataDTO.fieldValues.FirstOrDefault(s => s.fieldName == chechControlOptionName);
                    if (checkBoxValue != null)
                    {
                        keyValuePairs.Add(chechControlOptionName, checkBoxValue == null ? "false" : checkBoxValue.fieldValue);
                    }
                }

            }

            return keyValuePairs;

        }

        private void SetMetaObjectField(FormMetaData formmetaData)
        {
            foreach (var attribute in formmetaData.FormMetaDataAttribute)
            {


                if (attribute.TagName == "GonderenAdi")
                {
                    formmetaData.SenderCompanyName = attribute.FieldValue;
                }
                if (attribute.TagName == "AliciAdi")
                {
                    formmetaData.RecrivedCompanyName = attribute.FieldValue;
                }


                if (attribute.TagName == "SenderCompanyId")
                {
                    formmetaData.SenderCompanyId = (string.IsNullOrEmpty(attribute.FieldValue) ? 0 : int.Parse(attribute.FieldValue));
                }
                if (attribute.TagName == "RecrivedCompanyId")
                {
                    formmetaData.RecrivedCompanyId = (string.IsNullOrEmpty(attribute.FieldValue) ? 0 : int.Parse(attribute.FieldValue));
                }
            }
        }
        public DefaultReturn<FormMetaData> Save(FormMetaDataDTO formMetaDataDTO)
        {
            DefaultReturn<FormMetaData> defaultReturn = new DefaultReturn<FormMetaData>();

            try
            {

                var companyBranch = _branchRepository.Get(s => s.Id == formMetaDataDTO.BrachId);
                var definationType = _formDefinationRepository.Get(s => s.Id == formMetaDataDTO.formDefinationTypeid).FirstOrDefault();
                if (companyBranch == null)
                {
                    throw new Exception("BranchIsNotFoud");
                }
                if (definationType == null)
                {
                    throw new Exception("DefinationNotFount");
                }
                if (definationType.MainCompanyId != formMetaDataDTO.CompanyId)
                {
                    throw new Exception("ThisDefinationIsNotBelongToYourCompany");
                }

                var fieldGroups = _formDefinationRepository.GetFormGroups(definationType.Id).ToList();
                var definationFields = _formDefinationRepository.GetAllFields(definationType.Id).ToList();
                string returnMessage = string.Empty;
                var checkFieldValues = ChechPostValues(fieldGroups, definationFields, formMetaDataDTO, ref returnMessage);
                if (!checkFieldValues)
                {
                    throw new Exception(returnMessage);
                }
                var formmetaData = new FormMetaData()
                {
                    FormDefinationId = formMetaDataDTO.formDefinationTypeid,
                    CompanyBranchId = formMetaDataDTO.BrachId,
                    MainCompanyId = formMetaDataDTO.CompanyId,
                    CreatedId = formMetaDataDTO.UserId,
                    CreatedBy = formMetaDataDTO.UserName,
                    CreatedDate = DateTime.Now,
                    CustomSectorId = definationType.CustomSectorId,
                    BrancName = companyBranch.Name,
                    FormDefinationName = definationType.FormName,
                    Deleted = false,
                    DefaultForm = formMetaDataDTO.isDefault,
                    CustomWorkId = formMetaDataDTO.workid,
                    Id = formMetaDataDTO.id,
                };
                if (formmetaData.Id != 0)
                {
                    formmetaData.EditedBy = formMetaDataDTO.UserName;
                    formmetaData.EditedId = formMetaDataDTO.UserId;
                    formmetaData.EditedDate = DateTime.Now; 

                }
                formmetaData.FormMetaDataAttribute = new List<FormMetaDataAttribute>();
                foreach (var formDefinationField in definationFields)
                {

                    var fieldValues = GetFieldValue(formDefinationField, formMetaDataDTO);
                    foreach (var field in fieldValues.Keys)
                    {
                        var formdefinationField = definationFields.FirstOrDefault(s => s.TagName == field);
                        formmetaData.FormMetaDataAttribute.Add(new FormMetaDataAttribute()
                        {
                            FieldValue = fieldValues[field],
                            TagName = field,
                            FormDefinationFieldId = formDefinationField.Id,
                        });
                    }
                }
                SetMetaObjectField(formmetaData);
                if (formmetaData.Id == 0)
                {
                    defaultReturn.Data = _formMetaDataRepository.Save(formmetaData);

                }
                else
                {
                    defaultReturn.Data = _formMetaDataRepository.Update(formmetaData);

                }


            }
            catch (Exception ex)
            {
                defaultReturn.SetException(ex);
            }

            return defaultReturn;
        }

        public DefaultReturn<FormConvertContainerDTO> GetFormConvertList(int id, int mainCompanyId)
        {
            DefaultReturn<FormConvertContainerDTO> defaultReturn = new DefaultReturn<FormConvertContainerDTO>();

            try
            {
                var formData=_formMetaDataRepository.Get(id);
                
                if (formData.MainCompanyId != mainCompanyId)
                {
                    throw new Exception("CompanyNameIsNotSame");
                }

               var definationList= _formDefinationRepository.GetDefinationVersions(formData.FormDefinationId);

                defaultReturn.Data = new FormConvertContainerDTO();
                defaultReturn.Data.FormVersions = definationList.ToArray();



            }
            catch (Exception ex)
            {
                defaultReturn.SetException(ex);
            }

            return defaultReturn;
        }
    }
}
