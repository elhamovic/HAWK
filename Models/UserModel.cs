using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HAWK_v.Models
{
    public class UserModel
    {
       
       public int Id { get; set; }
        [Required]
        [DisplayName("Username:")]
        public string UserName { get; set; }
        [DisplayName("Password:")]
        public string Password { get; set; }
        [DisplayName("Upload Face Image:")]
        public string Image { get; set; }
    }
}
