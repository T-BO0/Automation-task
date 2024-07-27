using Automation_task.Utils;
using OpenQA.Selenium;

namespace Automation_task.Page
{
    public class LoginRegistrationModal : BasePage
    {
        public LoginRegistrationModal(DriverManager driverManager) : base(driverManager)
        {
        }
        public static By EmailInputLocator => By.CssSelector("[data-testid='login-modal-email-field']");
        public static By ContinueButtonLocator => By.CssSelector("[data-testid='login-modal-email-button']");
        public static By PasswordInputLocator => By.Id("password");
        public static By LoginButtonLocator => By.CssSelector("[data-testid='login-submit']");
        public static By CreateAccountButtonLocator => By.CssSelector("[data-testId='register-submit-button']");
        public static By InvalidEmailValidationLocator => By.CssSelector("[data-testid='form-field-error-notification']");

        public void InsertEmail(string email)
        {
            var element = _driverManager.WAitAndFindElement(EmailInputLocator);
            element.Clear();
            element.SendKeys(email);
        }

        public void ClickContinue()
        {
            var continueButton = _driverManager.WAitAndFindElement(ContinueButtonLocator);
            continueButton.Click();
        }

        public void InsertPassword(string password)
        {
            _driverManager.WAitAndFindElement(PasswordInputLocator).SendKeys(password);
        }

        public void ClickLoginButton()
        {
            var loginButton = _driverManager.WAitAndFindElement(LoginButtonLocator);
            loginButton.Click();
        }

        public void ClickCreateButton()
        {
            var element = _driverManager.WAitAndFindElement(CreateAccountButtonLocator);
            element.Click();
        }

        public bool ValidationTextIsVisible()
        {
            var elements = _driverManager.WAitAndFindElements(InvalidEmailValidationLocator);

            return elements.Count > 0;
        }

        public bool PasswordIsInvalid()
        {
            var element = _driverManager.WAitAndFindElement(PasswordInputLocator);
            string borderColor = element.GetCssValue("border-color");

            string expectedColor = "rgb(219, 55, 52)";
            return borderColor.Equals(expectedColor);
        }
    }
}