using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace Selenium.Test.Tests
{
    [TestFixture]
    public class WebTests: IDisposable
    {
        String test_url = "http://localhost:5000";
        private ChromeDriver _driver;
        private WebDriverWait _wait;

        [SetUp]
        public void Setup()
        {
            
            var options = new ChromeOptions();
            options.AddArgument("--headless"); 
            _driver = new ChromeDriver(options);
            
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void Test_HomePage_Title()
        {
            // URL del proyecto Web (Selenium.Web)
            _driver.Navigate().GoToUrl(test_url);

            string pageTitle = _driver.Title;
            Assert.That(pageTitle, Is.EqualTo("Home Page - Selenium Web"));
        }

        // [TearDown]
        // public void TearDown()
        // {
        //     // Cerrar el navegador después de cada prueba
        //     _driver.Quit();
        // }

        [TearDown]
        public void TearDown()
        {
            // Asegura que el driver sea correctamente liberado
            Dispose();
        }

         public void Dispose()
        {
            
            _driver?.Dispose();
        }
    }
}
