using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TienIchSinhVien.Models;

namespace TienIchSinhVien.Controllers
{
    public class YeuThichController : Controller
    {
        // GET: YeuThich
        TienIchSinhVienDb db = new TienIchSinhVienDb();
        public ActionResult Index()
        {


            return View();
        }


        public ActionResult yeuthichphong()
        {
            string iduser = User.Identity.GetUserId();
            var listyeuthichtro= db.YeuThich.Where(p=>p.UserId == iduser&& p.NamePost=="phongtro").ToList();
           List<PhongTro> ytphongtro = new List<PhongTro>();
            foreach(YeuThich item in listyeuthichtro)
            {
                PhongTro temp = db.PhongTro.FirstOrDefault(p => p.IdPhongTro == item.PostId);
                ytphongtro.Add(temp);
            }
            
            return View(ytphongtro);
        }
        public ActionResult yeuthichvieclam()
        {
            string iduser = User.Identity.GetUserId();
            var listyeuthichvieclam = db.YeuThich.Where(p => p.UserId == iduser && p.NamePost == "vieclam").ToList();
            List<ViecLam> ytvieclam = new List<ViecLam>();
            foreach (YeuThich item in listyeuthichvieclam)
            {
                ViecLam temp = db.ViecLam.FirstOrDefault(p => p.IdViecLam == item.PostId);
                ytvieclam.Add(temp);
            }

            return View(ytvieclam);
        }
        public ActionResult yeuthichraovat()
        {
            string iduser = User.Identity.GetUserId();
            var listyeuthichraovat = db.YeuThich.Where(p => p.UserId == iduser && p.NamePost == "raovat").ToList();
            List<RaoVat> ytraovat = new List<RaoVat>();
            foreach (YeuThich item in listyeuthichraovat)
            {
                RaoVat temp = db.RaoVat.FirstOrDefault(p => p.IdBaiRao == item.PostId);
                ytraovat.Add(temp);
            }

            return View(ytraovat);
        }

        public void Yeuthich(string namepost, int id)
        {

            YeuThich model = new YeuThich();
            model.UserId = User.Identity.GetUserId();

            if (db.YeuThich.Where(p => p.UserId == model.UserId && p.PostId == id).Count() == 0)
            {
                model.NamePost = namepost;
                model.PostId = id;
                db.YeuThich.Add(model);
                db.SaveChanges();


            }
            else
            {
                YeuThich a = db.YeuThich.FirstOrDefault(p => p.UserId == model.UserId && p.PostId == id);
                db.YeuThich.Remove(a);
                db.SaveChanges();

            }
            return;
        }
    }
}