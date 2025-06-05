using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;


namespace Testy
{
    public class Tests
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();

        }

        [Test]
        public void ContactFormTest()
        {
            driver.Navigate().GoToUrl("http://localhost:5173/contact");
            driver.FindElement(By.Id("name")).SendKeys("Test User");
            driver.FindElement(By.Id("email")).SendKeys("testowy@mail.com");
            driver.FindElement(By.Id("message")).SendKeys("Test message from Selenium WebDriver");
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            var toast = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("Toastify__toast")));
            string toastMsg = toast.Text;
            Console.WriteLine(toastMsg);
            Assert.That(toastMsg, Is.EqualTo("Wiadomość została wysłana!"));
        }

        [Test]
        public void EmptyContactForm()
        {
            driver.Navigate().GoToUrl("http://localhost:5173/contact");
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            Assert.That(driver.FindElement(By.Id("name")).GetAttribute("validationMessage"), Is.EqualTo("Please fill out this field."));
        }

        [Test]
        public void GenerateCVTest()
        {
            driver.Navigate().GoToUrl("http://localhost:5173/creator");
            driver.FindElement(By.Name("first_name")).SendKeys("Jan");
            driver.FindElement(By.Name("second_name")).SendKeys("Tester");
            driver.FindElement(By.Name("phone")).SendKeys("997");
            driver.FindElement(By.Name("email")).SendKeys("mail@testowy.com");
            driver.FindElement(By.Name("address")).SendKeys("Testowa 1, 00-001 Testowo");
            driver.FindElement(By.Name("social_media")).SendKeys("https://github.com/grzechu3o3");

            driver.FindElement(By.XPath("//button[text()='Podgląd CV']")).Click();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            var element = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//canvas")));
            Assert.That(element.Displayed, Is.True);
        }
        [TearDown]
        public void Teardown()
        {
            driver.Quit();
        }
    }
}