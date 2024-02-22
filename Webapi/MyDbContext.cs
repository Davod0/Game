namespace Webapi;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class MyDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Database.db");

    }
    public DbSet<Highscore> Highscore { get; set; }

}