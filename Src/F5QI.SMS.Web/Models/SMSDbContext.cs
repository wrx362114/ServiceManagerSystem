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
        private static object _sync = new object();
        private static bool Inited = false;
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
            bind.Property(a => a.SecurityStamp).HasMaxLength(36).IsFixedLength().IsConcurrencyToken();

            bind.Property(a => a.Name).IsVariableLength().IsUnicode().HasMaxLength(128);
            bind.HasMany(a => a.Groups)
                .WithMany(a => a.Fields)
                .Map(a =>
                {
                    a.ToTable($"R_{nameof(FieldDescription)}_{nameof(Models.FieldGroups)}")
                        .MapLeftKey($"{nameof(FieldDescription)}Id")
                        .MapRightKey($"{nameof(Models.FieldGroups)}Id");
                });
        }

        public virtual IDbSet<FieldGroups> FieldGroups { get; set; }
        private void BindFieldGroups(DbModelBuilder modelBuilder)
        {
            var bind = modelBuilder.Entity<FieldGroups>();
            bind.HasKey(a => a.Id).Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            bind.Property(a => a.SecurityStamp).HasMaxLength(36).IsFixedLength().IsConcurrencyToken();

            bind.Property(a => a.Name).IsVariableLength().IsUnicode().HasMaxLength(128);
            bind.Property(a => a.Remark).IsVariableLength().IsUnicode().HasMaxLength(256);
            bind.HasMany(a => a.Users)
                .WithMany(a => a.InfoGroup)
                .Map(a =>
                {
                    a.ToTable($"R_{nameof(Models.FieldGroups)}_{nameof(SMSUser)}")
                        .MapLeftKey($"{nameof(Models.FieldGroups)}Id")
                        .MapRightKey($"{nameof(SMSUser)}Id");
                });
        }

        public virtual IDbSet<ServiceDescription> Services { get; set; }
        private void BindServices(DbModelBuilder modelBuilder)
        {
            var bind = modelBuilder.Entity<ServiceDescription>();
            bind.HasKey(a => a.Id).Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            bind.Property(a => a.SecurityStamp).HasMaxLength(36).IsFixedLength().IsConcurrencyToken();

            bind.Property(a => a.Name).IsRequired().IsVariableLength().IsUnicode().HasMaxLength(128);
            bind.Property(a => a.Remark).IsOptional().IsVariableLength().IsUnicode().HasMaxLength(2048);
            bind.Property(a => a.Config).IsOptional().IsVariableLength().IsUnicode().IsMaxLength();
            bind.Property(a => a.Price).HasPrecision(18, 2);

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
            bind.Property(a => a.SecurityStamp).HasMaxLength(36).IsFixedLength().IsConcurrencyToken();

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
            bind.Property(a => a.SecurityStamp).HasMaxLength(36).IsFixedLength().IsConcurrencyToken();

            bind.Property(a => a.Name).IsVariableLength().IsUnicode().HasMaxLength(128);
            bind.Property(a => a.Amount).HasPrecision(18, 2);

            bind.HasMany(a => a.PaymentPlans)
                .WithRequired(a => a.Contract)
                .HasForeignKey(a => a.ContractId)
                .WillCascadeOnDelete(false);
            bind.HasMany(a => a.PlanJobs)
                .WithRequired(a => a.Contract)
                .HasForeignKey(a => a.ContractId)
                .WillCascadeOnDelete(false);
        }

        public virtual IDbSet<ServiceContractJobConfig> ServiceContractJobs { get; set; }
        private void BindServiceContractJobConfig(DbModelBuilder modelBuilder)
        {
            var bind = modelBuilder.Entity<ServiceContractJobConfig>();
            bind.HasKey(a => a.Id).Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            bind.Property(a => a.SecurityStamp).HasMaxLength(36).IsFixedLength().IsConcurrencyToken();

            bind.Property(a => a.Config).IsOptional().IsVariableLength().IsUnicode().IsMaxLength();
        }

        public virtual IDbSet<ServiceContractTemplate> ServiceContractTemplates { get; set; }
        private void BindServiceContractTemplate(DbModelBuilder modelBuilder)
        {
            var bind = modelBuilder.Entity<ServiceContractTemplate>();
            bind.HasKey(a => a.Id).Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            bind.Property(a => a.SecurityStamp).HasMaxLength(36).IsFixedLength().IsConcurrencyToken();

            bind.Property(a => a.Name).IsVariableLength().IsUnicode().HasMaxLength(128);
            bind.Property(a => a.Config).IsOptional().IsVariableLength().IsUnicode().IsMaxLength();
        }

        public virtual IDbSet<ServiceContractPaymentPlan> ServiceContractPaymentPlans { get; set; }
        private void BindServiceContractPaymentPlan(DbModelBuilder modelBuilder)
        {
            var bind = modelBuilder.Entity<ServiceContractPaymentPlan>();
            bind.HasKey(a => a.Id).Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            bind.Property(a => a.SecurityStamp).HasMaxLength(36).IsFixedLength().IsConcurrencyToken();

            bind.Property(a => a.Amount).HasPrecision(18, 2);

            bind.HasMany(a => a.Records)
                .WithRequired(a => a.PaymentPlan)
                .HasForeignKey(a => a.PaymentPlanId)
                .WillCascadeOnDelete(false);
        }


        public virtual IDbSet<PaymentRecord> PaymentRecords { get; set; }
        private void BindPaymentRecord(DbModelBuilder modelBuilder)
        {
            var bind = modelBuilder.Entity<PaymentRecord>();
            bind.HasKey(a => a.Id).Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            bind.Property(a => a.SecurityStamp).HasMaxLength(36).IsFixedLength().IsConcurrencyToken();

            bind.Property(a => a.Amount).HasPrecision(18, 2);
            bind.Property(a => a.ThirdPartyCode).IsVariableLength().IsUnicode().HasMaxLength(128);

        }

        public virtual IDbSet<OperationRecord> OperationRecords { get; set; }
        private void BindOperationRecord(DbModelBuilder modelBuilder)
        {
            var bind = modelBuilder.Entity<OperationRecord>();
            bind.HasKey(a => a.Id).Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            bind.Property(a => a.SecurityStamp).HasMaxLength(36).IsFixedLength().IsConcurrencyToken();

            bind.Property(a => a.Params).IsVariableLength().IsUnicode().IsMaxLength();

        }

        public virtual IDbSet<EnterpriseInfo> Enterprises { get; set; }
        private void BindEnterpriseInfo(DbModelBuilder modelBuilder)
        {
            var bind = modelBuilder.Entity<EnterpriseInfo>();
            bind.HasKey(a => a.Id).Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            bind.Property(a => a.SecurityStamp).HasMaxLength(36).IsFixedLength().IsConcurrencyToken();

            bind.Property(a => a.Name).IsVariableLength().IsUnicode().HasMaxLength(128);
            bind.HasRequired(m => m.UserInfo)
                .WithMany(m => m.Enterprises)
                .HasForeignKey(m => m.UserId)
                .WillCascadeOnDelete(false);

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
                .Entity<SMSUser>()
                .HasMany(a => a.ContractJobs)
                .WithRequired(a => a.Clerk)
                .HasForeignKey(a => a.ClerkId)
                .WillCascadeOnDelete(false);
            BindFields(modelBuilder);
            BindFieldGroups(modelBuilder);
            BindOperationRecord(modelBuilder);
            BindPaymentRecord(modelBuilder);
            BindServiceContract(modelBuilder);
            BindServiceContractJobConfig(modelBuilder);
            BindServiceContractPaymentPlan(modelBuilder);
            BindServiceContractTemplate(modelBuilder);
            BindServicePackage(modelBuilder);
            BindServices(modelBuilder);
        }
        
        public static SMSDbContext Create()
        {
            return new SMSDbContext();
        }
    }
}