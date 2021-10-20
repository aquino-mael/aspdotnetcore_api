using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Api.Data.Context
{
  public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
  {
    public MyContext CreateDbContext(string[] args)
    {
      var connectionString = "Server=localhost;Initial Catalog=dbapi;MultipleActiveResultSets=true;User ID=sa;Password=DockerSql2017!";
      var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
      optionsBuilder.UseSqlServer(connectionString);
      return new MyContext(optionsBuilder.Options);
    }
  }
}
