using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace WebAPI.Twilioo
{
    public class TwilioWhatsAppService
    {
        private IOptions<TwilioOptions> options;
        public TwilioWhatsAppService(IOptions<TwilioOptions> options)
        {
            this.options = options;
            TwilioClient.Init(options.Value.AccountSid, options.Value.AuthToken);
        }
        public void SendMessage(string message, string number)
        {
            var v = MessageResource.Create(
            body: message,
            from: new Twilio.Types.PhoneNumber("whatsapp:+14155238886"),
            to: new Twilio.Types.PhoneNumber($"whatsapp:+{number}"));
        }
    }
}
