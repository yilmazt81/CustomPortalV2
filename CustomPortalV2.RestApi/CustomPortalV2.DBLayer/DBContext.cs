using CustomPortalV2.Model.Sale;
using Microsoft.EntityFrameworkCore;
using CustomPortalV2.Model.Work;
using CustomPortalV2.Model.Custom;
using CustomPortalV2.Core.Model.Lang;
using CustomPortalV2.Model.Company;
using CustomPortalV2.Core.Model.App;
using CustomPortalV2.Core.Model.Company;
using CustomPortalV2.Core.Model.Autocomplete; 
using CustomPortalV2.Core.Model.Definations;
using CustomPortalV2.Core.Model.Log;
using CustomPortalV2.Core.Model.Form;
using CustomPortalV2.Core.Model.FDefination;
using CustomPortalV2.Core.Model.Custom;

namespace CustomPortalV2.DataAccessLayer
{
    public class DBContext : DbContext
    {
        public DBContext()
        {
        }

        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }

        public DbSet<SalePackage> SalePackages { get; set; }
        public DbSet<SalePackageItem> SalePackagesItems { get; set; }

        public DbSet<CustomWork> CustomWorks { get; set; }
        public DbSet<CustomWorkDocument> CustomWorkDocuments { get; set; }
        public DbSet<WorkFlow> WorkFlows { get; set; }
        public DbSet<WorkFlowStep> WorkFlowSteps { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Param> Parameters { get; set; }

        public DbSet<AppMenu> AppMenus { get; set; }

        public DbSet<UserSession> UserSessions { get; set; }

        public DbSet<AppLang> AppLangs { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Branch> Branches { get; set; }

        public DbSet<LoginRequestLog> LoginRequestLogs { get; set; }


        public DbSet<Country> Countrys { get; set; }

        public DbSet<CountryCity> CountryCitys { get; set; }



        public virtual DbSet<AppMenu> AppMenu { get; set; }  
        public virtual DbSet<AutocompleteFieldMap> AutocompleteFieldMap { get; set; }
        public virtual DbSet<BranchPackage> BranchPackage { get; set; }
        public virtual DbSet<ComboBoxItem> ComboBoxItem { get; set; } 
        public virtual DbSet<CompanyDefination> CompanyDefination { get; set; }
        public virtual DbSet<CopyDocumentLog> CopyDocumentLog { get; set; }
        
        public virtual DbSet<CustomeParam> CustomeParam { get; set; }
        public virtual DbSet<CustomSector> CustomSector { get; set; }
        public virtual DbSet<DefinationType> DefinationType { get; set; }
        public virtual DbSet<FormDefination> FormDefination { get; set; }
        public virtual DbSet<FormDefinationField> FormDefinationField { get; set; }
        public virtual DbSet<FormGroup> FormGroup { get; set; }
        public virtual DbSet<FormMetaData> FormMetaData { get; set; }
        public virtual DbSet<FormMetaDataAttribute> FormMetaDataAttribute { get; set; }
        public virtual DbSet<FormMetaDataAttributeHistory> FormMetaDataAttributeHistory { get; set; }
        public virtual DbSet<FormMetaDataCounter> FormMetaDataCounter { get; set; }
        public virtual DbSet<FormRule> FormRule { get; set; }
        public virtual DbSet<FormVersion> FormVersion { get; set; } 
        public virtual DbSet<RuleFormDefination> RuleFormDefination { get; set; }
        public virtual DbSet<SoftDocumentShare> SoftDocumentShare { get; set; }
        public virtual DbSet<UserRule> UserRule { get; set; }
        public virtual DbSet<UserRuleMenu> UserRuleMenu { get; set; }
        public virtual DbSet<CustomeField> CustomeField { get; set; }
        public virtual DbSet<CustomeFieldItem> CustomeFieldItem { get; set; }
        public virtual DbSet<FormMetaDataAttribute_CustomeField> FormMetaDataAttribute_CustomeField { get; set; }
        public virtual DbSet<LoginRequestLog> LoginRequestLog { get; set; } 
        public virtual DbSet<FieldType> FieldType { get; set; }
        public virtual DbSet<FormAttachmentType> FormAttachmentType { get; set; }
        public virtual DbSet<FormDefinationAttachment> FormDefinationAttachment { get; set; }
        public virtual DbSet<SoftImageChangeFormatRule> SoftImageChangeFormatRule { get; set; }
        public virtual DbSet<AutoComplateOldValue> AutoComplateOldValue { get; set; }
        public virtual DbSet<vGetFormDefinationTypes> vGetFormDefinationTypes { get; set; }
        public virtual DbSet<UserCreateSoftDocumentLog> UserCreateSoftDocumentLog { get; set; }
        public virtual DbSet<CustomeField_VirtualTable> CustomeField_VirtualTable { get; set; }
        public virtual DbSet<SystemEnums> SystemEnums { get; set; }
        public virtual DbSet<CustomeField_VirtualTableField> CustomeField_VirtualTableField { get; set; }
        public virtual DbSet<AutocompleteField> AutocompleteField { get; set; }
        public virtual DbSet<AutoComplateDefination> AutoComplateDefination { get; set; }
        public virtual DbSet<CustomeField_VirtualTableCalculateRow> CustomeField_VirtualTableCalculateRow { get; set; }
        public virtual DbSet<CompanyDefinationDefinationType> CompanyDefinationDefinationType { get; set; }
        public virtual DbSet<FormAttachmentFontStyle> FormAttachmentFontStyle { get; set; }
        public virtual DbSet<FormAttachmentField> FormAttachmentField { get; set; }
        public virtual DbSet<FormAttachmentExcelMap> FormAttachmentExcelMap { get; set; }
        public virtual DbSet<FormAttachmentExcelMapMultiField> FormAttachmentExcelMapMultiField { get; set; }
        public virtual DbSet<TranslateDictionary> TranslateDictionary { get; set; }
        public virtual DbSet<CustomWork> CustomWork { get; set; }
        public virtual DbSet<CustomWorkDocument> CustomWorkDocument { get; set; }
        public virtual DbSet<CustomWorkWorkFlow> CustomWorkWorkFlow { get; set; }
        public virtual DbSet<WorkFlow> WorkFlow { get; set; }
        public virtual DbSet<WorkFlowStep> WorkFlowStep { get; set; }
        public virtual DbSet<CustomWorkPermission> CustomWorkPermission { get; set; }
        public virtual DbSet<CustomWorkWorkFlowDocument> CustomWorkWorkFlowDocument { get; set; }
        public virtual DbSet<CustomWorkWorkFlowStep> CustomWorkWorkFlowStep { get; set; }
        public virtual DbSet<AppLang> AppLang { get; set; }
        public virtual DbSet<ApplicationLanguage> ApplicationLanguage { get; set; }
        public virtual DbSet<FormVersionPDFFieldMap> FormVersionPDFFieldMap { get; set; }
        public virtual DbSet<CompanyDefinationSenderTarget> CompanyDefinationSenderTarget { get; set; }
        public virtual DbSet<CustomProduct> CustomProduct { get; set; }

        public virtual DbSet<FontType> FontTypes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SalePackage>().ToTable("SalePackage");
            modelBuilder.Entity<SalePackage>().HasKey(k => k.Id);
            modelBuilder.Entity<SalePackage>().Property(k => k.Id).HasColumnName("Id");

            modelBuilder.Entity<CustomProduct>().ToTable("CustomProduct");
            modelBuilder.Entity<CustomProduct>().HasKey(k => k.Id);
            modelBuilder.Entity<CustomProduct>().Property(k => k.Id).HasColumnName("Id");

            

            modelBuilder.Entity<FormAttachmentExcelMap>().ToTable("FormAttachmentExcelMap");
            modelBuilder.Entity<FormAttachmentExcelMap>().HasKey(k => k.Id);
            modelBuilder.Entity<FormAttachmentExcelMap>().Property(k => k.Id).HasColumnName("Id");

            modelBuilder.Entity<SalePackageItem>().ToTable("SalePackageItem");
            modelBuilder.Entity<SalePackageItem>().HasKey(k => k.Id);
            modelBuilder.Entity<SalePackageItem>().Property(k => k.Id).HasColumnName("Id");

            modelBuilder.Entity<TranslateDictionary>().ToTable("TranslateDictionary");
            modelBuilder.Entity<TranslateDictionary>().HasKey(k => k.Id);
            modelBuilder.Entity<TranslateDictionary>().Property(k => k.Id).HasColumnName("Id");


            modelBuilder.Entity<FormAttachmentExcelMapMultiField>().ToTable("FormAttachmentExcelMapMultiField");
            modelBuilder.Entity<FormAttachmentExcelMapMultiField>().HasKey(k => k.Id);
            modelBuilder.Entity<FormAttachmentExcelMapMultiField>().Property(k => k.Id).HasColumnName("Id");

            modelBuilder.Entity<FontType>().ToTable("FontType");
            modelBuilder.Entity<FontType>().HasKey(k => k.Id);
            modelBuilder.Entity<FontType>().Property(k => k.Id).HasColumnName("Id");

            

            modelBuilder.Entity<CustomWork>().ToTable("CustomWork");
            modelBuilder.Entity<CustomWork>().HasKey(k => k.Id);
            modelBuilder.Entity<CustomWork>().Property(k => k.Id).HasColumnName("Id");


            modelBuilder.Entity<CustomWorkDocument>().ToTable("CustomWorkDocument");
            modelBuilder.Entity<CustomWorkDocument>().HasKey(k => k.Id);
            modelBuilder.Entity<CustomWorkDocument>().Property(k => k.Id).HasColumnName("Id");

            modelBuilder.Entity<WorkFlow>().ToTable("WorkFlow");
            modelBuilder.Entity<WorkFlow>().HasKey(k => k.Id);
            modelBuilder.Entity<WorkFlow>().Property(k => k.Id).HasColumnName("Id");


            modelBuilder.Entity<WorkFlowStep>().ToTable("WorkFlowStep");
            modelBuilder.Entity<WorkFlowStep>().HasKey(k => k.Id);
            modelBuilder.Entity<WorkFlowStep>().Property(k => k.Id).HasColumnName("Id");

            modelBuilder.Entity<Param>().ToTable("ApplicationParam");
            modelBuilder.Entity<Param>().HasKey(k => k.Id);
            modelBuilder.Entity<Param>().Property(k => k.Id).HasColumnName("Id");

            modelBuilder.Entity<AppMenu>().ToTable("AppMenu");
            modelBuilder.Entity<AppMenu>().HasKey(k => k.Id);
            modelBuilder.Entity<AppMenu>().Property(k => k.Id).HasColumnName("Id");
             

            modelBuilder.Entity<User>().ToTable("AppUser");
            modelBuilder.Entity<User>().HasKey(k => k.Id);
            modelBuilder.Entity<User>().Property(k => k.Id).HasColumnName("Id");

            modelBuilder.Entity<UserSession>().ToTable("AppUserSession");
            modelBuilder.Entity<UserSession>().HasKey(k => k.Id);
            modelBuilder.Entity<UserSession>().Property(k => k.Id).HasColumnName("Id");

            modelBuilder.Entity<AppLang>().ToTable("AppLang");
            modelBuilder.Entity<AppLang>().HasKey(k => k.Id);
            modelBuilder.Entity<AppLang>().Property(k => k.Id).HasColumnName("Id");

            modelBuilder.Entity<Company>().ToTable("MainCompany");
            modelBuilder.Entity<Company>().HasKey(k => k.Id);
            modelBuilder.Entity<Company>().Property(k => k.Id).HasColumnName("Id");

            modelBuilder.Entity<LoginRequestLog>().ToTable("LoginrequestLog");
            modelBuilder.Entity<LoginRequestLog>().HasKey(k => k.Id);
            modelBuilder.Entity<LoginRequestLog>().Property(k => k.Id).HasColumnName("Id");


            modelBuilder.Entity<Country>().ToTable("Country");
            modelBuilder.Entity<Country>().HasKey(k => k.Id);
            modelBuilder.Entity<Country>().Property(k => k.Id).HasColumnName("Id");

            modelBuilder.Entity<CountryCity>().ToTable("CountryCity");
            modelBuilder.Entity<CountryCity>().HasKey(k => k.Id);
            modelBuilder.Entity<CountryCity>().Property(k => k.Id).HasColumnName("Id");

            modelBuilder.Entity<Branch>().ToTable("CompanyBranch");
            modelBuilder.Entity<Branch>().HasKey(k => k.Id);
            modelBuilder.Entity<Branch>().Property(k => k.Id).HasColumnName("Id");
            modelBuilder.Entity<Branch>().Property(k => k.Name).HasColumnName("Branch");

            modelBuilder.Entity<CustomeField_VirtualTableCalculateRow>().ToTable("CustomeField_VirtualTableCalculateRow");
            modelBuilder.Entity<CustomeField_VirtualTableCalculateRow>().HasKey(k => k.Id);
            modelBuilder.Entity<CustomeField_VirtualTableCalculateRow>().Property(k => k.Id).HasColumnName("Id");


            modelBuilder.Entity<FormAttachmentFontStyle>().ToTable("FormAttachmentFontStyle");
            modelBuilder.Entity<FormAttachmentFontStyle>().HasKey(k => k.Id);
            modelBuilder.Entity<FormAttachmentFontStyle>().Property(k => k.Id).HasColumnName("Id");

            modelBuilder.Entity<FormAttachmentField>().ToTable("FormAttachmentField");
            modelBuilder.Entity<FormAttachmentField>().HasKey(k => k.Id);
            modelBuilder.Entity<FormAttachmentField>().Property(k => k.Id).HasColumnName("Id");

            //  modelBuilder.UseIdentityColumns();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {

            //builder.UseLazyLoadingProxies(true);

        }
    }
}

