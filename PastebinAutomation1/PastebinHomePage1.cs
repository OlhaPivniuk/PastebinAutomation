using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

public class PastebinHomePage1
{
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _wait;

    public PastebinHomePage1(IWebDriver driver)
    {
        _driver = driver;
        _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30)); // Збільшено час очікування
    }

    private IWebElement PasteCodeField => GetElement(By.Id("postform-text"));
    private IWebElement PasteExpirationDropdown => GetElement(By.Id("postform-expiration"));
    private IWebElement PasteNameField => GetElement(By.Id("postform-name"));
    private IWebElement CreateNewPasteButton => GetElement(By.XPath("//button[text()='Create New Paste']"));

    public void CreateNewPaste(string code, string expiration, string pasteName)
    {
        Console.WriteLine("Attempting to create new paste...");
        ClosePopUps();

        PasteCodeField.SendKeys(code);
        Console.WriteLine("Entered code.");

        try
        {
            SelectPasteExpiration(expiration);
            Console.WriteLine("Selected expiration.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to select expiration: {ex.Message}");
        }

        PasteNameField.SendKeys(pasteName);
        Console.WriteLine("Entered paste name.");

        CreateNewPasteButton.Click();
        Console.WriteLine("Clicked 'Create New Paste' button.");
    }

    private void SelectPasteExpiration(string expiration)
    {
        try
        {
            var selectElement = new SelectElement(PasteExpirationDropdown);
            selectElement.SelectByText(expiration);
        }
        catch (NoSuchElementException ex)
        {
            Console.WriteLine($"Expiration dropdown not found: {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error selecting expiration: {ex.Message}");
            throw;
        }
    }

    private void ClosePopUps()
    {
        // Попередні методи залишаються без змін
    }

    private IWebElement GetElement(By by)
    {
        try
        {
            return _wait.Until(ExpectedConditions.ElementIsVisible(by));
        }
        catch (WebDriverTimeoutException ex)
        {
            Console.WriteLine($"Element with locator {by} not found: {ex.Message}");
            throw;
        }
    }
}
