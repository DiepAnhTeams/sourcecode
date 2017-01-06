using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop_thien_thanh.Models;
namespace Shop_thien_thanh.Controllers
{
    public class DatHangController : Controller
    {
        FASHIONMODELDataContext db = new FASHIONMODELDataContext();
        // GET: DatHang
        public ActionResult ThemDatHang([Bind(Include = "Hoten,DienThoai,Email,DiaChi")] DatHang datHangMoi)
        {
            datHangMoi.TriGia = ((List<GioHangItem>)Session["giohang"]).Sum(m => m.ThanhTien);
            datHangMoi.NgayDat = DateTime.Now;
            datHangMoi.NgayGiao = null;
            datHangMoi.DaGiao = false;
            if (ModelState.IsValid)
            {
                try
                {
                    db.DatHangs.InsertOnSubmit(datHangMoi);
                    db.SubmitChanges();
                    foreach(GioHangItem item in (List<GioHangItem>)Session["giohang"])
                    {
                        DatHangCT datHangChiTiet = new DatHangCT()
                        {
                            DatHangID = datHangMoi.DatHangID,
                            SanPhamID = item.SanPhamID,
                            DonGia = item.DonGia,
                            ThanhTien = item.ThanhTien,
                            SoLuong = item.SoLuong
                        };
                        db.DatHangCTs.InsertOnSubmit(datHangChiTiet);
                    }
                    db.SubmitChanges();
                    Session["giohang"] = null;
                    return Content("<script> alert('Đặt hàng thành công. Chúng tôi sẽ liên hệ với bạn để xác nhận đơn hàng trong thời gian sớm nhất. Xin cảm ơn.');  window.location.replace('" + Url.Action("Index", "Home") + "'); </script>");

                }
                catch
                {
                    return Content("<script> alert('Xảy ra lỗi khi đặt hàng');  window.location.replace('" + Url.Action("Index", "Home") + "'); </script>");

                }
            }
            return View();
        }
    }
}