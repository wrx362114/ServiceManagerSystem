namespace F5QI.SMS.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EnterpriseInfoes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        UserId = c.Long(nullable: false),
                        SecurityStamp = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                        UserInfo_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserInfo_Id)
                .Index(t => t.UserInfo_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ServiceContractJobConfigs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        ContractId = c.Long(nullable: false),
                        ServiceId = c.Long(nullable: false),
                        Config = c.String(),
                        ClerkId = c.Long(nullable: false),
                        State = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        SecurityStamp = c.String(maxLength: 36, fixedLength: true),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ServiceContracts", t => t.ContractId)
                .ForeignKey("dbo.AspNetUsers", t => t.ClerkId)
                .Index(t => t.ContractId)
                .Index(t => t.ClerkId);
            
            CreateTable(
                "dbo.ServiceContracts",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 128),
                        FirstPartyUserId = c.Long(nullable: false),
                        FirstPartyEnterpriseId = c.Long(nullable: false),
                        SecondPartyUserId = c.Long(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SecurityStamp = c.String(maxLength: 36, fixedLength: true),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ServiceContractPaymentPlans",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PlanPayTime = c.DateTime(nullable: false),
                        ContractId = c.Long(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsPaid = c.Boolean(nullable: false),
                        SecurityStamp = c.String(maxLength: 36, fixedLength: true),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ServiceContracts", t => t.ContractId)
                .Index(t => t.ContractId);
            
            CreateTable(
                "dbo.PaymentRecords",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Method = c.Int(nullable: false),
                        State = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ThirdPartyCode = c.String(maxLength: 128),
                        PaymentPlanId = c.Long(nullable: false),
                        SecurityStamp = c.String(maxLength: 36, fixedLength: true),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ServiceContractPaymentPlans", t => t.PaymentPlanId)
                .Index(t => t.PaymentPlanId);
            
            CreateTable(
                "dbo.FieldGroups",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 128),
                        Remark = c.String(maxLength: 256),
                        SecurityStamp = c.String(maxLength: 36, fixedLength: true),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FieldDescriptions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Name = c.String(maxLength: 128),
                        Required = c.Boolean(nullable: false),
                        SecurityStamp = c.String(maxLength: 36, fixedLength: true),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.Long(nullable: false),
                        RoleId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.OperationRecords",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        BusinessId = c.Long(nullable: false),
                        Params = c.String(),
                        SecurityStamp = c.String(maxLength: 36, fixedLength: true),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.ServiceContractTemplates",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 128),
                        Type = c.Int(nullable: false),
                        Config = c.String(),
                        SecurityStamp = c.String(maxLength: 36, fixedLength: true),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ServicePackages",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 128),
                        Remark = c.String(maxLength: 2048),
                        SecurityStamp = c.String(maxLength: 36, fixedLength: true),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ServiceDescriptions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 128),
                        Config = c.String(),
                        Remark = c.String(maxLength: 2048),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SecurityStamp = c.String(maxLength: 36, fixedLength: true),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.R_FieldDescription_FieldGroups",
                c => new
                    {
                        FieldDescriptionId = c.Long(nullable: false),
                        FieldGroupsId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.FieldDescriptionId, t.FieldGroupsId })
                .ForeignKey("dbo.FieldDescriptions", t => t.FieldDescriptionId, cascadeDelete: true)
                .ForeignKey("dbo.FieldGroups", t => t.FieldGroupsId, cascadeDelete: true)
                .Index(t => t.FieldDescriptionId)
                .Index(t => t.FieldGroupsId);
            
            CreateTable(
                "dbo.R_FieldGroups_SMSUser",
                c => new
                    {
                        FieldGroupsId = c.Long(nullable: false),
                        SMSUserId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.FieldGroupsId, t.SMSUserId })
                .ForeignKey("dbo.FieldGroups", t => t.FieldGroupsId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.SMSUserId, cascadeDelete: true)
                .Index(t => t.FieldGroupsId)
                .Index(t => t.SMSUserId);
            
            CreateTable(
                "dbo.R_ServiceDescription_ServicePackage",
                c => new
                    {
                        ServiceDescriptionId = c.Long(nullable: false),
                        ServicePackageId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.ServiceDescriptionId, t.ServicePackageId })
                .ForeignKey("dbo.ServiceDescriptions", t => t.ServiceDescriptionId, cascadeDelete: true)
                .ForeignKey("dbo.ServicePackages", t => t.ServicePackageId, cascadeDelete: true)
                .Index(t => t.ServiceDescriptionId)
                .Index(t => t.ServicePackageId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.R_ServiceDescription_ServicePackage", "ServicePackageId", "dbo.ServicePackages");
            DropForeignKey("dbo.R_ServiceDescription_ServicePackage", "ServiceDescriptionId", "dbo.ServiceDescriptions");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.R_FieldGroups_SMSUser", "SMSUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.R_FieldGroups_SMSUser", "FieldGroupsId", "dbo.FieldGroups");
            DropForeignKey("dbo.R_FieldDescription_FieldGroups", "FieldGroupsId", "dbo.FieldGroups");
            DropForeignKey("dbo.R_FieldDescription_FieldGroups", "FieldDescriptionId", "dbo.FieldDescriptions");
            DropForeignKey("dbo.EnterpriseInfoes", "UserInfo_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ServiceContractJobConfigs", "ClerkId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ServiceContractJobConfigs", "ContractId", "dbo.ServiceContracts");
            DropForeignKey("dbo.ServiceContractPaymentPlans", "ContractId", "dbo.ServiceContracts");
            DropForeignKey("dbo.PaymentRecords", "PaymentPlanId", "dbo.ServiceContractPaymentPlans");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.R_ServiceDescription_ServicePackage", new[] { "ServicePackageId" });
            DropIndex("dbo.R_ServiceDescription_ServicePackage", new[] { "ServiceDescriptionId" });
            DropIndex("dbo.R_FieldGroups_SMSUser", new[] { "SMSUserId" });
            DropIndex("dbo.R_FieldGroups_SMSUser", new[] { "FieldGroupsId" });
            DropIndex("dbo.R_FieldDescription_FieldGroups", new[] { "FieldGroupsId" });
            DropIndex("dbo.R_FieldDescription_FieldGroups", new[] { "FieldDescriptionId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.PaymentRecords", new[] { "PaymentPlanId" });
            DropIndex("dbo.ServiceContractPaymentPlans", new[] { "ContractId" });
            DropIndex("dbo.ServiceContractJobConfigs", new[] { "ClerkId" });
            DropIndex("dbo.ServiceContractJobConfigs", new[] { "ContractId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.EnterpriseInfoes", new[] { "UserInfo_Id" });
            DropTable("dbo.R_ServiceDescription_ServicePackage");
            DropTable("dbo.R_FieldGroups_SMSUser");
            DropTable("dbo.R_FieldDescription_FieldGroups");
            DropTable("dbo.ServiceDescriptions");
            DropTable("dbo.ServicePackages");
            DropTable("dbo.ServiceContractTemplates");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.OperationRecords");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.FieldDescriptions");
            DropTable("dbo.FieldGroups");
            DropTable("dbo.PaymentRecords");
            DropTable("dbo.ServiceContractPaymentPlans");
            DropTable("dbo.ServiceContracts");
            DropTable("dbo.ServiceContractJobConfigs");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.EnterpriseInfoes");
        }
    }
}
