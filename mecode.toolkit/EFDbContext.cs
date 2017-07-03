using System.Data.Entity;

namespace mecode.toolkit
{
    public class EFDbContext : DbContext
    {
        public EFDbContext(string connstr)
            : base(connstr)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UpgradeLog>().HasKey(m => m.UpgradeLogGUID).ToTable("sys_UpgradeLog");
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<UpgradeLog> UpgradeLogs { get; set; }

    }
}
