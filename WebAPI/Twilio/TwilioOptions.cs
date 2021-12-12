using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Twilioo
{
    public class TwilioOptions
    {
        public string AccountSid { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
        public string AuthToken { get; set; }
    }
}
