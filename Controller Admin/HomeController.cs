using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop_thien_thanh.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        [NonAction]
        bool KiemTraDaDangNhap()
        {
            return Session["tendangnhap"] != null;
        }
        // GET: Admin/Admin
        public ActionResult Index()
        {
            if (KiemTraDaDangNhap() == false)
            {
                return Content("<script> alert('Bạn chưa đăng nhập!');  window.location.replace('" + Url.Action("Index", "Home", new { area = "" }) + "'); </script>");
            }
            if (Session["phanquyen"].ToString() == "Admin")
            {
                return View();
            }
            else
            {
                return Content("<script> alert('Bạn không có quyền đăng nhập!');  window.location.replace('" + Url.Action("Index", "Home", new { area = "" }) + "'); </script>");
            }
          
        }
    }
}