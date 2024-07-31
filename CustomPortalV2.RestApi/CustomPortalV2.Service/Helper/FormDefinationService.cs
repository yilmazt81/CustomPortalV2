using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Core.Model.Autocomplete;
using CustomPortalV2.Core.Model.DTO;
using CustomPortalV2.Core.Model.FDefination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Business.Helper
{
    public class FormDefinationService : IFormDefinationService
    {
     
        public DefaultReturn<AutocompleteField> GetAutocompleteField(int formdefinationId, string tagName)
        {
            throw new NotImplementedException();
        }

        public DefaultReturn<List<AutocompleteFieldMap>> GetAutocompleteFieldMaps(int autoComplateFieldId)
        {
            throw new NotImplementedException();
        }

        public DefaultReturn<ComboBoxItem> GetComboBoxItems(int mainCompanyId, string tagName)
        {
            throw new NotImplementedException();
        }

        public DefaultReturn<List<FormDefination>> GetCompanyDefination(int mainCompanyId, int sectorId)
        {
            throw new NotImplementedException();
        }

        public DefaultReturn<List<FormDefination>> GetCompanyDefinations(int mainCompanyId)
        {
            throw new NotImplementedException();
        }

        public DefaultReturn<FormDefination> GetFormDefination(int id)
        {
            throw new NotImplementedException();
        }

        public DefaultReturn<List<FormDefinationField>> GetFormDefinationFields(int formdefinationId, int formgroupId)
        {
            throw new NotImplementedException();
        }

        public DefaultReturn<List<FormGroup>> GetFormGroups(int formDefinationId)
        {
            throw new NotImplementedException();
        }

        public DefaultReturn<FormVersion> GetFormVersions(int formDefinationId)
        {
            throw new NotImplementedException();
        }

        public DefaultReturn<List<CustomSectorDTO>> GetSector(int mainCompanyId, int applicationLangId)
        {
            throw new NotImplementedException();
        }

        public DefaultReturn<FormDefination> Save(FormDefination formDefination)
        {
            throw new NotImplementedException();
        }

        public DefaultReturn<FormDefinationField> Save(FormDefinationField formDefinationField)
        {
            throw new NotImplementedException();
        }

        public DefaultReturn<FormGroup> Save(FormGroup formGroup)
        {
            throw new NotImplementedException();
        }

        public DefaultReturn<AutocompleteField> Save(AutocompleteField autocompleteField)
        {
            throw new NotImplementedException();
        }

        public DefaultReturn<AutocompleteFieldMap> Save(AutocompleteFieldMap autocompleteFieldMap)
        {
            throw new NotImplementedException();
        }

        public DefaultReturn<ComboBoxItem> Save(ComboBoxItem comboBoxItem)
        {
            throw new NotImplementedException();
        }
    }
}
