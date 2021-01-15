using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bookah.Models
{
    public class AppUser
    {
        [Key]

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string CellphoneNo { get; set; }
        public string ID_Number { get; set; }
        public string StudentNo { get; set; }
    }
}
