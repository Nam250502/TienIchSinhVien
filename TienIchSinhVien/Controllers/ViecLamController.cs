using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
            return View(viecLam.Where(p => p.TrangThai == 1).ToList().ToPagedList(page.Value, pageSize));
        }

        // GET: ViecLam/Details/5
        public ActionResult Details(int id)
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

        // GET: ViecLam/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ViecLam/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdViecLam,IdUser,TieuDe,AnhMinhHoa,Luong,DiaChi,NgayDang,ViTriUngTuyen,MoTa,TrangThai")] ViecLam viecLam)
        {
            if (ModelState.IsValid)
            {
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
        public ActionResult Edit([Bind(Include = "IdViecLam,IdUser,TieuDe,AnhMinhHoa,Luong,DiaChi,NgayDang,ViTriUngTuyen,MoTa,TrangThai")] ViecLam viecLam)
        {
            if (ModelState.IsValid)
            {
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
        public ActionResult Delete(int id)
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ViecLam viecLam = db.ViecLam.Find(id);
            db.ViecLam.Remove(viecLam);
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
