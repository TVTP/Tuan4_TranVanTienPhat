using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tuan4_TranVanTienPhat.Models;

namespace Tuan4_TranVanTienPhat.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        public List<GioHang> LayGioHang()
        {
            List<GioHang> listGioHang = Session["Giohang"] as List<GioHang>;
            if (listGioHang == null)
            {
                listGioHang = new List<GioHang>();
                Session["GioHang"] = listGioHang;
            }
            return listGioHang;
        }

        public ActionResult ThemGioHang(int id, string strURL)
        {
            List<GioHang> listGioHang = LayGioHang();
            GioHang sanpham = listGioHang.Find(n => n.masach == id);
            if (sanpham == null)
            {
                sanpham = new GioHang(id);
                listGioHang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                {
                    sanpham.iSoluong++;
                    return Redirect(strURL);
                }
            }

        }


        public int TongSoLuong()
        {
            int tsl = 0;
            List<GioHang> listGioHang = Session["Giohang"] as List<GioHang>;
            if (listGioHang != null)
            {
                tsl = listGioHang.Sum(n => n.iSoluong);
            }
            return tsl;
        }

        private int TongSoLuongSanPham()
        {
            int tsl = 0;
            List<GioHang> listGioHang = Session["Giohang"] as List<GioHang>;
            if (listGioHang != null)
            {
                tsl = listGioHang.Count;
            }
            return tsl;
        }

        private double TongTien()
        {
            double tt = 0;
            List<GioHang> listGioHang = Session["Giohang"] as List<GioHang>;
            if (listGioHang != null)
            {
                tt = listGioHang.Sum(n => n.dThanhtien);
            }
            return tt;
        }

        public ActionResult GioHang()
        {
            List<GioHang> listGioHang = LayGioHang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();
            return View(listGioHang);
        }

        public ActionResult GioHangPartial()
        {
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();
            return PartialView();
        }

        public ActionResult XoaGioHang(int id)
        {
            List<GioHang> listGioHang = LayGioHang();
            GioHang sanpham = listGioHang.SingleOrDefault(n => n.masach == id);
            if (sanpham != null)
            {
                listGioHang.RemoveAll(n => n.masach == id);
                return RedirectToAction("GioHang");
            }
            return RedirectToAction("GioHang");
        }

        public ActionResult CapnhatGioHang(int id, FormCollection collection)
        {
            List<GioHang> listGioHang = LayGioHang();
            GioHang sanpham = listGioHang.SingleOrDefault(n => n.masach == id);
            if (sanpham != null)
            {
                sanpham.iSoluong = int.Parse(collection["txtSoLg"].ToString());
            }
            return RedirectToAction("GioHang");

        }

        public ActionResult XoaTatCaGioHang()
        {
            List<GioHang> listGioHang = LayGioHang();
            listGioHang.Clear();
            return RedirectToAction("GioHang");
        }

        public ActionResult DatHang(int id)
        {
            List<GioHang> listGioHang = LayGioHang();
            List<Sach> Sach = new List<Sach>();
            GioHang sanpham = listGioHang.SingleOrDefault(n => n.masach == id);
            if (sanpham != null)
            {
                foreach (var item in Sach)
                {
                    item.soluongton -= TongSoLuong();
                }
            }
            listGioHang.Clear();
            return RedirectToAction("Home");
        }
    }
}