using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RVTR.Lodging.DataContext;
using RVTR.Lodging.DataContext.Repositories;
using RVTR.Lodging.ObjectModel.Models;
using Xunit;

namespace RVTR.Lodging.UnitTesting.Tests
{
  public class RepositoryTest
  {
    private static readonly SqliteConnection _connection = new SqliteConnection("Data Source=:memory:");
    private static readonly DbContextOptions<LodgingContext> _options = new DbContextOptionsBuilder<LodgingContext>().UseSqlite(_connection).Options;

    public static readonly IEnumerable<object[]> _records = new List<object[]>()
    {
      new object[]
      {
        new LodgingModel() { Id = 1, LocationId = 1 },
        new RentalModel() { Id = 1 },
        new ReviewModel() { Id = 1 }
      }
    };

    [Theory]
    [MemberData(nameof(_records))]
    public async void Test_Repository_DeleteAsync(LodgingModel lodging, RentalModel rental, ReviewModel review)
    {
      await _connection.OpenAsync();

      try
      {
        using (var ctx = new LodgingContext(_options))
        {
          await ctx.Database.EnsureCreatedAsync();
          ctx.Rentals.RemoveRange(ctx.Rentals);
          ctx.Lodgings.RemoveRange(ctx.Lodgings);
          ctx.SaveChanges();
          await ctx.Rentals.AddAsync(rental);
          await ctx.Reviews.AddAsync(review);
          await ctx.Lodgings.AddAsync(lodging);
          await ctx.SaveChangesAsync();
        }

        using (var ctx = new LodgingContext(_options))
        {
          var lodgings = new Repository<LodgingModel>(ctx);

          await lodgings.DeleteAsync(1);
          await ctx.SaveChangesAsync();

          Assert.Empty(await ctx.Lodgings.ToListAsync());
        }

        using (var ctx = new LodgingContext(_options))
        {
          var rentals = new Repository<RentalModel>(ctx);

          await rentals.DeleteAsync(1);
          await ctx.SaveChangesAsync();

          Assert.Empty(await ctx.Rentals.ToListAsync());
        }

        using (var ctx = new LodgingContext(_options))
        {
          var reviews = new Repository<ReviewModel>(ctx);

          await reviews.DeleteAsync(1);
          await ctx.SaveChangesAsync();

          Assert.Empty(await ctx.Reviews.ToListAsync());
        }
      }
      finally
      {
        await _connection.CloseAsync();
      }
    }

    [Theory]
    [MemberData(nameof(_records))]
    public async void Test_Repository_InsertAsync(LodgingModel lodging, RentalModel rental, ReviewModel review)
    {
      await _connection.OpenAsync();

      try
      {
        using (var ctx = new LodgingContext(_options))
        {
          await ctx.Database.EnsureCreatedAsync();
          ctx.Rentals.RemoveRange(ctx.Rentals);
          ctx.Lodgings.RemoveRange(ctx.Lodgings);
          ctx.SaveChanges();
        }

        using (var ctx = new LodgingContext(_options))
        {
          var lodgings = new Repository<LodgingModel>(ctx);

          await lodgings.InsertAsync(lodging);
          await ctx.SaveChangesAsync();

          Assert.NotEmpty(await ctx.Lodgings.ToListAsync());
        }

        using (var ctx = new LodgingContext(_options))
        {
          var rentals = new Repository<RentalModel>(ctx);

          await rentals.InsertAsync(rental);
          await ctx.SaveChangesAsync();

          Assert.NotEmpty(await ctx.Rentals.ToListAsync());
        }

        using (var ctx = new LodgingContext(_options))
        {
          var reviews = new Repository<ReviewModel>(ctx);

          await reviews.InsertAsync(review);
          await ctx.SaveChangesAsync();

          Assert.NotEmpty(await ctx.Reviews.ToListAsync());
        }
      }
      finally
      {
        await _connection.CloseAsync();
      }
    }

