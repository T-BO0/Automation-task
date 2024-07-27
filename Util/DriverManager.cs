using System;
using System.Collections.Generic;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Automation_task.Utils
{
    public class DriverManager
    {
        private const int WAIT_FOR_ELEMENT_TIMEOUT = 30;
        public DriverManager(BrowserType browserType = BrowserType.Chrome)
        {
            switch (browserType)
            {
                case BrowserType.Chrome:
                    Driver = new ChromeDriver();
                    break;
                case BrowserType.Firefox:
                    Driver = new FirefoxDriver();
                    break;
                case BrowserType.Edge:
                    Driver = new EdgeDriver();
                    break;
                case BrowserType.Safari:
                    Driver = new SafariDriver();
                    break;
                default:
                    throw new ArgumentException($"the given browzer name {nameof(browserType)} not supported", nameof(browserType));
            }

            Driver.Manage().Window.Maximize();
            WebDriverWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(WAIT_FOR_ELEMENT_TIMEOUT));
            Actions = new Actions(Driver);
            JavaScriptExecutor = (IJavaScriptExecutor)Driver;
        }

        public IWebDriver Driver { get; set; }
        public WebDriverWait WebDriverWait { get; set; }
        public Actions Actions { get; set; }
        public IJavaScriptExecutor JavaScriptExecutor { get; set; }

        public IWebElement WAitAndFindElement(By locator)
        {
            return WebDriverWait.Until(ExpectedConditions.ElementExists(locator));
        }
        public IList<IWebElement> WAitAndFindElements(By locator)
        {
            return WebDriverWait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(locator));
        }

        public void WaitUntilPageLoadsCompletely()
        {
            var js = (IJavaScriptExecutor)Driver;
            WebDriverWait.Until(wd => js.ExecuteScript("return document.readyState").ToString() == "complete");
        }

        public void GoTo(string url, Action waitForPageToLoad)
        {
            Driver.Navigate().GoToUrl(url);
            waitForPageToLoad();
        }

        public string GetCurrentUrl()
        {
            WaitUntilPageLoadsCompletely();
            return Driver.Url;
        }

        public void TakeScreenshot(string fileName)
        {
            ITakesScreenshot? screenshotDriver = Driver as ITakesScreenshot;
            Screenshot screenshot = screenshotDriver!.GetScreenshot();
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), $"../../../Report/{fileName}.png");
            screenshot.SaveAsFile(filePath);
            Console.WriteLine($"Screenshot saved: {filePath}");
        }

        public void Quit()
        {
            Driver.Quit();
        }
    }
}