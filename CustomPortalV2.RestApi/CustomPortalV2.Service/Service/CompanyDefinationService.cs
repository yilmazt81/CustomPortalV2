using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Business.Helper;
using CustomPortalV2.Core.Model.App;
using CustomPortalV2.Core.Model.Company;
using CustomPortalV2.Core.Model.Definations;
using CustomPortalV2.Core.Model.DTO;
using CustomPortalV2.DataAccessLayer.Concrete;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Business.Service
{
    public class CompanyDefinationService : ICompanyDefinationService
    {

        ICompanyAdresDefinationRepository _companyDefinationRepository ;
        IBranchRepository _branchRepository;
        IFormDefinationRepository _formdefinationRepository;
        public CompanyDefinationService(ICompanyAdresDefinationRepository companyDefination, 
            IBranchRepository branchRepository,
            IFormDefinationRepository formDefinationRepository)
        {
            _companyDefinationRepository = companyDefination;
            _branchRepository = branchRepository;
            _formdefinationRepository = formDefinationRepository;
        }

        public DefaultReturn<bool> DeleteCompany(int mainCompanyId, int id)
        {
            DefaultReturn<bool> defaultReturn = new DefaultReturn<bool>();
            try
            {
                var companyDefination = _companyDefinationRepository.GetCompanyDefinations(s => s.Id == id && s.MainCompanyId == mainCompanyId).FirstOrDefault();
                if (companyDefination == null)
                {
                    throw new Exception("CompanyDefinationIsNotDefined");
                }

                companyDefination.Deleted = true;
                _companyDefinationRepository.Update(companyDefination);

            }
            catch (Exception ex)
            {
                defaultReturn.SetException(ex);
            }


            return defaultReturn;

        }

        public DefaultReturn<List<CompanyDefination>> Filter(CompanyDefinationFilterDTO companyDefinationFilterDTO, int branchId)
        {
            DefaultReturn<List<CompanyDefination>> defaultReturn = new DefaultReturn<List<CompanyDefination>>();

            try
            {
                var branch = _branchRepository.Get(s => s.Id == branchId);
                var query = _companyDefinationRepository.GetIQueryable();
                var autoComplateField = _formdefinationRepository.GetAutoComplateField(companyDefinationFilterDTO.FormDefinationFieldId);

                query = query.Where(s => s.MainCompanyId == branch.MainCompanyId && !s.Deleted);
                if (!branch.CompanyAdmin)
                {
                    query = query.Where(s => s.CompanyBranchId == branch.Id);
                }
                if (!string.IsNullOrEmpty(companyDefinationFilterDTO.FilterValue))
                {
                    var filterValue=companyDefinationFilterDTO.FilterValue.ToUpperTrk();
                    query = query.Where(s => s.FieldForSearch.Contains(filterValue));
                }

                if (autoComplateField != null)
                {
                    var formdefinationTypeid = int.Parse(autoComplateField.FilterValue);
                    query = query.Where(s => s.CompanyDefinationDefinationType.Any(k => k.DefinationTypeId == formdefinationTypeid));
                }
                
                var companyDefinationList = query.ToList();
                defaultReturn.Data=companyDefinationList;

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

        public DefaultReturn<List<ControlAutoFieldDTO>> GetAutoComplateDefinationValues(int formdefinationTypeId, int addressId)
        {
            DefaultReturn<List<ControlAutoFieldDTO>> defaultReturn = new DefaultReturn<List<ControlAutoFieldDTO>>();
            try
            {
                var formdefination=_formdefinationRepository.GetFormDefinationField(formdefinationTypeId);

                var autoComplateFieldMaps = _formdefinationRepository.GetAutoComplateFieldMaps(formdefinationTypeId);
                var defination = _companyDefinationRepository.GetCompanyDefinations(s => s.Id == addressId).FirstOrDefault();
                defaultReturn.Data = new List<ControlAutoFieldDTO>();
                foreach (var autocomplete in autoComplateFieldMaps)
                {
                    var formDefinationField = _formdefinationRepository.GetDefinationField(formdefination.FormDefinationId, autocomplete.TagName);
                    if (formDefinationField == null)
                        continue;
                    var fieldValue = GetPropValue(defination, autocomplete.PropertyValue1);
                    if (!defaultReturn.Data.Any(s => s.FieldName == autocomplete.TagName))
                    {
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

                        defaultReturn.Data.Add(new ControlAutoFieldDTO()
                        {
                            ControlName = formDefinationField.GetControlName(),
                            FieldName= formDefinationField.TagName,
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

        public DefaultReturn<CompanyDefination> GetCompanyDefination(int companyId, int brachId, int id)
        {
            DefaultReturn<CompanyDefination> defaultReturn = new DefaultReturn<CompanyDefination>();

            try
            {
                var branch = _branchRepository.Get(s => s.Id == brachId && !s.Deleted);

                if (branch == null)
                {
                    throw new Exception("BranchIsnotDefined");
                }
                var companyDefination = _companyDefinationRepository.GetCompanyDefinations(s => s.Id == id && !s.Deleted).FirstOrDefault(); ;

                if (companyDefination == null)
                {
                    throw new Exception("CompanyDefinationIsNotDefined");
                }

                if (companyDefination.MainCompanyId != companyId)
                {
                    throw new Exception("DefinationIsNotSameCompany");
                }

                defaultReturn.Data = companyDefination;

            }
            catch (Exception ex)
            {
                defaultReturn.SetException(ex);
            }

            return defaultReturn;


        }

        public DefaultReturn<List<CompanyDefination>> GetCompanyDefinations(int companyId, int brachId)
        {
            DefaultReturn<List<CompanyDefination>> defaultReturn = new DefaultReturn<List<CompanyDefination>>();

            try
            {
                var branch = _branchRepository.Get(s => s.Id == brachId && !s.Deleted);
                if (branch == null)
                {
                    throw new Exception("BranchIsnotDefined");
                }
                if (!branch.CompanyAdmin)
                {
                    defaultReturn.Data = _companyDefinationRepository.GetCompanyDefinations(s => s.MainCompanyId == companyId && s.CompanyBranchId == brachId && !s.Deleted);

                }
                else
                {
                    defaultReturn.Data = _companyDefinationRepository.GetCompanyDefinations(s => s.MainCompanyId == companyId && !s.Deleted);

                }
            }
            catch (Exception ex)
            {
                defaultReturn.SetException(ex);
            }


            return defaultReturn;
        }

        public DefaultReturn<List<DefinationType>> GetDefinationTypes()
        {
            DefaultReturn<List<DefinationType>> defaultReturn = new DefaultReturn<List<DefinationType>>();
            try
            {

                defaultReturn.Data = _companyDefinationRepository.GetDefinationTypes().ToList();

            }
            catch (Exception ex)
            {
                defaultReturn.SetException(ex);
            }
            return defaultReturn;
        }

        public DefaultReturn<CompanyDefination> Save(CompanyDefination companyDefination)
        {
            DefaultReturn<CompanyDefination> defaultReturn = new DefaultReturn<CompanyDefination>();
            companyDefination.FieldForSearch = StringHelper.ToUpperTrk(companyDefination.CompanyName);

            if (!string.IsNullOrEmpty(companyDefination.DefinationTypeId))
            {
                var definationType = companyDefination.DefinationTypeId.Split(',');
                List<CompanyDefinationDefinationType> companyDefinationDefinationTypes = new List<CompanyDefinationDefinationType>();
                foreach (var def in definationType)
                {
                    if (string.IsNullOrEmpty(def))
                        continue;
                    var definationTypeId = int.Parse(def);
                    companyDefinationDefinationTypes.Add(new CompanyDefinationDefinationType() { DefinationTypeId = definationTypeId });

                }
                companyDefination.CompanyDefinationDefinationType = companyDefinationDefinationTypes;
            }

            if (companyDefination.Id == 0)
            {
                defaultReturn.Data = _companyDefinationRepository.Add(companyDefination);

            }
            else
            {
                defaultReturn.Data = _companyDefinationRepository.Update(companyDefination);

            }

            return defaultReturn;
        }
    }
}
