﻿using CustomPortalV2.Core.Model.App;
using CustomPortalV2.Core.Model.FDefination;
using CustomPortalV2.Model.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.DataAccessLayer.Concrete
{
    public interface IFormDefinationRepository
    {

        IEnumerable<FormDefination> Get(Expression<Func<FormDefination, bool>> predicate);
    }
}