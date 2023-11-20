using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Core.Contracts.Models
{
    public class NotificationModel
    {
        public string FormGroupName { get; set; }
        public string Notification { get; set; }
        public bool App {  get; set; }  
        public bool Email { get; set; }
        public bool Sms { get; set; }
    }
}
