using CustomPortalV2.Core.Model.DTO;
using CustomPortalV2.Core.Model.Work;
using CustomPortalV2.Model.Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Business.Concrete
{
    public interface IWorkFlowService
    {
        DefaultReturn<WorkFlowDocument> AddFreeWorkFlowDocument(WorkFlowDocument freeWorkFlowDocument);

        DefaultReturn<List<WorkFlow>> GetCompanyWorkFlow(int mainCompanyId, int branchId);

        DefaultReturn<WorkFlow> GetWorkFlow(int id,int mainCompanyId);

    }
}
