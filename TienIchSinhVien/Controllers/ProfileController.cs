using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using TienIchSinhVien.Models;
using TienIchSinhVien;
using System.Drawing.Printing;
using System.Web.UI;
using System.Linq;
using System.Web.Helpers;
using System;
using System.IO;
using System.Collections.Generic;

namespace TienIchSinhVien.Controllers
{
    public class ProfileController : Controller

    {
  
        private ApplicationUserManager _userManager;
        TienIchSinhVienDb db = new TienIchSinhVienDb();

        public ProfileController()
        {
        }

        public ProfileController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
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

        // GET: Profile
        public ActionResult Index()
        {

            var userId = User.Identity.GetUserId();
            var user = UserManager.FindById(userId);
            var model = new ProfileViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Name = user.Name,
                Anh = user.Anh,

            };
           

            return View(model);
        }

        // POST: Profile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(ProfileViewModel model)
        {
            if (ModelState.IsValid)
            {

                var userId = User.Identity.GetUserId();
                var user = await UserManager.FindByIdAsync(userId);

                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                user.Name = model.Name;
                user.Anh=model.Anh;

                var result = await UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "An error occurred while updating your profile.");
            }
            return View(model);
        }
        public async Task<ActionResult> Edit([Bind(Include = "Name,Email,PhoneNumber")] ProfileViewModel model, HttpPostedFileBase anhavt)
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            if (user != null)
            {
                if (anhavt != null && anhavt.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(anhavt.FileName);
                    string path = Path.Combine(Server.MapPath("~/Content/Images"), fileName);
                    anhavt.SaveAs(path);

                    user.Anh = "/Content/Images/" + fileName;
                }
                // Cập nhật các thông tin mới của người dùng
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;

                user.Name =model.Name;
                

                var result = await UserManager.UpdateAsync(user);


                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    // Xử lý các lỗi nếu có
                }
            }

            return RedirectToAction("Index");
        }
        // lấy danh sách bài viết đã đăng của mình
        public ActionResult PTDaDang()
        {
            var userId = User.Identity.GetUserId();
            var PTdadang = db.PhongTro.Where(p => p.IdUser == userId);

            return View(PTdadang);
        }
        public ActionResult VLDaDang()
        {
            var userId = User.Identity.GetUserId();
            var VLdadang = db.ViecLam.Where(p => p.IdUser == userId);

            return View(VLdadang);
        }
        public ActionResult RaoVat()
        {
            var userId = User.Identity.GetUserId();
            var VLdadang = db.RaoVat.Where(p => p.IdUser == userId);

            return View(VLdadang);
        }
        public ActionResult detaiprofile(string id)
        {
         

            var user = UserManager.FindById(id);
            var model = new ProfileViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Name = user.Name,
                Anh = user.Anh,
                UserId = user.Id,
                

            };
            loadDanhGia(id);
            return View(model);
        }

        public ActionResult loadDanhGia(string id)
        {
            ViewBag.loaduser = UserManager.FindById(id);
            List<DanhGiaUser> model = db.DanhGiaUser.Where(p=>p.UserId2 == id).ToList();
            
            return View(model);
            
        }

   
        public void DanhGia(string DanhGia, string UserId2)
        {

            
            DanhGiaUser model = new DanhGiaUser();
            
            model.UserId1 = User.Identity.GetUserId();
            var a = UserManager.FindById(model.UserId1);
            if(db.DanhGiaUser.Where(p=>p.UserId1==a.Id && p.UserId2 == UserId2).Count() == 0)
            {
                model.Anh = a.Anh;
                model.Name = a.Name;
                model.UserId2 = UserId2;
                model.DanhGia = DanhGia;

                db.DanhGiaUser.Add(model);
                db.SaveChanges();

            }
            else
            {
                ViewBag.eror = "da danh gia";
            }
            return ;
        
        }
       



    }
}
