using CustomPortalV2.Core.Model.Autocomplete;
using CustomPortalV2.Core.Model.DTO;
using CustomPortalV2.Core.Model.FDefination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Business.Concrete
{
    public interface IFormDefinationService
    {
        DefaultReturn<List<FormDefination>> GetCompanyDefinations(int mainCompanyId);
        DefaultReturn<List<FormDefination>> GetCompanyDefination(int mainCompanyId, int sectorId);

        DefaultReturn<FormDefination> GetFormDefination(int id);
        DefaultReturn<FormDefination> Save(FormDefination formDefination);
        DefaultReturn<FormVersion> Save(FormVersion formDefination);
        DefaultReturn<ComboBoxItem> Save(ComboBoxItem comboBoxItem);
        DefaultReturn<List<FormDefinationField>> GetFormDefinationFields(int formgroupId);
        DefaultReturn<List<FormDefinationField>> GetFormDefinationAddFields(int formdefinationId);
        DefaultReturn<FormDefinationField> GetFormDefinationField(int id);

        DefaultReturn<List<FormVersion>> GetDefinationFormVersions(int formdefinationId);
        DefaultReturn<List<FormDefinationAttachment>> GetDefinationFormAttachments(int formdefinationId);
        DefaultReturn<FormVersion> GetFormDefinationVersion(int id);
        DefaultReturn<FormDefinationAttachment> GetFormDefinationAttachment(int id);

        DefaultReturn<string> GetTemplateForm(int formdefinationId, int companyId);
          

        DefaultReturn<List<FormGroup>> GetFormGroups(int formDefinationId);
        DefaultReturn<FormGroup> GetFormGroup(int id);
        DefaultReturn<List<ComboBoxItem>> GetComboBoxItems(int mainCompanyId, string tagName);

        

        DefaultReturn<List<CustomSectorDTO>> GetSector(int mainCompanyId, int applicationLangId);
        DefaultReturn<FormGroup> SaveGroup(FormGroup formDefinationField);
        DefaultReturn<FormDefinationField> SaveFormDefinationField(FormDefinationField formGroup);
        DefaultReturn<bool> DeleteGroup(int formDefinationTypeId, int groupid, int companyId);

        DefaultReturn<List<FieldType>> GetFielTypes(int companyId);

        DefaultReturn<List<FontType>> GetFontTypes();

        DefaultReturn<List<GroupDTO>> GetFormGroupDTOs(int formDefinationId);

        DefaultReturn<AutocompleteField> GetAutoComplateField(int formdefinationFieldId);

        DefaultReturn<List<AutocompleteFieldMap>> GetAutoComplateFieldMaps(int formDefinationFieldid);

        DefaultReturn<List<ObjectFieldDTO>> GetObjectFieldList(string objectName,int userLangId);

        DefaultReturn<AutocompleteFieldMap> SaveAutoComplate(SaveAutoComplateDTO saveAutoComplate);

        DefaultReturn<bool> DeleteAutoComplate(int autoComplateFieldId);

        DefaultReturn<FormDefinationAttachment> Save(FormDefinationAttachment formDefinationAttachment, string[] taglist);

        DefaultReturn<List<CustomeField>> GetCustomeFields(int mainCompanyId);

        DefaultReturn<List<CustomeFieldItem>> GetCustomeField(int mainCompanyId, string fieldTypeName);

        DefaultReturn<CustomeField> SaveCustomeField(CustomeField customeField);

        DefaultReturn<List<CustomeFieldItem>> GetCustomeFieldItems(int customeFieldId);

        int GetCustomeFieldMaxOrder(int customeFieldId);

        DefaultReturn<CustomeFieldItem> SaveCustomeFieldItem(CustomeFieldItem customeFielditem);


        DefaultReturn<List<FormGroup>> CloneFormGroup(int[] sourceformGroupId, int targetDefinationId, int companyId, int userId);

        DefaultReturn<List<string>> GetDistinctFieldNames(int mainCompanyId);


    }
}
