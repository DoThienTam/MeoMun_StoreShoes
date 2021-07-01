using Admin.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace Admin.Tests.Controllers
{
    [TestClass]
    public class LoginControllerTest
    {
        [TestMethod]
        public void TestLogin()
        {
            var controller = new LoginController();
            var result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
        }
    }
}
