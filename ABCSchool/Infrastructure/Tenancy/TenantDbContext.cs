using Finbuckle.MultiTenant.EntityFrameworkCore.Stores.EFCoreStore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tenancy;

public class TenantDbContext : EFCoreStoreDbContext<ABCSchoolTenatInfo>
{

    public TenantDbContext(DbContextOptions<TenantDbContext> options)
        : base(options)
    {

    }

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //modelBuilder.Entity<ABCSchoolTenatInfo>()
        //            .ToTable("Tenants","Multitenancy");

        modelBuilder.Entity<ABCSchoolTenatInfo>(entity =>
        {
            //entity.ToTable("Tenants", "Multitenancy");
            entity.ToTable("Tenants");

            //entity.HasKey(e => e.Id)
            //      .HasName("PK_Tenants");

            //entity.Property(e => e.Id)
            //      .HasMaxLength(64)
            //      .IsRequired();

            entity.Property(e => e.Identifier)
                  .HasMaxLength(450)
                  .IsRequired();

            entity.Property(e => e.Name)
                  .HasMaxLength(60)
                  .IsRequired();

            entity.Property(e => e.Email)
                  .HasMaxLength(100)
                  .IsRequired();

            entity.Property(e => e.FirstName)
                  .HasMaxLength(60)
                  .IsRequired();

            entity.Property(e => e.LastName)
                  .HasMaxLength(50);

            entity.Property(e => e.ConnectionString)
                  .HasMaxLength(450);

            entity.Property(e => e.ValidUpTo)
                  .IsRequired();

            entity.Property(e => e.IsActive)
                  .IsRequired();

            //entity.HasIndex(e => e.Identifier)
            //      .HasDatabaseName("IX_Tenants_Identifier")
            //      .IsUnique();
        });
    }
}
