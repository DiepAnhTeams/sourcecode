using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop_thien_thanh.Models;
namespace Shop_thien_thanh.Areas.Admin.Controllers
{
 
    public class PhanLoaiController : Controller
    {

        FASHIONMODELDataContext db = new FASHIONMODELDataContext();
        
        [NonAction]
        bool KiemTraDaDangNhap()
        {
            return Session["tendangnhap"] != null;
        }

        // GET: Admin/PhanLoai
        public ActionResult Index()
        {
            if(KiemTraDaDangNhap() == false)
            {
                return Content("<script> alert('Bạn chưa đăng nhập!');  window.location.replace('" + Url.Action("Index", "Home", new { area = "" }) + "'); </script>");

            }
            if (Session["phanquyen"].ToString() != "Admin")
            {
                return Content("<script> alert('Bạn không có quyền đăng nhập!');  window.location.replace('" + Url.Action("Index", "Home", new { area = "" }) + "'); </script>");

            }
            return View(db.PhanLoais.ToList());
        }


        // GET: Admin/PhanLoai/Create
        public ActionResult Create()
        {
            if (KiemTraDaDangNhap() == false)
            {
                return Content("<script> alert('Bạn chưa đăng nhập!');  window.location.replace('" + Url.Action("Index", "Home", new { area=""}) + "'); </script>");

            }
            if (Session["phanquyen"].ToString() != "Admin")
            {
                return Content("<script> alert('Bạn không có quyền đăng nhập!');  window.location.replace('" + Url.Action("Index", "Home", new { area = "" }) + "'); </script>");

            }
            return View();
        }

        // POST: Admin/PhanLoai/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TenPhanLoai")]  PhanLoai model)
        {
            try
            {
                if (KiemTraDaDangNhap() == false)
                {
                    return Content("<script> alert('Bạn chưa đăng nhập!');  window.location.replace('" + Url.Action("Index", "Home", new { area=""}) + "'); </script>");
                }
                if (Session["phanquyen"].ToString() != "Admin")
                {
                    return Content("<script> alert('Bạn không có quyền đăng nhập!');  window.location.replace('" + Url.Action("Index", "Home", new { area = "" }) + "'); </script>");
                }
                if(db.PhanLoais.FirstOrDefault(m=>m.TenPhanLoai == model.TenPhanLoai) != null)
                {
                    ModelState.AddModelError("", "Tên phân loại bị trùng! Vui lòng nhập tên khác.");
                }
                if (ModelState.IsValid)
                {
                   
                    model.BiDanh = XuLyDuLieu.LoaiBoDauTiengViet(model.TenPhanLoai);
                    db.PhanLoais.InsertOnSubmit(model);
                    db.SubmitChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(model);
                }
            }
            catch
            {
                ModelState.AddModelError("", "Xảy ra lỗi khi thêm.");

                return View(model);
            }
        }

        // GET: Admin/PhanLoai/Edit/5
        public ActionResult Edit(int id)
        {
            if (KiemTraDaDangNhap() == false)
            {
                return Content("<script> alert('Bạn chưa đăng nhập!');  window.location.replace('" + Url.Action("Index", "Home", new { area = "" }) + "'); </script>");
            }
            if (Session["phanquyen"].ToString() != "Admin")
            {
                return Content("<script> alert('Bạn không có quyền đăng nhập!');  window.location.replace('" + Url.Action("Index", "Home", new { area = "" }) + "'); </script>");

            }
            PhanLoai model = db.PhanLoais.SingleOrDefault(m => m.PhanLoaiID == id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: Admin/PhanLoai/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PhanLoai temp, string tenPhanLoaiCu)
        {
            try
            {
                if (KiemTraDaDangNhap() == false)
                {
                    return Content("<script> alert('Bạn chưa đăng nhập!');  window.location.replace('" + Url.Action("Index", "Home", new { area = "" }) + "'); </script>");
                }
                if (Session["phanquyen"].ToString() != "Admin")
                {
                    return Content("<script> alert('Bạn không có quyền đăng nhập!');  window.location.replace('" + Url.Action("Index", "Home", new { area = "" }) + "'); </script>");

                }
                if (temp.TenPhanLoai != tenPhanLoaiCu && db.PhanLoais.FirstOrDefault(m => m.TenPhanLoai == temp.TenPhanLoai) != null)
                {
                    ModelState.AddModelError("", "Tên phân loại bị trùng! Vui lòng nhập tên khác.");
                }
                if (ModelState.IsValid)
                {

                    temp.BiDanh = XuLyDuLieu.LoaiBoDauTiengViet(temp.TenPhanLoai);

                    PhanLoai phanLoaiSua = db.PhanLoais.Single(m => m.PhanLoaiID == temp.PhanLoaiID);
                    phanLoaiSua.TenPhanLoai = temp.TenPhanLoai;
                    phanLoaiSua.BiDanh = temp.BiDanh;
                   
                    db.SubmitChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Xảy ra lỗi khi sửa");
                    return View(temp);
                }

            }
            catch
            {
                ModelState.AddModelError("", "Xảy ra lỗi khi sửa");
                return View(temp);
            }
        }

        // GET: Admin/PhanLoai/Delete/5
        public ActionResult Delete(int id)
        {
            if (KiemTraDaDangNhap() == false)
            {
                return Content("<script> alert('Bạn chưa đăng nhập!');  window.location.replace('" + Url.Action("Index", "Home", new { area = "" }) + "'); </script>");
            }
            if (Session["phanquyen"].ToString() != "Admin")
            {
                return Content("<script> alert('Bạn không có quyền đăng nhập!');  window.location.replace('" + Url.Action("Index", "Home", new { area = "" }) + "'); </script>");

            }
            PhanLoai model = db.PhanLoais.SingleOrDefault(m => m.PhanLoaiID == id);
            if(model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: Admin/PhanLoai/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection collection)
        {
            if (KiemTraDaDangNhap() == false)
            {
                return Content("<script> alert('Bạn chưa đăng nhập!');  window.location.replace('" + Url.Action("Index", "Home", new { area = "" }) + "'); </script>");
            }
            if (Session["phanquyen"].ToString() != "Admin")
            {
                return Content("<script> alert('Bạn không có quyền đăng nhập!');  window.location.replace('" + Url.Action("Index", "Home", new { area = "" }) + "'); </script>");

            }
            try
            {
                // TODO: Add delete logic here
                PhanLoai model = db.PhanLoais.Single(m=>m.PhanLoaiID == id);
                db.PhanLoais.DeleteOnSubmit(model);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Xảy ra lỗi khi xoá");
                return View();
            }
        }
    }
}
