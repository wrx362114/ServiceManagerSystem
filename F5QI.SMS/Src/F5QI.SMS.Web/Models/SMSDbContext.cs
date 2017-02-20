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
        private void BindFields(DbModelBuilder modelBuilder)
        {
            var bind = modelBuilder.Entity<FieldDescription>();
            bind.HasKey(a => a.Id);
            bind.Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            bind.Property(a => a.SecurityStamp).HasMaxLength(32).IsFixedLength().IsConcurrencyToken();

            bind.Property(a => a.Name).IsVariableLength().IsUnicode().HasMaxLength(128);
            bind.HasMany(a => a.Groups)
                .WithMany(a => a.Fields)
                .Map(a =>
                {
                    a.ToTable($"R_{nameof(FieldDescription)}_{nameof(FieldGroups)}")
                        .MapLeftKey($"{nameof(FieldDescription)}Id")
                        .MapRightKey($"{nameof(FieldGroups)}Id");
                });
        }

        public virtual IDbSet<FieldGroups> FieldGroups { get; set; }
        private void BindFieldGroups(DbModelBuilder modelBuilder)
        {
            var bind = modelBuilder.Entity<FieldGroups>();
            bind.HasKey(a => a.Id).Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            bind.Property(a => a.SecurityStamp).HasMaxLength(32).IsFixedLength().IsConcurrencyToken();

            bind.Property(a => a.Name).IsVariableLength().IsUnicode().HasMaxLength(128);
            bind.Property(a => a.Remark).IsVariableLength().IsUnicode().HasMaxLength(256);
            bind.HasMany(a => a.Users)
                .WithMany(a => a.InfoGroup)
                .Map(a =>
                {
                    a.ToTable($"R_{nameof(FieldGroups)}_{nameof(SMSUser)}")
                        .MapLeftKey($"{nameof(FieldGroups)}Id")
                        .MapRightKey($"{nameof(SMSUser)}Id");
                });
        }

        public virtual IDbSet<ServiceDescription> Services { get; set; }
        private void BindServices(DbModelBuilder modelBuilder)
        {
            var bind = modelBuilder.Entity<ServiceDescription>();
            bind.HasKey(a => a.Id).Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            bind.Property(a => a.SecurityStamp).HasMaxLength(32).IsFixedLength().IsConcurrencyToken();

            bind.Property(a => a.Name).IsVariableLength().IsUnicode().HasMaxLength(128);
            bind.Property(a => a.Remark).IsVariableLength().IsUnicode().HasMaxLength(2048);
            bind.Property(a => a.Config).IsVariableLength().IsUnicode().IsMaxLength();

            bind.HasMany(a => a.Packages)
                .WithMany(a => a.Services)
                .Map(a =>
                {
                    a.ToTable($"R_{nameof(ServiceDescription)}_{nameof(ServicePackage)}")
                        .MapLeftKey($"{nameof(ServiceDescription)}Id")
                        .MapRightKey($"{nameof(ServicePackage)}Id");
                });
        }

        public virtual IDbSet<ServicePackage> ServicePackages { get; set; }
        private void BindServicePackage(DbModelBuilder modelBuilder)
        {
            var bind = modelBuilder.Entity<ServicePackage>();
            bind.HasKey(a => a.Id).Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            bind.Property(a => a.SecurityStamp).HasMaxLength(32).IsFixedLength().IsConcurrencyToken();

            bind.Property(a => a.Name).IsVariableLength().IsUnicode().HasMaxLength(128);
            bind.Property(a => a.Remark).IsVariableLength().IsUnicode().HasMaxLength(2048);

            //bind.HasMany(a => a.Services)
            //    .WithMany(a => a.Packages)
            //    .Map(a =>
            //    {
            //        a.ToTable($"R_{nameof(ServiceDescription)}_{nameof(ServicePackage)}")
            //            .MapLeftKey($"{nameof(ServicePackage)}Id")
            //            .MapRightKey($"{nameof(ServiceDescription)}Id");
            //    })
        }

        public virtual IDbSet<ServiceContract> ServiceContracts { get; set; }
        private void BindServiceContract(DbModelBuilder modelBuilder)
        {
            var bind = modelBuilder.Entity<ServiceContract>();
            bind.HasKey(a => a.Id).Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            bind.Property(a => a.SecurityStamp).HasMaxLength(32).IsFixedLength().IsConcurrencyToken();

            bind.Property(a => a.Name).IsVariableLength().IsUnicode().HasMaxLength(128);
            bind.Property(a => a.Amount).HasPrecision(18, 2);

            bind.HasMany(a => a.PaymentPlans)
                .WithRequired(a => a.Contract)
                .HasForeignKey(a => a.ContractId)
                .WillCascadeOnDelete(true);
            bind.HasMany(a => a.PlanJobs)
                .WithRequired(a => a.Contract)
                .HasForeignKey(a => a.ContractId)
                .WillCascadeOnDelete(true);
        }

        public virtual IDbSet<ServiceContractJobConfig> ServiceContractJobs { get; set; }
        private void BindServiceContractJobConfig(DbModelBuilder modelBuilder)
        {
            var bind = modelBuilder.Entity<ServiceContractJobConfig>();
            bind.HasKey(a => a.Id).Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            bind.Property(a => a.SecurityStamp).HasMaxLength(32).IsFixedLength().IsConcurrencyToken();

            bind.Property(a => a.Config).IsVariableLength().IsUnicode().IsMaxLength();
        }

        public virtual IDbSet<ServiceContractTemplate> ServiceContractTemplates { get; set; }
        private void BindServiceContractTemplate(DbModelBuilder modelBuilder)
        {
            var bind = modelBuilder.Entity<ServiceContractTemplate>();
            bind.HasKey(a => a.Id).Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            bind.Property(a => a.SecurityStamp).HasMaxLength(32).IsFixedLength().IsConcurrencyToken();

            bind.Property(a => a.Name).IsVariableLength().IsUnicode().HasMaxLength(128);
            bind.Property(a => a.Config).IsVariableLength().IsUnicode().IsMaxLength();
        }

        public virtual IDbSet<ServiceContractPaymentPlan> ServiceContractPaymentPlans { get; set; }
        private void BindServiceContractPaymentPlan(DbModelBuilder modelBuilder)
        {
            var bind = modelBuilder.Entity<ServiceContractPaymentPlan>();
            bind.HasKey(a => a.Id).Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            bind.Property(a => a.SecurityStamp).HasMaxLength(32).IsFixedLength().IsConcurrencyToken();

            bind.Property(a => a.Amount).HasPrecision(20, 2);

            bind.HasMany(a => a.Records)
                .WithRequired(a => a.PaymentPlan)
                .HasForeignKey(a => a.PaymentPlanId)
                .WillCascadeOnDelete(true);
        }


        public virtual IDbSet<PaymentRecord> PaymentRecords { get; set; }
        public virtual IDbSet<OperationRecord> OperationRecords { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
                .Entity<SMSUser>()
                .HasMany(a => a.Jobs)
                .WithOptional(a => a.Clerk)
                .HasForeignKey(a => a.ClerkId)
                .WillCascadeOnDelete(true);
            BindFields(modelBuilder);
            BindFieldGroups(modelBuilder);
        }





        public static SMSDbContext Create()
        {
            return new SMSDbContext();
        }
    }
}