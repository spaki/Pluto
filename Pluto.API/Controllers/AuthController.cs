using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Pluto.API.Auth;
using Pluto.API.Controllers.Common;
using Pluto.API.Settings;
using Pluto.Domain.Bus;
using Pluto.Domain.Interfaces.Repositories;
using Pluto.Domain.Models;
using Pluto.Domain.Notifications;
using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Pluto.API.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IUserRepository userRepository;
        private readonly IDistributedCache cache;
        private readonly IMapper mapper;
        private readonly AppSettings appSettings;
        private readonly SigningConfigurations signingConfigurations;

        public AuthController(
            IMediatorHandler mediator, 
            IMediatorHandler bus, 
            INotificationHandler<DomainNotification> notifications, 
            IUserRepository userRepository, 
            IDistributedCache cache,
            IMapper mapper,
            AppSettings appSettings,
            SigningConfigurations signingConfigurations
        ) : base(mediator, bus, notifications)
        {
            this.userRepository = userRepository;
            this.cache = cache;
            this.mapper = mapper;
            this.appSettings = appSettings;
            this.signingConfigurations = signingConfigurations;
        }

        [BaererAuthorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = Guid.Parse(this.GetClaim(ClaimTypes.NameIdentifier));
            var user = await userRepository.GetAsync(userId);
            var result = mapper.Map<Domain.Dtos.User>(user);
            return Response(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AuthRequestDto authRequestDto)
        {
            var user = await GetUserByEmailAndPassword(authRequestDto);

            if (user == null)
                user = await GetUserByRefreshToken(authRequestDto);

            if(user == null)
                return StatusCode((int)HttpStatusCode.Forbidden, new AuthResultDto { Authenticated = false, Message = "Authentication failed!" });

            var result = AuthConfigHelper.GenerateToken(user, appSettings, cache, signingConfigurations);
            return Response(result);
        }

        private async Task<User> GetUserByEmailAndPassword(AuthRequestDto authRequestDto)
        {
            if (string.IsNullOrWhiteSpace(authRequestDto?.Email) || string.IsNullOrWhiteSpace(authRequestDto?.Password))
                return null;

            var user = await GetUserByEmail(authRequestDto);

            if (user == null)
                return null;

            if (user.CheckPassword(authRequestDto.Password))
                return user;

            return null;
        }

        private async Task<User> GetUserByRefreshToken(AuthRequestDto authRequestDto)
        {
            if (string.IsNullOrWhiteSpace(authRequestDto?.Email) || string.IsNullOrWhiteSpace(authRequestDto?.RefreshToken))
                return null;

            var storedToken = cache.GetString(authRequestDto.RefreshToken);

            if (string.IsNullOrWhiteSpace(storedToken))
                return null;

            var refreshTokenDataStored = JsonConvert.DeserializeObject<RefreshTokenData>(storedToken);

            if (refreshTokenDataStored == null)
                return null;

            if (authRequestDto.Email == refreshTokenDataStored.Email && authRequestDto.RefreshToken == refreshTokenDataStored.RefreshToken)
                return await GetUserByEmail(authRequestDto);

            return null;
        }

        private async Task<User> GetUserByEmail(AuthRequestDto authRequestDto) => await userRepository.FirstOrDefaultAsync(e => e.Email == authRequestDto.Email);
    }
}
