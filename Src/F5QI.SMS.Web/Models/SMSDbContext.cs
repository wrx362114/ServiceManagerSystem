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
            BindBase(nameof(Fields), bind);
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
            BindBase(nameof(FieldGroups), bind);

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
            BindBase(nameof(Services), bind);

            bind.Property(a => a.Name).IsRequired().IsVariableLength().IsUnicode().HasMaxLength(128);
            bind.Property(a => a.Remark).IsOptional().IsVariableLength().IsUnicode().HasMaxLength(2048);
            bind.Property(a => a.Config).IsOptional().IsVariableLength().IsUnicode().IsMaxLength();
            bind.Property(a => a.Price).HasPrecision(18, 2);
            bind.HasMany(a => a.Packages)
                .WithRequired(a => a.Service)
                .HasForeignKey(a => a.ServiceId)
                .WillCascadeOnDelete(false);
        }

        public virtual IDbSet<ServicePackage> ServicePackages { get; set; }
        private void BindServicePackage(DbModelBuilder modelBuilder)
        {
            var bind = modelBuilder.Entity<ServicePackage>();
            BindBase(nameof(ServicePackages), bind);

            bind.Property(a => a.Name).IsVariableLength().IsUnicode().HasMaxLength(128);
            bind.Property(a => a.Remark).IsVariableLength().IsUnicode().HasMaxLength(2048);
            bind.Property(a => a.Price).HasPrecision(18, 2);
            bind.HasMany(a => a.Services)
                .WithRequired(a => a.Package)
                .HasForeignKey(a => a.PackageId)
                .WillCascadeOnDelete(false);
        }

        public virtual IDbSet<R_ServiceDescription_ServicePackage> R_Service_Package { get; set; }
        private void BindR_Service_Package(DbModelBuilder modelBuilder)
        {
            var bind = modelBuilder.Entity<R_ServiceDescription_ServicePackage>();
            BindBase(nameof(R_Service_Package), bind);

            bind.Property(a => a.Price).HasPrecision(18, 2);
        }

        public virtual IDbSet<ServiceContract> ServiceContracts { get; set; }
        private void BindServiceContract(DbModelBuilder modelBuilder)
        {
            var bind = modelBuilder.Entity<ServiceContract>();
            BindBase(nameof(ServiceContracts), bind);

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
            BindBase(nameof(ServiceContractJobs), bind);

            bind.Property(a => a.Config).IsOptional().IsVariableLength().IsUnicode().IsMaxLength();
        }

        public virtual IDbSet<ServiceContractTemplate> ServiceContractTemplates { get; set; }
        private void BindServiceContractTemplate(DbModelBuilder modelBuilder)
        {
            var bind = modelBuilder.Entity<ServiceContractTemplate>();
            BindBase(nameof(ServiceContractTemplates), bind);

            bind.Property(a => a.Name).IsVariableLength().IsUnicode().HasMaxLength(128);
            bind.Property(a => a.Config).IsOptional().IsVariableLength().IsUnicode().IsMaxLength();
        }

        public virtual IDbSet<ServiceContractPaymentPlan> ServiceContractPaymentPlans { get; set; }
        private void BindServiceContractPaymentPlan(DbModelBuilder modelBuilder)
        {
            var bind = modelBuilder.Entity<ServiceContractPaymentPlan>();
            BindBase(nameof(ServiceContractPaymentPlans), bind);

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
            BindBase(nameof(PaymentRecords), bind);

            bind.Property(a => a.Amount).HasPrecision(18, 2);
            bind.Property(a => a.ThirdPartyCode).IsVariableLength().IsUnicode().HasMaxLength(128);

        }

        public virtual IDbSet<UserOperationRecords> OperationRecords { get; set; }
        private void BindOperationRecord(DbModelBuilder modelBuilder)
        {
            var bind = modelBuilder.Entity<UserOperationRecords>();

            BindBase(nameof(OperationRecords), bind);
            bind.Property(a => a.Params).IsVariableLength().IsUnicode().IsMaxLength();

        }

        public virtual IDbSet<EnterpriseInfo> Enterprises { get; set; }
        private void BindEnterpriseInfo(DbModelBuilder modelBuilder)
        {
            var bind = modelBuilder.Entity<EnterpriseInfo>();
            BindBase(nameof(Enterprises), bind);

            bind.Property(a => a.Name).IsVariableLength().IsUnicode().HasMaxLength(128);
            bind.HasRequired(m => m.UserInfo)
                .WithMany(m => m.Enterprises)
                .HasForeignKey(m => m.UserId)
                .WillCascadeOnDelete(false);

        }

        public virtual IDbSet<UserOperationRecords> UserOperationRecords { get; set; }
        private void BindUserOperationRecords(DbModelBuilder modelBuilder)
        {
            var bind = modelBuilder.Entity<UserOperationRecords>();
            BindBase(nameof(UserOperationRecords), bind);

            bind.HasRequired(m => m.UserInfo)
                .WithMany(m => m.OperationRecords)
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
            BindR_Service_Package(modelBuilder);
            BindServicePackage(modelBuilder);
            BindServices(modelBuilder);
            BindEnterpriseInfo(modelBuilder);
            BindUserOperationRecords(modelBuilder);
        }
        private void BindBase<T>(string name, System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<T> bind) where T : BaseModel
        {
            bind.HasKey(a => a.Id)
                .Property(a => a.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            bind.Property(a => a.SecurityStamp)
                .HasMaxLength(36)
                .IsFixedLength()
                .IsConcurrencyToken();
            bind.ToTable(name);
        }

        public static SMSDbContext Create()
        {
            return new SMSDbContext();
        }
    }
}