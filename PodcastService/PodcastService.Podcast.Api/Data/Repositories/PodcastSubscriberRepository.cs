using PodcastService.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PodcastService.Podcast.Api.Data.Repositories
{
    public class PodcastSubscriberRepository : BaseEntityFrameworkRepository<PodcastSubscribers>, IPodcastSubscribersRepository
    {
        public PodcastSubscriberRepository(PodcastContext context) : base(context)
        {
        }
    }
}
