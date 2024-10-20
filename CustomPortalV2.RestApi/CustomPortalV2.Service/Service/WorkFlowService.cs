using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Core.Model.DTO;
using CustomPortalV2.Core.Model.Work;
using CustomPortalV2.DataAccessLayer.Concrete;
using CustomPortalV2.Model.Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Business.Service
{
    public class WorkFlowService : IWorkFlowService
    {
        IWorkFlowRepository _workFlowRespository;
        public WorkFlowService(IWorkFlowRepository workFlowRespository)
        {
            _workFlowRespository = workFlowRespository;
        }



        public DefaultReturn<WorkFlowDocument> AddFreeWorkFlowDocument(WorkFlowDocument freeWorkFlowDocument)
        {
            throw new NotImplementedException();
        }

        public DefaultReturn<List<WorkFlow>> GetCompanyWorkFlow(int mainCompanyId, int branchId)
        {
            DefaultReturn<List<WorkFlow>> defaultReturn = new DefaultReturn<List<WorkFlow>>();
            var workFlowList = _workFlowRespository.GetWorkFlows(mainCompanyId, branchId);
            defaultReturn.Data = workFlowList.ToList();
            return defaultReturn;

        }

        public DefaultReturn<WorkFlow> GetWorkFlow(int id, int mainCompanyId)
        {
            throw new NotImplementedException();
        }

        public DefaultReturn<WorkFlow> Save(WorkFlow workFlow)
        {
            throw new NotImplementedException();
        }
    }
}
