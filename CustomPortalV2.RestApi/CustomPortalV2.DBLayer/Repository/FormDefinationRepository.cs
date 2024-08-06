using CustomPortalV2.Core.Model.App;
using CustomPortalV2.Core.Model.Autocomplete;
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

        public ComboBoxItem AddComboboxItem(ComboBoxItem comboBoxItem)
        {
                _dbContext.ComboBoxItem.Add(comboBoxItem);
                _dbContext.SaveChanges();

                return comboBoxItem;
            

        }

        public bool IsExistComboboxItem(ComboBoxItem comboBoxItem)
        {

            return _dbContext.ComboBoxItem.Any(s => s.MainCompanyId == comboBoxItem.MainCompanyId && s.ItemType == s.ItemType && s.TagName == comboBoxItem.TagName);

        }
            

            public FormDefinationField AddDefinationField(FormDefinationField formDefinationField)
        {
            _dbContext.FormDefinationField.Add(formDefinationField);

            _dbContext.SaveChanges();

            return formDefinationField;
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

        public IEnumerable<ComboBoxItem> GetComboBoxItems(int mainCompanyId, string fieldTag)
        {

            return _dbContext.ComboBoxItem.Where(s => s.MainCompanyId == mainCompanyId && s.ItemType == fieldTag).ToList();
        }

        public IEnumerable<CustomSector> GetCompanySectors(int companyId)
        {

            return _dbContext.CustomSector.Where(s => s.MainCompanyId == companyId).ToList();
        }

        public IEnumerable<FieldType> GetDefaultFieldTypes()
        {
            return _dbContext.FieldType.ToList();
        }

        public IEnumerable<FontType> GetFontTypes()
        {
            return _dbContext.FontTypes.ToArray();
        }

        public FormDefinationField GetFormDefinationField(int id)
        {
            return _dbContext.FormDefinationField.Single(s => s.Id == id);
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

        public bool IsExistFormDefinationField(int formDefinationId, string tagName)
        {

            return _dbContext.FormDefinationField.Any(s => s.FormDefinationId == formDefinationId && s.TagName == tagName);
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

        public FormDefinationField UpdateDefinationField(FormDefinationField definationField)
        {
            var dbField = _dbContext.FormDefinationField.Single(s => s.Id == definationField.Id);
            dbField.FieldCaption = definationField.FieldCaption;
            dbField.TagName = definationField.TagName;
            dbField.CellName = definationField.CellName;
            dbField.Deleted = definationField.Deleted;
            dbField.Mandatory = definationField.Mandatory;
            dbField.AutoComplate = definationField.AutoComplate;
            dbField.ControlType = definationField.ControlType;
            dbField.FormGroupId = definationField.FormGroupId;
            dbField.OrderNumber = definationField.OrderNumber;
            dbField.Bold = definationField.Bold;
            dbField.Italic = definationField.Italic;
            dbField.FontSize = definationField.FontSize;
            dbField.TranslateLanguage = definationField.TranslateLanguage;
            dbField.FontFamily = definationField.FontFamily;
            _dbContext.SaveChanges();

            return dbField;
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

        public AutocompleteField? GetAutoComplateField(int definationFieldId)
        {
            return _dbContext.AutocompleteField.FirstOrDefault(s => s.FormDefinationFieldId == definationFieldId);
        }

        public List<AutocompleteFieldMap> GetAutoComplateFieldMaps(int definationFieldId)
        {
           return _dbContext.AutocompleteFieldMap.Where(s=>s.FormDefinationFieldId== definationFieldId).ToList();
        }
    }
}
