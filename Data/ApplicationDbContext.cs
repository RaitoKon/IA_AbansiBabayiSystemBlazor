using IA_AbansiBabayiSystemBlazor.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace IA_AbansiBabayiSystemBlazor.Data;

public partial class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AddEvent> AddEvents { get; set; }

    public virtual DbSet<RegisteredTroopLeader> RegisteredTroopLeaders { get; set; }

    public virtual DbSet<RegisteredTroopMember> RegisteredTroopMembers { get; set; }

    public virtual DbSet<TroopCluster> TroopClusters { get; set; }

    public virtual DbSet<TroopInformation> TroopInformations { get; set; }

    public virtual DbSet<TroopLeaderAccount> TroopLeaderAccounts { get; set; }

    public virtual DbSet<TroopLeaderRegistration> TroopLeaderRegistrations { get; set; }

    public virtual DbSet<TroopMemberAccount> TroopMemberAccounts { get; set; }

    public virtual DbSet<TroopMemberRegistration> TroopMemberRegistrations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-2901UTC\\SQLEXPRESS;Database=abansibabayi_db;User ID=Tairo;Password=raitogarcia28;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AddEvent>(entity =>
        {
            entity.HasKey(e => e.EventId);

            entity.ToTable("ADD_EVENTS");

            entity.Property(e => e.EventId).HasColumnName("eventID");
            entity.Property(e => e.EventDate)
                .HasColumnType("datetime")
                .HasColumnName("eventDate");
            entity.Property(e => e.EventDescription)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("eventDescription");
            entity.Property(e => e.EventImagePath)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("eventImagePath");
            entity.Property(e => e.EventLocation)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("eventLocation");
            entity.Property(e => e.EventTitle)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("eventTitle");
        }); 

        modelBuilder.Entity<RegisteredTroopLeader>(entity =>
        {
            entity.HasKey(e => e.LeaderId);

            entity.ToTable("REGISTERED_TROOP_LEADER");

            entity.Property(e => e.LeaderId).HasColumnName("leaderID");
            entity.Property(e => e.ColeaderTroopNo).HasColumnName("coleaderTroopNo");
            entity.Property(e => e.LeaderBeneficiary)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("leaderBeneficiary");
            entity.Property(e => e.LeaderBirthdate).HasColumnName("leaderBirthdate");
            entity.Property(e => e.LeaderEmail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("leaderEmail");
            entity.Property(e => e.LeaderFname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("leaderFname");
            entity.Property(e => e.LeaderLname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("leaderLname");
            entity.Property(e => e.LeaderMinitial)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("leaderMInitial");
            entity.Property(e => e.LeaderPosition)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("leaderPosition");
            entity.Property(e => e.LeaderRegStatus)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("leaderRegStatus");
            entity.Property(e => e.LeaderRole)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("leaderRole");
            entity.Property(e => e.LeaderTorNT)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("leaderTorNT");
        });

        modelBuilder.Entity<RegisteredTroopMember>(entity =>
        {
            entity.HasKey(e => e.TroopMemId);

            entity.ToTable("REGISTERED_TROOP_MEMBER");

            entity.HasIndex(e => e.ClusterId, "IX_REGISTERED_TROOP_MEMBER_clusterID");

            entity.Property(e => e.TroopMemId).HasColumnName("troopMemID");
            entity.Property(e => e.ClusterId).HasColumnName("clusterID");
            entity.Property(e => e.TroopMemBeneficiary)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("troopMemBeneficiary");
            entity.Property(e => e.TroopMemBirthdate).HasColumnName("troopMemBirthdate");
            entity.Property(e => e.TroopMemEmail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("troopMemEmail");
            entity.Property(e => e.TroopMemFname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("troopMemFname");
            entity.Property(e => e.TroopMemGradeOrYear)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("troopMemGradeOrYear");
            entity.Property(e => e.TroopMemLname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("troopMemLname");
            entity.Property(e => e.TroopMemMinitial)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("troopMemMInitial");
            entity.Property(e => e.TroopMemRegStatus)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("troopMemRegStatus");
            entity.Property(e => e.TroopMemRole)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("troopMemRole");
            entity.Property(e => e.TroopMemScoutNumber).HasColumnName("troopMemScoutNumber");
            entity.Property(e => e.TroopMemTroopNumber).HasColumnName("troopMemTroopNumber");

            entity.HasOne(d => d.Cluster).WithMany(p => p.RegisteredTroopMembers).HasForeignKey(d => d.ClusterId);
        });

        modelBuilder.Entity<TroopCluster>(entity =>
        {
            entity.HasKey(e => e.ClusterId);

            entity.ToTable("TROOP_CLUSTER");

            entity.Property(e => e.ClusterId).HasColumnName("clusterID");
            entity.Property(e => e.ClusterName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("clusterName");
        });

        modelBuilder.Entity<TroopInformation>(entity =>
        {
            entity.HasKey(e => e.TroopInfoId).HasName("PK__TROOP_IN__F180B71A7B23DC87");

            entity.ToTable("TROOP_INFORMATION");

            entity.Property(e => e.TroopInfoId).HasColumnName("troopInfoId");
            entity.Property(e => e.LeaderId).HasColumnName("leaderID");
            entity.Property(e => e.TroopAddress)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("troopAddress");
            entity.Property(e => e.TroopAgeLevel).HasColumnName("troopAgeLevel");
            entity.Property(e => e.TroopBarangayCommittee)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("troopBarangayCommittee");
            entity.Property(e => e.TroopDistrictCommittee)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("troopDistrictCommittee");
            entity.Property(e => e.TroopMailingAddress)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("troopMailingAddress");
            entity.Property(e => e.TroopMemId).HasColumnName("troopMemID");
            entity.Property(e => e.TroopName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("troopName");
            entity.Property(e => e.TroopSponsoringGroup)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("troopSponsoringGroup");
            entity.Property(e => e.TroopStatus)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("troopStatus");
            entity.Property(e => e.TroopTroopTelNo).HasColumnName("troopTroopTelNo");
            entity.Property(e => e.TroopType)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("troopType");

            entity.HasOne(d => d.Leader).WithMany(p => p.TroopInformations)
                .HasForeignKey(d => d.LeaderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TROOP_INFORMATION_REGISTERED_TROOP_LEADER");

            entity.HasOne(d => d.TroopMem).WithMany(p => p.TroopInformations)
                .HasForeignKey(d => d.TroopMemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TROOP_INFORMATION_REGISTERED_TROOP_MEMBER");
        });

        modelBuilder.Entity<TroopLeaderAccount>(entity =>
        {
            entity.HasKey(e => e.AccountsId);

            entity.ToTable("TROOP_LEADER_ACCOUNTS");

            entity.HasIndex(e => e.LeaderId, "IX_TROOP_LEADER_ACCOUNTS_leaderID");

            entity.Property(e => e.AccountsId).HasColumnName("accountsID");
            entity.Property(e => e.LeaderId).HasColumnName("leaderID");
            entity.Property(e => e.Id).HasColumnName("Id"); // This is just a string, no navigation

            // Only configure the navigation that exists
            entity.HasOne(d => d.Leader)
                  .WithMany(p => p.TroopLeaderAccounts)
                  .HasForeignKey(d => d.LeaderId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<TroopLeaderRegistration>(entity =>
        {
            entity.HasKey(e => e.LeaderId);

            entity.ToTable("TROOP_LEADER_REGISTRATION");

            entity.Property(e => e.LeaderId).HasColumnName("leaderID");
            entity.Property(e => e.ColeaderTroopNo).HasColumnName("coleaderTroopNo");
            entity.Property(e => e.LeaderBeneficiary)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("leaderBeneficiary");
            entity.Property(e => e.LeaderBirthdate).HasColumnName("leaderBirthdate");
            entity.Property(e => e.LeaderEmail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("leaderEmail");
            entity.Property(e => e.LeaderFname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("leaderFname");
            entity.Property(e => e.LeaderLname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("leaderLname");
            entity.Property(e => e.LeaderMinitial)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("leaderMInitial");
            entity.Property(e => e.LeaderPosition)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("leaderPosition");
            entity.Property(e => e.LeaderRegStatus)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("leaderRegStatus");
            entity.Property(e => e.LeaderRole)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("leaderRole");
            entity.Property(e => e.LeaderTorNT)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("leaderTorNT");
        });

        modelBuilder.Entity<TroopMemberAccount>(entity =>
        {
            entity.HasKey(e => e.AccountsId);

            entity.ToTable("TROOP_MEMBER_ACCOUNTS");

            entity.HasIndex(e => e.TroopMemId, "IX_TROOP_MEMBER_ACCOUNTS_troopMemID");

            entity.Property(e => e.AccountsId).HasColumnName("accountsID");
            entity.Property(e => e.TroopMemId).HasColumnName("troopMemID");
            entity.Property(e => e.Id).HasColumnName("Id"); // just a string column

            // Configure only the navigation that exists
            entity.HasOne(d => d.TroopMem)
                  .WithMany(p => p.TroopMemberAccounts)
                  .HasForeignKey(d => d.TroopMemId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<TroopMemberRegistration>(entity =>
        {
            entity.HasKey(e => e.TroopMemId);

            entity.ToTable("TROOP_MEMBER_REGISTRATION");

            entity.Property(e => e.TroopMemId).HasColumnName("troopMemID");
            entity.Property(e => e.TroopMemBeneficiary)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("troopMemBeneficiary");
            entity.Property(e => e.TroopMemBirthdate).HasColumnName("troopMemBirthdate");
            entity.Property(e => e.TroopMemEmail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("troopMemEmail");
            entity.Property(e => e.TroopMemFname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("troopMemFname");
            entity.Property(e => e.TroopMemGradeOrYear)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("troopMemGradeOrYear");
            entity.Property(e => e.TroopMemLname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("troopMemLname");
            entity.Property(e => e.TroopMemMinitial)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("troopMemMInitial");
            entity.Property(e => e.TroopMemRegStatus)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("troopMemRegStatus");
            entity.Property(e => e.TroopMemRole)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("troopMemRole");
            entity.Property(e => e.TroopMemScoutNumber).HasColumnName("troopMemScoutNumber");
            entity.Property(e => e.TroopMemTroopNumber).HasColumnName("troopMemTroopNumber");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
