using System;
using System.Collections.Generic;
using IA_AbansiBabayiSystemBlazor.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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

    public virtual DbSet<TroopLeaderRegistration> TroopLeaders {  get; set; }

    public virtual DbSet<TroopMemberRegistration> TroopMembers { get; set; }

    public virtual DbSet<RegisteredTroopLeader> RegisteredTroopLeaders { get; set; }

    public virtual DbSet<RegisteredTroopMember> RegisteredTroopMembers { get; set; }

    public virtual DbSet<TroopLeaderAccount> RegisteredTroopLeaderAccounts { get; set; }

    public virtual DbSet<TroopMemberAccount> RegisteredTroopMemberAccounts { get; set; }

    public virtual DbSet<AddEvent> AddEvents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<TroopLeaderRegistration>(entity =>
        {
            entity.HasKey(e => e.LeaderId);

            entity.ToTable("TROOP_LEADER_REGISTRATION");

            entity.Property(e => e.LeaderId).HasColumnName("leaderID");
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
                .IsFixedLength()
                .HasColumnName("leaderFname");
            entity.Property(e => e.LeaderLname)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("leaderLname");
            entity.Property(e => e.LeaderMInitial)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("leaderMInitial");
            entity.Property(e => e.LeaderPosition)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("leaderPosition");
            entity.Property(e => e.CoLeaderTroopNumber).HasColumnName("coleaderTroopNo");
            entity.Property(e => e.LeaderRegStatus)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("leaderRegStatus");
            entity.Property(e => e.LeaderRole)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("leaderRole");
            entity.Property(e => e.LeaderTorNT)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("leaderTorNT");
        });

        modelBuilder.Entity<TroopMemberRegistration>(entity =>
        {
            entity.HasKey(e => e.TroopMemId);
            
            entity.ToTable("TROOP_MEMBER_REGISTRATION");

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
            entity.Property(e => e.TroopMemId).HasColumnName("troopMemID");
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

        modelBuilder.Entity<RegisteredTroopLeader>(entity =>
        {
            entity.HasKey(e => e.LeaderId);

            entity.ToTable("REGISTERED_TROOP_LEADER");

            entity.Property(e => e.LeaderId).HasColumnName("leaderID");
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
                .IsFixedLength()
                .HasColumnName("leaderFname");
            entity.Property(e => e.LeaderLname)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("leaderLname");
            entity.Property(e => e.LeaderMInitial)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("leaderMInitial");
            entity.Property(e => e.LeaderPosition)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("leaderPosition");
            entity.Property(e => e.LeaderRegStatus)
                .HasMaxLength(50)
                .IsFixedLength()
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
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("troopMemRole");
            entity.Property(e => e.TroopMemScoutNumber).HasColumnName("troopMemScoutNumber");
            entity.Property(e => e.TroopMemTroopNumber).HasColumnName("troopMemTroopNumber");
        });

        modelBuilder.Entity<TroopLeaderAccount>(entity =>
        {
            entity.HasKey(e => e.AccountsId).HasName("PK__TROOP_LE__4A22408A41AD76EC");

            entity.ToTable("TROOP_LEADER_ACCOUNTS");

            entity.Property(e => e.AccountsId).HasColumnName("accountsID");
            entity.Property(e => e.Id).HasMaxLength(450);
            entity.Property(e => e.LeaderId).HasColumnName("leaderID");
        });

        modelBuilder.Entity<TroopMemberAccount>(entity =>
        {
            entity.HasKey(e => e.AccountsId).HasName("PK__TROOP_ME__4A22408AB573DA3D");

            entity.ToTable("TROOP_MEMBER_ACCOUNTS");

            entity.Property(e => e.AccountsId).HasColumnName("accountsID");
            entity.Property(e => e.Id).HasMaxLength(450);
            entity.Property(e => e.TroopMemId).HasColumnName("troopMemID");
        });

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
            entity.Property(e => e.EventTitle)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("eventTitle");
            entity.Property(e => e.EventLocation)
                .HasMaxLength(100)
                .HasColumnName("eventLocation")
                .IsUnicode(false)
                .HasColumnName("eventLocation");

        });

        base.OnModelCreating(modelBuilder);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

}
