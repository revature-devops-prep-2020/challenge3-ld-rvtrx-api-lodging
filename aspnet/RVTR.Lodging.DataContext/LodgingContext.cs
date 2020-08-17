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

            modelBuilder.Entity<LodgingModel>().HasData(
               new LodgingModel()
               {
                   Id = 1,
                   Location = null,
                   Name = "Test",
                   Rentals =
                     new List<RentalModel>()
                     {new RentalModel(){
                         Id = 1,
                         Name = "string",
                         LodgingId = 1,
                         Occupancy = 0,
                         Type = "cabin",
                         Status = "booked",
                         Price = 0,
                         DiscountedPrice = 0
                     }},
                   Reviews = new List<ReviewModel>()
                   {

                   }
               });
        }
    }
}
