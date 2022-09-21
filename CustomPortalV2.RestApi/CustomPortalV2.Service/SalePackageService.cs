using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomPortalV2.DBLayer;
using CustomPortalV2.Model.Sale;
using Microsoft.EntityFrameworkCore;

namespace CustomPortalV2.Service
{
    public class SalePackageService : ISalePackageService
    {
        DBContext _context;
        public SalePackageService(DBContext context)
        {
            _context = context;
        }

        public SalePackageItem[] GetSalePackageItem(int packageId)
        {
            return _context.SalePackagesItems.Where(s => s.SalePackageId == packageId).ToArray();
        }

        public SalePackage[] GetSalePackages()
        {
            return _context.SalePackages.ToArray();
        }
    }


    public interface ISalePackageService
    {

        public SalePackage[] GetSalePackages();

        public SalePackageItem[] GetSalePackageItem(int packageId);
    }
}
