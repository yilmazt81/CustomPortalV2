using CustomPortalV2.Core.Model.App;
using CustomPortalV2.Core.Model.Autocomplete;
using CustomPortalV2.Core.Model.FDefination;
using CustomPortalV2.Model.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.DataAccessLayer.Concrete
{
    public interface IFormDefinationRepository
    {
        IEnumerable<FormDefination> Get(Expression<Func<FormDefination, bool>> predicate);
        IEnumerable<CustomSector> GetCompanySectors(int companyId);
        CustomSector GetCompanySector(int id);

        IEnumerable<FormGroup> GetFormGroups(int formDefinationId);
        FormDefination Add(FormDefination formDefination);
        FormDefination Update(FormDefination formDefination);
        FormGroup AddGroup(FormGroup formGroup);
        FormGroup UpdateGroup(FormGroup formGroup);

        IEnumerable<FormDefinationField> GetFormGroupFields(int formGroupId);
        IEnumerable<FormDefinationField> GetAllFields(int formdefinationTypeid);

        FormDefinationField GetDefinationField(int definationTypeId, string tagName);

        FormGroup GetFormGroup(int id);

        IEnumerable<FieldType> GetDefaultFieldTypes();

        IEnumerable<FontType> GetFontTypes();

        FormDefinationField AddDefinationField(FormDefinationField formDefinationField);
        FormDefinationField UpdateDefinationField(FormDefinationField formDefinationField);

        bool IsExistFormDefinationField(int formDefinationId, string tagName);

        FormDefinationField GetFormDefinationField(int id);
        IEnumerable<FormDefinationField> GetFormDefinationFields(int formdefinationId);

        IEnumerable<ComboBoxItem> GetComboBoxItems(int mainCompanyId, string fieldTag);
        IEnumerable<ComboBoxItem> GetComboBoxItems(int mainCompanyId);
        ComboBoxItem AddComboboxItem(ComboBoxItem comboBoxItem);

        bool IsExistComboboxItem(ComboBoxItem comboBoxItem);

        AutocompleteField? GetAutoComplateField(int definationFieldId);

        List<AutocompleteFieldMap> GetAutoComplateFieldMaps(int definationFieldId);

        AutocompleteField Add(AutocompleteField autocompleteField);
        AutocompleteField Update(AutocompleteField autocompleteField);

        AutocompleteFieldMap Add(AutocompleteFieldMap autocompleteFieldmap);
        AutocompleteFieldMap Update(AutocompleteFieldMap autocompleteFieldmap);


        bool DeleteAutoComplateFieldMap(int autocomplateFieldMapid);

        IEnumerable<FormVersion> GetDefinationVersions(int formDefinationId);

        FormVersion Add(FormVersion formVersion);
        FormVersion Update(FormVersion formVersion);

        FormVersion GetFormDefinationVersion(int id);

        IEnumerable<FormDefinationAttachment> GetFormDefinationAttachments(int formDefinationId);
        FormDefinationAttachment GetFormDefinationAttachment(int id);

        FormDefinationAttachment Add(FormDefinationAttachment formDefinationAttachment);
        FormDefinationAttachment Update(FormDefinationAttachment formDefinationAttachment);

        bool IsExistAttachmentFontStype(int formAttachmentId, string tagName);

        IEnumerable<FormAttachmentFontStyle> GetFormAttachmentFontStyles(int formAttachmentId);
        FormAttachmentFontStyle Add(FormAttachmentFontStyle formAttachmentFontStyle);
        FormAttachmentFontStyle Update(FormAttachmentFontStyle formAttachmentFontStyle);

        IEnumerable<FormMetaDataAttachmentFilter> GetFormDefinationFilters(int formDefinationId);

        IEnumerable<CustomeField> GetCustomeFields(int mainCompanyId);
        CustomeField? GetCustomeField(int mainCompanyId, string customeFieldName);

        IEnumerable<CustomeFieldItem> GetCustomeFielList(int mainCompanyId);


        IEnumerable<CustomeField_VirtualTable> GetCustomeVirtualTables(int mainCompanyId);
        IEnumerable<CustomeField_VirtualTableField> GetCustomeVirtualTableFields(int mainCompanyId);

        IEnumerable<TranslateDictionary> GetTranslateDictionaries();

        IEnumerable<SoftImageChangeFormatRule> GetSoftImageChangeFormatRules(int formDefinationId);

        CustomeField Update(CustomeField customeField);
        CustomeField Add(CustomeField customeField);

        IEnumerable<CustomeFieldItem> GetCustomeFieldItems(int customeFieldId);

        CustomeFieldItem Add(CustomeFieldItem customeFieldItem);

        CustomeFieldItem Update(CustomeFieldItem customeFieldItem);



    }
}
