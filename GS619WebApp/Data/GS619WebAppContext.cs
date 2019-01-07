using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GS619Challenge.Models;

namespace GS619WebApp.Models
{
    public class GS619WebAppContext : DbContext
    {
        public GS619WebAppContext (DbContextOptions<GS619WebAppContext> options)
            : base(options)
        {
        }

        public DbSet<GS619Challenge.Models.Activity> Activity { get; set; }
    }
}
