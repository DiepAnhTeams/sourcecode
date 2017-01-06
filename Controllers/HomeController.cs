using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop_thien_thanh.Models;   
namespace Shop_thien_thanh.Controllers
{
    public class HomeController : Controller
    {

        FASHIONMODELDataContext data = new FASHIONMODELDataContext();
        public ActionResult Index()
        {
            PhanLoai thoiTrangNam = data.PhanLoais.First(m => m.PhanLoaiID == 2);
            List<SanPham> dsSanPham = data.SanPhams.Where(m => thoiTrangNam.Nhomsps.Contains(m.Nhomsp)).Take(8).ToList();
            PhanLoai thoiTrangNu = data.PhanLoais.First(m => m.PhanLoaiID == 1);
            ViewBag.dsSanPhamNu = data.SanPhams.Where(m => thoiTrangNu.Nhomsps.Contains(m.Nhomsp)).Take(8).ToList();
            return View(dsSanPham);
        }

        public ActionResult About()
        {
            ViewBag.Title = "Giới thiệu";
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  // chống mạo danh 
        public ActionResult Contact(HopThu model)
        {
         
            if(ModelState.IsValid)   // ko có lỗi nào
            {
                model.NgayCapNhat = DateTime.Now;
                data.HopThus.InsertOnSubmit(model);
                data.SubmitChanges();
                return Content("<script language='javascript' type='text/javascript'>alert('Gửi thành công!'); window.location.replace('"+Url.Action("Index")+"');</script>");
            }
            else  // có bất kỳ lỗi nào 
            {
                return View(model);
            }

        }

        public PartialViewResult _MenuPartial()
        {
            List<PhanLoai> dsPhanLoai = data.PhanLoais.ToList();
            ViewBag.dsNhomSp = data.Nhomsps.ToList();
            return PartialView(dsPhanLoai);
        }

        public PartialViewResult _SanPhamMoiPartial()
        {
            List<SanPham> dsSanPhamMoi = data.SanPhams.OrderByDescending(m => m.SanPhamID).Take(10).ToList();
            return PartialView(dsSanPhamMoi);
        }

        public PartialViewResult _TinThoiTrangPartial()
        {
            List<BaiViet> dsBaiViet = (from bv in data.BaiViets select bv).Take(5).ToList();
            return PartialView(dsBaiViet);
        }
    }
}