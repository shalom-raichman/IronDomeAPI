using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace IronDomeAPI.Data
{
    public class IronDomeAPIDbContext : DbContext
    {
        public IronDomeAPIDbContext(DbContextOptions<IronDomeAPIDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<IronDomeAPI.Models.Attack> Attacks { get; set; } = default!;
        public DbSet<IronDomeAPI.Models.Defence> Defences { get; set; } = default!;
    }
}
