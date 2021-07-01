using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShoe.Models;

namespace WebShoe.Controllers
{
    public class GioHangsController : Controller
    {
        // GET: GioHangs
        CT25Team25Entities db = new CT25Team25Entities();
        private List<DonHang> cart = null;
        public GioHangsController()
        {
            var Session = System.Web.HttpContext.Current.Session;
            if (Session["cart"] != null)
                cart = Session["cart"] as List<DonHang>;
            else
            {
                cart = new List<DonHang>();
                Session["cart"] = cart;
            }
        }

        // GET: Cart
        public ActionResult Index()
        {
            var hashtable = new Hashtable();
            foreach (var ChitietDH in cart)
            {
                if (hashtable[ChitietDH.MaSP] != null)
                {
                    (hashtable[ChitietDH.MaSP] as DonHang).SoLuong += ChitietDH.SoLuong;
                }
                else
                {
                    hashtable[ChitietDH.MaSP] = ChitietDH;
                }
            }
            cart.Clear();
            foreach (DonHang chitietDH in hashtable.Values)
            {
                cart.Add(chitietDH);
            }
            ViewBag.Success = "Không có sản phẩm nào trong giỏ hàng";
            return View(cart);
        }

        [HttpPost]
        public ActionResult AddtoCart(int productid, int soluong = 1)
        {
            var product = db.SanPhams.Find(productid);
            cart.Add(new DonHang
            {
                SanPham = product,
                SoLuong = soluong,
            });
            return RedirectToAction("Index");
        }

        public ActionResult UpdateCart([Microsoft.AspNetCore.Mvc.FromForm] int productid, [Microsoft.AspNetCore.Mvc.FromForm] int soluong)
        {
            cart = (List<DonHang>)Session["cart"];
            var product = cart.Find(p => p.SanPham.MaSP == productid);
            if (product != null)
            {
                product.SoLuong = soluong;
            }
            Session["cart"] = cart;
            return RedirectToAction("Index");
        }

        public ActionResult xoaSP(int id)
        {
            cart = (List<DonHang>)Session["cart"];
            var product = cart.Find(p => p.SanPham.MaSP == id);
            if (product != null)
            {
                cart.Remove(product);
            }
            Session["cart"] = cart;
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult AddOrder(FormCollection fc)
        {
            int orderid = 0;
            cart = Session["cart"] as List<DonHang>;
            DonHang dh = new DonHang()
            {
                KhachHang_ID = Convert.ToInt32(fc["MaKH"]),
                TENKH = fc["TenKH"],
                DiaChi = fc["diachi"],
                PHONE = fc["sdt"],
                
                ORDERDATE = DateTime.Now,
                
                TongTien = Convert.ToDouble(fc["thanhtien"])
            };
            db.DonHangs.Add(dh);
            db.SaveChanges();
            orderid = dh.MaDH;
            foreach (var item in cart)
            {
                ChiTietGioHang chitiet = new ChiTietGioHang()
                {
                    MaCTGH = orderid,
                  
                    SanPham_ID = item.SanPham.MaSP,
                   
                    TenSP = item.SanPham.TenSP,
                    
                };

                db.ChiTietGioHangs.Add(chitiet);
                db.SaveChanges();
            }
            Session["cart"] = null;
            return RedirectToAction("OrderSuccess");
        }
        public ActionResult OrderSuccess()
        {

            return View();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
