using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PodcastService.Podcast.Api.Data
{
    public class Podcast
    {
        public int Id { get; set; }

        /// <summary>
        /// UserId создателя
        /// </summary>
        public string CreatorId { get; set; }
        
        /// <summary>
        /// Название подкаста
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание подкаста
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime? CreatedUtc { get; set; }

        /// <summary>
        /// Подписчики
        /// </summary>
        public ICollection<PodcastSubscribers> PodcastSubscribers { get; set; } = new HashSet<PodcastSubscribers>();
    }
}
