using CustomPortalV2.Core.Model.DTO;
using CustomPortalV2.Core.Model.FDefination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Business.Concrete
{
    public interface IFormDefinationAttachmentService
    {
        DefaultReturn<FormAttachmentType> Save(FormAttachmentType formAttachmentType);

        DefaultReturn<FormDefinationAttachment> Save(FormDefinationAttachment formDefinationAttachment);

        DefaultReturn<List<FormAttachmentFontStyle>> GetFormAttachmentFontStyles(int formAttachmentId);

        DefaultReturn<FormAttachmentFontStyle> Save(FormAttachmentFontStyle formAttachmentFont);

        DefaultReturn<FormAttachmentFontStyle> GetFontStyle(int id);

        DefaultReturn<bool> DeleteFormStyle(int id);


    }
}
