using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Business.Helper;
using CustomPortalV2.Core.Model.Autocomplete;
using CustomPortalV2.Core.Model.DTO;
using CustomPortalV2.Core.Model.FDefination;
using CustomPortalV2.DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public DefaultReturn<AutocompleteField> GetAutocompleteField(int formdefinationId, string tagName)
        {
            throw new NotImplementedException();
        }

        public DefaultReturn<List<AutocompleteFieldMap>> GetAutocompleteFieldMaps(int autoComplateFieldId)
        {
            throw new NotImplementedException();
        }

        public DefaultReturn<List<ComboBoxItem>> GetComboBoxItems(int mainCompanyId, string tagName)
        {
            DefaultReturn<List<ComboBoxItem>> defaultReturn = new DefaultReturn<List<ComboBoxItem>>();
            try
            {
                defaultReturn.Data= _formDefinationService.GetComboBoxItems(mainCompanyId, tagName).ToList();
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

            defaultReturn.Data = _formDefinationService.Get(s => s.MainCompanyId == mainCompanyId && s.CustomSectorId == sectorId).ToList();
            return defaultReturn;
        }

        public DefaultReturn<List<FormDefination>> GetCompanyDefinations(int mainCompanyId)
        {
            DefaultReturn<List<FormDefination>> defaultReturn = new DefaultReturn<List<FormDefination>>();

            defaultReturn.Data = _formDefinationService.Get(s => s.MainCompanyId == mainCompanyId && !s.Deleted.Value).ToList();


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
            DefaultReturn<FormDefinationField> defaultReturn    =new DefaultReturn<FormDefinationField>();

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

        public DefaultReturn<List<FormGroup>> GetFormGroups(int formDefinationId)
        {
            DefaultReturn<List<FormGroup>> defaultReturn = new DefaultReturn<List<FormGroup>>();

            defaultReturn.Data = _formDefinationService.GetFormGroups(formDefinationId).ToList();
            return defaultReturn;
        }

        public DefaultReturn<FormVersion> GetFormVersions(int formDefinationId)
        {
            throw new NotImplementedException();
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
            DefaultReturn<ComboBoxItem> defaultReturn   =new DefaultReturn<ComboBoxItem>();

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

        public DefaultReturn<FormDefinationField> SaveFormDefinationField(FormDefinationField formDefinationField)
        {
            DefaultReturn < FormDefinationField >   defaultReturn = new DefaultReturn<FormDefinationField >();
            try
            {
                if (formDefinationField.Id == 0)
                {
                    if (_formDefinationService.IsExistFormDefinationField(formDefinationField.FormDefinationId, formDefinationField.TagName))
                    {
                        throw new Exception("FieldAllradyExist");
                    }
                    defaultReturn.Data= _formDefinationService.AddDefinationField(formDefinationField);

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
    }
}
