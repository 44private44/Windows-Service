using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Window_Services_SendEmail.DataModels
{
    public partial class BirthdayUser
    {
        public string UserName { get; set; }
        public DateTime? Birthdate { get; set; }
    }
}
