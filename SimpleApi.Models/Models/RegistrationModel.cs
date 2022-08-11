using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleApi.Models.Models
{
    public class RegistrationModel
    {
        [Key]
        public int Id { get; set; }
        public string  FirtName { get; set; }
        public string SecondName { get; set; }
        public int Age { get; set; }
        [EmailAddress]
        public string  EMail { get; set; }
        public string  PhoneNumber { get; set; }
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; }

    }
}
