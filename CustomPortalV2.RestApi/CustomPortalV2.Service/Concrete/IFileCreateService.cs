using CustomPortalV2.Core.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Business.Concrete
{
    public interface IFileCreateService
    {

        DefaultReturn<string> ConvertAttachment(int id, int attachmentTypeid, int companyId, int userId,string tempFolder);

        DefaultReturn<string> ConvertFormVersion(int id, int formVersionId, int companyId, int userId,string tempFolder);

    }
}
