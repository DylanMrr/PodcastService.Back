using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PodcastService.Podcast.Api.Data
{
    public class PodcastSubscribers
    {
        public int Id { get; set; }

        /// <summary>
        /// subscriber
        /// </summary>
        public string UserId { get; set; }

        public int PodcastId { get; set; }
        public Podcast Podcast { get; set; }
    }
}
