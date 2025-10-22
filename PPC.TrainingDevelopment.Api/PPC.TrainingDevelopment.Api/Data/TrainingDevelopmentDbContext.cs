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
                        entity.Property(e => e.AccommodationTravel).HasColumnType("decimal(18,2)");
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
            }
      }
}