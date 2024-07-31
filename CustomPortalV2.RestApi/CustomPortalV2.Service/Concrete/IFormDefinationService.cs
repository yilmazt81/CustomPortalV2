using CustomPortalV2.Core.Model.Autocomplete;
using CustomPortalV2.Core.Model.DTO;
using CustomPortalV2.Core.Model.FDefination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Business.Concrete
{
    public interface IFormDefinationService
    {
        DefaultReturn< List<FormDefination>> GetCompanyDefinations(int mainCompanyId);
        DefaultReturn<List<FormDefination>> GetCompanyDefination(int mainCompanyId, int sectorId); 

        DefaultReturn<FormDefination> GetFormDefination(int id); 

        DefaultReturn<FormDefination> Save(FormDefination formDefination);

        DefaultReturn<FormDefinationField> Save(FormDefinationField formDefinationField);

        DefaultReturn<FormGroup> Save(FormGroup formGroup);

        DefaultReturn<AutocompleteField> Save(AutocompleteField autocompleteField);
        DefaultReturn<AutocompleteFieldMap> Save(AutocompleteFieldMap autocompleteFieldMap);

        DefaultReturn<ComboBoxItem> Save(ComboBoxItem comboBoxItem);
        DefaultReturn<List<FormDefinationField>> GetFormDefinationFields(int formdefinationId, int formgroupId);

        DefaultReturn<AutocompleteField> GetAutocompleteField(int formdefinationId, string tagName);
        DefaultReturn<List<AutocompleteFieldMap>> GetAutocompleteFieldMaps(int autoComplateFieldId);

        DefaultReturn<List<FormGroup>> GetFormGroups(int formDefinationId);

        DefaultReturn<ComboBoxItem> GetComboBoxItems(int mainCompanyId,string tagName);

        DefaultReturn<FormVersion> GetFormVersions(int formDefinationId);

        DefaultReturn<List<CustomSectorDTO>> GetSector(int mainCompanyId, int applicationLangId);



    }
}
