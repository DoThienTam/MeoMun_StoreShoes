using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebShoe.Models;

namespace WebShoe.Areas.Admin.Controllers
{
    public class QT_LoginController : Controller
    {
        // GET: Admin/QT_Login
       
            private CT25Team25Entities db;

        public QT_LoginController()
        {
            db = new CT25Team25Entities();
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Models.QuanTri model)
        {
            using (var context = new CT25Team25Entities())
            {
                var account = context.QuanTris.Where(acc => acc.USERSNAME == model.USERSNAME && acc.PASSWORD == model.PASSWORD).FirstOrDefault();
                bool isValid = context.QuanTris.Any(x => x.USERSNAME == model.USERSNAME && x.PASSWORD == model.PASSWORD);
                if (isValid)
                {
                    Session["FULLNAME"] = account.FULLNAME;
                    Session["USERNAME"] = account.USERSNAME;
                    Session["PASSWORD"] = account.PASSWORD;
                    FormsAuthentication.SetAuthCookie(model.USERSNAME, false);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid username and password :( :(");
                Session["Message"] = "Sai username hoặc password :(";
                return View();
            }
        }

        public ActionResult DKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DKy(FormCollection fc)
        {

            KhachHang kh = new KhachHang()
            {
                USERNAME = fc["Username"],
                DiaChi = fc["DiaChi"],
                PASSWORD = fc["Password"],
                NgaySinh = Convert.ToDateTime(fc["Ngaysinh"]),
                EMAIL = fc["Email"],
                TenKH = fc["TenKH"],
                PHONE = fc["phone"],
                GioiTinh = fc["gioitinh"],
            };
            ViewBag.Success = "Đăng kí thành công";
            db.KhachHangs.Add(kh);
            db.SaveChanges();
            return RedirectToAction("Index", "Admin");
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Admin");
        }

    }
}
    
