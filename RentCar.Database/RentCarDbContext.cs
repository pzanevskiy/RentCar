using Microsoft.EntityFrameworkCore;
using RentCar.Database.Entities.CarEntities;
using RentCar.Database.Entities.LocationEntities;
using RentCar.Database.Entities.OrderEntities;
using RentCar.Database.Entities.UserEntities;

namespace RentCar.Database
{
    public class RentCarDbContext : DbContext
    {
        public RentCarDbContext(DbContextOptions options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasOne(x => x.PickUpAddress).WithMany().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Order>()
                .HasOne(x => x.ReturnAddress).WithMany().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Order>()
                .HasOne(x => x.User).WithMany().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Order>()
                .HasMany(x => x.Penalties).WithOne(x => x.Order).OnDelete(DeleteBehavior.NoAction);
        }

        public virtual DbSet<LoyaltyProgram> LoyaltyProgram { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<UsersRoles> UsersRoles { get; set; }
        public virtual DbSet<User> User { get; set; }

        public virtual DbSet<Penalty> Penalty { get; set; }
        public virtual DbSet<Enhancement> Enhancement { get; set; }
        public virtual DbSet<OrderStatus> OrderStatus { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<EnhancementsOrders> EnhancementsOrders { get; set; }

        public virtual DbSet<Brand> Brand { get; set; }
        public virtual DbSet<CarModel> CarModel { get; set; }
        public virtual DbSet<CarType> CarType { get; set; }
        public virtual DbSet<Car> Car { get; set; }
        public virtual DbSet<CitiesCars> CitiesCars { get; set; }

        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
    }
}
