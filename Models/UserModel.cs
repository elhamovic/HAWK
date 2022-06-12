using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HAWK_v.Models
{
    /// <summary>
    /// This Class Represent the UserModel attributes
    /// </summary>
    public class UserModel
    {
       public int Id { get; set; }
        [Required]
        [DisplayName("Username:")]
        public string UserName { get; set; }
        [Required]
        [DisplayName("Password:")]
        public string Password { get; set; }
        [Required]
        [DisplayName("Upload Face Image:")]
        public IFormFile Image { get; set; }
        public string ImageData { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
        public int Dno { get; set; }
        public string Email { get; set; }
    }
}
