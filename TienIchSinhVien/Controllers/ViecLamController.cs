using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TienIchSinhVien.Models;

namespace TienIchSinhVien.Controllers
{
    public class ViecLamController : Controller
    {
        //    }
        private ApplicationUserManager _userManager;
        TienIchSinhVienDb db = new TienIchSinhVienDb();



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

        // GET: ViecLam
        public ActionResult Index(int? page)
        {

            if (page == null) page = 1;
            int pageSize = 12;
            var viecLam = db.ViecLam;
            var upvieclam = viecLam.Where(p => p.TrangThai == 1).ToList();
            var loginuser = User.Identity.GetUserId();
            foreach (ViecLam i in upvieclam)
            {
                YeuThich yeuThich = db.YeuThich.FirstOrDefault(p => p.UserId == loginuser && p.PostId == i.IdViecLam && p.NamePost == "vieclam");
                if (yeuThich != null)
                    i.isShowYeuThich = true;
            }
            return View(upvieclam.ToPagedList(page.Value, pageSize));
           
        }

        // GET: ViecLam/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViecLam viecLam = db.ViecLam.Find(id);

            var user = UserManager.FindById(viecLam.IdUser);
            Detailvieclamviewmodel detail = new Detailvieclamviewmodel();
            detail.ViecLam = viecLam;
            detail.Anh = user.Anh;
            detail.Name = user.Name;
            detail.UserId = viecLam.IdUser;

            if (viecLam == null)
            {
                return HttpNotFound();
            }
            return View(detail);
        }

        // GET: ViecLam/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ViecLam/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "IdViecLam,TieuDe,AnhMinhHoa,Luong,DiaChi,NgayDang,ViTriUngTuyen,MoTa,TrangThai")] ViecLam viecLam, HttpPostedFileBase anh)
        {
            if (ModelState.IsValid)
            {
                if (anh != null && anh.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(anh.FileName);
                    string path = Path.Combine(Server.MapPath("~/Content/Images"), fileName);
                    anh.SaveAs(path);

                    viecLam.AnhMinhHoa = "/Content/Images/" + fileName;
                }

                viecLam.IdUser = User.Identity.GetUserId();
                DateTime now = DateTime.Now;
                viecLam.NgayDang = now;
              
                viecLam.TrangThai = 0;
                var user = UserManager.FindById(viecLam.IdUser);
                viecLam.PhoneNumber = user.PhoneNumber;
                db.ViecLam.Add(viecLam);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(viecLam);
        }

        // GET: ViecLam/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViecLam viecLam = db.ViecLam.Find(id);
            if (viecLam == null)
            {
                return HttpNotFound();
            }
            return View(viecLam);
        }

        // POST: ViecLam/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "IdViecLam,TieuDe,AnhMinhHoa,Luong,DiaChi,NgayDang,ViTriUngTuyen,MoTa,TrangThai")] ViecLam viecLam, HttpPostedFileBase anh)
        {
            if (ModelState.IsValid)
            {
                if (anh != null && anh.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(anh.FileName);
                    string path = Path.Combine(Server.MapPath("~/Content/Images"), fileName);
                    anh.SaveAs(path);

                    viecLam.AnhMinhHoa = "/Content/Images/" + fileName;
                }
                viecLam.IdUser = User.Identity.GetUserId();
                DateTime now = DateTime.Now;
                viecLam.NgayDang = now;

                viecLam.TrangThai = 0;
                var user = UserManager.FindById(viecLam.IdUser);
                viecLam.PhoneNumber = user.PhoneNumber;
                db.Entry(viecLam).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(viecLam);
        }

        //GET: ViecLam/Delete/5
        [Authorize]
        public ActionResult HidenPost(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViecLam viecLam = db.ViecLam.Find(id);
            if (viecLam == null)
            {
                return HttpNotFound();
            }
            return View(viecLam);
        }

        // POST: ViecLam/Delete/5
        [HttpPost, ActionName("HidenPost")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult HidenPostConfirmed(int id)
        {
            ViecLam viecLam = db.ViecLam.Find(id);
            if (viecLam.TrangThai == 3)
            {
                viecLam.TrangThai = 0;
            }
            else
            {
                viecLam.TrangThai = 3;
            }
           
            db.SaveChanges();
            return RedirectToAction("Index");
        }


      
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
