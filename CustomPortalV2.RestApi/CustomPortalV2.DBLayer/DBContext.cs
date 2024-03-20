using CustomPortalV2.Model.Sale;
using Microsoft.EntityFrameworkCore;
using CustomPortalV2.Model.Work;
using CustomPortalV2.Model.Custom; 
using CustomPortalV2.Core.Model.Lang;
using CustomPortalV2.Model.Company;
using CustomPortalV2.Core.Model.App;
using CustomPortalV2.Core.Model.Company;

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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SalePackage>().ToTable("SalePackage");
            modelBuilder.Entity<SalePackage>().HasKey(k => k.Id);
            modelBuilder.Entity<SalePackage>().Property(k => k.Id).HasColumnName("Id");


            modelBuilder.Entity<SalePackageItem>().ToTable("SalePackageItem");
            modelBuilder.Entity<SalePackageItem>().HasKey(k => k.Id);
            modelBuilder.Entity<SalePackageItem>().Property(k => k.Id).HasColumnName("Id");



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






            //  modelBuilder.UseIdentityColumns();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {

            //builder.UseLazyLoadingProxies(true);

        }
    }
}

