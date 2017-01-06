using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop_thien_thanh.Models;
using PagedList.Mvc;
using PagedList;
using System.IO;
namespace Shop_thien_thanh.Areas.Admin.Controllers
{
    public class SanPhamController : Controller
    {
        FASHIONMODELDataContext db = new FASHIONMODELDataContext();
        // GET: Admin/SanPham
        [NonAction]
        bool KiemTraDaDangNhap()
        {
            return Session["tendangnhap"] != null;
        }
       
        public ActionResult Index(int? page)
        {
            if (!KiemTraDaDangNhap())
            {
                return Content("<script> alert('Bạn chưa đăng nhập!');  window.location.replace('" + Url.Action("Index", "Home", new { area = "" }) + "'); </script>");
            }
            if (Session["phanquyen"].ToString() != "Admin")
            {
                return Content("<script> alert('Bạn không có quyền đăng nhập!');  window.location.replace('" + Url.Action("Index", "Home", new { area = "" }) + "'); </script>");

            }
            return View(db.SanPhams.AsEnumerable().OrderBy(m=>m.SanPhamID).ToPagedList(page ?? 1, 10));
        }

        // GET: Admin/SanPham/Details/5
        public ActionResult Details(int id, int? page)
        {
            SanPham model = db.SanPhams.SingleOrDefault(m => m.SanPhamID == id);
            if(model == null)
            {
                return HttpNotFound();
            }
            
            return View(model);
        }

        // GET: Admin/SanPham/Create
        public ActionResult Create(int? page)
        {
            if (!KiemTraDaDangNhap())
            {
                return Content("<script> alert('Bạn chưa đăng nhập!');  window.location.replace('" + Url.Action("Index", "Home", new { area = "" }) + "'); </script>");
            }
            if (Session["phanquyen"].ToString() != "Admin")
            {
                return Content("<script> alert('Bạn không có quyền đăng nhập!');  window.location.replace('" + Url.Action("Index", "Home", new { area = "" }) + "'); </script>");

            }
            ViewBag.NhomspID = new SelectList(db.Nhomsps.ToList(), "NhomspID", "TenNhomsp");
            return View();
        }

        // POST: Admin/SanPham/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude ="NgungBan,NgayCapNhat,BiDanh,Hinh")] SanPham model, HttpPostedFileBase file)
        {
            if (!KiemTraDaDangNhap())
            {
                return Content("<script> alert('Bạn chưa đăng nhập!');  window.location.replace('" + Url.Action("Index", "Home", new { area = "" }) + "'); </script>");
            }
            if (Session["phanquyen"].ToString() != "Admin")
            {
                return Content("<script> alert('Bạn không có quyền đăng nhập!');  window.location.replace('" + Url.Action("Index", "Home", new { area = "" }) + "'); </script>");

            }

            model.NgayCapNhat = DateTime.Now;
                model.NgungBan = false;
                model.BiDanh = XuLyDuLieu.LoaiBoDauTiengViet(model.TenSanPham);
                if(ModelState.IsValid)
                {
                    try
                    {
                        db.SanPhams.InsertOnSubmit(model);
                        db.SubmitChanges();

                        if(file != null && file.ContentLength > 0)
                        {
                            string extendFile = Path.GetExtension(file.FileName);
                            if(extendFile.ToLower() != ".jpg" && extendFile.ToLower() != ".png" && extendFile.ToLower() != ".jpeg")
                            {
                                ModelState.AddModelError("", "File hình ảnh có đuôi không hợp lệ. Đuôi hợp lệ là các đuôi sau: .jpg, .jpeg, .png");
                                ViewBag.NhomspID = new SelectList(db.Nhomsps.ToList(), "NhomspID", "TenNhom", model.NhomspID);
                                return View(model);
                            }
                            else
                            {
                                model.Hinh = model.SanPhamID.ToString() + extendFile;
                                file.SaveAs(Server.MapPath("~/Photos/Sanpham/" + model.Hinh));
                                db.SubmitChanges();
                            }
                        }
                    }
                    catch
                    {
                        ModelState.AddModelError("", "Xảy ra lỗi khi thêm");
                        ViewBag.NhomspID = new SelectList(db.Nhomsps.ToList(), "NhomspID", "TenNhomsp", model.NhomspID);
                        return View(model);
                    }
                    int page = (int)Math.Ceiling(db.SanPhams.Count() / 10.0);
                    return RedirectToAction("Index",new { page = page });
                }
                else
                {
                    ViewBag.NhomspID = new SelectList(db.Nhomsps.ToList(), "NhomspID", "TenNhomsp", model.NhomspID);
                    return View(model);
                }
         
        }

