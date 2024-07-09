using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Core.Model.DTO;
using CustomPortalV2.Core.Model.FDefination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Business.Service
{
    public class FormDefinationAttachmentService : IFormDefinationAttachmentService
    {
        public DefaultReturn<bool> DeleteFormStyle(int id)
        {
            throw new NotImplementedException();
        }

        public DefaultReturn<FormAttachmentFontStyle> GetFontStyle(int id)
        {
            throw new NotImplementedException();
        }

        public DefaultReturn<List<FormAttachmentFontStyle>> GetFormAttachmentFontStyles(int formAttachmentId)
        {
            throw new NotImplementedException();
        }

        public DefaultReturn<FormAttachmentType> Save(FormAttachmentType formAttachmentType)
        {
            throw new NotImplementedException();
        }

        public DefaultReturn<FormDefinationAttachment> Save(FormDefinationAttachment formDefinationAttachment)
        {
            throw new NotImplementedException();
        }

        public DefaultReturn<FormAttachmentFontStyle> Save(FormAttachmentFontStyle formAttachmentFont)
        {
            throw new NotImplementedException();
        }
    }
}
