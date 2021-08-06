using PodcastService.Podcast.Api.Data.Dto.PodcastSubscribers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PodcastService.Podcast.Api.Extensions.Mappers
{
    public static class PodcastSubscriberMapper
    {
        public static Data.PodcastSubscribers MapToPodcastSubscriber(
            this SubscribeToPodcastDto subscribeToPodcast, 
            string subscriberId,
            Data.Podcast podcast)
        {
            var podcastSubscriber = new Data.PodcastSubscribers()
            {
                UserId = subscriberId,
                Podcast = podcast,
                PodcastId = podcast.Id
            };
            return podcastSubscriber;
        }
    }
}
