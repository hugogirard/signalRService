using Microsoft.EntityFrameworkCore;

public class IntelligentHouseDbContext : DbContext
{
    public DbSet<Thermostat> Thermostats { get; set; }

    public DbSet<House> Houses { get; set; }
    public DbSet<Address> Addresses { get; set; }

    public IntelligentHouseDbContext(DbContextOptions<IntelligentHouseDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<House>()
            .HasOne(h => h.Address)
            .WithOne(a => a.House)
            .HasForeignKey<Address>(a => a.Id);

        base.OnModelCreating(modelBuilder);
    }
}