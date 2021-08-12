using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PodcastService.Core.Messages;
using PodcastService.Podcast.Api.Data.Dto.Podcast;
using PodcastService.Podcast.Api.Data.Repositories;
using PodcastService.Podcast.Api.Services;
using PodcastService.Podcast.Api.Extensions.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace PodcastService.Podcast.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class PodcastController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly IPodcastRepository _podcastRepository;
        private readonly IPodcastSubscribersRepository _podcastSubscribersRepository;

        public PodcastController(
            IIdentityService identityService,
            IPodcastRepository podcastRepository,
            IPodcastSubscribersRepository podcastSubscribersRepository)
        {
            _identityService = identityService;
            _podcastRepository = podcastRepository;
            _podcastSubscribersRepository = podcastSubscribersRepository;
        }

        [HttpPost("createpodcast")]
        public async Task<ActionResult> CreatePodcast([FromBody] CreatePodcastDto createPodcastDto)
        {
            var userId = _identityService.GetUserIdentity();
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest(new HttpRequestError() { Message = "Не удалось найти ID" });
            }
            var podcast = createPodcastDto.MapToPodcast(userId);

            try
            {
                var createdItem = await _podcastRepository.AddAsync(podcast);
                return StatusCode(StatusCodes.Status201Created, createdItem.Entity);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new HttpRequestError() { Message = "Ошибка сохранения" });
            }
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PodcastInfoDto>> Get(int id)
        {
            var podcast = await _podcastRepository.GetAsync(id);
            if (podcast == null)
            {
                return BadRequest(new HttpRequestError() { Message = "Подкаст не найден" });
            }

            var subscribers = _podcastSubscribersRepository.GetSubscribersByPodcastId(id);

            var podcastInfoDto = podcast.MapToPodcastInfoDto(subscribers);
            return Ok(podcastInfoDto);
        }

        //todo список пользователей смаппить к читаемой форме
        [AllowAnonymous]
        [HttpGet("subscribers/{id:int}")]
        public async Task<IActionResult> GetSubscribers(int id)
        {
            var podcast = await _podcastRepository.GetAsync(id);
            if (podcast == null)
            {
                return BadRequest(new HttpRequestError() { Message = "Подкаст не найден" });
            }

            var subscribers = _podcastSubscribersRepository.GetSubscribersByPodcastId(id);

            return Ok(subscribers.Select(x => new { x.Id, x.UserId }));
        }
    }
}
