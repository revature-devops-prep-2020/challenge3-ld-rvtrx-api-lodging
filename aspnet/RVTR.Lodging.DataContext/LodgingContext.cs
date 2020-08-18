using Microsoft.EntityFrameworkCore;
using RVTR.Lodging.ObjectModel.Models;

namespace RVTR.Lodging.DataContext
{
  /// <summary>
  /// Represents the _Lodging_ context
  /// </summary>
  public class LodgingContext : DbContext
  {
    public DbSet<LodgingModel> Lodgings { get; set; }
    public DbSet<RentalModel> Rentals { get; set; }
    public DbSet<ReviewModel> Reviews { get; set; }

    public LodgingContext(DbContextOptions<LodgingContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<AddressModel>().HasKey(e => e.Id);
      modelBuilder.Entity<LocationModel>().HasKey(e => e.Id);
      modelBuilder.Entity<LodgingModel>().HasKey(e => e.Id);
      modelBuilder.Entity<RentalModel>().HasKey(e => e.Id);
      modelBuilder.Entity<ReviewModel>().HasKey(e => e.Id);
      Seed(ref modelBuilder);
    }

    public static void Seed(ref ModelBuilder modelBuilder)
    {
      //Lodgings
      modelBuilder.Entity<LodgingModel>()
          .HasData(
          //Lodging 1
          new LodgingModel { },
          //Lodging 2
          new LodgingModel { },
          //Lodging 3
          new LodgingModel { },
          //Lodging 4
          new LodgingModel { }
        );

      //Rentals
      modelBuilder.Entity<RentalModel>()
        .HasData(
        //rentals for lodging 1
        new RentalModel
        {
          Id = 1,
          Name = "100",
          LodgingId = 1,
          Occupancy = 4,
          Type = "tent",
          Status = "available",
          Price = 100,
          DiscountedPrice = 70
        },
        new RentalModel
        {
          Id = 2,
          Name = "101",
          LodgingId = 1,
          Occupancy = 5,
          Type = "RV",
          Status = "available",
          Price = 300,
          DiscountedPrice = 280
        },
        new RentalModel
        {
          Id = 3,
          Name = "102",
          LodgingId = 1,
          Occupancy = 5,
          Type = "RV",
          Status = "booked",
          Price = 300,
          DiscountedPrice = 280
        },
        new RentalModel
        {
          Id = 4,
          Name = "104",
          LodgingId = 1,
          Occupancy = 4,
          Type = "tent",
          Status = "booked",
          Price = 100,
          DiscountedPrice = 70
        },
        //rentals for lodging 2
        new RentalModel
        {
          Id = 1,
          Name = "100",
          LodgingId = 2,
          Occupancy = 4,
          Type = "tent",
          Status = "available",
          Price = 100,
          DiscountedPrice = 70
        },
        new RentalModel
        {
          Id = 2,
          Name = "101",
          LodgingId = 2,
          Occupancy = 5,
          Type = "RV",
          Status = "available",
          Price = 300,
          DiscountedPrice = 280
        },
        new RentalModel
        {
          Id = 3,
          Name = "102",
          LodgingId = 2,
          Occupancy = 5,
          Type = "RV",
          Status = "booked",
          Price = 300,
          DiscountedPrice = 280
        },
        new RentalModel
        {
          Id = 4,
          Name = "104",
          LodgingId = 2,
          Occupancy = 4,
          Type = "tent",
          Status = "booked",
          Price = 100,
          DiscountedPrice = 70
        },
        //rentals for lodging 3
        new RentalModel
        {
          Id = 1,
          Name = "100",
          LodgingId = 3,
          Occupancy = 4,
          Type = "tent",
          Status = "available",
          Price = 100,
          DiscountedPrice = 70
        },
        new RentalModel
        {
          Id = 4,
          Name = "104",
          LodgingId = 3,
          Occupancy = 4,
          Type = "tent",
          Status = "booked",
          Price = 100,
          DiscountedPrice = 70
        },
        //rentals for lodging 4
        new RentalModel
        {
          Id = 2,
          Name = "101",
          LodgingId = 2,
          Occupancy = 5,
          Type = "RV",
          Status = "available",
          Price = 300,
          DiscountedPrice = 280
        },
        new RentalModel
        {
          Id = 3,
          Name = "102",
          LodgingId = 2,
          Occupancy = 5,
          Type = "RV",
          Status = "booked",
          Price = 300,
          DiscountedPrice = 280
        }
        );
    }
  }
}
