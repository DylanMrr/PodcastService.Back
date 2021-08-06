using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PodcastService.Core.Messages;
using PodcastService.Podcast.Api.Data.Dto.PodcastSubscribers;
using PodcastService.Podcast.Api.Data.Repositories;
using PodcastService.Podcast.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PodcastService.Podcast.Api.Extensions.Mappers;

namespace PodcastService.Podcast.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PodcastSubscribersController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly IPodcastSubscribersRepository _podcastSubscribersRepository;
        private readonly IPodcastRepository _podcastRepository;

        public PodcastSubscribersController(
            IIdentityService identityService,
            IPodcastSubscribersRepository podcastSubscribersRepository,
            IPodcastRepository podcastRepository)
        {
            _identityService = identityService;
            _podcastSubscribersRepository = podcastSubscribersRepository;
            _podcastRepository = podcastRepository;
        }

        [HttpPost("subscribe")]
        public async Task<ActionResult> SubscribeToPodcast([FromBody] SubscribeToPodcastDto subscribeToPodcastDto)
        {
            var userId = _identityService.GetUserIdentity();
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest(new HttpRequestError() { Message = "Не удалось найти ID" });
            }

            var podcast = await _podcastRepository.GetAsync(subscribeToPodcastDto.PodcastId);
            if (podcast == null)
            {
                return BadRequest(new HttpRequestError() { Message = "Не удалось найти подкаст" });
            }

            var podcastSubscriber = subscribeToPodcastDto.MapToPodcastSubscriber(userId, podcast);
            try
            {
                var createdItem = await _podcastSubscribersRepository.AddAsync(podcastSubscriber);
                return StatusCode(StatusCodes.Status201Created, createdItem.Entity);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new HttpRequestError() { Message = "Ошибка сохранения" });
            }
        }
    }
}
