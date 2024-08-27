using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Core.Model.Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Business.Service
{
    public class WorkFlowService:IWorkFlowService
    {
        public WorkFlowService() { }

        public FreeWorkFlowDocument AddFreeWorkFlowDocument(FreeWorkFlowDocument freeWorkFlowDocument)
        {

            return freeWorkFlowDocument;
        }
    }
}
