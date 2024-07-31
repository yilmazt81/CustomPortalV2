using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Business.Helper;
using CustomPortalV2.Core.Model.App;
using CustomPortalV2.Core.Model.Definations;
using CustomPortalV2.Core.Model.DTO;
using CustomPortalV2.DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Business.Service
{
    public class CompanyDefinationService : ICompanyDefinationService
    {

        ICompanyAdresDefinationRepository _companyDefinationRepository = null;
        IBranchRepository _branchRepository = null;
        public CompanyDefinationService(ICompanyAdresDefinationRepository companyDefination, IBranchRepository branchRepository)
        {
            _companyDefinationRepository = companyDefination;
            _branchRepository = branchRepository;
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
