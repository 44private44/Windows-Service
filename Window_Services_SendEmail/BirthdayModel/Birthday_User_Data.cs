using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Window_Services_SendEmail.BirthdayModel
{
    public class Birthday_User_Data
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = String.Empty;
        public DateTime Date { get; set; } = DateTime.MinValue; 

    }
}
