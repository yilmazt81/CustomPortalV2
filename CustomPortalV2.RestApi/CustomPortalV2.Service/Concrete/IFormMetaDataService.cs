﻿using CustomPortalV2.Core.Model.App;
using CustomPortalV2.Core.Model.DTO;
using CustomPortalV2.Core.Model.Form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Business.Concrete
{
    public interface IFormMetaDataService
    {

        DefaultReturn<List<FormMetaData>> GetBranchFormMetaData(int companyId,int brachId);
        DefaultReturn<FormMetaData> GetCompanyFormMetaData(int companyId,int branchId,int id);

        DefaultReturn<FormMetaData> GetFormMetaData(User user,int id);

        DefaultReturn<List<FormMetaData>> FilterForms(FormMetaDataFilterPost formMetaData);

        DefaultReturn<FormMetaData> Save(FormMetaDataDTO formMetaDataDTO);

        DefaultReturn<FormConvertContainerDTO> GetFormConvertList(int id, int mainCompanyId);

        DefaultReturn<FormMetaData> CloneForm(int mainCompanyId, int branchId, int userId, string username, int sourceformId);

        DefaultReturn<bool> DeleteForm(int mainCompanyId, int brachId, int id);
        /*

        DefaultReturn<FormMetaData> CloneDocument(int companyId, int brachId, int userId,string userFullName, int sourceFormId); */

    }
}
