using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop_thien_thanh.Models;
using Microsoft.AspNet.Identity;
namespace Shop_thien_thanh.Controllers
{
    public class KhachHangController : Controller
    {
        FASHIONMODELDataContext db = new FASHIONMODELDataContext();
        // GET: TaiKhoan
        public ActionResult DangKy(string message)
        {
            ViewBag.Message = message;
            return View();
        }

        // Cách 1
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult DangKy(FormCollection frm)
        //{
        //    KhachHang khachHangMoi = new KhachHang();

        //    if (frm["NhapLaiMatKhau"] != frm["MatKhau"])
        //    {
        //        ModelState.AddModelError("", "Nhập lại mật khẩu không khớp.");
        //        return View(frm);
        //    }
        //    if(frm["TenDangNhap"] == "")
        //    {
        //        ModelState.AddModelError("", "Bạn chưa nhập tên đăng nhập.");
        //        return View(frm);
        //    }
        //    if (frm["MatKhau"] == "")
        //    {
        //        ModelState.AddModelError("", "Bạn chưa nhập mật khẩu.");
        //        return View(frm);
        //    }
        //    if(frm["HoTen"] == "")
        //    {
        //        if (frm["HoTen"] == "")
        //        {
        //            ModelState.AddModelError("", "Bạn chưa nhập họ tên.");
        //            return View(frm);
        //        }
        //    }
        //    khachHangMoi.HoTen = frm["HoTen"];
        //    khachHangMoi.MatKhau = frm["MatKhau"];
        //    khachHangMoi.TenDangNhap = frm["TenDangNhap"];
        //    khachHangMoi.NgaySinh = DateTime.Parse(frm["NgaySinh"]);
        //    if(bool.Parse(frm["GioiTinh"]) == true)
        //    {
        //        khachHangMoi.GioiTinh = true;
        //    }
        //    else
        //    {
        //        khachHangMoi.GioiTinh = false;
        //    }
        //    khachHangMoi.Email = frm["Email"];
        //    khachHangMoi.DienThoai = frm["DienThoai"];

        //    db.KhachHangs.InsertOnSubmit(khachHangMoi);
        //    db.SubmitChanges();
        //    return RedirectToAction("DangKy", new { message = "Đăng ký thành công." });
        //}

        // Cách 2
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKy(KhachHang khachHangMoi, string NhapLaiMatKhau)
        {
            if(db.KhachHangs.FirstOrDefault(m=>m.TenDangNhap == khachHangMoi.TenDangNhap) != null)
            {
                ModelState.AddModelError("loitendangnhap", "Tên đăng nhập đã tồn tại.");

            }
            if (NhapLaiMatKhau != khachHangMoi.MatKhau)
            {
                ModelState.AddModelError("", "Nhập lại mật khẩu không khớp.");
            }
            
            if (ModelState.IsValid == false)
            {
                return View(khachHangMoi);
            }
            else
            {


                db.KhachHangs.InsertOnSubmit(khachHangMoi);
                db.SubmitChanges();
                return RedirectToAction("DangKyThanhCong");
            }
        }

        public ViewResult DangKyThanhCong()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangNhap(KhachHang kh)
        {
            KhachHang khachhangDangNhap = db.KhachHangs.FirstOrDefault(m => m.TenDangNhap == kh.TenDangNhap && m.MatKhau == kh.MatKhau);
            if (khachhangDangNhap != null)
            {
                // Đăng nhập thành công
                // Tạo ra 2 biến Session có name là tendangnhap, hoten 
                Session["tendangnhap"] = kh.TenDangNhap;
                Session["hoten"] = khachhangDangNhap.HoTen;
                Session["phanquyen"] = khachhangDangNhap.PhanQuyen;
                if (khachhangDangNhap.PhanQuyen == "Admin")
                {
                    
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
                else
                {

                    return Content("<script>  window.location.replace('" + Url.Action("Index", "Home") + "'); </script>");

                }
            }
            else
            {
                return Content("<script> alert('Sai tài khoản hoặc mật khẩu');  window.location.replace('" + Url.Action("Index", "Home") + "'); </script>");
            }
        }

        public ActionResult DangXuat()
        {
            Session["tendangnhap"] = null;
            Session["hoten"] = null;
            Session["phanquyen"] = null;
            return RedirectToAction("Index", "Home", new { area=""});
        }
            
    }
}