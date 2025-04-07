using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixly.Services.Database
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PhotoCategory> PhotoCategories { get; set; }
        public DbSet<PhotoTag> PhotoTags { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
    }
}
