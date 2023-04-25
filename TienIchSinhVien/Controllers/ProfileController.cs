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
        public async Task<ActionResult> Edit(string id, string email, string phoneNumber, string Name,string UserName,string Anh)
        {
            var userId = User.Identity.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                // Cập nhật các thông tin mới của người dùng
                user.Email = email;
                user.PhoneNumber = phoneNumber;
                user.UserName=UserName;
                user.Name = Name;
                user.Anh = Anh;

                var result = await _userManager.UpdateAsync(user);

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
    }
}
