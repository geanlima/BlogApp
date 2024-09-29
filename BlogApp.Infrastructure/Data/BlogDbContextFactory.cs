using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BlogApp.Infrastructure.Data;

public class BlogDbContextFactory : IDesignTimeDbContextFactory<BlogDbContext>
{
    public BlogDbContext CreateDbContext(string[] args)
    {
        var path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "BlogApp.Api");        

        IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(path)
               .AddJsonFile("appsettings.Development.json")
               .Build();

        var optionsBuilder = new DbContextOptionsBuilder<BlogDbContext>();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        optionsBuilder.UseSqlServer(connectionString);

        return new BlogDbContext(optionsBuilder.Options);
    }
}
