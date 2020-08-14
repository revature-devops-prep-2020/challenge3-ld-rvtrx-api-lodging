using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RVTR.Lodging.DataContext;
using RVTR.Lodging.ObjectModel.Models;
using RVTR.Lodging.DataContext.Repositories;
using System.Collections.Generic;
using Xunit;

namespace RVTR.Lodging.UnitTesting.Tests
{
  public class LodgingRepoTests
  {
    private static readonly SqliteConnection _connection = new SqliteConnection("Data Source=:memory:");
    private static readonly DbContextOptions<LodgingContext> _options = new DbContextOptionsBuilder<LodgingContext>().UseSqlite(_connection).Options;

    public static readonly IEnumerable<object[]> _records = new List<object[]>()
    {
      new object[]
      {
        new LodgingModel() { Id = 1 },
        new RentalModel() { Id = 1 },
        new ReviewModel() { Id = 1 }
      }
    };
  }
}
