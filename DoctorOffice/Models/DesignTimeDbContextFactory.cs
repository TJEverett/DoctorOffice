using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DoctorOffice.Models
{
  public class DoctorOfficeContextFactory : IDesignTimeDbContextFactory<DoctorOfficeContext>
  {
    DoctorOfficeContext IDesignTimeDbContextFactory<DoctorOfficeContext>.CreateDbContext(string[] args)
    {
      IConfigurationRoot configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

      var builder = new DbContextOptionsBuilder<DoctorOfficeContext>();
      string connectionString = configuration.GetConnectionString("Defaultconnection");

      builder.UseMySql(connectionString);

      return new DoctorOfficeContext(builder.Options);
    }
  }
}