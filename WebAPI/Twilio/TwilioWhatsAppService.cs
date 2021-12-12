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
            TwilioClient.Init(options.Value.AccountSid, "");
        }
        public void SendMessage()
        {
            MessageResource.Create(
            body: "Hello there!",
            from: new Twilio.Types.PhoneNumber("whatsapp:+14155238886"),
            to: new Twilio.Types.PhoneNumber("whatsapp:+15005550006"));
        }
    }
}
