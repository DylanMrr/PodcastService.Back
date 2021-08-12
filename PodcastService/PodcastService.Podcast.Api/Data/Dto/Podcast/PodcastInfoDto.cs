using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PodcastService.Podcast.Api.Data.Dto.Podcast
{
    public class PodcastInfoDto
    {
        public int Id { get; set; }
        public string CreatorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedUtc { get; set; }
        public IEnumerable<Data.PodcastSubscribers> PodcastSubscribers { get; set; }
    }
}
