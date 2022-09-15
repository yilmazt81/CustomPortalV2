using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomPortalV2.Model.Sale;

namespace CustomPortalV2.Service
{
    public class SalePackageService : ISalePackage
    {
        public SalePackage[] GetSalePackages()
        {
           
        }
    }


    public interface ISalePackage
    {

        public SalePackage[] GetSalePackages();
    }
}
