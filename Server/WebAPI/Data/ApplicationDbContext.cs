using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options) { }

        public DbSet<WeatherForecast> weatherForecasts {get; set;}
        public DbSet<ObservedLocation> locations { get; set; }
        public DbSet<WebAPI.Models.User> Users { get; set; }
    }
}
