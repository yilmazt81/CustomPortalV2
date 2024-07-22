using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Core.Model.Company;
using CustomPortalV2.Core.Model.DTO;
using CustomPortalV2.DataAccessLayer.Concrete;
using CustomPortalV2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Business.Service
{
    public class BranchService : IBranchService
    {
        IBranchRepository _branchRepository;
        public BranchService(IBranchRepository branchRepository)
        {

            _branchRepository = branchRepository;
        }
        private DefaultReturn<Branch> ChecFields(Branch branchDefination)
        {
            DefaultReturn<Branch> returnType = new DefaultReturn<Branch>();


            if (string.IsNullOrEmpty(branchDefination.Name))
            {
                returnType.ReturnCode = 2;
                returnType.ReturnMessage = "Şube Adı girmelisiniz";
                return returnType;
            }
            /* if (branchDefination.Name.IsSaveSqlInjection())
             {
                 returnType.ReturnCode = 3;
                 returnType.ReturnMessage = "Şube Adı Zararlı giriş";
             }*/
            if (branchDefination.BranchPackageId == 0)
            {
                returnType.ReturnCode = 4;
                returnType.ReturnMessage = "Paket Seçimi Yapmalısınız.";
            }
            if (branchDefination.MainCompanyId == 0)
            {
                returnType.ReturnCode = 6;
                returnType.ReturnMessage = "Firma bilgisi gerekli";

            }
            if (branchDefination.UserRuleId == 0)
            {
                returnType.ReturnCode = 5;
                returnType.ReturnMessage = "Yetki Seçimi Yapmalısınız.";
            }

            return returnType;
        }
        public DefaultReturn<Branch> AddBranch(Branch branch)
        {
            DefaultReturn<Branch> returnBranch = new DefaultReturn<Branch>();

            var returnT = ChecFields(branch);

            var newBranch = _branchRepository.AddBranch(branch);

            returnBranch.Data = newBranch;


            return returnBranch;
        }

        public DefaultReturn<Branch> GetBranch(int companyId, int id)
        {
            DefaultReturn<Branch> defaultReturn = new DefaultReturn<Branch>();
            try
            {
                var data = _branchRepository.GetBranches(s => s.Id == id).FirstOrDefault();
                if (data==null)
                {
                    throw new Exception("BranshisNotExist");
                }
                if (data.MainCompanyId != companyId)
                {
                    throw new Exception("BranchISNotSameCompany");
                }
                defaultReturn.Data = data;
            }
            catch (Exception ex)
            {
                defaultReturn.SetException(ex);
            }
          
            return defaultReturn;
        }



        public DefaultReturn<List<Branch>> GetCompanyBraches(int companyId, int branchId)
        {
            DefaultReturn<List<Branch>> defaultReturn = new DefaultReturn<List<Branch>>();

            try
            {
                var userBranch = _branchRepository.Get(s => s.Id == branchId);
                if (userBranch == null)
                {
                    throw new Exception("Branchisnull");
                }
                if (userBranch.MainCompanyId != companyId)
                {
                    throw new Exception("MainCompanyIdDiffrend");
                }
                if (userBranch.CompanyAdmin)
                {
                    var branchList = _branchRepository.GetBranches(s => s.MainCompanyId == companyId && !s.Deleted);
                    defaultReturn.Data = branchList;
                }
                else
                {
                    var branchList = _branchRepository.GetBranches(s => s.Id == branchId &&  !s.Deleted );
                    defaultReturn.Data= branchList;
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
