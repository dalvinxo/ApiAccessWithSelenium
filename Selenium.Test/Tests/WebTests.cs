using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace Selenium.Test.Tests
{
    [TestFixture]
    public class WebTests : IDisposable
    {
        // String test_url = "http://localhost:5000";
        String test_url = "http://localhost:5070/Post/Create/336";
        private ChromeDriver _driver;
        private WebDriverWait _wait;

        [SetUp]
        public void Setup()
        {

            var options = new ChromeOptions();
            // options.AddArgument("--headless");
            _driver = new ChromeDriver(options);
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
        }

        [Test]
        public void Test_HomePage_Title()
        {
            // URL del proyecto Web (Selenium.Web)
            _driver.Navigate().GoToUrl(test_url);

            string pageTitle = _driver.Title;
            Assert.That(pageTitle, Is.EqualTo("Crear Artículo - Games PC"));
        }

        [Test]
        public void Test_FillAndSubmitForm()
        {
            _driver.Navigate().GoToUrl(test_url);

            // Esperar a que el formulario esté listo
            _wait.Until(driver => driver.FindElement(By.CssSelector("form")));

            // Rellenar el campo 'Name'
            var nameField = _driver.FindElement(By.Id("Name"));
            nameField.Clear();
            nameField.SendKeys("Kevin James");

            // Seleccionar el género 'Hombre'
            var genreMaleOption = _driver.FindElement(By.CssSelector("input[value='Hombre']"));
            genreMaleOption.Click();

            // Rellenar el campo 'Age'
            var ageField = _driver.FindElement(By.Id("Age"));
            ageField.Clear();
            ageField.SendKeys("20");

            // Seleccionar 'País' del menú desplegable
            var paisDropdown = new SelectElement(_driver.FindElement(By.Id("PaisId")));
            paisDropdown.SelectByValue("1"); // Cambiar a un valor válido en tu lista de países

            var titleField = _driver.FindElement(By.Id("Title"));
            titleField.Clear();
            titleField.SendKeys("Annarchi online desde Latino América");

            var descriptionField = _driver.FindElement(By.Id("Description"));
            descriptionField.Clear();
            descriptionField.SendKeys("Annarchi online es un MMO ARPG que no debemos perder de vista este año. Un sistema de combate amplio y funcional, gran cantidad de contenido, una campaña disfrutable");


            var rateField = _driver.FindElement(By.Id("rate-input"));
            rateField.Clear();
            rateField.SendKeys("8");

            var submitButton = _driver.FindElement(By.CssSelector("button[type='submit']"));
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", submitButton);



            Assert.That(_driver.Url, Is.EqualTo("http://localhost:5070/Post"));
        }


        [TearDown]
        public void TearDown()
        {

            Dispose();
        }

        public void Dispose()
        {

            _driver?.Dispose();
        }
    }
}
