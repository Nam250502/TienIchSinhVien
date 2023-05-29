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
            var upraovat = raoVat.Where(p => p.TrangThai == 1).ToList();
            var loginuser = User.Identity.GetUserId();
            foreach (RaoVat i in upraovat)
            {
                YeuThich yeuThich = db.YeuThich.FirstOrDefault(p => p.UserId == loginuser && p.PostId == i.IdBaiRao && p.NamePost == "raovat");
                if (yeuThich != null)
                    i.isShowYeuThich = true;
            }
            return View(upraovat.ToPagedList(page.Value, pageSize));
           // return View(raoVat.Where(p => p.TrangThai == 1).ToList().ToPagedList(page.Value, pageSize));
        }
        public ActionResult Details(int? id)
        {
      

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RaoVat raovat = db.RaoVat.Find(id);

            var user = UserManager.FindById(raovat.IdUser);
            Detailraovatviewmodel detail = new Detailraovatviewmodel();
            detail.RaoVat = raovat;
            detail.Anh = user.Anh;
            detail.Name = user.Name;
            detail.UserId = raovat.IdUser;

            if (raovat == null)
            {
                return HttpNotFound();
            }
            return View(detail);
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
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdBaiRao,TieuDe,AnhMinhHoa,Gia,DiaChi,NgayDang,MoTa,TrangThai,PhoneNumber,LoaiHang")] RaoVat raoVat, HttpPostedFileBase anh)
        {
            if (ModelState.IsValid)
            {
                if (anh != null && anh.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(anh.FileName);
                    string path = Path.Combine(Server.MapPath("~/Content/Images"), fileName);
                    anh.SaveAs(path);

                    raoVat.AnhMinhHoa = "/Content/Images/" + fileName;
                }

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
        [Authorize]
        public ActionResult Edit([Bind(Include = "IdBaiRao,TieuDe,AnhMinhHoa,Gia,DiaChi,NgayDang,MoTa,TrangThai,PhoneNumber,LoaiHang")] RaoVat raoVat, HttpPostedFileBase anh)
        {
            if (ModelState.IsValid)
            {
                if (anh != null && anh.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(anh.FileName);
                    string path = Path.Combine(Server.MapPath("~/Content/Images"), fileName);
                    anh.SaveAs(path);

                    raoVat.AnhMinhHoa = "/Content/Images/" + fileName;
                }
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
        public ActionResult HidenPost(int? id)
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
        [HttpPost, ActionName("HidenPost")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult HidenPostConfirmed(int id)
        {
            RaoVat raoVat = db.RaoVat.Find(id);
            if (raoVat.TrangThai == 3)
            {
                raoVat.TrangThai = 0;
            }
            else
            {
                raoVat.TrangThai = 3;
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
