using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Business.Helper;
using CustomPortalV2.Core.Model.Definations;
using CustomPortalV2.Core.Model.DTO;
using CustomPortalV2.DataAccessLayer.Concrete;
using CustomPortalV2.DataAccessLayer.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Business.Service
{
    public class CustomProductService : ICustomProductService
    {
        IBranchRepository _branchRepository;
        ICustomProductRepository _customProductRepository;
        IFormDefinationRepository _formDefinationRepository;

        public CustomProductService(ICustomProductRepository customProductRepository,
            IBranchRepository branchRepository,
            IFormDefinationRepository formDefinationRepository)
        {
            _customProductRepository = customProductRepository;
            _branchRepository = branchRepository;
            _formDefinationRepository = formDefinationRepository;
        }

        public DefaultReturn<bool> Delete(int mainCompanyId, int userId, int id)
        {

            DefaultReturn<bool> defaultReturn = new DefaultReturn<bool>();
            var product = _customProductRepository.Get(id);
            product.Deleted = true;
            product.EditedId = userId;
            product.EditedDate = DateTime.Now;
            _customProductRepository.Update(product);

            return defaultReturn;
        }

        public DefaultReturn<List<CustomProduct>> Filter(DefinationFilterDTO companyDefinationFilterDTO, int branchId)
        {
            DefaultReturn<List<CustomProduct>> defaultReturn = new DefaultReturn<List<CustomProduct>>();

            try
            {
                var branch = _branchRepository.Get(s => s.Id == branchId);
                var query = _customProductRepository.GetIQueryable();
                var autoComplateField = _formDefinationRepository.GetAutoComplateField(companyDefinationFilterDTO.FormDefinationFieldId);

                query = query.Where(s => s.MainCompanyId == branch.MainCompanyId && !s.Deleted);
                if (!branch.CompanyAdmin)
                {
                    query = query.Where(s => s.CompanyBranchId == branch.Id);
                }
                if (!string.IsNullOrEmpty(companyDefinationFilterDTO.FilterValue))
                {
                    var filterValue = companyDefinationFilterDTO.FilterValue;
                    query = query.Where(s => s.ProductName.Contains(filterValue));
                }


                var companyDefinationList = query.ToList();
                defaultReturn.Data = companyDefinationList;

            }
            catch (Exception ex)
            {
                defaultReturn.SetException(ex);
            }
            return defaultReturn;
        }
        private object GetPropValue(object src, string propName)
        {
            if (string.IsNullOrEmpty(propName))
                return null;
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
        public DefaultReturn<List<ControlAutoFieldDTO>> GetAutoComplateDefinationValues(int formdefinationId, string productIdlist)
        {
            DefaultReturn<List<ControlAutoFieldDTO>> defaultReturn = new DefaultReturn<List<ControlAutoFieldDTO>>();
            try
            {
                var formdefination = _formDefinationRepository.GetFormDefinationField(formdefinationId);

                var autoComplateFieldMaps = _formDefinationRepository.GetAutoComplateFieldMaps(formdefinationId);
                defaultReturn.Data = new List<ControlAutoFieldDTO>();
                foreach (var productId in productIdlist.Split(','))
                {
                    if (string.IsNullOrEmpty(productId))
                    {
                        continue;
                    }


                    var defination = _customProductRepository.Get(int.Parse(productId));

                    foreach (var autocomplete in autoComplateFieldMaps)
                    {
                        var formDefinationField = _formDefinationRepository.GetDefinationField(formdefination.FormDefinationId, autocomplete.TagName);
                        if (formDefinationField == null)
                            continue;
                        var fieldValue = GetPropValue(defination, autocomplete.PropertyValue1);

                        string secondField = string.Empty;
                        if (!string.IsNullOrEmpty(autocomplete.PropertyValue2))
                        {
                            var secondValue = GetPropValue(defination, autocomplete.PropertyValue2);
                            fieldValue += " " + (secondValue == null ? string.Empty : " " + secondValue.ToString());
                        }
                        if (!string.IsNullOrEmpty(autocomplete.PropertyValue3))
                        {
                            var secondValue = GetPropValue(defination, autocomplete.PropertyValue3);
                            fieldValue += " " + (secondValue == null ? string.Empty : " " + secondValue.ToString());
                        }

                        if (!defaultReturn.Data.Any(s => s.FieldName == autocomplete.TagName))
                        {
                            if (formDefinationField.ControlType == "Text" || formDefinationField.ControlType == "Hidden")
                            {

                                defaultReturn.Data.Add(new ControlAutoFieldDTO()
                                {
                                    ControlName = formDefinationField.GetControlName(),
                                    FieldName = formDefinationField.TagName,
                                    ControlValue = fieldValue != null ? fieldValue.ToString() : string.Empty
                                });

                            }
                            else if (formDefinationField.ControlType == "RadioBox" || formDefinationField.ControlType == "CheckBox")
                            {
                                var checkItems = _formDefinationRepository.GetComboBoxItems(1, formDefinationField.TagName);
                                var fieldvalue = (fieldValue == null ? String.Empty : fieldValue.ToString());
                                var fieldValues = fieldvalue.Split(',');

                                foreach (var checkItem in checkItems)
                                {
                                    var tagCheckBox = $"{formDefinationField.GetControlName()}_{checkItem.TagName}";
                                    if (autoComplateFieldMaps.Any(s => fieldValues.Contains(checkItem.TagName)))
                                    {
                                        defaultReturn.Data.Add(new ControlAutoFieldDTO()
                                        {
                                            ControlName = tagCheckBox,
                                            FieldName = formDefinationField.TagName,
                                            ControlValue = "True",
                                        });
                                    }
                                }
                            }

                        }
                        else
                        {
                            if (formDefinationField.ControlType == "Text" || formDefinationField.ControlType == "Hidden")
                            {
                                var oldValue = defaultReturn.Data.First(s => s.FieldName == autocomplete.TagName);
                                oldValue.ControlValue += "," + (fieldValue != null ? fieldValue.ToString() : string.Empty);
                            } 
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

        public DefaultReturn<List<CustomProduct>> GetCompanyProducts(int mainCompanyId, int branchId)
        {
            DefaultReturn<List<CustomProduct>> defaultReturn = new DefaultReturn<List<CustomProduct>>();
            var branch = _branchRepository.Get(s => s.Id == branchId);
            if (!branch.CompanyAdmin)
            {
                defaultReturn.Data = _customProductRepository.GetCustomProducts(s => s.MainCompanyId == mainCompanyId && s.CompanyBranchId == branchId && !s.Deleted).ToList();
            }
            else
            {
                defaultReturn.Data = _customProductRepository.GetCustomProducts(s => s.MainCompanyId == mainCompanyId && !s.Deleted).ToList();

            }

            return defaultReturn;
        }

        public DefaultReturn<CustomProduct> GetProduct(int mailProductId, int branchId, int id)
        {
            DefaultReturn<CustomProduct> defaultReturn = new DefaultReturn<CustomProduct>();

            defaultReturn.Data = _customProductRepository.Get(id);
            return defaultReturn;
        }

        public DefaultReturn<CustomProduct> Save(CustomProduct customProduct)
        {
            DefaultReturn<CustomProduct> defaultReturn = new DefaultReturn<CustomProduct>();

            var sector = _formDefinationRepository.GetCompanySector(customProduct.CustomSectorId);
            customProduct.CustomSectorName = sector.Name;
            if (customProduct.Id == 0)
            {
                defaultReturn.Data = _customProductRepository.Add(customProduct);
            }
            else
            {
                defaultReturn.Data = _customProductRepository.Update(customProduct);
            }

            return defaultReturn;
        }
    }
}
