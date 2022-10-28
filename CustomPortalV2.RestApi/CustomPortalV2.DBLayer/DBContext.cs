using CustomPortalV2.Model.Sale;
using Microsoft.EntityFrameworkCore;
using CustomPortalV2.Model.Work;
using CustomPortalV2.Model.Custom;
using CustomPortalV2.Model.App;

namespace CustomPortalV2.DBLayer
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

            modelBuilder.UseIdentityColumns();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {

            //builder.UseLazyLoadingProxies(true);

        }
    }
}

