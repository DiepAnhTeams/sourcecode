using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop_thien_thanh.Models;
namespace Shop_thien_thanh.Controllers
{
    public class GioHangController : Controller
    {
        FASHIONMODELDataContext db = new FASHIONMODELDataContext();
        // GET: GioHang
        public ActionResult Index()
        {
            List<GioHangItem> gioHang = Session["giohang"] as List<GioHangItem>;
            return View(gioHang);
        }
        public ActionResult AddItem(int? id)
        {
            if(id == null)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            SanPham sanPham = db.SanPhams.FirstOrDefault(m => m.SanPhamID == id);
            if(sanPham == null)
                return RedirectToAction("Index", "Home", new { area = "" });
            else
            {
                if(Session["giohang"] == null)
                {
                    Session["giohang"] = new List<GioHangItem>();
                }
                GioHangItem item = ((List<GioHangItem>)Session["giohang"]).FirstOrDefault(m => m.SanPhamID == id);
                if(item == null)
                {
                    GioHangItem itemMoi = new GioHangItem()
                    {
                        SanPhamID = id.Value,
                        Hinh = sanPham.Hinh,
                        TenSanPham = sanPham.TenSanPham,
                        DonGia = sanPham.DonGia,
                        SoLuong = 1
                    };
                    ((List<GioHangItem>)Session["giohang"]).Add(itemMoi);
                }
                else
                {
                    item.SoLuong++;

                }
            }
            return Redirect(Request.UrlReferrer.ToString());
        }
        public ActionResult DeleteItem(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            GioHangItem item = ((List<GioHangItem>)Session["giohang"]).FirstOrDefault(m => m.SanPhamID == id);
            if(item != null)
            {
                ((List<GioHangItem>)Session["giohang"]).Remove(item);
            }
            return RedirectToAction("Index");
        }

        public ActionResult UpdateItem(int id, int SoLuong)
        {
            GioHangItem item = ((List<GioHangItem>)Session["giohang"]).FirstOrDefault(m => m.SanPhamID == id);
            if (item != null)
            {
                item.SoLuong = SoLuong;
                
            }
            return RedirectToAction("Index");
        }
    }
}