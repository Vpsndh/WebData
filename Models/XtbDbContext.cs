using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebData.Areas.Admin.Models;

namespace WebData.Models;

public partial class XtbDbContext : DbContext
{
    public XtbDbContext()
    {
    }

    public XtbDbContext(DbContextOptions<XtbDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Nguon> Nguons { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=BOSS\\BOSS; Database=XtbDB;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(250);
            entity.Property(e => e.Rank).HasMaxLength(50);
            entity.Property(e => e.RolesId).HasColumnName("RolesID");
            entity.Property(e => e.UserName).HasMaxLength(50);

            entity.HasOne(d => d.Roles).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.RolesId)
                .HasConstraintName("FK_Accounts_Roles");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CatId);

            entity.Property(e => e.CatId).HasColumnName("CatID");
            entity.Property(e => e.Alias).HasMaxLength(50);
            entity.Property(e => e.CatName).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(50);
        });

        modelBuilder.Entity<Nguon>(entity =>
        {
            entity.Property(e => e.NguonId).HasColumnName("NguonID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.LoaiDb).HasColumnName("LoaiDB");
            entity.Property(e => e.NguonName)
                .HasMaxLength(10)
                .IsFixedLength();

            entity.HasOne(d => d.Account).WithMany(p => p.Nguons)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK_Nguons_Accounts");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.Property(e => e.PostId).HasColumnName("PostID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.Alias).HasMaxLength(250);
            entity.Property(e => e.Author).HasMaxLength(100);
            entity.Property(e => e.CatId).HasColumnName("CatID");
            entity.Property(e => e.CreateAt).HasColumnType("datetime");
            entity.Property(e => e.Keys).HasMaxLength(250);
            entity.Property(e => e.NguonBct)
                .HasMaxLength(250)
                .HasColumnName("NguonBCT");
            entity.Property(e => e.SoBct).HasColumnName("SoBCT");
            entity.Property(e => e.Title).HasMaxLength(250);
            entity.Property(e => e.TtthuTin)
                .HasMaxLength(50)
                .HasColumnName("TTThuTin");

            entity.HasOne(d => d.Account).WithMany(p => p.Posts)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK_Posts_Accounts");

            entity.HasOne(d => d.Cat).WithMany(p => p.Posts)
                .HasForeignKey(d => d.CatId)
                .HasConstraintName("FK_Posts_Categories");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RolesId);

            entity.Property(e => e.RolesId).HasColumnName("RolesID");
            entity.Property(e => e.RolesDes).HasMaxLength(50);
            entity.Property(e => e.RolesName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

}
