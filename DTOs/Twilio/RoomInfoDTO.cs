using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Twilio
{
    public class RoomInfoDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Participants { get; set; }
    }
}
