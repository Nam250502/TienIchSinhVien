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
    public class RaoVatController : Controller
    {
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
            var raoVat = db.RaoVat;
            return View(raoVat.Where(p => p.TrangThai == 1).ToList().ToPagedList(page.Value, pageSize));
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RaoVat raoVat = db.RaoVat.Find(id);
            if (raoVat == null)
            {
                return HttpNotFound();
            }
            return View(raoVat);
        }

        // GET: RaoVat/Create
        public ActionResult Create()
        {
           
            RaoVat raovat = new RaoVat();
            raovat.loaiHangs = db.LoaiHang.ToList();
            return View(raovat);
        }

        // POST: RaoVat/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdBaiRao,IdUser,TieuDe,AnhMinhHoa,Gia,DiaChi,NgayDang,MoTa,TrangThai,PhoneNumber,LoaiHang")] RaoVat raoVat)
        {
            if (ModelState.IsValid)
            {
                DateTime now = DateTime.Now;

                raoVat.NgayDang = now;
                raoVat.IdUser = User.Identity.GetUserId();
                raoVat.TrangThai = 0;
                var user = UserManager.FindById(raoVat.IdUser);
                raoVat.PhoneNumber = user.PhoneNumber;
                db.RaoVat.Add(raoVat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LoaiHang = new SelectList(db.LoaiHang, "IdLoaihang", "TenLoaiHang", raoVat.LoaiHang);
            return View(raoVat);
        }

        // GET: RaoVat/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RaoVat raoVat = db.RaoVat.Find(id);
            if (raoVat == null)
            {
                return HttpNotFound();
            }
            ViewBag.LoaiHang = new SelectList(db.LoaiHang, "IdLoaihang", "TenLoaiHang", raoVat.LoaiHang);
            return View(raoVat);
        }

        // POST: RaoVat/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdBaiRao,IdUser,TieuDe,AnhMinhHoa,Gia,DiaChi,NgayDang,MoTa,TrangThai,PhoneNumber,LoaiHang")] RaoVat raoVat)
        {
            if (ModelState.IsValid)
            {
                DateTime now = DateTime.Now;

                raoVat.NgayDang = now;
                raoVat.IdUser = User.Identity.GetUserId();
                raoVat.TrangThai = 0;
                var user = UserManager.FindById(raoVat.IdUser);
                raoVat.PhoneNumber = user.PhoneNumber;
                db.RaoVat.Add(raoVat);
                db.Entry(raoVat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LoaiHang = new SelectList(db.LoaiHang, "IdLoaihang", "TenLoaiHang", raoVat.LoaiHang);
            return View(raoVat);
        }

        // GET: RaoVat/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RaoVat raoVat = db.RaoVat.Find(id);
            if (raoVat == null)
            {
                return HttpNotFound();
            }
            return View(raoVat);
        }

        // POST: RaoVat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RaoVat raoVat = db.RaoVat.Find(id);
            db.RaoVat.Remove(raoVat);
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
