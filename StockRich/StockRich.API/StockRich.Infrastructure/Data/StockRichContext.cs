using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using StockRich.Infrastructure.Models;

namespace StockRich.Infrastructure.Data
{
    public partial class StockRichContext : DbContext
    {
        public StockRichContext()
        {
        }

        public StockRichContext(DbContextOptions<StockRichContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CompanyInfo> CompanyInfos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompanyInfo>(entity =>
            {
                entity.HasComment("公司基本資料");

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("gen_random_uuid()")
                    .HasComment("唯一值");

                entity.Property(e => e.Alias).HasComment("公司簡稱");

                entity.Property(e => e.BeginDate).HasComment("成立日期");

                entity.Property(e => e.Capital).HasComment("實收資本額");

                entity.Property(e => e.ChairMan).HasComment("董事長");

                entity.Property(e => e.ContactEmail).HasComment("電子郵件信箱");

                entity.Property(e => e.ContactTelephone).HasComment("連絡電話");

                entity.Property(e => e.GeneralManager).HasComment("總經理");

                entity.Property(e => e.IndustryType).HasComment("產業別");

                entity.Property(e => e.Name).HasComment("公司全名");

                entity.Property(e => e.PublicDate).HasComment("上市日期");

                entity.Property(e => e.SpokesMan).HasComment("發言人");

                entity.Property(e => e.StockCode).HasComment("股號");

                entity.Property(e => e.Website).HasComment("網址");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
