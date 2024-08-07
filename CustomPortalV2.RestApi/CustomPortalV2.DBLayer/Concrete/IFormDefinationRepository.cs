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

        IEnumerable<FormGroup> GetFormGroups(int formDefinationId);
        FormDefination Add(FormDefination formDefination);
        FormDefination Update(FormDefination formDefination);
        FormGroup AddGroup(FormGroup formGroup);
        FormGroup UpdateGroup(FormGroup formGroup);

        IEnumerable<FormDefinationField> GetFormGroupFields(int formGroupId);

        FormDefinationField GetDefinationField(int definationTypeId, string tagName);

        FormGroup GetFormGroup(int id);

        IEnumerable<FieldType> GetDefaultFieldTypes();

        IEnumerable<FontType> GetFontTypes();

        FormDefinationField AddDefinationField(FormDefinationField formDefinationField);
        FormDefinationField UpdateDefinationField(FormDefinationField formDefinationField);

        bool IsExistFormDefinationField(int formDefinationId, string tagName);

        FormDefinationField GetFormDefinationField(int id);

        IEnumerable<ComboBoxItem>   GetComboBoxItems(int mainCompanyId,string fieldTag);

        ComboBoxItem AddComboboxItem(ComboBoxItem comboBoxItem);

        bool IsExistComboboxItem(ComboBoxItem comboBoxItem);

        AutocompleteField? GetAutoComplateField(int definationFieldId);

        List<AutocompleteFieldMap> GetAutoComplateFieldMaps(int definationFieldId);

        AutocompleteField Add(AutocompleteField autocompleteField);
        AutocompleteField Update(AutocompleteField autocompleteField);

        AutocompleteFieldMap Add(AutocompleteFieldMap autocompleteFieldmap);
        AutocompleteFieldMap Update(AutocompleteFieldMap autocompleteFieldmap);


        bool DeleteAutoComplateFieldMap(int autocomplateFieldMapid);
    }
}
