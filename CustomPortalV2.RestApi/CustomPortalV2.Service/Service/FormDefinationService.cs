using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Business.Helper;
using CustomPortalV2.Core.Model.Autocomplete;
using CustomPortalV2.Core.Model.Company;
using CustomPortalV2.Core.Model.Definations;
using CustomPortalV2.Core.Model.DTO;
using CustomPortalV2.Core.Model.FDefination;
using CustomPortalV2.Core.Model.Form;
using CustomPortalV2.DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Business.Service
{
    public class FormDefinationService : IFormDefinationService
    {
        IFormDefinationRepository _formDefinationService;
        IAppLangRepository _appLangRepository;
        public FormDefinationService(IFormDefinationRepository formDefinationRepository, IAppLangRepository appLangRepository)
        {
            _formDefinationService = formDefinationRepository;
            _appLangRepository = appLangRepository;
        }

        public DefaultReturn<bool> DeleteGroup(int formDefinationTypeId, int groupid, int companyId)
        {
            DefaultReturn<bool> defaultReturn = new DefaultReturn<bool>();
            try
            {
                var formDefination = _formDefinationService.Get(s => s.Id == formDefinationTypeId).FirstOrDefault();

                if (formDefination == null)
                {
                    throw new Exception("FormDefinationNotFount");
                }

                if (formDefination.MainCompanyId != companyId)
                {
                    throw new Exception("CompanyDiffrend");
                }

                var group = GetFormGroup(groupid);
                group.Data.Deleted = true;
                _formDefinationService.UpdateGroup(group.Data);
            }
            catch (Exception ex)
            {
                defaultReturn.SetException(ex);
            }

            return defaultReturn;
        }

        public DefaultReturn<AutocompleteField> GetAutoComplateField(int formdefinationFieldId)
        {
            DefaultReturn<AutocompleteField> defaultReturn = new DefaultReturn<AutocompleteField>();
            try
            {
                var autoComplate = _formDefinationService.GetAutoComplateField(formdefinationFieldId);

                if (autoComplate == null)
                {
                    var fieldName = GetFormDefinationField(formdefinationFieldId);
                    autoComplate = new AutocompleteField()
                    {
                        FormDefinationFieldId = formdefinationFieldId,
                        ComplateObject = "CompanyDefination",
                        FieldName = fieldName.Data.TagName,
                        FilterValue = "",
                        RelationalFieldName = "",
                    };
                }

                defaultReturn.Data = autoComplate;
            }
            catch (Exception ex)
            {
                defaultReturn.SetException(ex);
            }

            return defaultReturn;
        }

        public DefaultReturn<List<AutocompleteFieldMap>> GetAutoComplateFieldMaps(int formDefinationFieldid)
        {
            DefaultReturn<List<AutocompleteFieldMap>> defaultReturn = new DefaultReturn<List<AutocompleteFieldMap>>();

            try
            {
                defaultReturn.Data = _formDefinationService.GetAutoComplateFieldMaps(formDefinationFieldid);
            }
            catch (Exception ex)
            {
                defaultReturn.SetException(ex);
            }

            return defaultReturn;
        }

        public DefaultReturn<List<ComboBoxItem>> GetComboBoxItems(int mainCompanyId, string tagName)
        {
            DefaultReturn<List<ComboBoxItem>> defaultReturn = new DefaultReturn<List<ComboBoxItem>>();
            try
            {
                defaultReturn.Data = _formDefinationService.GetComboBoxItems(mainCompanyId, tagName).ToList();
            }
            catch (Exception ex)
            {
                defaultReturn.SetException(ex);
            }

            return defaultReturn;
        }

        public DefaultReturn<List<FormDefination>> GetCompanyDefination(int mainCompanyId, int sectorId)
        {
            DefaultReturn<List<FormDefination>> defaultReturn = new DefaultReturn<List<FormDefination>>();

            defaultReturn.Data = _formDefinationService.Get(s => s.MainCompanyId == mainCompanyId && s.CustomSectorId == sectorId && s.Deployed).ToList();
            return defaultReturn;
        }

        public DefaultReturn<List<FormDefination>> GetCompanyDefinations(int mainCompanyId)
        {
            DefaultReturn<List<FormDefination>> defaultReturn = new DefaultReturn<List<FormDefination>>();

            defaultReturn.Data = _formDefinationService.Get(s => s.MainCompanyId == mainCompanyId && !s.Deleted).ToList();


            return defaultReturn;
        }

        public DefaultReturn<List<FieldType>> GetFielTypes(int companyId)
        {
            DefaultReturn<List<FieldType>> defaultReturn = new DefaultReturn<List<FieldType>>();

            defaultReturn.Data = _formDefinationService.GetDefaultFieldTypes().ToList();

            return defaultReturn;
        }

        public DefaultReturn<List<FontType>> GetFontTypes()
        {
            DefaultReturn<List<FontType>> defaultReturn = new DefaultReturn<List<FontType>>();

            defaultReturn.Data = _formDefinationService.GetFontTypes().ToList();
            return defaultReturn;
        }

        public DefaultReturn<FormDefination> GetFormDefination(int id)
        {
            DefaultReturn<FormDefination> defaultReturn = new DefaultReturn<FormDefination>();
            try
            {
                defaultReturn.Data = _formDefinationService.Get(s => s.Id == id).FirstOrDefault();

            }
            catch (Exception ex)
            {
                defaultReturn.SetException(ex);
            }

            return defaultReturn;
        }

        public DefaultReturn<FormDefinationField> GetFormDefinationField(int id)
        {
            DefaultReturn<FormDefinationField> defaultReturn = new DefaultReturn<FormDefinationField>();

            try
            {
                defaultReturn.Data = _formDefinationService.GetFormDefinationField(id);

            }
            catch (Exception ex)
            {
                defaultReturn.SetException(ex);
            }

            return defaultReturn;

        }
        private FormDefinationField GetFormDefinationField(int definationFieldId, string tagName)
        {
            return _formDefinationService.GetDefinationField(definationFieldId, tagName);
        }

        public DefaultReturn<List<FormDefinationField>> GetFormDefinationFields(int formgroupId)
        {
            DefaultReturn<List<FormDefinationField>> defaultReturn = new DefaultReturn<List<FormDefinationField>>();

            defaultReturn.Data = _formDefinationService.GetFormGroupFields(formgroupId).ToList();
            return defaultReturn;
        }

        public DefaultReturn<FormGroup> GetFormGroup(int id)
        {
            DefaultReturn<FormGroup> defaultReturn = new DefaultReturn<FormGroup>();
            defaultReturn.Data = _formDefinationService.GetFormGroup(id);

            return defaultReturn;
        }

        public DefaultReturn<List<GroupDTO>> GetFormGroupDTOs(int formDefinationId)
        {
            DefaultReturn<List<GroupDTO>> defaultReturn = new DefaultReturn<List<GroupDTO>>();
            var getFormGroups = GetFormGroups(formDefinationId);
            var formDefination = GetFormDefination(formDefinationId);
            defaultReturn.Data = new List<GroupDTO>();
            foreach (var group in getFormGroups.Data)
            {
                GroupDTO groupDTO = new GroupDTO()
                {
                    Name = group.Name,
                    FormNumber = group.FormNumber,
                    GroupTag = group.GroupTag,
                    id = group.Id,
                };
                groupDTO.FormFields = new List<DefinationFieldDTO>();
                var groupFields = GetFormDefinationFields(group.Id);

                foreach (var oneField in groupFields.Data)
                {
                    DefinationFieldDTO definationFieldDTO = new DefinationFieldDTO(oneField);
                    if (definationFieldDTO.ControlType == "ComboBox" || definationFieldDTO.ControlType == "CheckBox" || definationFieldDTO.ControlType == "RadioBox")
                    {
                        var comboitems = GetComboBoxItems(formDefination.Data.MainCompanyId, definationFieldDTO.TagName);

                        definationFieldDTO.ComboBoxItems = comboitems.Data;
                    }

                    groupDTO.FormFields.Add(definationFieldDTO);
                }
                defaultReturn.Data.Add(groupDTO);
            }

            return defaultReturn;
        }

        public DefaultReturn<List<FormGroup>> GetFormGroups(int formDefinationId)
        {
            DefaultReturn<List<FormGroup>> defaultReturn = new DefaultReturn<List<FormGroup>>();

            defaultReturn.Data = _formDefinationService.GetFormGroups(formDefinationId).ToList();
            return defaultReturn;
        }


        public DefaultReturn<List<ObjectFieldDTO>> GetObjectFieldList(string objectName, int userLangId)
        {
            DefaultReturn<List<ObjectFieldDTO>> defaultReturn = new DefaultReturn<List<ObjectFieldDTO>>();
            defaultReturn.Data = new List<ObjectFieldDTO>();

            if (objectName == "CompanyDefination")
            {
                CompanyDefination companyDefination = new CompanyDefination();

                foreach (var prop in companyDefination.GetType().GetProperties())
                {
                    var caption = _appLangRepository.Get($"CompanyDefination_{prop.Name}", userLangId, prop.Name);
                    defaultReturn.Data.Add(new ObjectFieldDTO(prop.Name, caption));
                }
            }
            else if (objectName == "ProductDefination")
            {
                CustomProduct product = new CustomProduct();
                foreach (var prop in product.GetType().GetProperties())
                {
                    var caption = _appLangRepository.Get($"ProductDefination_{prop.Name}", userLangId, prop.Name);
                    defaultReturn.Data.Add(new ObjectFieldDTO(prop.Name, caption));
                }
            }
            else if (objectName == "Country")
            {
                Country country = new Country();
                foreach (var prop in country.GetType().GetProperties())
                {
                    var caption = _appLangRepository.Get($"Country_{prop.Name}", userLangId, prop.Name);
                    defaultReturn.Data.Add(new ObjectFieldDTO(prop.Name, caption));
                }
            }
            else
            {
                AutoComplateDefination autocompleteField = new AutoComplateDefination();

                foreach (var prop in autocompleteField.GetType().GetProperties())
                {
                    var caption = _appLangRepository.Get($"AutoComplate_{prop.Name}", userLangId, prop.Name);
                    defaultReturn.Data.Add(new ObjectFieldDTO(prop.Name, caption));
                }

            }
            return defaultReturn;
        }

        public DefaultReturn<List<CustomSectorDTO>> GetSector(int mainCompanyId, int applicationLangId)
        {
            DefaultReturn<List<CustomSectorDTO>> defaultReturn = new DefaultReturn<List<CustomSectorDTO>>();
            var sectorList = _formDefinationService.GetCompanySectors(mainCompanyId);
            defaultReturn.Data = new List<CustomSectorDTO>();

            foreach (var sector in sectorList)
            {
                var translateText = _appLangRepository.Get($"CustomSector{sector.Name.ToTrktoING()}", applicationLangId, sector.Name);
                defaultReturn.Data.Add(new CustomSectorDTO() { id = sector.Id, Name = translateText });
            }

            return defaultReturn;
        }

        public DefaultReturn<FormDefination> Save(FormDefination formDefination)
        {
            DefaultReturn<FormDefination> defaultReturn = new DefaultReturn<FormDefination>();
            try
            {
                var sectorList = _formDefinationService.GetCompanySectors(formDefination.MainCompanyId);
                var sector = sectorList.First(s => s.Id == formDefination.CustomSectorId);
                formDefination.CustomSectorName = sector.Name;

                if (formDefination.Id == 0)
                {
                    defaultReturn.Data = _formDefinationService.Add(formDefination);
                }
                else
                {
                    defaultReturn.Data = _formDefinationService.Update(formDefination);
                }
            }
            catch (Exception ex)
            {
                defaultReturn.SetException(ex);
            }

            return defaultReturn;
        }



        public DefaultReturn<ComboBoxItem> Save(ComboBoxItem comboBoxItem)
        {
            DefaultReturn<ComboBoxItem> defaultReturn = new DefaultReturn<ComboBoxItem>();

            try
            {
                if (_formDefinationService.IsExistComboboxItem(comboBoxItem))
                {
                    throw new Exception("ComboItemAlreadyExist");
                }
                defaultReturn.Data = _formDefinationService.AddComboboxItem(comboBoxItem);

            }
            catch (Exception ex)
            {
                defaultReturn.SetException(ex);
            }


            return defaultReturn;

        }

        public DefaultReturn<AutocompleteFieldMap> SaveAutoComplate(SaveAutoComplateDTO saveAutoComplate)
        {
            DefaultReturn<AutocompleteFieldMap> defaultReturn = new DefaultReturn<AutocompleteFieldMap>();
            try
            {
                var mainFieldType = GetFormDefinationField(saveAutoComplate.Complate.FormDefinationFieldId);
                saveAutoComplate.Complate.FieldName = mainFieldType.Data.TagName;
                mainFieldType.Data.AutoComlateType = saveAutoComplate.Complate.ComplateObject;
                _formDefinationService.UpdateDefinationField(mainFieldType.Data);
                if (saveAutoComplate.Complate.Id == 0)
                {
                    _formDefinationService.Add(saveAutoComplate.Complate);
                }
                else
                {
                    _formDefinationService.Update(saveAutoComplate.Complate);
                }

                var mapFieldName = GetFormDefinationField(mainFieldType.Data.FormDefinationId, saveAutoComplate.Map.TagName);
                saveAutoComplate.Map.FieldCaption = mapFieldName.FieldCaption;
                if (saveAutoComplate.Map.Id == 0)
                {
                    defaultReturn.Data = _formDefinationService.Add(saveAutoComplate.Map);
                }
                else
                {

                    defaultReturn.Data = _formDefinationService.Update(saveAutoComplate.Map);
                }

            }
            catch (Exception ex)
            {

                defaultReturn.SetException(ex);
            }


            return defaultReturn;
        }

        public DefaultReturn<FormDefinationField> SaveFormDefinationField(FormDefinationField formDefinationField)
        {
            DefaultReturn<FormDefinationField> defaultReturn = new DefaultReturn<FormDefinationField>();
            try
            {
                if (formDefinationField.Id == 0)
                {
                    if (_formDefinationService.IsExistFormDefinationField(formDefinationField.FormDefinationId, formDefinationField.TagName))
                    {
                        throw new Exception("FieldAllradyExist");
                    }
                    defaultReturn.Data = _formDefinationService.AddDefinationField(formDefinationField);

                }
                else
                {
                    defaultReturn.Data = _formDefinationService.UpdateDefinationField(formDefinationField);

                }
            }
            catch (Exception ex)
            {
                defaultReturn.SetException(ex);
            }

            return defaultReturn;
        }



        public DefaultReturn<FormGroup> SaveGroup(FormGroup formGroup)
        {
            DefaultReturn<FormGroup> defaultReturn = new DefaultReturn<FormGroup>();

            if (formGroup.Id == 0)
            {
                defaultReturn.Data = _formDefinationService.AddGroup(formGroup);
            }
            else
            {
                defaultReturn.Data = _formDefinationService.UpdateGroup(formGroup);
            }

            return defaultReturn;
        }

        public DefaultReturn<bool> DeleteAutoComplate(int autoComplateFieldId)
        {
            DefaultReturn<bool> defaultReturn = new DefaultReturn<bool>();
            defaultReturn.Data = _formDefinationService.DeleteAutoComplateFieldMap(autoComplateFieldId);


            return defaultReturn;
        }

        public DefaultReturn<string> GetTemplateForm(int formdefinationId, int companyId)
        {
            DefaultReturn<string> defaultReturn = new DefaultReturn<string>();

            try
            {
                var formDefination = _formDefinationService.Get(s => s.Id == formdefinationId && s.MainCompanyId == companyId).FirstOrDefault();
                if (formDefination == null)
                {
                    throw new Exception("FormDefinationNotFount");
                }

                var formGroups = _formDefinationService.GetFormGroups(formdefinationId);
                string templateFile = string.Empty;
                using (WebClient webClient = new())
                {
                    var templateArray = webClient.DownloadData(formDefination.TemplatePath);
                    templateFile = System.Text.Encoding.Default.GetString(templateArray);
                }
                defaultReturn.Data = templateFile;
                foreach (var oneGroup in formGroups)
                {
                    defaultReturn.Data = defaultReturn.Data.Replace($"@Group_{oneGroup.GroupTag}@", oneGroup.FormNumber + " " + oneGroup.Name);

                    var formFields = _formDefinationService.GetFormGroupFields(oneGroup.Id);

                    foreach (var oneField in formFields.Where(s => !s.Deleted))
                    {
                        string labelTag = $"@Label_{oneField.TagName.Trim()}@";
                        defaultReturn.Data = defaultReturn.Data.Replace(labelTag, oneField.FieldCaption);

                        if (oneField.ControlType == "Text")
                        {
                            string controlHtml = $"<input type=\"text\"   id=\"{oneField.GetControlName()}\"  name=\"{oneField.TagName}\" />";
                            defaultReturn.Data = defaultReturn.Data.Replace($"@input_{oneField.TagName}@", controlHtml);


                        }
                        if (oneField.ControlType == "Hidden")
                        {
                            string controlHtml = $"<input type=\"hidden\"   id=\"{oneField.GetControlName()}\"  name=\"{oneField.TagName}\" />";
                            defaultReturn.Data = defaultReturn.Data + controlHtml;

                        }
                        else if (oneField.ControlType == "DateTime")
                        {
                            string controlHtml = $"<input  type=\"date\" id=\"{oneField.GetControlName()}\"   name=\"{oneField.TagName}\" />";
                            defaultReturn.Data = defaultReturn.Data.Replace($"@input_{oneField.TagName}@", controlHtml);
                        }
                        else if (oneField.ControlType == "ComboBox")
                        {
                            var checkItems = _formDefinationService.GetComboBoxItems(companyId, oneField.TagName).ToList();

                            string justControl = $"<select   name='{oneField.TagName}' id='{oneField.GetControlName()}'>";
                            foreach (var oneItem in checkItems)
                            {
                                var option = $"<option value=\"{oneItem.TagName}\">{oneItem.Name}</option>";
                                justControl += option;

                            }
                            justControl += "</select></div>";

                            defaultReturn.Data = defaultReturn.Data.Replace($"@input_{oneField.TagName}@", justControl);

                        }
                        else if (oneField.ControlType == "RadioBox" || oneField.ControlType == "CheckBox")
                        {
                            var checkItems = _formDefinationService.GetComboBoxItems(companyId, oneField.TagName).ToList();
                            string ControlType = oneField.ControlType == "CheckBox" ? "checkbox" : "radio";
                            string justControl = "";
                            foreach (var boxItem in checkItems)
                            {
                                justControl += $"<input  name='{oneField.TagName}' type='" + ControlType + $"' id='{oneField.GetControlName()}_{oneField.TagName}' value='{oneField.TagName}_{boxItem.TagName}'  caption=\"{boxItem.Name}\"  >";
                            }

                            defaultReturn.Data = defaultReturn.Data.Replace($"@input_{oneField.TagName}@", justControl);
                        }

                        if (oneField.AutoComplate)
                        {
                            string htmlButton = $"<div  datacontent=\"{oneField.AutoComlateType}\" id=\"{oneField.Id}\" data=\"{oneField.Id}\"  type=\"button\">...</div>";
                            defaultReturn.Data = defaultReturn.Data.Replace($"@autoComplate_{oneField.TagName}@", htmlButton);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                defaultReturn.SetException(ex);
            }
            return defaultReturn;
        }

        public DefaultReturn<List<FormVersion>> GetDefinationFormVersions(int formdefinationId)
        {
            DefaultReturn<List<FormVersion>> defaultReturn = new DefaultReturn<List<FormVersion>>();

            defaultReturn.Data = _formDefinationService.GetDefinationVersions(formdefinationId).ToList();


            return defaultReturn;
        }

        public DefaultReturn<FormVersion> Save(FormVersion formDefination)
        {
            DefaultReturn<FormVersion> defaultReturn = new DefaultReturn<FormVersion>();

            if (formDefination.Id == 0)
            {
                defaultReturn.Data = _formDefinationService.Add(formDefination);
            }
            else
            {
                defaultReturn.Data = _formDefinationService.Update(formDefination);
            }

            return defaultReturn;
        }

        public DefaultReturn<FormVersion> GetFormDefinationVersion(int id)
        {
            DefaultReturn<FormVersion> defaultReturn = new DefaultReturn<FormVersion>();

            defaultReturn.Data = _formDefinationService.GetFormDefinationVersion(id);
            return defaultReturn;
        }

        public DefaultReturn<List<FormDefinationAttachment>> GetDefinationFormAttachments(int formdefinationId)
        {
            DefaultReturn<List<FormDefinationAttachment>> defaultReturn = new DefaultReturn<List<FormDefinationAttachment>>();

            defaultReturn.Data = _formDefinationService.GetFormDefinationAttachments(formdefinationId).ToList();
            return defaultReturn;
        }

        public DefaultReturn<FormDefinationAttachment> GetFormDefinationAttachment(int id)
        {
            DefaultReturn<FormDefinationAttachment> defaultReturn = new DefaultReturn<FormDefinationAttachment>();

            defaultReturn.Data = _formDefinationService.GetFormDefinationAttachment(id);
            return defaultReturn;
        }

        public DefaultReturn<FormDefinationAttachment> Save(FormDefinationAttachment formDefinationAttachment, string[] fielTags)
        {
            DefaultReturn<FormDefinationAttachment> defaultReturn = new DefaultReturn<FormDefinationAttachment>();

            var fieldList = _formDefinationService.GetAllFields(formDefinationAttachment.FormDefinationId);

            if (formDefinationAttachment.Id == 0)
            {
                defaultReturn.Data = _formDefinationService.Add(formDefinationAttachment);
                foreach (var oneTag in fielTags)
                {
                    var field = fieldList.FirstOrDefault(s => s.TagName == oneTag);
                    _formDefinationService.Add(new FormAttachmentFontStyle()
                    {
                        Bold = formDefinationAttachment.Bold,
                        FontSize = formDefinationAttachment.FontSize,
                        Italic = formDefinationAttachment.Italic,
                        TagName = oneTag,
                        FieldCaption = (field == null ? oneTag : field.FieldCaption),
                        FontFamily = formDefinationAttachment.FontFamily,
                        FormDefinationAttachmentId = defaultReturn.Data.Id
                    });
                }

            }
            else
            {
                defaultReturn.Data = _formDefinationService.Update(formDefinationAttachment);

                foreach (var oneTag in fielTags)
                {
                    if (!_formDefinationService.IsExistAttachmentFontStype(formDefinationAttachment.Id, oneTag))
                    {
                        var field = fieldList.FirstOrDefault(s => s.TagName == oneTag);
                        _formDefinationService.Add(new FormAttachmentFontStyle()
                        {
                            Bold = formDefinationAttachment.Bold,
                            FontSize = formDefinationAttachment.FontSize,
                            Italic = formDefinationAttachment.Italic,
                            TagName = oneTag,
                            FieldCaption = (field == null ? oneTag : field.FieldCaption),
                            FontFamily = formDefinationAttachment.FontFamily,
                            FormDefinationAttachmentId = defaultReturn.Data.Id
                        });
                    }
                }
            }

            return defaultReturn;



        }

        public DefaultReturn<List<CustomeField>> GetCustomeFields(int mainCompanyId)
        {
            DefaultReturn<List<CustomeField>> defaultReturn = new DefaultReturn<List<CustomeField>>();

            defaultReturn.Data = _formDefinationService.GetCustomeFields(mainCompanyId).ToList();
            return defaultReturn;
        }

        public DefaultReturn<CustomeField> SaveCustomeField(CustomeField customeField)
        {
            DefaultReturn<CustomeField> defaultReturn = new DefaultReturn<CustomeField>();
            try
            {

                if (customeField.Id == 0)
                {
                    defaultReturn.Data = _formDefinationService.Add(customeField);
                }
                else
                {
                    defaultReturn.Data = _formDefinationService.Update(customeField);
                }
            }
            catch (Exception ex)
            {
                defaultReturn.SetException(ex);
            }

            return defaultReturn;
        }

        public DefaultReturn<List<CustomeFieldItem>> GetCustomeFieldItems(int customeFieldId)
        {
            DefaultReturn<List<CustomeFieldItem>> defaultReturn = new DefaultReturn<List<CustomeFieldItem>>();

            defaultReturn.Data = _formDefinationService.GetCustomeFieldItems(customeFieldId).ToList();

            return defaultReturn;
        }
    }
}
