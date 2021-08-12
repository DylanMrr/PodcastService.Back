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

        public static PodcastSubscriberInfoDto MapToPodcastSubscriberInfoDto(
            this Data.PodcastSubscribers podcastSubscribers)
        {
            var podcastSubscriberDto = new PodcastSubscriberInfoDto()
            {
                Id = podcastSubscribers.Id,
                PodcastId = podcastSubscribers.PodcastId,
                UserId = podcastSubscribers.UserId
            };
            return podcastSubscriberDto;
        }
    }
}
