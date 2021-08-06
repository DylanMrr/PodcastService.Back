using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PodcastService.Podcast.Api.Data.Dto.Podcast
{
    public class CreatePodcastDto
    {
        public string PodcastName { get; set; }

        public string Description { get; set; }
    }
}
