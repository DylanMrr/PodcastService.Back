using PodcastService.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PodcastService.Podcast.Api.Data.Repositories
{
    public class PodcastRepository: BaseEntityFrameworkRepository<Podcast>, IPodcastRepository
    {
        public PodcastRepository(PodcastContext context) : base(context)
        {
        }
    }
}
