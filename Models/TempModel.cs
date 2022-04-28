using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HAWK_v.Models
{
    public class TempModel
    { 
        public int Id { get; set; }
        [Required]
        public string PStartDate { get; set; }
        [Required]
        public string PEndDate { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [DisplayName("Upload Face Image:")]
        public IFormFile Image { get; set; }
        public string ImageData { get; set; }
        [Required]
        public int Dno { get; set; }
    }
}
