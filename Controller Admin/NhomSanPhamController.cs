using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop_thien_thanh.Models;
namespace Shop_thien_thanh.Areas.Admin.Controllers
{
    public class NhomSanPhamController : Controller
    {
        FASHIONMODELDataContext db = new FASHIONMODELDataContext();
        [NonAction]
        bool KiemTraDaDangNhap()
        {
            return Session["tendangnhap"] != null;
        }
        // GET: Admin/NhomSanPham
        public ActionResult Index()
        {
            if(!KiemTraDaDangNhap())
            {
               return Content("<script> alert('Bạn chưa đăng nhập!');  window.location.replace('" + Url.Action("Index", "Home", new { area = "" }) + "'); </script>");
            }
            if (Session["phanquyen"].ToString() != "Admin")
            {
                return Content("<script> alert('Bạn không có quyền đăng nhập!');  window.location.replace('" + Url.Action("Index", "Home", new { area = "" }) + "'); </script>");

            }
            return View(db.Nhomsps.ToList());
        }

        // GET: Admin/NhomSanPham/Create
        public ActionResult Create()
        {
            if (!KiemTraDaDangNhap())
            {
                return Content("<script> alert('Bạn chưa đăng nhập!');  window.location.replace('" + Url.Action("Index", "Home", new { area = "" }) + "'); </script>");
            }
            if (Session["phanquyen"].ToString() != "Admin")
            {
                return Content("<script> alert('Bạn không có quyền đăng nhập!');  window.location.replace('" + Url.Action("Index", "Home", new { area = "" }) + "'); </script>");

            }

            ViewBag.PhanLoaiID = new SelectList(db.PhanLoais.ToList(), "PhanLoaiID", "TenPhanLoai");

            return View();
        }

        // POST: Admin/NhomSanPham/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude ="BiDanh")] Nhomsp model)
        {
            try
            {
                if (!KiemTraDaDangNhap())
                {
                    return Content("<script> alert('Bạn chưa đăng nhập!');  window.location.replace('" + Url.Action("Index", "Home", new { area = "" }) + "'); </script>");
                }
                if (Session["phanquyen"].ToString() != "Admin")
                {
                    return Content("<script> alert('Bạn không có quyền đăng nhập!');  window.location.replace('" + Url.Action("Index", "Home", new { area = "" }) + "'); </script>");

                }
                if(ModelState.IsValid)
                {
                    model.BiDanh = XuLyDuLieu.LoaiBoDauTiengViet(model.TenNhomsp);
                    db.Nhomsps.InsertOnSubmit(model);
                    db.SubmitChanges();
                    return RedirectToAction("Index");
                } 
                else
                {
                    ViewBag.PhanLoaiID = new SelectList(db.PhanLoais.ToList(), "PhanLoaiID", "TenPhanLoai");
                    return View(model);
                }

              
            }
            catch
            {
                ModelState.AddModelError("", "Xảy ra lỗi khi thêm");
                ViewBag.PhanLoaiID = new SelectList(db.PhanLoais.ToList(), "PhanLoaiID", "TenPhanLoai");
                return View(model);
            }
        }

        // GET: Admin/NhomSanPham/Edit/5
        public ActionResult Edit(int id)
        {
            if (!KiemTraDaDangNhap())
            {
                return Content("<script> alert('Bạn chưa đăng nhập!');  window.location.replace('" + Url.Action("Index", "Home", new { area = "" }) + "'); </script>");
            }
            if (Session["phanquyen"].ToString() != "Admin")
            {
                return Content("<script> alert('Bạn không có quyền đăng nhập!');  window.location.replace('" + Url.Action("Index", "Home", new { area = "" }) + "'); </script>");

            }
            Nhomsp model = db.Nhomsps.SingleOrDefault(m => m.NhomspID == id);
            if (model == null)
            {
                return HttpNotFound();
            }
            ViewBag.PhanLoaiID = new SelectList(db.PhanLoais.ToList(), "PhanLoaiID", "TenPhanLoai", model.PhanLoaiID);
            return View(model);
        }

        // POST: Admin/NhomSanPham/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Nhomsp temp)
        {
            try
            {
                if (!KiemTraDaDangNhap())
                {
                    return Content("<script> alert('Bạn chưa đăng nhập!');  window.location.replace('" + Url.Action("Index", "Home", new { area = "" }) + "'); </script>");
                }
                if (Session["phanquyen"].ToString() != "Admin")
                {
                    return Content("<script> alert('Bạn không có quyền đăng nhập!');  window.location.replace('" + Url.Action("Index", "Home", new { area = "" }) + "'); </script>");

                }
                if(ModelState.IsValid)
                {
                    temp.BiDanh = XuLyDuLieu.LoaiBoDauTiengViet(temp.TenNhomsp);
                    Nhomsp model = db.Nhomsps.Single(m => m.NhomspID == temp.NhomspID);
                    model.TenNhomsp = temp.TenNhomsp;
                    model.BiDanh = temp.BiDanh;
                    model.PhanLoaiID = temp.PhanLoaiID;        
                    db.SubmitChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.PhanLoaiID = new SelectList(db.PhanLoais.ToList(), "PhanLoaiID", "TenPhanLoai", temp.PhanLoaiID);
                    return View(temp);
                }

               
            }
            catch
            {
                ViewBag.PhanLoaiID = new SelectList(db.PhanLoais.ToList(), "PhanLoaiID", "TenPhanLoai", temp.PhanLoaiID);
                ModelState.AddModelError("", "Xảy ra lỗi khi sửa.");
                return View(temp);
            }
        }

        // GET: Admin/NhomSanPham/Delete/5
        public ActionResult Delete(int id)
        {
            if (!KiemTraDaDangNhap())
            {
                return Content("<script> alert('Bạn chưa đăng nhập!');  window.location.replace('" + Url.Action("Index", "Home", new { area = "" }) + "'); </script>");
            }
            if (Session["phanquyen"].ToString() != "Admin")
            {
                return Content("<script> alert('Bạn không có quyền đăng nhập!');  window.location.replace('" + Url.Action("Index", "Home", new { area = "" }) + "'); </script>");

            }
            Nhomsp model = db.Nhomsps.SingleOrDefault(m => m.NhomspID == id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: Admin/NhomSanPham/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                if (!KiemTraDaDangNhap())
                {
                    return Content("<script> alert('Bạn chưa đăng nhập!');  window.location.replace('" + Url.Action("Index", "Home", new { area = "" }) + "'); </script>");
                }
                if (Session["phanquyen"].ToString() != "Admin")
                {
                    return Content("<script> alert('Bạn không có quyền đăng nhập!');  window.location.replace('" + Url.Action("Index", "Home", new { area = "" }) + "'); </script>");

                }
                Nhomsp model = db.Nhomsps.Single(m => m.NhomspID == id);
                db.Nhomsps.DeleteOnSubmit(model);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Xảy ra lỗi khi xóa");
                return View();
            }
        }
    }
}
