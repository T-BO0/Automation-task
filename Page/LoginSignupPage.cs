using Automation_task.Utils;
using OpenQA.Selenium;

namespace Automation_task.Page
{
    public class LoginSignupPage : BasePage
    {
        public LoginSignupPage(DriverManager driverManager) : base(driverManager)
        {
        }

        public static By EmailInputLocator => By.CssSelector("[data-testid=email-input]");
        public static By PasswordInputLocator => By.CssSelector("[data-testid=password-strength-input]");
        public static By ContinueButtonLocator => By.CssSelector("[data-testid=login-next-button]");
        public static By CreateAccountButtonLocator => By.CssSelector("[data-testid=register-submit-button]");
        public static By LogInButtonLocator => By.CssSelector("[data-testid=login-submit]");
        public static By InvalidEmailValidationTextLocator => By.CssSelector("[data-testid=form-field-error-notification]");
        public static By InvalidPasswordValidationTextLocator => By.XPath("//input[contains(@class,'PasswordStrengthInput_inputError__KPFLU')]");


        public bool TheRegistrationSignUpPageIsOpen()
        {
            return base.CheckIfPageOpen(EmailInputLocator);
        }

        public void ClickContinueButton()
        {
            var element = _driverManager.WAitAndFindElement(ContinueButtonLocator);
            element.Click();
        }

        public void ClickCreatAccountButton()
        {
            var element = _driverManager.WAitAndFindElement(CreateAccountButtonLocator);
            element.Click();

        }

        public void ClickLoginButton()
        {
            var element = _driverManager.WAitAndFindElement(LogInButtonLocator);
            element.Click();
        }

        public void InsertEmail(string email)
        {
            var emailInput = _driverManager.WAitAndFindElement(EmailInputLocator);
            emailInput.Clear();
            emailInput.SendKeys(email);
        }

        public void InsertPassword(string password)
        {
            var passwordInput = _driverManager.WAitAndFindElement(PasswordInputLocator);
            passwordInput.Clear();
            passwordInput.SendKeys(password);

        }

        public bool IsInvalidPassword()
        {
            var elements = _driverManager.Driver.FindElements(InvalidPasswordValidationTextLocator);
            return elements.Count > 0;
        }

        public bool IsInvalidEmail()
        {
            var elements = _driverManager.Driver.FindElements(InvalidEmailValidationTextLocator);
            return elements.Count > 0;
        }
    }
}