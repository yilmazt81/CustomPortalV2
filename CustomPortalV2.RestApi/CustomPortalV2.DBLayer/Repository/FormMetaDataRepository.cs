using CustomPortalV2.Core.Model.Form;
using CustomPortalV2.Core.Model.Log;
using CustomPortalV2.DataAccessLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.DataAccessLayer.Repository
{
    public class FormMetaDataRepository : IFormMetaDataRepository
    {
        DBContext _dbContext;
        public FormMetaDataRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddCopyDocumentLog(CopyDocumentLog copyDocumentLog)
        {
            _dbContext.CopyDocumentLog.Add(copyDocumentLog);
            _dbContext.SaveChanges();
        }

        public IEnumerable<FormMetaData> Get(Expression<Func<FormMetaData, bool>> predicate, int maxCount)
        {
            return _dbContext.FormMetaData.Take(maxCount).Where(predicate).OrderByDescending(s => s.Id).ToList();
        }

        public FormMetaData? Get(int id)
        {
            return _dbContext.FormMetaData.Include(s => s.FormMetaDataAttribute).Include(s => s.FormMetaDataAttribute_CustomeField).Single(s => s.Id == id);
        }

        public FormMetaData Save(FormMetaData formMetaData)
        {
            _dbContext.FormMetaData.Add(formMetaData);
            _dbContext.SaveChanges();
            return formMetaData;
        }

        public FormMetaData Update(FormMetaData formMetaData)
        {
            var dbFormData = _dbContext.FormMetaData.Include(s => s.FormMetaDataAttribute).Single(s => s.Id == formMetaData.Id);
            dbFormData.EditedBy = formMetaData.EditedBy;
            dbFormData.EditedDate = formMetaData.EditedDate;
            dbFormData.EditedId = formMetaData.EditedId;
            dbFormData.SenderCompanyId = formMetaData.SenderCompanyId;
            dbFormData.SenderCompanyName = formMetaData.SenderCompanyName;
            dbFormData.RecrivedCompanyName = formMetaData.RecrivedCompanyName;
            dbFormData.RecrivedCompanyId = formMetaData.RecrivedCompanyId;
            dbFormData.FormDefinationId = formMetaData.FormDefinationId;
            dbFormData.FormDefinationName = formMetaData.FormDefinationName;
            dbFormData.CustomSectorId = formMetaData.CustomSectorId;
            dbFormData.Deleted=formMetaData.Deleted;

            var oldAttributes = dbFormData.FormMetaDataAttribute;
            foreach (var oldAttribute in dbFormData.FormMetaDataAttribute)
            {
                if (!formMetaData.FormMetaDataAttribute.Any(s => s.TagName == oldAttribute.TagName))
                {
#pragma warning disable CS8601 // Possible null reference assignment.
                    _dbContext.FormMetaDataAttributeHistory.Add(new FormMetaDataAttributeHistory()
                    {
                        TagName = oldAttribute.TagName,
                        EditedBy = formMetaData.EditedBy,
                        EditedDate = formMetaData.EditedDate.Value,
                        EditedId = formMetaData.EditedId.Value,
                        FormMetaDataId = dbFormData.Id,
                        NewValue = "",
                        FormMetaDataAttributeId = oldAttribute.Id,
                        OldValue = oldAttribute.FieldValue,
                        ModifyByPartner=false

                    });
#pragma warning restore CS8601 // Possible null reference assignment.
                    oldAttribute.FieldValue = "";

                }
            }

            foreach (var newAttribute in formMetaData.FormMetaDataAttribute)
            {
                var oldAttribute = dbFormData.FormMetaDataAttribute.FirstOrDefault(s => s.TagName == newAttribute.TagName);
                if (oldAttribute == null)
                {
                    _dbContext.FormMetaDataAttribute.Add(new FormMetaDataAttribute()
                    {
                        FieldValue = newAttribute.FieldValue,
                        TagName = newAttribute.TagName,
                        FormMetaDataId = formMetaData.Id,
                        FormDefinationFieldId = newAttribute.FormDefinationFieldId,
                    });
                }
                else
                {
                    if (oldAttribute.FieldValue != newAttribute.FieldValue)
                    { 

                        _dbContext.FormMetaDataAttributeHistory.Add(new FormMetaDataAttributeHistory()
                        {
                            TagName = oldAttribute.TagName,
                            EditedBy = formMetaData.EditedBy,
                            EditedDate = formMetaData.EditedDate.Value,
                            EditedId = formMetaData.EditedId.Value,
                            FormMetaDataId = dbFormData.Id,
                            NewValue = newAttribute.FieldValue,
                            FormMetaDataAttributeId = oldAttribute.Id,
                            OldValue = oldAttribute.FieldValue,
                            ModifyByPartner=false,

                        });
                        oldAttribute.FieldValue = newAttribute.FieldValue;
                        oldAttribute.FormDefinationFieldId = newAttribute.FormDefinationFieldId;
                    }

                 
                }
            }
            _dbContext.SaveChanges();
            return dbFormData;

        }
    }
}