        // GET: Admin/SanPham/Edit/5
        public ActionResult Edit(int id, int? page)
        {
            if (!KiemTraDaDangNhap())
            {
                return Content("<script> alert('Bạn chưa đăng nhập!');  window.location.replace('" + Url.Action("Index", "Home", new { area = "" }) + "'); </script>");
            }
            if (Session["phanquyen"].ToString() != "Admin")
            {
                return Content("<script> alert('Bạn không có quyền đăng nhập!');  window.location.replace('" + Url.Action("Index", "Home", new { area = "" }) + "'); </script>");

            }
            SanPham model = db.SanPhams.SingleOrDefault(m => m.SanPhamID == id);
            if(model == null)
            {
                return HttpNotFound();
            }
            ViewBag.NhomspID = new SelectList(db.Nhomsps.ToList(), "NhomspID", "TenNhomsp", model.NhomspID);

            return View(model);
        }

        // POST: Admin/SanPham/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Exclude ="Hinh,NgayCapNhat,BiDanh")] SanPham temp, HttpPostedFileBase file, int? page)
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
                if (ModelState.IsValid)
                {
                    SanPham model = db.SanPhams.Single(m => m.SanPhamID == temp.SanPhamID);
                    model.TenSanPham = temp.TenSanPham;
                    model.NhomspID = temp.NhomspID;
                    model.DonGia = temp.DonGia;
                    model.GiaKM = temp.GiaKM;
                    model.BiDanh = XuLyDuLieu.LoaiBoDauTiengViet(temp.TenSanPham);
                    model.NgungBan = temp.NgungBan;
                    model.MoTa = temp.MoTa;
                    if(file != null && file.ContentLength > 0)
                    {
                        string extendFile = Path.GetExtension(file.FileName);
                        if (extendFile.ToLower() != ".jpg" && extendFile.ToLower() != ".png" && extendFile.ToLower() != ".jpeg")
                        {
                            ModelState.AddModelError("", "File hình ảnh có đuôi không hợp lệ. Đuôi hợp lệ là các đuôi sau: .jpg, .jpeg, .png");
                            ViewBag.NhomspID = new SelectList(db.Nhomsps.ToList(), "NhomspID", "TenNhom", temp.NhomspID);
                            return View(model);
                        }
                        else
                        {
                            file.SaveAs(Server.MapPath("~/Photos/Sanpham/" + model.Hinh));
                        }
                    }
                    db.SubmitChanges();
                }
                else
                {
                    ViewBag.NhomspID = new SelectList(db.Nhomsps.ToList(), "NhomspID", "TenNhom", temp.NhomspID);
                    return View(temp);
                }
                return RedirectToAction("Index",new { page = page ?? 1});
            }
            catch
            {
                ModelState.AddModelError("", "Xảy ra lỗi khi sửa");
                ViewBag.NhomspID = new SelectList(db.Nhomsps.ToList(), "NhomspID", "TenNhom", temp.NhomspID);
                return View(temp);
            }
        }

        // GET: Admin/SanPham/Delete/5
        public ActionResult Delete(int id, int? page)
        {
            if (!KiemTraDaDangNhap())
            {
                return Content("<script> alert('Bạn chưa đăng nhập!');  window.location.replace('" + Url.Action("Index", "Home", new { area = "" }) + "'); </script>");
            }
            if (Session["phanquyen"].ToString() != "Admin")
            {
                return Content("<script> alert('Bạn không có quyền đăng nhập!');  window.location.replace('" + Url.Action("Index", "Home", new { area = "" }) + "'); </script>");

            }
            SanPham model = db.SanPhams.SingleOrDefault(m => m.SanPhamID == id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: Admin/SanPham/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id, int? page)
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
                    SanPham model = db.SanPhams.Single(m => m.SanPhamID == id);
                    db.SanPhams.DeleteOnSubmit(model);
                    db.SubmitChanges();
                    return RedirectToAction("Index",new { page = page ?? 1});
            }
            catch
            {
                ModelState.AddModelError("", "Xảy ra lỗi khi xóa");
                return View();
            }
        }
    }
}
