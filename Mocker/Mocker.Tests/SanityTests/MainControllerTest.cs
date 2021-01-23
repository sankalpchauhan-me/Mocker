using DBLib.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mocker.Controllers;
using Mocker.Tests.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocker.Tests.SanityTests
{
    [TestClass]
    class MainControllerTest
    {
        [TestMethod]
        public void GetAllCheck_ReturnAllModels()
        {
            List<Developer> devs = MockDataClass.GetDevelopers();
            MainController mainController = new MainController();
            mainController.GetAll();
        }
    }
}
