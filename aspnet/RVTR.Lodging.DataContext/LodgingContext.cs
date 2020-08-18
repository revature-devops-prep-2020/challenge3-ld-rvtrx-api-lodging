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

      // Lodgings
      modelBuilder.Entity<LodgingModel>().HasData(
          //Lodging 1
          new LodgingModel
          {
            Id = 1,
            Name = "Dragon Fly",
            LocationId = 1,
            Bathrooms = 2
          },
          //Lodging 2
          new LodgingModel
          {
            Id = 2,
            Name = "Galleywinter",
            LocationId = 2,
            Bathrooms = 3
          },
          //Lodging 3
          new LodgingModel
          {
            Id = 3,
            Name = "Red Creek",
            LocationId = 3,
            Bathrooms = 5
          },
          //Lodging 4
          new LodgingModel
          {
            Id = 4,
            Name = "Lotus Belle",
            LocationId = 4,
            Bathrooms = 6
          });

      // Rentals
      modelBuilder.Entity<RentalModel>().HasData(
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
          Id = 5,
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
          Id = 6,
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
          Id = 7,
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
          Id = 8,
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
          Id = 9,
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
          Id = 10,
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
          Id = 11,
          Name = "101",
          LodgingId = 4,
          Occupancy = 5,
          Type = "RV",
          Status = "available",
          Price = 300,
          DiscountedPrice = 280
        },
        new RentalModel
        {
          Id = 12,
          Name = "102",
          LodgingId = 4,
          Occupancy = 5,
          Type = "RV",
          Status = "booked",
          Price = 300,
          DiscountedPrice = 280
        });

      //Locations
      modelBuilder.Entity<LocationModel>().HasData(
        new LocationModel
        {
          Id = 1,
          Latitude = "38.0755N",
          Longitude = "77.9889W"

        },
        new LocationModel
        {
          Id = 2,
          Latitude = "38.0103N",
          Longitude = "78.8152W"
        },
        new LocationModel
        {
          Id = 3,
          Latitude = "",
          Longitude = ""
        },
        new LocationModel
        {
          Id = 4,
          Latitude = "",
          Longitude = ""

        });

      // Addresses
      modelBuilder.Entity<AddressModel>().HasData(
        //Address for location 1
        new AddressModel
        {
          Id = 1,
          City = "Palm Bay",
          Country = "USA",
          PostalCode = "32908",
          StateProvince = "FL",
          Street = "750 Osmosis Dr SW",
          LocationId = 1,
        },

        //Address for location 2
        new AddressModel
        {
          Id = 2,
          City = "Afton",
          Country = "USA",
          PostalCode = "22920",
          StateProvince = "VA",
          Street = "8801 Dick Woods Rd",
          LocationId = 2,
        },

        //Address for location 3
        new AddressModel
        {
          Id = 3,
          City = "Hanna",
          Country = "USA",
          PostalCode = "84031",
          StateProvince = "UT",
          Street = "5761 Upper, Red Creek Rd",
          LocationId = 3,
        },

        //Address for location 4
        new AddressModel
        {
          Id = 4,
          City = "Topanga",
          Country = "USA",
          PostalCode = "90290",
          StateProvince = "CA",
          Street = "101 S Topanga Canyon Blvd",
          LocationId = 4,
        });
    }
  }
}

