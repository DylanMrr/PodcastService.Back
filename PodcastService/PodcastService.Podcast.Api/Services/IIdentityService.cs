using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PodcastService.Podcast.Api.Services
{
    public interface IIdentityService
    {
        string GetUserIdentity();
    }
}
