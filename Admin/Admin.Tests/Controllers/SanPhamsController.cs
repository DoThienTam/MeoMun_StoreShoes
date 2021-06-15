using Admin.Controllers;
using Admin.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Admin.Tests.Controllers
{
    [TestClass]
    public class SanPhamsControllerTest
    {
        [TestMethod]
        public void TestIndex()
        {
            var controller = new SanPhamsController();

            var result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as List<SanPham>;
            Assert.IsNotNull(model);

            var db = new CT25Team25Entities2();
            Assert.AreEqual(db.SanPhams.Count(), model.Count);
        }

        [TestMethod]
        public void TestCreateG()
        {
            var controller = new SanPhamsController();

            var result = controller.Create() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestCreateP()
        {
            var rand = new Random();
            var product = new SanPham
            {
                TenSP = rand.NextDouble().ToString(),
                SoLuong = -rand.Next(),
                DonGia = -rand.Next()
            };

            var controller = new SanPhamsController();
            var result0 = controller.Create(product) as ViewResult;

            Assert.IsNotNull(result0);
            Assert.AreEqual("Đơn giá phải lớn hơn 0", controller.ModelState["DonGia"].Errors[0].ErrorMessage);
            Assert.AreEqual("Số lượng phải lớn hơn 0", controller.ModelState["SoLuong"].Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void TestEditP()
        {
            var rand = new Random();
            var product = new SanPham
            {
                TenSP = rand.NextDouble().ToString(),
                SoLuong = -rand.Next(),
                DonGia = -rand.Next()
            };

            var controller = new SanPhamsController();
            var result0 = controller.Edit(product) as ViewResult;

            Assert.IsNotNull(result0);
            Assert.AreEqual("Đơn giá phải lớn hơn 0", controller.ModelState["DonGia"].Errors[0].ErrorMessage);
            Assert.AreEqual("Số lượng phải lớn hơn 0", controller.ModelState["SoLuong"].Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void TestEditG()
        {
            var controller = new SanPhamsController();
            var result0 = controller.Edit(ToString()) as HttpNotFoundResult;
            Assert.IsNotNull(result0);

            var db = new CT25Team25Entities2();
            var product = db.SanPhams.First();
            var result = controller.Edit(product.MaSP) as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as SanPham;
            Assert.IsNotNull(model);
            Assert.AreEqual(product.MaSP, model.MaSP);
            Assert.AreEqual(product.TenSP, model.TenSP);
            Assert.AreEqual(product.SoLuong, model.SoLuong);
            Assert.AreEqual(product.LoaiSanPham.MaLSP, model.LoaiSanPham.MaLSP);
            Assert.AreEqual(product.ImagePath, model.ImagePath);
            Assert.AreEqual(product.SIZE, model.SIZE);
        }

        [TestMethod]
        public void TestDeleteG()
        {
            var controller = new SanPhamsController();
            var result0 = controller.Delete(ToString()) as HttpNotFoundResult;
            Assert.IsNotNull(result0);

            var db = new CT25Team25Entities2();
            var product = db.SanPhams.First();
            var result = controller.Edit(product.MaSP) as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as SanPham;
            Assert.IsNotNull(model);
            Assert.AreEqual(product.MaSP, model.MaSP);
            Assert.AreEqual(product.TenSP, model.TenSP);
            Assert.AreEqual(product.SoLuong, model.SoLuong);
            Assert.AreEqual(product.LoaiSanPham.MaLSP, model.LoaiSanPham.MaLSP);
            Assert.AreEqual(product.ImagePath, model.ImagePath);
            Assert.AreEqual(product.SIZE, model.SIZE); ;
        }

        [TestMethod]
        public void TestDetails()
        {
            var controller = new SanPhamsController();
            var result0 = controller.Details("") as HttpNotFoundResult;
            Assert.IsNotNull(result0);

            var db = new CT25Team25Entities2();
            var product = db.SanPhams.First();
            var result = controller.Details(product.MaSP) as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as SanPham;
            Assert.IsNotNull(model);
            Assert.AreEqual(product.MaSP, model.MaSP);
            Assert.AreEqual(product.TenSP, model.TenSP);
            Assert.AreEqual(product.SoLuong, model.SoLuong);
            Assert.AreEqual(product.LoaiSanPham.MaLSP, model.LoaiSanPham.MaLSP);
            Assert.AreEqual(product.ImagePath, model.ImagePath);
            Assert.AreEqual(product.SIZE, model.SIZE);
        }

        [TestMethod]
        public void TestSearch2()
        {
            var db = new CT25Team25Entities2();
            var products = db.SanPhams.ToList();
            var keyword = products.First().TenSP.Split().First();
            products = products.Where(p => p.TenSP.ToLower().Contains(keyword.ToLower())).ToList();

            var controller = new SanPhamsController();
            var result = controller.Search(keyword) as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as List<SanPham>;
            Assert.IsNotNull(model);

            Assert.AreEqual("Search2", result.ViewName);
            Assert.AreEqual(products.Count(), model.Count);
            Assert.AreEqual(keyword, result.ViewData["keyword"]);
        }
    }
}
