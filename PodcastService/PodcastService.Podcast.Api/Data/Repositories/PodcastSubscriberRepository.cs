using Microsoft.EntityFrameworkCore;
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

        public async Task<PodcastSubscribers> GetByPodcastId(string subscriberId, int podcastId)
        {
            return await _db.Set<PodcastSubscribers>()
                .Where(x => x.PodcastId == podcastId && x.UserId == subscriberId)
                .FirstOrDefaultAsync();
        }

        public IEnumerable<PodcastSubscribers> GetSubscribersByPodcastId(int podcastId)
        {
            return _db.Set<PodcastSubscribers>()
                .Where(x => x.PodcastId == podcastId)
                .AsNoTracking();
        }
    }
}
