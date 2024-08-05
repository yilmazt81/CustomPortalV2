using CustomPortalV2.Core.Model.FDefination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Core.Model.DTO
{
    public class GroupDTO
    {
        public int id { get; set; }
        public string Name { get; set; }

        public string GroupTag { get; set; }
        public string FormNumber { get; set; }



        public List<DefinationFieldDTO> FormFields { get; set; }

    }

    public class DefinationFieldDTO
    {
        public DefinationFieldDTO() { }
        public DefinationFieldDTO(FormDefinationField definationField)
        {
            Caption = definationField.FieldCaption;
            TagName = definationField.TagName;
            ControlType = definationField.ControlType;
            AutoComplate= definationField.AutoComplate;
            id=definationField.Id;
        }

        public int id { get; set; }
        public string Caption { get; set; }
        public string TagName { get; set; }
        public bool AutoComplate { get; set; }

        public string ControlType { get; set; }
        public List<ComboBoxItem> ComboBoxItems { get; set; }
    }
}
