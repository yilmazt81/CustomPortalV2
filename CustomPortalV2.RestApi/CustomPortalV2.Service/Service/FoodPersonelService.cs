using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Business.Helper;
using CustomPortalV2.Core.Model.Definations;
using CustomPortalV2.Core.Model.DTO;
using CustomPortalV2.DataAccessLayer.Concrete;
using CustomPortalV2.DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Business.Service
{
    public class FoodPersonelService : IFoodPersonelService
    {
        IFoodPersonelRepository _foodPersonelRepository;
        IFormDefinationRepository _formdefinationRepository;
        public FoodPersonelService(IFoodPersonelRepository foodPersonelRepository, IFormDefinationRepository formDefinationRepository)
        {
            _foodPersonelRepository = foodPersonelRepository;
            _formdefinationRepository = formDefinationRepository;
        }
        public DefaultReturn<List<FoodPersonel>> Filter(DefinationFilterDTO companyDefinationFilterDTO)
        {
            DefaultReturn<List<FoodPersonel>> defaultReturn = new DefaultReturn<List<FoodPersonel>>();
            if (!string.IsNullOrEmpty(companyDefinationFilterDTO.FilterValue))
            {
                defaultReturn.Data = _foodPersonelRepository.GetFoodPersonel(s => s.FullName.Contains(companyDefinationFilterDTO.FilterValue)).ToList();
            }
            else
            {
                defaultReturn.Data = _foodPersonelRepository.GetFoodPersonel(s => s.Id != 0).ToList();
            }

            return defaultReturn;
        }
        private object GetPropValue(object src, string propName)
        {
            if (string.IsNullOrEmpty(propName))
                return null;
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
        public DefaultReturn<List<ControlAutoFieldDTO>> GetAutoComplateDefinationValues(int formdefinationId, int personelid)
        {
            DefaultReturn<List<ControlAutoFieldDTO>> defaultReturn = new DefaultReturn<List<ControlAutoFieldDTO>>();
            try
            {
                var personels = _foodPersonelRepository.GetFoodPersonel(s => s.Id == personelid);
                if (personels.Count() == 0)
                {
                    defaultReturn.ReturnMessage = "CannotFindPersonel";
                    defaultReturn.ReturnCode = 5;

                }
                var formdefination = _formdefinationRepository.GetFormDefinationField(formdefinationId);

                var personel = personels.FirstOrDefault();
                var autoComplateFieldMaps = _formdefinationRepository.GetAutoComplateFieldMaps(formdefinationId);

                defaultReturn.Data = new List<ControlAutoFieldDTO>();
                foreach (var autocomplete in autoComplateFieldMaps)
                {
                    var formDefinationField = _formdefinationRepository.GetDefinationField(formdefination.FormDefinationId, autocomplete.TagName);
                    if (formDefinationField == null)
                        continue;
                    var fieldValue = GetPropValue(personel, autocomplete.PropertyValue1);
                    if (!defaultReturn.Data.Any(s => s.FieldName == autocomplete.TagName))
                    {
                        string secondField = string.Empty;
                        if (!string.IsNullOrEmpty(autocomplete.PropertyValue2))
                        {
                            var secondValue = GetPropValue(personel, autocomplete.PropertyValue2);
                            fieldValue += " " + (secondValue == null ? string.Empty : " " + secondValue.ToString());
                        }
                        if (!string.IsNullOrEmpty(autocomplete.PropertyValue3))
                        {
                            var secondValue = GetPropValue(personel, autocomplete.PropertyValue3);
                            fieldValue += " " + (secondValue == null ? string.Empty : " " + secondValue.ToString());
                        }

                        defaultReturn.Data.Add(new ControlAutoFieldDTO()
                        {
                            ControlName = formDefinationField.GetControlName(),
                            FieldName = formDefinationField.TagName,
                            ControlValue = fieldValue != null ? fieldValue.ToString() : string.Empty
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                defaultReturn.SetException(ex);
            }

            return defaultReturn;
        }
    }
}
