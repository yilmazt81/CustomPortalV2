using CustomPortalV2.Core.Model.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.DataAccessLayer.Concrete
{
    public interface ILogRepository
    {
        void AddCreateSoftDocumentLog(UserCreateSoftDocumentLog userCreateSoftDocumentLog);
    }
}
