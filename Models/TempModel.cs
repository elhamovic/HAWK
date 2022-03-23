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
        [DisplayName("Permission Start Date:")]
        public string PStartDate { get; set; }
        [DisplayName("Permission End Date:")]
        public string PEndDate { get; set; }

    }
}
