﻿using CustomPortalV2.Core.Model.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.DataAccessLayer.Concrete
{
    public interface IParamRepository
    {
        Param? GetParam(string name);

        void AddParam(Param param);
    }
}
