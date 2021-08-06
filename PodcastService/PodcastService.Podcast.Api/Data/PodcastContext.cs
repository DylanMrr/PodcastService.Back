using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PodcastService.Podcast.Api.Data
{
    public class PodcastContext : DbContext
    {
        public DbSet<Podcast> Podcasts { get; set; }
        public DbSet<PodcastSubscribers> PodcastSubscribers { get; set; }

        public PodcastContext(DbContextOptions<PodcastContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
