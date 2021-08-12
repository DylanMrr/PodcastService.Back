using PodcastService.Podcast.Api.Data;
using PodcastService.Podcast.Api.Data.Dto.Podcast;
using System.Collections.Generic;

namespace PodcastService.Podcast.Api.Extensions.Mappers
{
    public static class PodcastMapper
    {
        public static Data.Podcast MapToPodcast(this CreatePodcastDto createPodcastDto, string creatorId)
        {
            var podcast = new Data.Podcast()
            {
                Name = createPodcastDto.PodcastName,
                Description = createPodcastDto.Description,
                CreatorId = creatorId,
                CreatedUtc = System.DateTime.UtcNow
            };
            return podcast;
        }

        public static PodcastInfoDto MapToPodcastInfoDto(
            this Data.Podcast podcast, 
            IEnumerable<PodcastSubscribers> podcastSubscribers)
        {
            var podcastInfoDto = new PodcastInfoDto()
            {
                Id = podcast.Id,
                CreatorId = podcast.CreatorId,
                Description = podcast.Description,
                Name = podcast.Name,
                PodcastSubscribers = podcastSubscribers,
                CreatedUtc = podcast.CreatedUtc,
            };
            return podcastInfoDto;
        }
    }
}
