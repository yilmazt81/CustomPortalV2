using CustomPortalV2.Core.Model.App;
using CustomPortalV2.Core.Model.Autocomplete;
using CustomPortalV2.Core.Model.FDefination;
using CustomPortalV2.DataAccessLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.DataAccessLayer.Repository
{
    public class FormDefinationRepository : IFormDefinationRepository
    {
        DBContext _dbContext;
        public FormDefinationRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public FormDefination Add(FormDefination formDefination)
        {
            formDefination.CreatedDate = DateTime.Now;
            _dbContext.FormDefination.Add(formDefination);

            _dbContext.SaveChanges();

            return formDefination;
        }

        public ComboBoxItem AddComboboxItem(ComboBoxItem comboBoxItem)
        {
            _dbContext.ComboBoxItem.Add(comboBoxItem);
            _dbContext.SaveChanges();

            return comboBoxItem;


        }

        public bool IsExistComboboxItem(ComboBoxItem comboBoxItem)
        {

            return _dbContext.ComboBoxItem.Any(s => s.MainCompanyId == comboBoxItem.MainCompanyId && s.ItemType == comboBoxItem.ItemType && s.TagName == comboBoxItem.TagName);

        }


        public FormDefinationField AddDefinationField(FormDefinationField formDefinationField)
        {
            _dbContext.FormDefinationField.Add(formDefinationField);

            _dbContext.SaveChanges();

            return formDefinationField;
        }

        public FormGroup AddGroup(FormGroup formGroup)
        {
            _dbContext.FormGroup.Add(formGroup);

            _dbContext.SaveChanges();

            return formGroup;
        }

        public IEnumerable<FormDefination> Get(Expression<Func<FormDefination, bool>> predicate)
        {

            return _dbContext.FormDefination.Where(predicate).ToList();
        }

        public IEnumerable<ComboBoxItem> GetComboBoxItems(int mainCompanyId, string fieldTag)
        {

            return _dbContext.ComboBoxItem.Where(s => s.MainCompanyId == mainCompanyId && s.ItemType == fieldTag).ToList();
        }

        public IEnumerable<CustomSector> GetCompanySectors(int companyId)
        {

            return _dbContext.CustomSector.Where(s => s.MainCompanyId == companyId).ToList();
        }
        public CustomSector GetCompanySector(int id)
        {

            return _dbContext.CustomSector.Single(s => s.Id == id);
        }
        public IEnumerable<FieldType> GetDefaultFieldTypes()
        {
            return _dbContext.FieldType.ToList();
        }

        public IEnumerable<FontType> GetFontTypes()
        {
            return _dbContext.FontTypes.ToArray();
        }

        public FormDefinationField GetFormDefinationField(int id)
        {
            return _dbContext.FormDefinationField.Single(s => s.Id == id);
        }

        public FormGroup GetFormGroup(int id)
        {

            return _dbContext.FormGroup.Single(s => s.Id == id);
        }

        public IEnumerable<FormDefinationField> GetFormGroupFields(int formGroupId)
        {
            return _dbContext.FormDefinationField.Where(s => s.FormGroupId == formGroupId).OrderBy(s => s.OrderNumber).ToList();
        }

        public IEnumerable<FormGroup> GetFormGroups(int formDefinationId)
        {
            return _dbContext.FormGroup.Where(s => s.FormDefinationId == formDefinationId && !s.Deleted).ToList().OrderBy(s => s.OrderNumber).ToList();
        }

        public bool IsExistFormDefinationField(int formDefinationId, string tagName)
        {

            return _dbContext.FormDefinationField.Any(s => s.FormDefinationId == formDefinationId && s.TagName == tagName);
        }

        public FormDefination Update(FormDefination formDefination)
        {
            var dbDefination = _dbContext.FormDefination.Single(s => s.Id == formDefination.Id);
            dbDefination.FormName = formDefination.FormName;
            dbDefination.CustomSectorId = formDefination.CustomSectorId;
            dbDefination.CustomSectorName = formDefination.CustomSectorName;
            dbDefination.Deleted = formDefination.Deleted;
            dbDefination.Deployed = formDefination.Deployed;
            dbDefination.PublicDefiation = formDefination.PublicDefiation;
            if (!string.IsNullOrEmpty(formDefination.TemplatePath))
            {
                dbDefination.TemplatePath = formDefination.TemplatePath;
                dbDefination.TemplateFileName = formDefination.TemplateFileName;
            }
            dbDefination.DesingTemplate = formDefination.DesingTemplate;
            _dbContext.SaveChanges();

            return dbDefination;
        }

        public FormDefinationField UpdateDefinationField(FormDefinationField definationField)
        {
            var dbField = _dbContext.FormDefinationField.Single(s => s.Id == definationField.Id);
            dbField.FieldCaption = definationField.FieldCaption;
            dbField.TagName = definationField.TagName;
            dbField.CellName = definationField.CellName;
            dbField.Deleted = definationField.Deleted;
            dbField.Mandatory = definationField.Mandatory;
            dbField.AutoComplate = definationField.AutoComplate;
            dbField.ControlType = definationField.ControlType;
            dbField.FormGroupId = definationField.FormGroupId;
            dbField.OrderNumber = definationField.OrderNumber;
            dbField.Bold = definationField.Bold;
            dbField.Italic = definationField.Italic;
            dbField.FontSize = definationField.FontSize;
            dbField.TranslateLanguage = definationField.TranslateLanguage;
            dbField.FontFamily = definationField.FontFamily;
            dbField.AutoComlateType = definationField.AutoComlateType;

            _dbContext.SaveChanges();

            return dbField;
        }

        public FormGroup UpdateGroup(FormGroup formGroup)
        {
            var dbGroup = _dbContext.FormGroup.Single(s => s.Id == formGroup.Id);
            dbGroup.Deleted = formGroup.Deleted;
            dbGroup.OrderNumber = formGroup.OrderNumber;
            dbGroup.FormNumber = formGroup.FormNumber;
            dbGroup.AllowEditCustomer = formGroup.AllowEditCustomer;
            dbGroup.Name = formGroup.Name;
            dbGroup.GroupTag = formGroup.GroupTag;
            if (dbGroup.Deleted)
            {
                var groupFieldList = _dbContext.FormDefinationField.Where(s => s.FormGroupId == formGroup.Id);
                foreach (var field in groupFieldList) {
                    field.Deleted = dbGroup.Deleted;
                }
            }

            _dbContext.SaveChanges();

            return dbGroup;
        }

        public AutocompleteField? GetAutoComplateField(int definationFieldId)
        {
            return _dbContext.AutocompleteField.FirstOrDefault(s => s.FormDefinationFieldId == definationFieldId);
        }

        public List<AutocompleteFieldMap> GetAutoComplateFieldMaps(int definationFieldId)
        {
            return _dbContext.AutocompleteFieldMap.Where(s => s.FormDefinationFieldId == definationFieldId).ToList();
        }

        public AutocompleteField Add(AutocompleteField autocompleteField)
        {
            var dbDefination = _dbContext.AutocompleteField.FirstOrDefault(s => s.FormDefinationFieldId == autocompleteField.FormDefinationFieldId);
            if (dbDefination == null)
            {
                _dbContext.AutocompleteField.Add(autocompleteField);
                _dbContext.SaveChanges();

                return autocompleteField;
            }
            else
            {
                autocompleteField.Id = dbDefination.Id;

                return Update(autocompleteField);
            }


        }

        public AutocompleteField Update(AutocompleteField autocompleteField)
        {
            var dbAutoComplateField = _dbContext.AutocompleteField.Single(s => s.Id == autocompleteField.Id);

            dbAutoComplateField.FilterValue = autocompleteField.FilterValue;
            dbAutoComplateField.RelationalFieldName = autocompleteField.RelationalFieldName;
            dbAutoComplateField.FieldName = autocompleteField.FieldName;
            dbAutoComplateField.ComplateObject = autocompleteField.ComplateObject;

            return dbAutoComplateField;
        }

        public AutocompleteFieldMap Add(AutocompleteFieldMap autocompleteFieldmap)
        {
            _dbContext.AutocompleteFieldMap.Add(autocompleteFieldmap);
            _dbContext.SaveChanges();
            return autocompleteFieldmap;
        }

        public AutocompleteFieldMap Update(AutocompleteFieldMap autocompleteFieldmap)
        {
            var dbAutoComplateFieldmap = _dbContext.AutocompleteFieldMap.Single(s => s.Id == autocompleteFieldmap.Id);

            dbAutoComplateFieldmap.FieldCaption = autocompleteFieldmap.FieldCaption;
            dbAutoComplateFieldmap.TagName = autocompleteFieldmap.TagName;
            dbAutoComplateFieldmap.PropertyValue1 = autocompleteFieldmap.PropertyValue1;
            dbAutoComplateFieldmap.PropertyValue2 = autocompleteFieldmap.PropertyValue2;
            dbAutoComplateFieldmap.PropertyValue3 = autocompleteFieldmap.PropertyValue3;
            _dbContext.SaveChanges();
            return dbAutoComplateFieldmap;
        }

        public FormDefinationField GetDefinationField(int definationTypeId, string tagName)
        {
            return _dbContext.FormDefinationField.Single(s => s.FormDefinationId == definationTypeId && s.TagName == tagName && !s.Deleted);
        }

        public bool DeleteAutoComplateFieldMap(int autocomplateFieldMapid)
        {
            var dbComplate = _dbContext.AutocompleteFieldMap.Single(s => s.Id == autocomplateFieldMapid);
            _dbContext.AutocompleteFieldMap.Remove(dbComplate);
            _dbContext.SaveChanges();
            return true;

        }

        public IEnumerable<FormDefinationField> GetAllFields(int formdefinationTypeid)
        {
            return _dbContext.FormDefinationField.Where(s => s.FormDefinationId == formdefinationTypeid && !s.Deleted);
        }

        public IEnumerable<FormVersion> GetDefinationVersions(int formDefinationId)
        {
            return _dbContext.FormVersion.Where(s => s.FormDefinationId == formDefinationId).ToArray();
        }

        public FormVersion Add(FormVersion formVersion)
        {
            _dbContext.FormVersion.Add(formVersion);

            _dbContext.SaveChanges();

            return formVersion;
        }

        public FormVersion Update(FormVersion formVersion)
        {
            var dbVersion = _dbContext.FormVersion.Single(s => s.FormDefinationId == formVersion.Id);
            dbVersion.FormLanguage = formVersion.FormLanguage;
            dbVersion.FileName = formVersion.FilePath;
            dbVersion.EditedBy = formVersion.EditedBy;
            dbVersion.EditedDate = formVersion.EditedDate;
            dbVersion.EditedId = formVersion.EditedId;

            _dbContext.SaveChanges();

            return dbVersion;
        }

        public FormVersion GetFormDefinationVersion(int id)
        {
            return _dbContext.FormVersion.Single(s => s.Id == id);
        }

        public IEnumerable<FormDefinationAttachment> GetFormDefinationAttachments(int formDefinationId)
        {
            return _dbContext.FormDefinationAttachment.Where(s => s.FormDefinationId == formDefinationId).ToList();
        }

        public FormDefinationAttachment GetFormDefinationAttachment(int id)
        {
            return _dbContext.FormDefinationAttachment.Single(s => s.Id == id);
        }

        public FormDefinationAttachment Add(FormDefinationAttachment formDefinationAttachment)
        {
            _dbContext.FormDefinationAttachment.Add(formDefinationAttachment);
            _dbContext.SaveChanges();

            return formDefinationAttachment;
        }

        public FormDefinationAttachment Update(FormDefinationAttachment formDefinationAttachment)
        {
            var dbDefination = _dbContext.FormDefinationAttachment.Single(s => s.Id == formDefinationAttachment.Id);
            dbDefination.FontSize = formDefinationAttachment.FontSize;
            dbDefination.FontFamily = formDefinationAttachment.FontFamily;
            dbDefination.Italic = formDefinationAttachment.Italic;
            dbDefination.Bold = formDefinationAttachment.Bold;
            dbDefination.EditedBy = formDefinationAttachment.EditedBy;
            dbDefination.EditedDate = formDefinationAttachment.EditedDate;
            dbDefination.EditedId = formDefinationAttachment.EditedId;
            if (!string.IsNullOrEmpty(formDefinationAttachment.FilePath))
            {
                dbDefination.FilePath = formDefinationAttachment.FilePath;
                dbDefination.FileName = formDefinationAttachment.FileName;

            }

            _dbContext.SaveChanges();

            return dbDefination;
        }

        public bool IsExistAttachmentFontStype(int formAttachmentId, string tagName)
        {

            return _dbContext.FormAttachmentFontStyle.Any(s => s.FormDefinationAttachmentId == formAttachmentId && s.TagName == tagName);
        }

        public FormAttachmentFontStyle Add(FormAttachmentFontStyle formAttachmentFontStyle)
        {
            _dbContext.FormAttachmentFontStyle.Add(formAttachmentFontStyle);

            _dbContext.SaveChanges();
            return formAttachmentFontStyle;
        }

        public FormAttachmentFontStyle Update(FormAttachmentFontStyle formAttachmentFontStyle)
        {
            var dbFontStyle = _dbContext.FormAttachmentFontStyle.Single(s => s.Id == formAttachmentFontStyle.Id);
            dbFontStyle.FieldCaption = formAttachmentFontStyle.FieldCaption;
            dbFontStyle.TagName = formAttachmentFontStyle.TagName;
            dbFontStyle.Italic = formAttachmentFontStyle.Italic;
            dbFontStyle.FontSize = formAttachmentFontStyle.FontSize;
            dbFontStyle.Bold = formAttachmentFontStyle.Bold;


            return dbFontStyle;
        }

        public IEnumerable<FormMetaDataAttachmentFilter> GetFormDefinationFilters(int formDefinationId)
        {
            return _dbContext.FormMetaDataAttachmentFilters.Where(s => s.FormDefinationId == formDefinationId).ToArray();
        }

        public IEnumerable<FormAttachmentFontStyle> GetFormAttachmentFontStyles(int formAttachmentId)
        {
            return _dbContext.FormAttachmentFontStyle.Where(s => s.FormDefinationAttachmentId == formAttachmentId).ToArray();
        }

        public IEnumerable<ComboBoxItem> GetComboBoxItems(int mainCompanyId)
        {
            return _dbContext.ComboBoxItem.Where(s => s.MainCompanyId == mainCompanyId).ToArray();
        }

        public IEnumerable<CustomeField> GetCustomeFields(int mainCompanyId)
        {

            return _dbContext.CustomeField.Where(s => s.MainCompanyId == mainCompanyId && !s.Deleted).ToArray();
        }

        public IEnumerable<CustomeFieldItem> GetCustomeFielList(int mainCompanyId)
        {
            return _dbContext.CustomeFieldItem.Where(s => s.MainCompanyId == mainCompanyId && !s.Deleted).ToArray();
        }

        public IEnumerable<CustomeField_VirtualTable> GetCustomeVirtualTables(int mainCompanyId)
        {
            return _dbContext.CustomeField_VirtualTable.Where(s => s.MainCompanyId == mainCompanyId).ToArray();
        }

        public IEnumerable<CustomeField_VirtualTableField> GetCustomeVirtualTableFields(int mainCompanyId)
        {
            return _dbContext.CustomeField_VirtualTableField.Where(s => s.MainCompanyId == mainCompanyId).ToArray();
        }

        public IEnumerable<TranslateDictionary> GetTranslateDictionaries()
        {
            return _dbContext.TranslateDictionary.ToArray();
        }

        public IEnumerable<SoftImageChangeFormatRule> GetSoftImageChangeFormatRules(int formDefinationId)
        {
            return _dbContext.SoftImageChangeFormatRule.Where(s => s.FormDefinationId == formDefinationId).ToArray();
        }

        public CustomeField Update(CustomeField customeField)
        {
            var dbCustomeField = _dbContext.CustomeField.Single(s => s.Id == customeField.Id);

            dbCustomeField.FieldName = customeField.FieldName;
            dbCustomeField.RowHeightRuleValue = customeField.RowHeightRuleValue;
            dbCustomeField.HeaderHeightRuleValue = customeField.HeaderHeightRuleValue;

            dbCustomeField.editedDate = DateTime.Now;
            dbCustomeField.HeaderHeight = customeField.HeaderHeight;
            dbCustomeField.RowHeight = customeField.RowHeight;
            dbCustomeField.ElementType = customeField.ElementType;


            _dbContext.SaveChanges();

            return dbCustomeField;
        }

        public CustomeField Add(CustomeField customeField)
        {

            _dbContext.CustomeField.Add(customeField);
            _dbContext.SaveChanges();

            return customeField;

        }

        public IEnumerable<CustomeFieldItem> GetCustomeFieldItems(int customeFieldId)
        {

            return _dbContext.CustomeFieldItem.Where(s => s.CustomeFieldId == customeFieldId && !s.Deleted).ToList();
        }

        public CustomeFieldItem Add(CustomeFieldItem customeFieldItem)
        {
            _dbContext.CustomeFieldItem.Add(customeFieldItem);
            _dbContext.SaveChanges();

            return customeFieldItem;
        }

        public CustomeFieldItem Update(CustomeFieldItem customeFieldItem)
        {
            var dbItem = _dbContext.CustomeFieldItem.Single(s => s.Id == customeFieldItem.Id);
            dbItem.Mandatory = customeFieldItem.Mandatory;

            dbItem.FieldCaption = customeFieldItem.FieldCaption;
            dbItem.ControlType = customeFieldItem.ControlType;
            dbItem.TagName = customeFieldItem.TagName;

            dbItem.AutoComplate = customeFieldItem.AutoComplate;
            dbItem.Deleted = customeFieldItem.Deleted;
            dbItem.MaxCharacter = customeFieldItem.MaxCharacter;
            dbItem.TableWith = customeFieldItem.TableWith;
            dbItem.OrderNumber = customeFieldItem.OrderNumber;
            dbItem.HeaderWidth = customeFieldItem.HeaderWidth;
            dbItem.HeaderWidthRuleValue = customeFieldItem.HeaderWidthRuleValue;


            dbItem.Bold = customeFieldItem.Bold;
            dbItem.Italic = customeFieldItem.Italic;
            dbItem.FontSize = customeFieldItem.FontSize;
            _dbContext.SaveChanges();
            return dbItem;

        }

        public CustomeField? GetCustomeField(int mainCompanyId, string customeFieldName)
        {
            return _dbContext.CustomeField.FirstOrDefault(s => s.MainCompanyId == mainCompanyId && s.FieldTagName == customeFieldName);
        }

        public IEnumerable<FormDefinationField> GetFormDefinationFields(int formdefinationId)
        {
            return _dbContext.FormDefinationField.Where(s => s.FormDefinationId == formdefinationId && !s.Deleted).ToList().ToList();
        }

        public FormGroup CloneFormGroup(FormGroup sourceFormGroup, FormDefination targetFormDefination)
        {
            var newFormGroup = new FormGroup()
            {
                AllowEditCustomer = sourceFormGroup.AllowEditCustomer,
                FormDefinationId = targetFormDefination.Id,
                FormNumber = sourceFormGroup.FormNumber,
                GroupTag = sourceFormGroup.GroupTag,
                Name = sourceFormGroup.Name,
                OrderNumber = sourceFormGroup.OrderNumber,
            };
            var formgroupFields = GetFormGroupFields(sourceFormGroup.Id);

            List<AutocompleteField> autocompleteFields = new List<AutocompleteField>();


            _dbContext.FormGroup.Add(newFormGroup);
            _dbContext.SaveChanges();
            foreach (var field in formgroupFields)
            {


                var autocompleteField = GetAutoComplateField(field.Id);

                var newField = new FormDefinationField()
                {
                    FontSize = field.FontSize,
                    FormGroupId = newFormGroup.Id,
                    FormDefinationId = targetFormDefination.Id,
                    FontFamily = field.FontFamily,
                    FieldCaption = field.FieldCaption,
                    Italic = field.Italic,
                    Mandatory = field.Mandatory,
                    DefaultProp = field.DefaultProp,
                    ControlType = field.ControlType,
                    CellName = field.CellName,
                    OrderNumber = field.OrderNumber,
                    TranslateLanguage = field.TranslateLanguage,
                    TagName = field.TagName,
                    AutoComlateType = field.AutoComlateType,
                    AutoComplate = field.AutoComplate,
                    Bold = field.Bold,

                };

                _dbContext.FormDefinationField.Add(newField);
                _dbContext.SaveChanges();
                if (autocompleteField != null)
                {
                    var newautoComplate = new AutocompleteField()
                    {
                        ComplateObject = autocompleteField.ComplateObject,
                        FieldName = autocompleteField.FieldName,
                        FilterValue = autocompleteField.FilterValue,
                        RelationalFieldName = autocompleteField.RelationalFieldName,
                        FormDefinationFieldId = newField.Id
                    };

                    _dbContext.AutocompleteField.Add(newautoComplate);
                    _dbContext.SaveChanges();

                    var autoComlateMaps = GetAutoComplateFieldMaps(field.Id);

                    var items = autoComlateMaps.Select(s => new AutocompleteFieldMap()
                    {
                        FieldCaption = s.FieldCaption,
                        FormDefinationFieldId = newField.Id,
                        FormGroupId = newFormGroup.Id,
                        PropertyValue1 = s.PropertyValue1,
                        PropertyValue2 = s.PropertyValue2,
                        PropertyValue3 = s.PropertyValue3,
                        TagName = s.TagName
                    }).ToList();

                    _dbContext.AutocompleteFieldMap.AddRange(items);
                    _dbContext.SaveChanges();
                }
            }



            return newFormGroup;

        }

        public FormDefination GetFormDefination(int id)
        {
            return _dbContext.FormDefination.Single(s => s.Id == id);
        }

        public IEnumerable<string> GetDistinctFieldNames(int mainCompanyId)
        {

            var result = _dbContext.FormDefination
                .Join(_dbContext.FormDefinationField,
                        c => c.Id,
                        o => o.FormDefinationId,
                        (c, o) => new { c, o })
                .Where(x => x.c.MainCompanyId == mainCompanyId && !x.c.Deleted && !x.o.Deleted)
                .Select(x => x.o.TagName)
                .Distinct()
                .ToList();

            return result;
        }
    }
}
