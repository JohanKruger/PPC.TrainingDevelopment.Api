using Microsoft.EntityFrameworkCore;
using PPC.TrainingDevelopment.Api.Models;

namespace PPC.TrainingDevelopment.Api.Data
{
      public class TrainingDevelopmentDbContext : DbContext
      {
            public TrainingDevelopmentDbContext(DbContextOptions<TrainingDevelopmentDbContext> options) : base(options)
            {
            }

            public DbSet<LookupValue> LookupValues { get; set; }
            public DbSet<Employee> Employees { get; set; }
            public DbSet<EmployeeLookup> EmployeeLookups { get; set; }
            public DbSet<NonEmployee> NonEmployees { get; set; }
            public DbSet<TrainingEvent> TrainingEvents { get; set; }
            public DbSet<TrainingRecordEvent> TrainingRecordEvents { get; set; }
            public DbSet<AuditLog> AuditLogs { get; set; }
            public DbSet<UserPermission> UserPermissions { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                  base.OnModelCreating(modelBuilder);

                  // Configure LookupValue entity
                  modelBuilder.Entity<LookupValue>(entity =>
                  {
                        entity.HasKey(e => e.LookupId);
                        entity.Property(e => e.LookupId).ValueGeneratedOnAdd();
                        entity.Property(e => e.LookupType).IsRequired().HasMaxLength(50);
                        entity.Property(e => e.Value).IsRequired().HasMaxLength(100);
                        entity.Property(e => e.Code).HasMaxLength(20);
                        entity.Property(e => e.IsActive).IsRequired();

                        // Configure self-referencing relationship
                        entity.HasOne(e => e.Parent)
                        .WithMany(e => e.Children)
                        .HasForeignKey(e => e.ParentId)
                        .OnDelete(DeleteBehavior.Restrict);

                        // Add index for better query performance
                        entity.HasIndex(e => e.LookupType);
                        entity.HasIndex(e => new { e.LookupType, e.IsActive });
                  });

                  // Configure Employee entity
                  modelBuilder.Entity<Employee>(entity =>
                  {
                        entity.HasKey(e => e.PersonnelNumber);
                        entity.Property(e => e.PersonnelNumber).HasMaxLength(20);
                        entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
                        entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
                        entity.Property(e => e.KnownName).HasMaxLength(50);
                        entity.Property(e => e.Initials).HasMaxLength(10);
                        entity.Property(e => e.Race).HasMaxLength(50);
                        entity.Property(e => e.Gender).HasMaxLength(20);
                        entity.Property(e => e.EELevel).HasMaxLength(50);
                        entity.Property(e => e.EECategory).HasMaxLength(50);
                        entity.Property(e => e.JobTitle).HasMaxLength(100);
                        entity.Property(e => e.JobGrade).HasMaxLength(20);
                        entity.Property(e => e.IDNumber).HasMaxLength(13);
                        entity.Property(e => e.Site).HasMaxLength(100);
                        entity.Property(e => e.HighestQualification).HasMaxLength(100);
                        entity.Property(e => e.Notes).HasMaxLength(1000);
                  });

                  // Configure EmployeeLookup entity
                  modelBuilder.Entity<EmployeeLookup>(entity =>
                  {
                        entity.HasKey(e => e.PersonnelNumber);
                        entity.Property(e => e.PersonnelNumber).HasMaxLength(20);
                        entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
                        entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
                        entity.Property(e => e.KnownName).HasMaxLength(50);
                        entity.Property(e => e.Initials).HasMaxLength(10);
                        entity.Property(e => e.Race).HasMaxLength(50);
                        entity.Property(e => e.Gender).HasMaxLength(20);
                        entity.Property(e => e.EELevel).HasMaxLength(50);
                        entity.Property(e => e.EECategory).HasMaxLength(50);

                        // Add indexes for better query performance
                        entity.HasIndex(e => e.LastName);
                        entity.HasIndex(e => e.Race);
                        entity.HasIndex(e => e.Gender);
                        entity.HasIndex(e => e.EELevel);
                        entity.HasIndex(e => e.EECategory);
                        entity.HasIndex(e => e.Disability);
                  });

                  // Configure NonEmployee entity
                  modelBuilder.Entity<NonEmployee>(entity =>
                  {
                        entity.HasKey(e => e.IDNumber);
                        entity.Property(e => e.IDNumber).IsRequired().HasMaxLength(13);
                  });

                  // Configure TrainingEvent entity
                  modelBuilder.Entity<TrainingEvent>(entity =>
                  {
                        entity.HasKey(e => e.TrainingEventId);
                        entity.Property(e => e.TrainingEventId).ValueGeneratedOnAdd();
                        entity.Property(e => e.PersonnelNumber).HasMaxLength(20);
                        entity.Property(e => e.IDNumber).HasMaxLength(13);
                        entity.Property(e => e.EventTypeId).IsRequired();
                        entity.Property(e => e.TrainingEventNameId).IsRequired();
                        entity.Property(e => e.RegionId).IsRequired();
                        entity.Property(e => e.ProvinceId).IsRequired();
                        entity.Property(e => e.MunicipalityId).IsRequired();
                        entity.Property(e => e.SiteId).IsRequired();

                        // Configure foreign key relationships
                        entity.HasOne(e => e.Employee)
                        .WithMany()
                        .HasForeignKey(e => e.PersonnelNumber)
                        .OnDelete(DeleteBehavior.Restrict);

                        // Removed foreign key constraint for NonEmployee to allow IDNumber without requiring record in NonEmployees table

                        // Configure lookup relationships
                        entity.HasOne(e => e.EventType)
                        .WithMany()
                        .HasForeignKey(e => e.EventTypeId)
                        .OnDelete(DeleteBehavior.Restrict);

                        entity.HasOne(e => e.TrainingEventName)
                        .WithMany()
                        .HasForeignKey(e => e.TrainingEventNameId)
                        .OnDelete(DeleteBehavior.Restrict);

                        entity.HasOne(e => e.Region)
                        .WithMany()
                        .HasForeignKey(e => e.RegionId)
                        .OnDelete(DeleteBehavior.Restrict);

                        entity.HasOne(e => e.Province)
                        .WithMany()
                        .HasForeignKey(e => e.ProvinceId)
                        .OnDelete(DeleteBehavior.Restrict);

                        entity.HasOne(e => e.Municipality)
                        .WithMany()
                        .HasForeignKey(e => e.MunicipalityId)
                        .OnDelete(DeleteBehavior.Restrict);

                        entity.HasOne(e => e.Site)
                        .WithMany()
                        .HasForeignKey(e => e.SiteId)
                        .OnDelete(DeleteBehavior.Restrict);

                        // Add indexes for better query performance
                        entity.HasIndex(e => e.PersonnelNumber);
                        entity.HasIndex(e => e.IDNumber);
                        entity.HasIndex(e => e.EventTypeId);
                        entity.HasIndex(e => e.TrainingEventNameId);
                        entity.HasIndex(e => e.RegionId);
                        entity.HasIndex(e => e.ProvinceId);
                        entity.HasIndex(e => e.MunicipalityId);
                        entity.HasIndex(e => e.SiteId);
                  });

                  // Configure TrainingRecordEvent entity
                  modelBuilder.Entity<TrainingRecordEvent>(entity =>
                  {
                        entity.HasKey(e => e.TrainingRecordEventId);
                        entity.Property(e => e.TrainingRecordEventId).ValueGeneratedOnAdd();
                        entity.Property(e => e.TrainingEventId).IsRequired();
                        entity.Property(e => e.StartDate).IsRequired();
                        entity.Property(e => e.EndDate).IsRequired();
                        entity.Property(e => e.PersonnelNumber).HasMaxLength(20);
                        entity.Property(e => e.CostTrainingMaterials).HasColumnType("decimal(18,2)");
                        entity.Property(e => e.CostTrainers).HasColumnType("decimal(18,2)");
                        entity.Property(e => e.CostTrainingFacilities).HasColumnType("decimal(18,2)");
                        entity.Property(e => e.ScholarshipsBursaries).HasColumnType("decimal(18,2)");
                        entity.Property(e => e.CourseFees).HasColumnType("decimal(18,2)");
                        entity.Property(e => e.Accommodation).HasColumnType("decimal(18,2)");
                        entity.Property(e => e.Travel).HasColumnType("decimal(18,2)");
                        entity.Property(e => e.AdministrationCosts).HasColumnType("decimal(18,2)");
                        entity.Property(e => e.EquipmentDepreciation).HasColumnType("decimal(18,2)");

                        // Configure foreign key relationship
                        entity.HasOne(e => e.TrainingEvent)
                        .WithMany(te => te.TrainingRecordEvents)
                        .HasForeignKey(e => e.TrainingEventId)
                        .OnDelete(DeleteBehavior.Cascade);

                        // Add indexes for better query performance
                        entity.HasIndex(e => e.TrainingEventId);
                        entity.HasIndex(e => e.PersonnelNumber);
                        entity.HasIndex(e => e.StartDate);
                        entity.HasIndex(e => e.EndDate);
                        entity.HasIndex(e => e.Evidence);
                  });

                  // Configure AuditLog entity
                  modelBuilder.Entity<AuditLog>(entity =>
                  {
                        entity.HasKey(e => e.AuditLogId);
                        entity.Property(e => e.AuditLogId).ValueGeneratedOnAdd();
                        entity.Property(e => e.UserId).IsRequired().HasMaxLength(50);
                        entity.Property(e => e.UserName).IsRequired().HasMaxLength(100);
                        entity.Property(e => e.HttpMethod).IsRequired().HasMaxLength(10);
                        entity.Property(e => e.RequestPath).IsRequired().HasMaxLength(500);
                        entity.Property(e => e.QueryString).HasMaxLength(2000);
                        entity.Property(e => e.Controller).IsRequired().HasMaxLength(100);
                        entity.Property(e => e.Action).IsRequired().HasMaxLength(100);
                        entity.Property(e => e.RequestBody).HasColumnType("nvarchar(max)");
                        entity.Property(e => e.ResponseBody).HasColumnType("nvarchar(max)");
                        entity.Property(e => e.StatusCode).IsRequired();
                        entity.Property(e => e.Timestamp).IsRequired();
                        entity.Property(e => e.DurationMs).IsRequired();
                        entity.Property(e => e.IpAddress).HasMaxLength(45);
                        entity.Property(e => e.UserAgent).HasMaxLength(500);
                        entity.Property(e => e.ExceptionDetails).HasMaxLength(2000);
                        entity.Property(e => e.AdditionalInfo).HasMaxLength(500);

                        // Add indexes for better query performance
                        entity.HasIndex(e => e.UserId);
                        entity.HasIndex(e => e.Controller);
                        entity.HasIndex(e => e.Action);
                        entity.HasIndex(e => e.Timestamp);
                        entity.HasIndex(e => e.StatusCode);
                        entity.HasIndex(e => new { e.Controller, e.Action });
                        entity.HasIndex(e => new { e.UserId, e.Timestamp });
                  });

                  // Configure UserPermission entity
                  modelBuilder.Entity<UserPermission>(entity =>
                  {
                        entity.HasKey(e => e.PermissionId);
                        entity.Property(e => e.PermissionId).ValueGeneratedOnAdd();
                        entity.Property(e => e.Username).IsRequired().HasMaxLength(100);
                        entity.Property(e => e.PermissionCode).IsRequired().HasMaxLength(100);
                        entity.Property(e => e.CreatedDate).IsRequired().HasDefaultValueSql("GETDATE()");

                        // Add indexes for better query performance
                        entity.HasIndex(e => e.Username);
                        entity.HasIndex(e => e.PermissionCode);
                        entity.HasIndex(e => new { e.Username, e.PermissionCode }).IsUnique();
                        entity.HasIndex(e => e.CreatedDate);
                  });
            }
      }
}