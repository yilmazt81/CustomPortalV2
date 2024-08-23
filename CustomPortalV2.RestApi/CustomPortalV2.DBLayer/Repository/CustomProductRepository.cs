using CustomPortalV2.Core.Model.Definations;
using CustomPortalV2.DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.DataAccessLayer.Repository
{
    public class CustomProductRepository : ICustomProductRepository
    {
        DBContext _dbContext;

        public CustomProductRepository()
        {
        }

        public CustomProductRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public CustomProduct Add(CustomProduct customProduct)
        {
            _dbContext.CustomProduct.Add(customProduct);
            _dbContext.SaveChanges();
            return customProduct;
        }

        public CustomProduct Get(int id)
        {
            return _dbContext.CustomProduct.Single(s => s.Id == id);
        }

        public IEnumerable<CustomProduct> GetCustomProducts(Expression<Func<CustomProduct, bool>> predicate)
        {
            return _dbContext.CustomProduct.Where(predicate);
        }

        public IQueryable<CustomProduct> GetIQueryable()
        {
            return _dbContext.CustomProduct.AsQueryable();
        }

        public CustomProduct Update(CustomProduct customProduct)
        {
            var dbProduct = _dbContext.CustomProduct.Single(s => s.Id == customProduct.Id);
            dbProduct.ProductName = customProduct.ProductName;
            dbProduct.ProductName_TRK = customProduct.ProductName_TRK;
            dbProduct.IntendedUse = customProduct.IntendedUse;
            dbProduct.TransferTemperature = customProduct.TransferTemperature;
            dbProduct.ProductCulture = customProduct.ProductCulture;
            dbProduct.GtipCode = customProduct.GtipCode;
            dbProduct.Transfercondition = customProduct.Transfercondition;
            dbProduct.EditedBy = customProduct.EditedBy;
            dbProduct.EditedDate = DateTime.Now;
            dbProduct.EditedId = customProduct.EditedId;
            dbProduct.ScientificName = customProduct.ScientificName;

            _dbContext.SaveChanges();

            return dbProduct;
        }
    }
}
