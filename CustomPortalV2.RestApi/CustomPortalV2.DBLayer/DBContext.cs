using CustomPortalV2.Model.Sale;
using Microsoft.EntityFrameworkCore;

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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SalePackage>().ToTable("SalePackage");
            modelBuilder.Entity<SalePackage>().HasKey(k => k.Id);
            modelBuilder.Entity<SalePackage>().Property(k => k.Id).HasColumnName("Id");


            modelBuilder.Entity<SalePackageItem>().ToTable("SalePackageItem");
            modelBuilder.Entity<SalePackageItem>().HasKey(k => k.Id);
            modelBuilder.Entity<SalePackageItem>().Property(k => k.Id).HasColumnName("Id");



            modelBuilder.UseIdentityColumns();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {

            //builder.UseLazyLoadingProxies(true);

        }
    }
}

