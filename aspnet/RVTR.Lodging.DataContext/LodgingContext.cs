using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RVTR.Lodging.ObjectModel.Models;

namespace RVTR.Lodging.DataContext
{
  /// <summary>
  /// Represents the _Lodging_ context
  /// </summary>
  public class LodgingContext : DbContext
  {
    /// <summary>
    ///
    /// </summary>
    /// <value></value>
    public DbSet<LodgingModel> Lodgings { get; set; }

    /// <summary>
    ///
    /// </summary>
    /// <value></value>
    public DbSet<RentalModel> Rentals { get; set; }

    /// <summary>
    ///
    /// </summary>
    /// <value></value>
    public DbSet<RentalUnitModel> RentalUnits { get; set; }

    /// <summary>
    ///
    /// </summary>
    /// <value></value>
    public DbSet<ReviewModel> Reviews { get; set; }

    /// <summary>
    ///
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    public LodgingContext(DbContextOptions<LodgingContext> options) : base(options) { }

    /// <summary>
    ///
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<AddressModel>().HasKey(e => e.Id);
      modelBuilder.Entity<LocationModel>().HasKey(e => e.Id);
      modelBuilder.Entity<LodgingModel>().HasKey(e => e.Id);
      modelBuilder.Entity<RentalModel>().HasKey(e => e.Id);
      modelBuilder.Entity<ReviewModel>().HasKey(e => e.Id);

      modelBuilder.Entity<LodgingModel>().HasData(new List<LodgingModel>()
      {
        new LodgingModel() { Id = 1, LocationId = 1, Name = "Dragon Fly", Bathrooms = 2 },
        new LodgingModel() { Id = 2, LocationId = 2, Name = "Galleywinter", Bathrooms = 3 },
        new LodgingModel() { Id = 3, LocationId = 3, Name = "Red Creek", Bathrooms = 5 },
        new LodgingModel() { Id = 4, LocationId = 4, Name = "Lotus Belle", Bathrooms = 6 },
      });

      modelBuilder.Entity<RentalModel>().HasData(new List<RentalModel>()
      {
        new RentalModel() { Id = 1, LodgingId = 1, LotNumber = "100", Status = "Available", Price = 100, DiscountedPrice = 70 },
        new RentalModel() { Id = 2, LodgingId = 1, LotNumber = "101", Status = "Available", Price = 300, DiscountedPrice = 280 },
        new RentalModel() { Id = 3, LodgingId = 1, LotNumber = "102", Status = "Booked", Price = 300, DiscountedPrice = 280 },
        new RentalModel() { Id = 4, LodgingId = 1, LotNumber = "103", Status = "Booked", Price = 100, DiscountedPrice = 70 },
        new RentalModel() { Id = 5, LodgingId = 2, LotNumber = "100", Status = "Available", Price = 100, DiscountedPrice = 70 },
        new RentalModel() { Id = 6, LodgingId = 2, LotNumber = "101", Status = "Available", Price = 300, DiscountedPrice = 280 },
        new RentalModel() { Id = 7, LodgingId = 2, LotNumber = "102", Status = "Booked", Price = 300, DiscountedPrice = 280 },
        new RentalModel() { Id = 8, LodgingId = 2, LotNumber = "103", Status = "Booked", Price = 100, DiscountedPrice = 70 },
        new RentalModel() { Id = 9, LodgingId = 3, LotNumber = "100", Status = "Available", Price = 100, DiscountedPrice = 70 },
        new RentalModel() { Id = 10, LodgingId = 3, LotNumber = "101", Status = "Booked", Price = 100, DiscountedPrice = 70 },
        new RentalModel() { Id = 11, LodgingId = 4, LotNumber = "100", Status = "Available", Price = 300, DiscountedPrice = 280 },
        new RentalModel() { Id = 12, LodgingId = 4, LotNumber = "101", Status = "Booked", Price = 300, DiscountedPrice = 280 },
      });

      modelBuilder.Entity<RentalUnitModel>().HasData(new List<RentalUnitModel>()
      {
        new RentalUnitModel() { Id = 1, RentalId = 1, Capacity = 4, Name = "Tent", Size = "5x5" },
        new RentalUnitModel() { Id = 2, RentalId = 2, Capacity = 5, Name = "RV", Size = "10x10" },
        new RentalUnitModel() { Id = 3, RentalId = 3, Capacity = 5, Name = "RV", Size = "10x10" },
        new RentalUnitModel() { Id = 4, RentalId = 4, Capacity = 4, Name = "Tent", Size = "5x5" },
        new RentalUnitModel() { Id = 5, RentalId = 5, Capacity = 4, Name = "Tent", Size = "5x5" },
        new RentalUnitModel() { Id = 6, RentalId = 6, Capacity = 5, Name = "RV", Size = "10x10" },
        new RentalUnitModel() { Id = 7, RentalId = 7, Capacity = 5, Name = "RV", Size = "10x10" },
        new RentalUnitModel() { Id = 8, RentalId = 8, Capacity = 4, Name = "Tent", Size = "5x5" },
        new RentalUnitModel() { Id = 9, RentalId = 9, Capacity = 4, Name = "Tent", Size = "5x5" },
        new RentalUnitModel() { Id = 10, RentalId = 10, Capacity = 5, Name = "Tent", Size = "5x5" },
        new RentalUnitModel() { Id = 11, RentalId = 11, Capacity = 4, Name = "RV", Size = "10x10" },
        new RentalUnitModel() { Id = 12, RentalId = 12, Capacity = 5, Name = "RV", Size = "10x10" },
      });

      modelBuilder.Entity<LocationModel>().HasData(new List<LocationModel>()
      {
        new LocationModel() { Id = 1, AddressId = 1, Latitude = "38.0755", Longitude = "77.9889" },
        new LocationModel() { Id = 2, AddressId = 2, Latitude = "38.0755", Longitude = "77.9889" },
        new LocationModel() { Id = 3, AddressId = 3, Latitude = "38.0755", Longitude = "77.9889" },
        new LocationModel() { Id = 4, AddressId = 4, Latitude = "38.0755", Longitude = "77.9889" },
      });

      modelBuilder.Entity<AddressModel>().HasData(new List<AddressModel>()
      {
        new AddressModel() { Id = 1, City = "Palm Bay", Country = "USA", PostalCode = "32908", StateProvince = "FL", Street = "750 Osmosis Dr SW" },
        new AddressModel() { Id = 2, City = "Afton", Country = "USA", PostalCode = "22920", StateProvince = "VA", Street = "8801 Dick Woods Rd" },
        new AddressModel() { Id = 3, City = "Hanna", Country = "USA", PostalCode = "84031", StateProvince = "UT", Street = "5761 Upper, Red Creek Rd" },
        new AddressModel() { Id = 4, City = "Topanga", Country = "USA", PostalCode = "90290", StateProvince = "CA", Street = "101 S Topanga Canyon Blvd" },
      });
    }
  }
}
