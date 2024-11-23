using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace Selenium.Test.Tests
{
    [TestFixture]
    public class GameFormTests : IDisposable
    {
        private readonly string test_url = "http://localhost:4200/games/540";
        private ChromeDriver _driver;
        private WebDriverWait _wait;

        [SetUp]
        public void Setup()
        {
            var options = new ChromeOptions();
            _driver = new ChromeDriver(options);
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(3));
        }

        [Test]
        public void Test_FillAndSubmitGameForm()
        {
            _driver.Navigate().GoToUrl(test_url);
            _driver.Manage().Window.Maximize();
            // Esperar que el formulario esté visible
            _wait.Until(driver => driver.FindElement(By.CssSelector("form")));

            // Llenar el campo 'Nombre del Usuario'
            var nameField = _driver.FindElement(By.Id("name"));
            nameField.Clear();
            nameField.SendKeys("Carlos Perez");
            Thread.Sleep(1000);
            // Llenar el campo 'Cédula'
            var cedulaField = _driver.FindElement(By.Id("cedula"));
            cedulaField.Clear();
            cedulaField.SendKeys("001-1234567-8");
            Thread.Sleep(1000);
            // Llenar el campo 'Teléfono'
            var telefonoField = _driver.FindElement(By.Id("telefono"));
            telefonoField.Clear();
            telefonoField.SendKeys("(809) 123-4567");
            Thread.Sleep(1000);
            // Seleccionar el género 'Hombre'
            var genreOption = _driver.FindElement(By.CssSelector("input[value='Hombre']"));
            genreOption.Click();

            // Llenar el campo 'Edad'
            var ageField = _driver.FindElement(By.Id("age"));
            ageField.Clear();
            ageField.SendKeys("30");

            // Seleccionar un país del menú desplegable
            var countryDropdown = new SelectElement(_driver.FindElement(By.Id("paisId")));
            countryDropdown.SelectByValue("3"); // Cambiar por un ID válido de tu base de datos

            // Llenar el campo 'Título'
            var titleField = _driver.FindElement(By.Id("title"));
            titleField.Clear();
            titleField.SendKeys("Introducción a la Valorant");

            // Llenar el campo 'Descripción'
            var descriptionField = _driver.FindElement(By.Id("description"));
            descriptionField.Clear();
            descriptionField.SendKeys("Una introducción básica a la inteligencia artificial y sus aplicaciones actuales en diversos campos");
            Thread.Sleep(1000);
            // Llenar el campo 'Valoración'
            var rateField = _driver.FindElement(By.Id("rate"));
            rateField.Clear();
            rateField.SendKeys("6");

            // Hacer clic en el botón 'Guardar'
            var submitButton = _driver.FindElement(By.CssSelector("button[type='submit']"));
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", submitButton);



            Assert.That(_driver.Url, Is.EqualTo("http://localhost:4200/games/540"));

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
