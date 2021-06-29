using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Admin.Models;

namespace Admin.Controllers
{
    public class LoginController : Controller
    {
        private CT25Team25Entities2 db;

        public LoginController()
        {
            db = new CT25Team25Entities2();
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Models.QuanTri model)
        {
            using (var context = new CT25Team25Entities2())
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

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }

    }
}
