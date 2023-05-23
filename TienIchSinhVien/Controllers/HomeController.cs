using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TienIchSinhVien.Models;

namespace TienIchSinhVien.Controllers
{
    public class HomeController : Controller
    {
        TienIchSinhVienDb db = new TienIchSinhVienDb();
        public ActionResult Index()
        {
            return View();
           
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult AddFavorite(int postId)
        {
            // Lấy thông tin người dùng hiện tại (có thể làm theo cách phù hợp với ứng dụng của bạn)
            string currentUserId = User.Identity.GetUserId();

            // Kiểm tra xem bài viết đã được yêu thích hay chưa
            bool isAlreadyFavorited = db.YeuThich.Any(f => f.PostId == postId && f.UserId == currentUserId);

            if (!isAlreadyFavorited)
            {
                // Tạo một bản ghi yêu thích mới
                var favorite = new YeuThich();
                {
                    favorite.PostId = postId;
                    favorite.UserId = currentUserId;
                   
                };

                // Lưu bản ghi yêu thích vào cơ sở dữ liệu
                db.YeuThich.Add(favorite);
                db.SaveChanges();
            }

            // Redirect hoặc thực hiện các xử lý khác sau khi thêm yêu thích

            return RedirectToAction("Index", "Post");
        }
    }
   
}