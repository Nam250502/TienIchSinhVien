using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace TienIchSinhVien.Models
{
    public class ProfileViewModel
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }
     
        public string Name { get; set; }
        public string Anh { get; set; }
    }
}