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

        public PodcastController(
            IIdentityService identityService,
            IPodcastRepository podcastRepository)
        {
            _identityService = identityService;
            _podcastRepository = podcastRepository;
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

            var podcastInfoDto = podcast.MapToPodcastInfoDto();
            return Ok(podcastInfoDto);
        }
    }
}
