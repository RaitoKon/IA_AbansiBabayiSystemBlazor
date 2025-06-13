using System;
using System.Collections.Generic;
using IA_AbansiBabayiSystemBlazor.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace IA_AbansiBabayiSystemBlazor.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Login> Logins { get; set; }

    public virtual DbSet<TroopLeaderRegistration> TroopLeaders {  get; set; }

    public virtual DbSet<TroopMemberRegistration> TroopMembers { get; set; }

    public virtual DbSet<RegisteredTroopLeader> RegisteredTroopLeaders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Login>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__tmp_ms_x__CB9A1CDF86BD24EB");

            entity.ToTable("LOGIN");

            entity.HasIndex(e => e.Email, "UQ_LOGIN_Email").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("userID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.AuthRoleId).HasColumnName("authRoleID");

            entity.HasOne(d => d.AuthRoleNavigation)
                .WithMany(p => p.Logins)
                .HasForeignKey(d => d.AuthRoleId)
                .HasConstraintName("FK_LOGIN_AUTHORIZATION_ROLES");
        });

        modelBuilder.Entity<AuthorizationRole>(entity =>
        {
            entity.HasKey(e => e.AuthRoleId).HasName("PK_TROOP_ROLES");

            entity.ToTable("AUTHORIZATION_ROLES");

            entity.Property(e => e.AuthRoleId).HasColumnName("authRoleID");
            entity.Property(e => e.AuthRoleName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("authRoleName");
        });

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
            entity.Property(e => e.CoLeaderToopNumber).HasColumnName("coleaderTroopNo");
            entity.Property(e => e.LeaderRegStatus)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("leaderRegStatus");
            entity.Property(e => e.LeaderRole)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("leaderRole");
            entity.Property(e => e.LeaderTorNT)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("leaderTorNT");
        });

        modelBuilder.Entity<TroopMemberRegistration>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("TROOP_MEMBER_REGISTRATION");

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
        });

        modelBuilder.Entity<RegisteredTroopLeader>(entity =>
        {
            entity.HasKey(e => e.LeaderId);

            entity.ToTable("REGISTERED_TROOP_LEADER");

            entity.Property(e => e.LeaderId).HasColumnName("leaderID");
            entity.Property(e => e.AuthRoleId).HasColumnName("authRoleID");
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
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("leaderTorNT");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

}
