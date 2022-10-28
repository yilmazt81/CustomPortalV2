using CustomPortalV2.DBLayer;
using CustomPortalV2.Model.Company;
 
namespace CustomPortalV2.Service
{
    public class CompanyService : ICompanyService
    {
        DBContext dBContext;
        public CompanyService(DBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public void SaveCompany(Company company)
        {

        }
    }

    public interface ICompanyService
    {
        void SaveCompany(Company company);
    }
}
