using Microsoft.EntityFrameworkCore;

namespace ConnectSQL.Data
{
    public class MyDbContext: DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options) { }

        //----
        #region DbSet
        public DbSet<HangHoa> HangHoas { get; set; }
        public DbSet<Loai> Loais { get; set; }
        public DbSet<DonHang> DonHangs { get; set; }
        public DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }


        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DonHang>(e =>
            {
                e.ToTable("DonHang");
                e.HasKey(dh=> dh.MaDh);
                e.Property(dh=> dh.NgayDat).HasDefaultValueSql("getutcdate()");
            });

            modelBuilder.Entity<ChiTietDonHang>(e =>
            {
                e.ToTable("ChiTietDonHang");
                e.HasKey(e=> new
                {
                    e.MaDh,
                    e.MaHH
                });

                e.HasOne(e => e.DonHang)
                .WithMany(e => e.ChiTietDonHangs)
                .HasForeignKey(e => e.MaDh)
                .HasConstraintName("FK_CTDH_DonHang");

                e.HasOne(e => e.HangHoa)
                .WithMany(e => e.ChiTietDonHangs)
                .HasForeignKey(e => e.MaHH)
                .HasConstraintName("FK_CTDH_HangHoa");
            });
        }
    }
}
