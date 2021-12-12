using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.User
{
    public class SaveUserPhoneNumberDTO
    {
        public bool ReceivePushNotifications { get; set; }
        public string MobileNumber { get; set; }
    }
}
