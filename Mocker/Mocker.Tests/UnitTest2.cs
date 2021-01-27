using DBModels.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mocker.Controllers;
using Mocker.Tests.Utility;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace Mocker.Tests
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            List<Developer> devs = MockDataClass.GetDevelopers();
            UserController userController = new UserController();
            userController.Request = new HttpRequestMessage();
            userController.Configuration = new HttpConfiguration();

            //Act
            IHttpActionResult postActionResult = userController.RegisterUser(devs[0]);
            var contentResult = postActionResult as CreatedNegotiatedContentResult<Developer>;
            Developer dev0 = contentResult.Content;

            IHttpActionResult postActionResult1 = userController.RegisterUser(devs[1]);
            var contentResult1 = postActionResult1 as CreatedNegotiatedContentResult<Developer>;
            Developer dev1 = contentResult1.Content;


            IHttpActionResult getActionResult1 = userController.GetRegisteredUser(devs[0].UserId);
            var getResult1 = getActionResult1 as OkNegotiatedContentResult<Developer>;
            Developer getdev1 = getResult1.Content;

            IHttpActionResult getActionResult2 = userController.GetRegisteredUser(devs[1].UserId);
            var getResult2 = getActionResult2 as OkNegotiatedContentResult<Developer>;
            Developer getdev2 = getResult2.Content;

            //Assert
            Assert.AreEqual(dev0, getdev1);
            Assert.AreEqual(dev1, getdev2);
        }
    }
}
