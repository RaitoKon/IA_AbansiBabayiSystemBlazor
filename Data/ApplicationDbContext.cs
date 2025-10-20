using IA_AbansiBabayiSystemBlazor.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace IA_AbansiBabayiSystemBlazor.Data;

public partial class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AccountStatus> AccountStatuses { get; set; }

    public virtual DbSet<AddEventsOrAnnouncement> AddEventsOrAnnouncements { get; set; }

    public virtual DbSet<EventsOrAnnouncementsTarget> EventsOrAnnouncementsTargets { get; set; }
    
    public virtual DbSet<ScoutJoinedEvent> ScoutJoinedEvents { get; set; }

    public virtual DbSet<LeaderPosition> LeaderPositions { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<ProductPurchase> ProductPurchases { get; set; }

    public virtual DbSet<ProductSale> ProductSales { get; set; }

    public virtual DbSet<TroopDetail> TroopDetails { get; set; }

    public virtual DbSet<TroopInformation> TroopInformations { get; set; }

    public virtual DbSet<TroopLeader> TroopLeaders { get; set; }

    public virtual DbSet<TroopMember> TroopMembers { get; set; }

    public virtual DbSet<TroopMemberScoutLevel> TroopMemberScoutLevels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<AccountStatus>(entity =>
        {
            entity.ToTable("AccountStatus");

            entity.Property(e => e.AccountStatusDescription)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AddEventsOrAnnouncement>(entity =>
        {
            entity.HasKey(e => e.EventsOrAnnouncementsId).HasName("PK__EventsOr__7942DA228A18BC72");

            entity.Property(e => e.DatePosted)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EventOrAnnouncementType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EventsOrAnnouncementsDateFrom).HasColumnType("datetime");
            entity.Property(e => e.EventsOrAnnouncementsDateTo).HasColumnType("datetime");
            entity.Property(e => e.EventsOrAnnouncementsDescription)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.EventsOrAnnouncementsImagePath)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.EventsOrAnnouncementsLocation)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.EventsOrAnnouncementsTitle)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EventsOrAnnouncementsTarget>(entity =>
        {
            entity.HasKey(e => e.EventsOrAnnouncementsTargetId).HasName("PK_EventsOrAnnouncementTarget");

            entity.ToTable("EventsOrAnnouncementsTarget");

            entity.Property(e => e.TargetPeople)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.EventsOrAnnouncements).WithMany(p => p.EventsOrAnnouncementsTargets)
                .HasForeignKey(d => d.EventsOrAnnouncementsId)
                .HasConstraintName("FK_EventsOrAnnouncementsTarget_AddEventsOrAnnouncements");
        });

        modelBuilder.Entity<ScoutJoinedEvent>(entity =>
        {
            entity.HasKey(e => e.ScoutJoinedEventsId);

            entity.Property(e => e.DateModified).HasColumnType("datetime");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<LeaderPosition>(entity =>
        {
            entity.HasKey(e => e.LeaderPositionId).HasName("PK_LeaderRole");

            entity.ToTable("LeaderPosition");

            entity.Property(e => e.LeaderPositionName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LeaderPosition");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.ProductCategoryId).HasColumnName("ProductCategoryID");
            entity.Property(e => e.ProductDescription)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.ProductImagePath)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ProductName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ProductPrice).HasColumnType("decimal(8, 2)");

            entity.HasOne(d => d.ProductCategory).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductCategoryId)
                .HasConstraintName("FK_Product_ProductCategory");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.ToTable("ProductCategory");

            entity.Property(e => e.ProductCategoryId).HasColumnName("ProductCategoryID");
            entity.Property(e => e.ProductCategoryName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ProductPurchase>(entity =>
        {
            entity.Property(e => e.ProductPurchaseId).HasColumnName("ProductPurchaseID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.ProductPurchaseDate).HasColumnType("datetime");
            entity.Property(e => e.ProductPurchasePrice).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.TotalCost)
                .HasComputedColumnSql("([ProductPurchaseQuantity]*[ProductPurchasePrice])", false)
                .HasColumnType("decimal(19, 2)");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductPurchases)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_ProductPurchases_Product");
        });

        modelBuilder.Entity<ProductSale>(entity =>
        {
            entity.Property(e => e.ProductSaleId).HasColumnName("ProductSaleID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.ProductSaleDate).HasColumnType("datetime");
            entity.Property(e => e.ProductSalePrice).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.TotalCost)
                .HasComputedColumnSql("([ProductSaleQuantity]*[ProductSalePrice])", true)
                .HasColumnType("decimal(19, 2)");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductSales)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_ProductSales_Product");
        });

        modelBuilder.Entity<TroopDetail>(entity =>
        {
            entity.HasKey(e => e.TroopDetailsId);

            entity.Property(e => e.TroopAddress)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.TroopAgeLevel)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.TroopBarangayCommittee)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.TroopBirthdate).HasColumnType("datetime");
            entity.Property(e => e.TroopDistrictCommittee)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.TroopMailingAddress)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.TroopName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TroopSponsoringGroup)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.TroopStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TroopType)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TroopInformation>(entity =>
        {
            entity.HasKey(e => e.TroopInfoId);

            entity.HasIndex(e => e.TroopDetailsId, "IX_TroopInformations_TroopDetailsId");

            entity.HasIndex(e => e.TroopLeaderId, "IX_TroopInformations_TroopLeaderId");

            entity.HasIndex(e => e.TroopMemId, "IX_TroopInformations_TroopMemId");

            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.TroopLeader).WithMany(p => p.TroopInformations)
                .HasForeignKey(d => d.TroopLeaderId)
                .HasConstraintName("FK_TroopInformations_RegisteredTroopLeaders_TroopLeaderId");

        });

        modelBuilder.Entity<TroopMember>(entity =>
        {
            entity.HasKey(e => e.TroopMemId).HasName("PK_RegisteredTroopMembers");

            entity.HasIndex(e => e.ApplicationUserId, "IX_TroopMembers_ApplicationUserId");

            entity.Property(e => e.TroopMemBeneficiary)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.TroopMemBirthdate).HasColumnType("datetime");
            entity.Property(e => e.TroopMemDateRegistered).HasColumnType("datetime");
            entity.Property(e => e.TroopMemEmail)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TroopMemFname)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TroopMemGradeOrYear)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TroopMemLname)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TroopMemMname)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TroopMemRegStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TroopMemRegisteredEmail)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UserRole)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TroopLeader>(entity =>
        {
            entity.HasKey(e => e.LeaderId).HasName("PK_RegisteredTroopLeaders");

            entity.HasIndex(e => e.ApplicationUserId, "IX_TroopLeaders_ApplicationUserId");

            entity.HasIndex(e => e.LeaderPositionId, "IX_TroopLeaders_LeaderPositionId");

            entity.Property(e => e.LeaderBeneficiary)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.LeaderBirthdate).HasColumnType("datetime");
            entity.Property(e => e.LeaderDateRegistered).HasColumnType("datetime");
            entity.Property(e => e.LeaderEmail)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LeaderFname)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LeaderLname)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LeaderMname)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LeaderRegStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LeaderRegisteredEmail)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UserRole)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LeaderTorNt)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LeaderTorNT");
        });

        modelBuilder.Entity<TroopMemberScoutLevel>(entity =>
        {
            entity.HasKey(e => e.TroopMemScoutLevelId).HasName("PK_TroopMemberRoles");

            entity.ToTable("TroopMemberScoutLevel");

            entity.Property(e => e.TroopMemScoutLevel)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            entity.HasOne(u => u.AccountStatus)
                  .WithMany(s => s.AspNetUsers)
                  .HasForeignKey(u => u.AccountStatusId)
                  .HasConstraintName("FK_AspNetUsers_AccountStatus");
        });

        base.OnModelCreating(modelBuilder);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
