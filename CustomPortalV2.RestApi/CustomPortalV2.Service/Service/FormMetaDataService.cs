using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Core.Model.App;
using CustomPortalV2.Core.Model.Company;
using CustomPortalV2.Core.Model.DTO;
using CustomPortalV2.Core.Model.Form;
using CustomPortalV2.DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Business.Service
{
    public class FormMetaDataService : IFormMetaDataService
    {
        IFormMetaDataRepository _formMetaDataRepository;
        IBranchRepository _branchRepository;
        public FormMetaDataService(IFormMetaDataRepository formMetaDataRepository, IBranchRepository branchRepository)
        {
            _formMetaDataRepository = formMetaDataRepository;
            _branchRepository = branchRepository;
        }
        public DefaultReturn<List<FormMetaData>> FilterForms(FormMetaDataFilterPost formMetaData)
        {
            throw new NotImplementedException();
        }

        public DefaultReturn<List<FormMetaData>> GetBranchFormMetaData(int companyId, int brachId)
        {
            DefaultReturn<List<FormMetaData>> defaultReturn= new DefaultReturn<List<FormMetaData>>();   
            var branch = _branchRepository.Get(s => s.Id == brachId);
            
            if (branch.CompanyAdmin)
            {
                defaultReturn.Data = _formMetaDataRepository.Get(s => s.MainCompanyId == companyId && s.Deleted,50).ToList().OrderBy(s=>s.Id).ToList();
            }
            else
            {
                defaultReturn.Data = _formMetaDataRepository.Get(s => s.MainCompanyId == companyId && s.CompanyBranchId==brachId && s.Deleted, 50).ToList().OrderBy(s => s.Id).ToList();
            }


            return defaultReturn;

        }

        public DefaultReturn<FormMetaData> GetCompanyFormMetaData(int companyId, int branchId, int id)
        {

            DefaultReturn<FormMetaData> defaultReturn = new DefaultReturn<FormMetaData>();

            try
            {
                if (id == 0)
                {
                    defaultReturn.Data = new FormMetaData()
                    {
                        MainCompanyId = companyId,
                        CompanyBranchId = branchId,
                        CreatedDate= DateTime.Now,
                    };

                }
                else
                {
                    var branch = _branchRepository.Get(s => s.Id == branchId);
                    var formMetaData = _formMetaDataRepository.Get(s => s.Id == id, 1).FirstOrDefault();
                    if (formMetaData.MainCompanyId != companyId)
                    {
                        throw new Exception("DocumentISNotInYourCompany");
                    }
                    if (!branch.CompanyAdmin)
                    {
                        if (branch.Id != formMetaData.CompanyBranchId)
                        {
                            throw new Exception("DocumentISNotBelongToyou");
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

        public DefaultReturn<FormMetaData> GetFormMetaData(User user, int id)
        {
            throw new NotImplementedException();
        }
    }
}
