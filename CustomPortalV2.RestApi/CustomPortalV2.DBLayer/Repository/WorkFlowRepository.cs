using CustomPortalV2.Core.Model.Work;
using CustomPortalV2.DataAccessLayer.Concrete;
using CustomPortalV2.Model.Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.DataAccessLayer.Repository
{
    public class WorkFlowRepository : IWorkFlowRepository
    {
        DBContext _dbContext;
        public WorkFlowRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }


        public WorkFlow Add(WorkFlow workFlow)
        {
            throw new NotImplementedException();
        }

      
        public WorkFlowDocument AddDocument(WorkFlowDocument freeWorkFlowDocument)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WorkFlow> GetWorkFlows(int mainCompanyId, int branchId)
        {
            var branch = _dbContext.Branches.Single(s => s.Id == branchId);
            if (!branch.CompanyAdmin)
            {
                return _dbContext.WorkFlow.Where(s => s.MainCompanyId == mainCompanyId && s.CompanyBranchId == branchId).ToArray();
            }
            else
            {
                return _dbContext.WorkFlow.Where(s => s.MainCompanyId == mainCompanyId).ToArray();

            } 
        }

        public WorkFlow Update(WorkFlow workFlow)
        {
            throw new NotImplementedException();
        }
    }
}
