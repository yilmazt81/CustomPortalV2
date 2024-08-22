using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Core.Model.DTO;
using CustomPortalV2.DataAccessLayer.Concrete;
using CustomPortalV2.TemplateProcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CustomPortalV2.Business.Service
{
    public class FileCreateService : IFileCreateService
    {
        IFormMetaDataRepository _formMetaDataRepository;
        IFirebaseStorage _firebaseStorage;
        IFormDefinationRepository _formDefinationRepository;
        public FileCreateService(IFormMetaDataRepository formMetaDataRepository,
                        IFirebaseStorage firebaseStorage,
                        IFormDefinationRepository formDefinationRepository)
        {
            _firebaseStorage = firebaseStorage;
            _formMetaDataRepository = formMetaDataRepository;
            _formDefinationRepository = formDefinationRepository;
        }
        public DefaultReturn<string> ConvertAttachment(int id, int attachmentTypeid, int companyId, int userId, string tempFolder)
        {
            DefaultReturn<string> defaultReturn = new DefaultReturn<string>();
            try
            {
                var formData = _formMetaDataRepository.Get(id);
                if (formData.CompanyBranchId != companyId)
                {
                    throw new Exception("FormIsNotCompany");
                }
                var formAttachment = _formDefinationRepository.GetFormDefinationAttachment(attachmentTypeid);
                var attachmentFontStyle = _formDefinationRepository.GetFormAttachmentFontStyles(attachmentTypeid).ToArray();
                var comboBoxItems = _formDefinationRepository.GetComboBoxItems(formData.MainCompanyId).ToArray();
                var customeFields = _formDefinationRepository.GetCustomeFields(companyId).ToArray();
                var customeFieldItems = _formDefinationRepository.GetCustomeFielList(companyId).ToArray();
                var customeVirtualTables = _formDefinationRepository.GetCustomeVirtualTables(companyId).ToArray();
                var customeVirtualTableFields = _formDefinationRepository.GetCustomeVirtualTableFields(companyId).ToArray();
                var translateDictonary = _formDefinationRepository.GetTranslateDictionaries().ToArray();
                if (!formAttachment.Active)
                {
                    throw new Exception("AttachmentNotActive");
                }

                string newFileName = Path.Combine(tempFolder, Guid.NewGuid().ToString() + Path.GetExtension(formAttachment.FileName));
                using (WebClient webClient = new WebClient())
                {
                    webClient.DownloadFile(formAttachment.FilePath, newFileName);

                }
                var formDefinationFields=_formDefinationRepository.GetAllFields(formData.FormDefinationId).ToArray();
                if (Path.GetExtension(formAttachment.FileName) == ".docx")
                {
                    using (SoftCreatorWord softCreatorWord = new SoftCreatorWord())
                    {
                        softCreatorWord.ChangeTemplateForm(newFileName,
                            formDefinationFields,
                            formData.FormMetaDataAttribute_CustomeField.ToArray(),
                            formData.FormMetaDataAttribute.ToArray(), null, formData.FormDefinationId, formAttachment.Id, 0, attachmentFontStyle, comboBoxItems, customeFields, customeFieldItems, customeVirtualTables, customeVirtualTableFields, translateDictonary);
                    }
                }
            }
            catch (Exception ex)
            {
                defaultReturn.SetException(ex);
            }

            return defaultReturn;
        }

        public DefaultReturn<string> ConvertFormVersion(int id, int formVersionId, int companyId, int userId, string tempFolder)
        {
            DefaultReturn<string> defaultReturn = new DefaultReturn<string>();
            try
            {
                var formData = _formMetaDataRepository.Get(id);
                if (formData.CompanyBranchId != companyId)
                {
                    throw new Exception("FormIsNotCompany");
                }
                var formVersion = _formDefinationRepository.GetFormDefinationVersion(formVersionId); 
                var comboBoxItems = _formDefinationRepository.GetComboBoxItems(formData.MainCompanyId).ToArray();
                var customeFields = _formDefinationRepository.GetCustomeFields(companyId).ToArray();
                var customeFieldItems = _formDefinationRepository.GetCustomeFielList(companyId).ToArray();
                var customeVirtualTables = _formDefinationRepository.GetCustomeVirtualTables(companyId).ToArray();
                var customeVirtualTableFields = _formDefinationRepository.GetCustomeVirtualTableFields(companyId).ToArray();
                var translateDictonary = _formDefinationRepository.GetTranslateDictionaries().ToArray();
                if (!formVersion.Active)
                {
                    throw new Exception("Version");
                }

                string newFileName = Path.Combine(tempFolder, Guid.NewGuid().ToString() + Path.GetExtension(formVersion.FileName));
                using (WebClient webClient = new WebClient())
                {
                    webClient.DownloadFile(formVersion.FilePath, newFileName);

                }
                var formDefinationFields = _formDefinationRepository.GetAllFields(formData.FormDefinationId).ToArray();
                if (Path.GetExtension(formVersion.FileName) == ".docx")
                {
                    using (SoftCreatorWord softCreatorWord = new SoftCreatorWord())
                    {
                        softCreatorWord.ChangeTemplateForm(newFileName,
                            formDefinationFields,
                            formData.FormMetaDataAttribute_CustomeField.ToArray(),
                            formData.FormMetaDataAttribute.ToArray(), null, formData.FormDefinationId, 0, 0,new Core.Model.FDefination.FormAttachmentFontStyle[0], comboBoxItems, customeFields, customeFieldItems, customeVirtualTables, customeVirtualTableFields, translateDictonary);
                    }
                }

            }
            catch (Exception ex)
            {
                defaultReturn.SetException(ex);
            }

            return defaultReturn;
        }
    }
}
