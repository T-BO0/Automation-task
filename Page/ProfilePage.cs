using Automation_task.Utils;
using OpenQA.Selenium;

namespace Automation_task.Page
{
    public class ProfilePage : BasePage
    {
        public ProfilePage(DriverManager driverManager) : base(driverManager)
        {
        }

        public static By FirstNameLabelLocator => By.XPath("//label[contains(text(),'First name')]");
        public static By FirstNameInputLocator => By.XPath("//input[@name='firstName']");
        public static By LastNameinputLocator => By.XPath("//input[@name='lastName']");
        public static By UpdatePersonalInfoButtonLocator => By.XPath("//button[@type='submit']/span[contains(text(),'Update')]");
        public static By UpdateInfoIndicatorLocator => By.CssSelector("[data-testid='alert-notification']");
        public static By ValidationErrorLocator => By.CssSelector("[data-testid='form-field-error-notification']");


        public bool TheProfilePageIsOpen()
        {
            return base.CheckIfPageOpen(FirstNameLabelLocator);
        }
        public void GoTo()
        {
            _driverManager.GoTo("https://www.trivago.com/en-US/profile/account-settings", () => _driverManager.WAitAndFindElement(By.XPath("//button[@type='submit']/span[contains(text(),'Update')]")));
        }

        public void InsertNewFirstName(string newFirstName)
        {
            var input = _driverManager.WAitAndFindElement(FirstNameInputLocator);
            input.SendKeys(newFirstName);
        }

        public void InsertNewLastName(string newLastName)
        {
            var input = _driverManager.WAitAndFindElement(LastNameinputLocator);
            input.SendKeys(newLastName);
        }

        public void ClickUpdatePersonalInfoButton()
        {
            var button = _driverManager.WAitAndFindElement(UpdatePersonalInfoButtonLocator);
            button.Click();
        }

        public bool TheProfileinfoIsUpdated()
        {
            var elements = _driverManager.WAitAndFindElements(UpdateInfoIndicatorLocator);
            return elements.Count > 0;
        }

        public bool TheValidationErrorisVisible()
        {
            var elements = _driverManager.WAitAndFindElements(ValidationErrorLocator);
            return elements.Count > 0;
        }
    }
}