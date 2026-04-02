using CustomPortalV2.Core.Model.DTO;
using CustomPortalV2.DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.DataAccessLayer.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        DBContext _dbContext;

        public DashboardRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public DashboardReport GetDateRecords(int mainCompanyId)
        {
            DashboardReport dashboardReport = new DashboardReport();

            var dayCount = _dbContext.FormMetaDataCounter.Count(x => x.MaincompanyId == mainCompanyId && x.CounterDate.Date == DateTime.Now.Date);
            var weekCount = _dbContext.FormMetaDataCounter.Count(x => x.MaincompanyId == mainCompanyId && x.CounterDate >= DateTime.Now.AddDays(-7));
            var yearCount = _dbContext.FormMetaDataCounter.Count(x => x.MaincompanyId == mainCompanyId && x.CounterDate.Year==DateTime.Now.Year);
            dashboardReport.DayCount= dayCount;
            dashboardReport.WeekCount= weekCount;
            dashboardReport.YearCount= yearCount;
            return dashboardReport;
        }
    }
}
