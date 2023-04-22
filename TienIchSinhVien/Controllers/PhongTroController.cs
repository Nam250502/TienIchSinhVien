using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TienIchSinhVien.Models;

namespace TienIchSinhVien.Controllers
{
    public class PhongTroController : Controller
    {
    //    private TienIchSinhVienDb db = new TienIchSinhVienDb();

    //    GET: PhongTro
    //    public ActionResult Index()
    //    {
    //        return View(db.PhongTro.ToList());
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


    public ActionResult Index(int? page)
        {

            if (page == null) page = 1;
            int pageSize = 12;
            var phongTro = db.PhongTro;
            return View(phongTro.Where(p=>p.TrangThai==1).ToList().ToPagedList(page.Value, pageSize));
        }

        // GET: PhongTro/Details/5
        public ActionResult Details(int id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhongTro phongTro = db.PhongTro.Find(id);
            
            if (phongTro == null)
            {
                return HttpNotFound();
            }
            return View(phongTro);
        }

        // GET: PhongTro/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: PhongTro/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPhongTro,IdUser,TieuDe,AnhMinhHoa,Gia,DiaChi,DienTich,MoTa,TrangThai")] PhongTro phongTro)
        {
            if (ModelState.IsValid)
            {
                DateTime now = DateTime.Now;

                phongTro.NgayDang = now;
                phongTro.IdUser = User.Identity.GetUserId();
                phongTro.TrangThai = 0;
                var user = UserManager.FindById(phongTro.IdUser);
                phongTro.PhoneNumber = user.PhoneNumber;

                db.PhongTro.Add(phongTro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(phongTro);
        }

        // GET: PhongTro/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhongTro phongTro = db.PhongTro.Find(id);
            if (phongTro == null)
            {
                return HttpNotFound();
            }
            return View(phongTro);
        }

        // POST: PhongTro/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPhongTro,IdUser,TieuDe,AnhMinhHoa,Gia,DiaChi,NgayDang,DienTich,MoTa,TrangThai")] PhongTro phongTro)
        {
            if (ModelState.IsValid)
            {
                DateTime now = DateTime.Now;

                phongTro.NgayDang = now;
                phongTro.IdUser = User.Identity.GetUserId();
                phongTro.TrangThai = 0;
                var user = UserManager.FindById(phongTro.IdUser);
                phongTro.PhoneNumber = user.PhoneNumber;
                db.Entry(phongTro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(phongTro);
        }

        //GET: PhongTro/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhongTro phongTro = db.PhongTro.Find(id);
            if (phongTro == null)
            {
                return HttpNotFound();
            }
            return View(phongTro);
        }

        // POST: PhongTro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PhongTro phongTro = db.PhongTro.Find(id);
            db.PhongTro.Remove(phongTro);
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
