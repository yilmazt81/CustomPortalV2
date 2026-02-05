using CustomPortalV2.Core.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Business.Concrete
{
    public interface IOpenAIService
    {
        Task<DefaultReturn<string>> SendPromtAsync(AIPostRequest promt);
    }
}
