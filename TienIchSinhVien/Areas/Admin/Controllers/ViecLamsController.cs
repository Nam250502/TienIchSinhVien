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
    public class ViecLamsController : Controller
    {
        private TienIchSinhVienDb db = new TienIchSinhVienDb();

        // GET: Admin/ViecLams
        public ActionResult Index()
        {
            return View(db.ViecLam.Where(p => p.TrangThai == 0).ToList());
        }

        // GET: Admin/ViecLams/Details/5
        public ActionResult Details(int? id)
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

        // GET: Admin/ViecLams/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ViecLams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdViecLam,IdUser,TieuDe,AnhMinhHoa,Luong,DiaChi,NgayDang,ViTriUngTuyen,MoTa,TrangThai,TinhTrang")] ViecLam viecLam)
        {
            if (ModelState.IsValid)
            {
                db.ViecLam.Add(viecLam);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(viecLam);
        }

        // GET: Admin/ViecLams/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Admin/ViecLams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdViecLam,IdUser,TieuDe,AnhMinhHoa,Luong,DiaChi,NgayDang,ViTriUngTuyen,MoTa,TrangThai,TinhTrang")] ViecLam viecLam)
        {
            if (ModelState.IsValid)
            {
                db.Entry(viecLam).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(viecLam);
        }

        // GET: Admin/ViecLams/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Admin/ViecLams/Delete/5
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
        public void DuyetBai(int id, int value)
        {

            ViecLam ViecLam = db.ViecLam.Find(id);
            if (value == 1 || value == 2)
            {
                ViecLam.TrangThai = value;
                db.SaveChanges();
            }

            return;
        }
    }
}
