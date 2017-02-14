using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace F5QI.SMS.Web.Models
{
    public class SMSDbContext :
        IdentityDbContext<
            SMSUser,
            SMSRole,
            long,
            SMSUserLogin,
            SMSUserRole,
            SMSUserClaim
            >
    {
        public SMSDbContext()
            : base("DefaultConnection")
        {
        }

        public virtual IDbSet<FieldDescription> Fields { get; set; }
        public virtual IDbSet<FieldGroups> FieldGroups { get; set; }
        public virtual IDbSet<ServiceDescription> Services { get; set; }
        public virtual IDbSet<ServicePackage> ServicePackages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            BindFields(modelBuilder);
            BindFieldGroups(modelBuilder);
        }

        private void BindFields(DbModelBuilder modelBuilder)
        {
            var bind = modelBuilder.Entity<FieldDescription>();
            bind.HasKey(a => a.Id).Property(a => a.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            bind.Property(a => a.SecurityStamp)
                .IsConcurrencyToken();

            bind.Property(a => a.Name)
                .IsRequired()
                .IsVariableLength()
                .IsUnicode()
                .HasMaxLength(128);
            bind.HasMany(a => a.Groups)
                .WithMany(a => a.Fields)
                .Map(a =>
                {
                    a.ToTable($"R_{nameof(FieldDescription)}_{nameof(FieldGroups)}")
                        .MapLeftKey($"{nameof(FieldDescription)}Id")
                        .MapRightKey($"{nameof(FieldGroups)}Id");
                });
        }

        private void BindFieldGroups(DbModelBuilder modelBuilder)
        {
            var bindgroup = modelBuilder.Entity<FieldGroups>();
            bindgroup.HasKey(a => a.Id).Property(a => a.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            bindgroup.Property(a => a.SecurityStamp)
                .IsConcurrencyToken();
            bindgroup.Property(a => a.Name)
                .IsRequired()
                .IsVariableLength()
                .IsUnicode()
                .HasMaxLength(128);
            bindgroup.HasMany(a => a.Users)
                .WithMany(a => a.InfoGroup)
                .Map(a =>
                {
                    a.ToTable($"R_{nameof(FieldGroups)}_{nameof(SMSUser)}")
                        .MapLeftKey($"{nameof(FieldGroups)}Id")
                        .MapRightKey($"{nameof(SMSUser)}Id");
                });
        }
         
        private void BindServices(DbModelBuilder modelBuilder)
        {
            var bind = modelBuilder.Entity<ServiceDescription>();
            bind.HasKey(a => a.Id).Property(a => a.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            bind.Property(a => a.SecurityStamp)
                .IsConcurrencyToken();

            bind.Property(a => a.Name)
                .IsRequired()
                .IsVariableLength()
                .IsUnicode()
                .HasMaxLength(128);
            bind.Property(a => a.Remark) 
                .IsVariableLength()
                .IsUnicode()
                .HasMaxLength(2048);
            bind.Property(a => a.Config)
                .IsMaxLength()
                .IsRequired()
                .IsUnicode();
            bind.HasMany(a => a.Packages)
                .WithMany(a => a.Services)
                .Map(a =>
                {
                    a.ToTable($"R_{nameof(ServiceDescription)}_{nameof(ServicePackage)}")
                        .MapLeftKey($"{nameof(ServiceDescription)}Id")
                        .MapRightKey($"{nameof(ServicePackage)}Id");
                });
        }

        private void BindServicePackage(DbModelBuilder modelBuilder)
        {
            var bind = modelBuilder.Entity<ServicePackage>();
            bind.HasKey(a => a.Id).Property(a => a.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            bind.Property(a => a.SecurityStamp)
                .IsConcurrencyToken();

            bind.Property(a => a.Name)
                .IsRequired()
                .IsVariableLength()
                .IsUnicode()
                .HasMaxLength(128);
            bind.Property(a => a.Remark)
                .IsVariableLength()
                .IsUnicode()
                .HasMaxLength(2048); 
            bind.HasMany(a => a.Services)
                .WithMany(a => a.Packages)
                .Map(a =>
                {
                    a.ToTable($"R_{nameof(ServiceDescription)}_{nameof(ServicePackage)}")
                        .MapLeftKey($"{nameof(ServicePackage)}Id")
                        .MapRightKey($"{nameof(ServiceDescription)}Id");
                });
        }
        public static SMSDbContext Create()
        {
            return new SMSDbContext();
        }
    }
}