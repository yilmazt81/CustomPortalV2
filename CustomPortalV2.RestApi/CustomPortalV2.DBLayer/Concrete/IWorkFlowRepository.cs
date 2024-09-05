using CustomPortalV2.Core.Model.Work;
using CustomPortalV2.Model.Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.DataAccessLayer.Concrete
{
    public interface IWorkFlowRepository
    {
        WorkFlowDocument AddDocument(WorkFlowDocument freeWorkFlowDocument);

        WorkFlow Add(WorkFlow workFlow);
        WorkFlow Update(WorkFlow workFlow);


    }
}
