using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PodcastService.Category.Api.Data
{
    public class CategoryContext: DbContext
    {
        public DbSet<Category> Categories { get; set; }

        public CategoryContext(DbContextOptions<CategoryContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
