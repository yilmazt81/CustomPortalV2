﻿using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Core.Model.DTO;
using CustomPortalV2.Core.Model.Work;
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
        public WorkFlowService() { }

   

        public DefaultReturn<WorkFlowDocument> AddFreeWorkFlowDocument(WorkFlowDocument freeWorkFlowDocument)
        {
            throw new NotImplementedException();
        }

        public DefaultReturn<List<WorkFlow>> GetCompanyWorkFlow(int mainCompanyId, int branchId)
        {
            throw new NotImplementedException();
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
