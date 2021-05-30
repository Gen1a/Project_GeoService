using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project_GeoService.Models;

namespace Project_GeoService.Data
{
    public class Project_GeoServiceContext : DbContext
    {
        public Project_GeoServiceContext (DbContextOptions<Project_GeoServiceContext> options)
            : base(options)
        {
        }

        public DbSet<Country> Country { get; set; }
    }
}
