using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop_thien_thanh.Models;
using PagedList.Mvc;
using PagedList;
namespace Shop_thien_thanh.Controllers
{
    public class SanPhamController : Controller
    {
        FASHIONMODELDataContext data = new FASHIONMODELDataContext();
        // GET: SanPham
        
        public ActionResult LocTheoLoai(int phanloaiID, int? page)
        {
            PhanLoai phanLoai = (from pl in data.PhanLoais where pl.PhanLoaiID == phanloaiID select pl).FirstOrDefault();
            if(phanLoai == null)
            {
                return HttpNotFound();
            }
            ViewBag.TenPhanLoai = phanLoai.TenPhanLoai;
            List<SanPham> model = (from sp in data.SanPhams where phanLoai.Nhomsps.Contains(sp.Nhomsp) orderby  sp.SanPhamID descending  select sp).ToList();

            return View(model.ToPagedList(page ?? 1, 8));
        }

        public ActionResult LocTheoNhom(int nhomspID, int? page)
        {
            Nhomsp nhomSP = (from nsp in data.Nhomsps where nsp.NhomspID == nhomspID select nsp).FirstOrDefault();
            if(nhomSP == null)
            {
                return HttpNotFound();
            }
            ViewBag.TenNhom = nhomSP.TenNhomsp;
            List<SanPham> model = (from sp in data.SanPhams where sp.NhomspID == nhomspID orderby sp.SanPhamID descending select sp).ToList();
            return View(model.ToPagedList(page ?? 1, 8));
        }


        public ActionResult ChiTiet(int id)
        {
            // Lọc sản phẩm theo tham số id

            // Cách 1: 
           // SanPham model = data.SanPhams.FirstOrDefault(m => m.SanPhamID == id);

            // Cách 2:
            SanPham model = (from sp in data.SanPhams where sp.SanPhamID == id select sp).FirstOrDefault();
            if(model == null)
            {
                return HttpNotFound();
            }
            ViewBag.sanPhamCungNhom = (from sp in data.SanPhams where sp.NhomspID == model.NhomspID && sp.SanPhamID != model.SanPhamID select sp).Take(8).ToList(); 
            return View(model);
        }
        public ActionResult TimKiem(string tukhoa)
        {
            List<SanPham> model = (from sp in data.SanPhams where sp.TenSanPham.Contains(tukhoa) select sp).ToList();
            ViewBag.TuKhoa = tukhoa;
            return View(model);
        }
    }
}