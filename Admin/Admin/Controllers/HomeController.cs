using Admin.Models;
using System;
using System.Linq;
using System.Web.Mvc;


namespace Admin.Controllers
{
    public class HomeController : Controller
    {
        private CT25Team25Entities2 db = new CT25Team25Entities2();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search(string searchString)
        {
            var links = from l in db.SanPhams // lấy toàn bộ liên kết
                        select l;

            if (!String.IsNullOrEmpty(searchString)) // kiểm tra chuỗi tìm kiếm có rỗng/null hay không
            {
                links = links.Where(s => s.TenSP.Contains(searchString)); //lọc theo chuỗi tìm kiếm
            }
            return View();
        }
    }
}