using CustomPortalV2.Core.Model.Definations;
using CustomPortalV2.Core.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Business.Concrete
{
    public interface IFoodPersonelService
    {
        DefaultReturn<List<FoodPersonel>> Filter(DefinationFilterDTO companyDefinationFilterDTO);
        DefaultReturn<List<ControlAutoFieldDTO>> GetAutoComplateDefinationValues(int formdefinationId, int personelid);
    }
}
