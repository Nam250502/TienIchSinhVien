using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TienIchSinhVien.Models;

namespace TienIchSinhVien.Areas.Admin.Controllers
{
    public class RaoVatADController : Controller
    {
        private TienIchSinhVienDb db = new TienIchSinhVienDb();

        // GET: Admin/RaoVatAD
        public ActionResult Index()
        {
            return View(db.RaoVat.Where(p => p.TrangThai == 0).ToList());
        }

        // GET: Admin/RaoVatAD/Details/5
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

        // GET: Admin/RaoVatAD/Create
        public ActionResult Create()
        {
            ViewBag.LoaiHang = new SelectList(db.LoaiHang, "IdLoaihang", "TenLoaiHang");
            return View();
        }

        // POST: Admin/RaoVatAD/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdBaiRao,IdUser,TieuDe,AnhMinhHoa,Gia,DiaChi,NgayDang,MoTa,TrangThai,PhoneNumber,LoaiHang")] RaoVat raoVat)
        {
            if (ModelState.IsValid)
            {
                db.RaoVat.Add(raoVat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LoaiHang = new SelectList(db.LoaiHang, "IdLoaihang", "TenLoaiHang", raoVat.LoaiHang);
            return View(raoVat);
        }

        // GET: Admin/RaoVatAD/Edit/5
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

        // POST: Admin/RaoVatAD/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdBaiRao,IdUser,TieuDe,AnhMinhHoa,Gia,DiaChi,NgayDang,MoTa,TrangThai,PhoneNumber,LoaiHang")] RaoVat raoVat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(raoVat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LoaiHang = new SelectList(db.LoaiHang, "IdLoaihang", "TenLoaiHang", raoVat.LoaiHang);
            return View(raoVat);
        }

        // GET: Admin/RaoVatAD/Delete/5
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

        // POST: Admin/RaoVatAD/Delete/5
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
        public void DuyetBai(int id, int value)
        {

            RaoVat raovat = db.RaoVat.Find(id);
            if (value == 1 || value == 2)
            {
                raovat.TrangThai = value;
                db.SaveChanges();
            }

            return;
        }
    }
}