    [Fact]
    public async void Test_Repository_SelectAsync()
    {
      await _connection.OpenAsync();

      try
      {
        using (var ctx = new LodgingContext(_options))
        {
          await ctx.Database.EnsureCreatedAsync();
          ctx.Rentals.RemoveRange(ctx.Rentals);
          ctx.Lodgings.RemoveRange(ctx.Lodgings);
          ctx.SaveChanges();
        }

        using (var ctx = new LodgingContext(_options))
        {
          var lodgings = new Repository<LodgingModel>(ctx);

          var actual = await lodgings.SelectAsync();

          Assert.Empty(actual);
        }

        using (var ctx = new LodgingContext(_options))
        {
          var rentals = new Repository<RentalModel>(ctx);

          var actual = await rentals.SelectAsync();

          Assert.Empty(actual);
        }

        using (var ctx = new LodgingContext(_options))
        {
          var reviews = new Repository<ReviewModel>(ctx);

          var actual = await reviews.SelectAsync();

          Assert.Empty(actual);
        }
      }
      finally
      {
        await _connection.CloseAsync();
      }
    }

    [Fact]
    public async void Test_Repository_SelectAsync_ById()
    {
      await _connection.OpenAsync();

      try
      {
        using (var ctx = new LodgingContext(_options))
        {
          await ctx.Database.EnsureCreatedAsync();
          ctx.Rentals.RemoveRange(ctx.Rentals);
          ctx.Lodgings.RemoveRange(ctx.Lodgings);
          ctx.SaveChanges();
        }

        using (var ctx = new LodgingContext(_options))
        {
          var lodgings = new Repository<LodgingModel>(ctx);

          var actual = await lodgings.SelectAsync(1);

          Assert.Null(actual);
        }

        using (var ctx = new LodgingContext(_options))
        {
          var rentals = new Repository<RentalModel>(ctx);

          var actual = await rentals.SelectAsync(1);

          Assert.Null(actual);
        }

        using (var ctx = new LodgingContext(_options))
        {
          var reviews = new Repository<ReviewModel>(ctx);

          var actual = await reviews.SelectAsync(1);

          Assert.Null(actual);
        }
      }
      finally
      {
        await _connection.CloseAsync();
      }
    }

    [Theory]
    [MemberData(nameof(_records))]
    public async void Test_Repository_Update(LodgingModel lodging, RentalModel rental, ReviewModel review)
    {
      await _connection.OpenAsync();

      try
      {
        using (var ctx = new LodgingContext(_options))
        {
          await ctx.Database.EnsureCreatedAsync();
          ctx.Rentals.RemoveRange(ctx.Rentals);
          ctx.Lodgings.RemoveRange(ctx.Lodgings);
          ctx.SaveChanges();
          await ctx.Lodgings.AddAsync(lodging);
          await ctx.Rentals.AddAsync(rental);
          await ctx.Reviews.AddAsync(review);
          await ctx.SaveChangesAsync();
        }

        using (var ctx = new LodgingContext(_options))
        {
          var lodgings = new Repository<LodgingModel>(ctx);
          var expected = await ctx.Lodgings.FirstAsync();

          expected.Name = "name";
          lodgings.Update(expected);
          await ctx.SaveChangesAsync();

          var actual = await ctx.Lodgings.FirstAsync();

          Assert.Equal(expected, actual);
        }

        using (var ctx = new LodgingContext(_options))
        {
          var rentals = new Repository<RentalModel>(ctx);
          var expected = await ctx.Rentals.FirstAsync();

          expected.LotNumber = "lot number";
          rentals.Update(expected);
          await ctx.SaveChangesAsync();

          var actual = await ctx.Rentals.FirstAsync();

          Assert.Equal(expected, actual);
        }

        using (var ctx = new LodgingContext(_options))
        {
          var reviews = new Repository<ReviewModel>(ctx);
          var expected = await ctx.Reviews.FirstAsync();

          expected.Comment = "comment";
          reviews.Update(expected);
          await ctx.SaveChangesAsync();

          var actual = await ctx.Reviews.FirstAsync();

          Assert.Equal(expected, actual);
        }
      }
      finally
      {
        await _connection.CloseAsync();
      }
    }
  }
}
