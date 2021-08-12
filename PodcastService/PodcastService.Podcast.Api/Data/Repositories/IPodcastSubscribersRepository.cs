using PodcastService.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PodcastService.Podcast.Api.Data.Repositories
{
    public interface IPodcastSubscribersRepository: IRepository<PodcastSubscribers>
    {
        Task<PodcastSubscribers> GetByPodcastId(string subscriberId, int podcastId);
        IEnumerable<PodcastSubscribers> GetSubscribersByPodcastId(int podcastId);
    }
}
