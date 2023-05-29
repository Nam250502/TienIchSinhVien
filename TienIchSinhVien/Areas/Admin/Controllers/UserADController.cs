using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using TienIchSinhVien.Models;
using TienIchSinhVien.Areas.Admin.model;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TienIchSinhVien.Areas.Admin.Controllers
{
    public class UserADController : Controller
    {
        private ApplicationUserManager _userManager;
        TienIchSinhVienDb db = new TienIchSinhVienDb();

        public UserADController(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }




        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set
            {
                _userManager = value;
            }
        }

        public ActionResult Index()
        {
            var users = _userManager.Users.ToList();

            return View(users);
        }
    }
}