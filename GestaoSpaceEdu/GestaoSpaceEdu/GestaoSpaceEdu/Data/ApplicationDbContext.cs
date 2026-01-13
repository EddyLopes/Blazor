using GestaoSpaceEdu.Data.Interceptors;
using GestaoSpaceEdu.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GestaoSpaceEdu.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Company> Companies { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<FinancialTransaction> FinancialTransactions { get; set; }
    public DbSet<Document> Documents { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //base.OnConfiguring(optionsBuilder);
        optionsBuilder.AddInterceptors(new SoftDeleteInterceptor());
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<FinancialTransaction>()
               .Property(x => x.Repeat)
               .HasConversion<string>();

        builder.Entity<FinancialTransaction>()
               .Property(x => x.TypeFinancialTransaction)
               .HasConversion<string>();

        builder.Entity<Company>()
               .HasMany(x => x.Accounts)
               .WithOne(x => x.Company)
               .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Company>()
               .HasIndex(x => x.TaxId)
               .IsUnique();

        builder.Entity<Account>().HasQueryFilter(x => x.DeletedAt == null);
        builder.Entity<Category>().HasQueryFilter(x => x.DeletedAt == null);
        builder.Entity<Company>().HasQueryFilter(x => x.DeletedAt == null);
        builder.Entity<Document>().HasQueryFilter(x => x.DeletedAt == null);
        builder.Entity<FinancialTransaction>().HasQueryFilter(x => x.DeletedAt == null);
        builder.Entity<ApplicationUser>().HasQueryFilter(x => x.DeletedAt == null);
    }
}
