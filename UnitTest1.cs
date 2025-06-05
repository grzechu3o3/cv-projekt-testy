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
        public void LoginTest()
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
        [TearDown]
        public void Teardown()
        {
            driver.Quit();
        }
    }
}