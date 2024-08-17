using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace PastebinAutomation.Tests
{
    [TestFixture]
    public class PastebinTests
    {
        private IWebDriver _driver;
        private PastebinHomePage _homePage;

        [SetUp]
        public void SetUp()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            _driver.Navigate().GoToUrl("https://pastebin.com/");
            _homePage = new PastebinHomePage(_driver);
        }

        [Test]
        public void CreateNewPasteTest()
        {
            _homePage.EnterCode("Hello from WebDriver");
            _homePage.SelectPasteExpiration("10 Minutes");
            _homePage.EnterPasteName("helloweb");
            _homePage.CreateNewPaste();

            Assert.That(_driver.Title.Contains("helloweb"), Is.True);
        }

        [TearDown]
        public void TearDown()
        {
            if (_driver != null)
            {
                _driver.Quit();
                _driver.Dispose();
            }
        }
    }
}