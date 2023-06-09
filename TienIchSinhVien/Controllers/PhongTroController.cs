﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TienIchSinhVien.Models;

namespace TienIchSinhVien.Controllers
{
    public class PhongTroController : Controller
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
            var phongTro = db.PhongTro;
            var upphongtro = phongTro.Where(p => p.TrangThai == 1).ToList();
            var loginuser = User.Identity.GetUserId();
            foreach(PhongTro i in upphongtro)
            {
                YeuThich yeuThich = db.YeuThich.FirstOrDefault(p => p.UserId == loginuser && p.PostId == i.IdPhongTro && p.NamePost == "phongtro");
                if (yeuThich != null) 
                i.isShowYeuThich = true;
            }
            return View(upphongtro.ToPagedList(page.Value, pageSize));
        }

        // GET: PhongTro/Details/5
        public ActionResult Details(int id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhongTro phongTro = db.PhongTro.Find(id);

            var user = UserManager.FindById(phongTro.IdUser);
            Detailphongtroviewmodel detail = new Detailphongtroviewmodel();
            detail.PhongTro = phongTro;
            detail.Anh = user.Anh;
            detail.Name = user.Name;
            detail.UserId = phongTro.IdUser;

            if (phongTro == null)
            {
                return HttpNotFound();
            }
            return View(detail);
        }

        // GET: PhongTro/Create
        [Authorize]
        public ActionResult Create()
        {

            return View();
        }

        // POST: PhongTro/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPhongTro,TieuDe,Gia,DiaChi,DienTich,MoTa")] PhongTro phongTro, HttpPostedFileBase anh)
        {
            if (ModelState.IsValid)
            {
                if (anh != null && anh.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(anh.FileName);
                    string path = Path.Combine(Server.MapPath("~/Content/Images"), fileName);
                    anh.SaveAs(path);

                    phongTro.AnhMinhHoa = "/Content/Images/" + fileName;
                }

                DateTime now = DateTime.Now;
                phongTro.IdUser = User.Identity.GetUserId();
                phongTro.NgayDang = now;
                phongTro.TrangThai = 0;
                var user = UserManager.FindById(phongTro.IdUser);
                phongTro.PhoneNumber = user.PhoneNumber;

                db.PhongTro.Add(phongTro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(phongTro);
        }

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
        [Authorize]
        public ActionResult Edit([Bind(Include = "IdPhongTro,TieuDe,Gia,DiaChi,DienTich,MoTa")] PhongTro phongTro, HttpPostedFileBase anh)
        {
            if (ModelState.IsValid)
            {

                if (anh != null && anh.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(anh.FileName);
                    string path = Path.Combine(Server.MapPath("~/Content/Images"), fileName);
                    anh.SaveAs(path);

                    phongTro.AnhMinhHoa = "/Content/Images/" + fileName;
                }
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
        public ActionResult HidenPost(int id)
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
        [HttpPost, ActionName("HidenPost")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult HidenPostConfirmed(int id)
        {
            PhongTro phongTro = db.PhongTro.Find(id);
            if (phongTro.TrangThai == 3)
            {
                phongTro.TrangThai = 0;

            }
            else
            {
                phongTro.TrangThai = 3;
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
