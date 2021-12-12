using DTOs.Twilio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio.AspNet.Common;
using Twilio.Base;
using Twilio.Jwt.AccessToken;
using Twilio.Rest.Video.V1;
using Twilio.Rest.Video.V1.Room;
using WebAPI.Data;
using WebAPI.Twilioo;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TwilioController : ControllerBase
    {
        private TwilioOptions twilioOptions;
        private ApplicationDbContext applicationDbContext;
        public TwilioController(Microsoft.Extensions.Options.IOptions<TwilioOptions> twilioOptions, ApplicationDbContext applicationDbContext)
        {
            this.twilioOptions = twilioOptions.Value;
            this.applicationDbContext = applicationDbContext;
        }

        [HttpGet("token")]
        public async Task<TwilioJWTDTO> GetToken()
        {
            return new TwilioJWTDTO
            {
                Token = new Token(
                    twilioOptions.AccountSid,
                    twilioOptions.ApiKey,
                    twilioOptions.ApiSecret,
                    User.Identity.Name,
                    grants: new HashSet<IGrant> { new VideoGrant() })
                .ToJwt()
            };
        }

        [HttpGet("allRooms")]
        public async Task<ActionResult<IEnumerable<RoomInfoDTO>>> GetAllRooms()
        {
            var rooms = await RoomResource.ReadAsync();
            var tasks = rooms.Select(
                room => GetRoomDetailsAsync(
                    room,
                    ParticipantResource.ReadAsync(
                        room.Sid,
                        ParticipantResource.StatusEnum.Connected)));

            return await Task.WhenAll(tasks);
        }
        private async Task<RoomInfoDTO> GetRoomDetailsAsync(
                RoomResource room,
                Task<ResourceSet<ParticipantResource>> participantTask)
        {
            var participants = await participantTask;
            return new RoomInfoDTO
            {
                Name = room.UniqueName,
            };
        }

        [HttpPost("callback")]
        public async Task<ActionResult> CallBack([FromServices] TwilioWhatsAppService twilioWhatsAppService)
        {
            //twilioWhatsAppService.SendMessage("heyu", "41793454087");
            return Ok();
        }
    }
}
