﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.FDefination
{
    public class CustomeField_VirtualTableField
    {
        public int Id { get; set; }
        public int CustomeField_VirtualTableId { get; set; }
        public string HeaderText { get; set; }
        public string CalculateField { get; set; }
        public Nullable<int> OrderNumber { get; set; }
        public Nullable<int> HeaderWidthRuleValue { get; set; }
        public string HeaderWidth { get; set; }
        public Nullable<int> FontSize { get; set; }
        public Nullable<bool> Bold { get; set; }
        public Nullable<bool> Italic { get; set; }

        public int MainCompanyId { get; set; }
        public virtual CustomeField_VirtualTable CustomeField_VirtualTable { get; set; }
    }
}
