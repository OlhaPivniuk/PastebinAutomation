using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace PastebinAutomation
{
    public class PastebinHomePage
    {
        private readonly IWebDriver _driver;

        public PastebinHomePage(IWebDriver driver)
        {
            _driver = driver;
        }

        public IWebElement CodeInputField => _driver.FindElement(By.Id("postform-text"));
        public IWebElement PasteExpirationDropdown => _driver.FindElement(By.Id("select2-postform-expiration-container"));
        public IWebElement PasteNameInputField => _driver.FindElement(By.Id("postform-name"));
        public IWebElement CreateNewPasteButton => _driver.FindElement(By.XPath("//button[text()='Create New Paste']"));

        public void EnterCode(string code)
        {
            CodeInputField.SendKeys(code);
        }

        public void SelectPasteExpiration(string expiration)
        {
            PasteExpirationDropdown.Click();
            var option = _driver.FindElement(By.XPath($"//li[text()='{expiration}']"));
            option.Click();
        }

        public void EnterPasteName(string pasteName)
        {
            PasteNameInputField.SendKeys(pasteName);
        }

        public void CreateNewPaste()
        {
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", CreateNewPasteButton);

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
            wait.Until(driver => CreateNewPasteButton.Displayed && CreateNewPasteButton.Enabled);

            try
            {
                CreateNewPasteButton.Click();
            }
            catch (ElementClickInterceptedException)
            {
                ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", CreateNewPasteButton);
            }
        }
    }
}