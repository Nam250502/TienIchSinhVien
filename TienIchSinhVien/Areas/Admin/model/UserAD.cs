using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TienIchSinhVien.Models;

namespace TienIchSinhVien.Areas.Admin.model
{
    public class UserAD : IdentityUser
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        int CoutPost = 0;
    }
}