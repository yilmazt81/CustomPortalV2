using CustomPortalV2.Core.Model.App;
using CustomPortalV2.Core.Model.FDefination;
using CustomPortalV2.DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.DataAccessLayer.Repository
{
    public class FormDefinationRepository : IFormDefinationRepository
    {
        DBContext _dbContext;
        public FormDefinationRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public FormDefination Add(FormDefination formDefination)
        {
            _dbContext.FormDefination.Add(formDefination);

            _dbContext.SaveChanges();

            return formDefination;
        }

        public FormGroup AddGroup(FormGroup formGroup)
        {
            _dbContext.FormGroup.Add(formGroup);

            _dbContext.SaveChanges();

            return formGroup;
        }

        public IEnumerable<FormDefination> Get(Expression<Func<FormDefination, bool>> predicate)
        {

            return _dbContext.FormDefination.Where(predicate).ToList();
        }

        public IEnumerable<CustomSector> GetCompanySectors(int companyId)
        {

            return _dbContext.CustomSector.Where(s => s.MainCompanyId == companyId).ToList();
        }

        public FormGroup GetFormGroup(int id)
        {

            return _dbContext.FormGroup.Single(s => s.Id == id);
        }

        public IEnumerable<FormDefinationField> GetFormGroupFields(int formGroupId)
        {
            return _dbContext.FormDefinationField.Where(s => s.FormGroupId == formGroupId).OrderBy(s => s.OrderNumber).ToList();
        }

        public IEnumerable<FormGroup> GetFormGroups(int formDefinationId)
        { 
            return _dbContext.FormGroup.Where(s => s.FormDefinationId == formDefinationId && !s.Deleted).ToList().OrderBy(s => s.OrderNumber).ToList();
        }

        public FormDefination Update(FormDefination formDefination)
        {
            var dbDefination = _dbContext.FormDefination.Single(s => s.Id == formDefination.Id);
            dbDefination.FormName = formDefination.FormName;
            dbDefination.CustomSectorId = formDefination.CustomSectorId;
            dbDefination.CustomSectorName = formDefination.CustomSectorName;
            dbDefination.Deleted = formDefination.Deleted;
            _dbContext.SaveChanges();

            return dbDefination;
        }

        public FormGroup UpdateGroup(FormGroup formGroup)
        {
            var dbGroup = _dbContext.FormGroup.Single(s => s.Id == formGroup.Id);
            dbGroup.Deleted = formGroup.Deleted;
            dbGroup.OrderNumber = formGroup.OrderNumber;
            dbGroup.FormNumber = formGroup.FormNumber;
            dbGroup.AllowEditCustomer = formGroup.AllowEditCustomer;
            dbGroup.Name = formGroup.Name;

            _dbContext.SaveChanges();

            return dbGroup;
        }
    }
}
