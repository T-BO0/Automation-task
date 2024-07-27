using System.Linq;
using Automation_task.Page;
using Automation_task.Util;
using NUnit.Framework;
using Serilog;

namespace Automation_task.Test
{
    [TestFixture]
    public class RegistrationTests : BaseTest
    {

        [Test]
        public void RegistrationPositiveTest()
        {
            Log.Information(" ");
            Log.Information("<<<< Starting Positive test of Registration");

            var test = _extent.CreateTest("Registration positive");

            try
            {
                var welcomePage = new WelcomePage(_driverManager);
                var loginRegistrationModal = new LoginRegistrationModal(_driverManager);
                var loginSignupPage = new LoginSignupPage(_driverManager);
                var correctPassword = _configuration["CorrectPassword"];

                Log.Information($"going to welcome page -> {nameof(RegistrationPositiveTest)}");
                welcomePage.GoTo();
                Assert.IsTrue(welcomePage
                    .WelcomePageIsOpen(), $"expected {nameof(welcomePage.WelcomePageIsOpen)} to return true but was false");

                Log.Information($"clicking the Login button -> {nameof(RegistrationPositiveTest)}");
                welcomePage.ClickLoginButton();

                if (welcomePage.TheRegisterLoginModalIsVisible())
                {
                    string currentWindowHandle = _driverManager.Driver.CurrentWindowHandle;

                    Log.Information($"inserting email and clicking continue on login/registration popup -> {nameof(RegistrationPositiveTest)}");
                    loginRegistrationModal.InsertEmail(PayloadGenerator.GenerateEmail());
                    loginRegistrationModal.ClickContinue();

                    var newWindowHandle = _driverManager.Driver.WindowHandles
                        .FirstOrDefault(wh => wh != currentWindowHandle);

                    if (newWindowHandle is not null)
                    {
                        Log.Information($"switching to password window from login/registration popup -> {nameof(RegistrationPositiveTest)}");
                        _driverManager.Driver.SwitchTo().Window(newWindowHandle);
                    }

                    Log.Information($"inserting password and clicking create button -> {nameof(RegistrationPositiveTest)}");
                    loginRegistrationModal.InsertPassword(correctPassword!);
                    loginRegistrationModal.ClickCreateButton();

                    Log.Information($"switching back yo main window -> {nameof(RegistrationPositiveTest)}");
                    _driverManager.Driver.SwitchTo().Window(currentWindowHandle);
                }
                else
                {
                    Assert.IsTrue(loginSignupPage
                        .TheRegistrationSignUpPageIsOpen(), $"expected {nameof(loginSignupPage.TheRegistrationSignUpPageIsOpen)} to return true but was false");

                    Log.Information($"inserting email and clicking continue on login/registration page -> {nameof(RegistrationPositiveTest)}");
                    loginSignupPage.InsertEmail(PayloadGenerator.GenerateEmail());
                    loginSignupPage.ClickContinueButton();

                    Log.Information($"inserting password and clicking Create button registration page -> {nameof(RegistrationPositiveTest)}");
                    loginSignupPage.InsertPassword(correctPassword!);
                    loginSignupPage.ClickCreatAccountButton();
                }

                Assert.IsTrue(welcomePage
                    .IsLogedIn(), $"expected {nameof(welcomePage.IsLogedIn)} to return true but was false");

                Log.Information($"logout -> {nameof(RegistrationPositiveTest)}");
                welcomePage.ClickLogoutButton();

                test.Pass($"Test case: {nameof(RegistrationPositiveTest)} passed");
            }
            catch (System.Exception e)
            {
                test.Fail($"Test case: {nameof(RegistrationPositiveTest)} failed\n {e.StackTrace}");

                Log.Error($"Test case: {nameof(RegistrationPositiveTest)}. taking screenshot");
                _driverManager.TakeScreenshot(nameof(RegistrationPositiveTest));

                Log.Error($"Test case: {nameof(RegistrationPositiveTest)} Failed\n {e.StackTrace} \n message -> {e.Message}");
                throw;
            }
        }


        [TestCase(" ")]
        [TestCase("incorectEmail1")]
        [TestCase("IncorectEmail@gmail")]
        [TestCase("IncorectEmailgmail.com")]
        public void RegistrationNegativeWithInvalidEmailTest(string invalidEmail)
        {
            Log.Information(" ");
            Log.Information($" <<<< Starting invalid email negative registration case with email: {invalidEmail} >>>>");

            var test = _extent.CreateTest($"Registration with invalid email: '{invalidEmail}'");

            try
            {
                var welcomePage = new WelcomePage(_driverManager);
                var loginRegistrationModal = new LoginRegistrationModal(_driverManager);
                var loginSignupPage = new LoginSignupPage(_driverManager);

                Log.Information($"navigating to welcome page -> {nameof(RegistrationNegativeWithInvalidEmailTest)} with email: {invalidEmail}");
                welcomePage.GoTo();
                Assert.IsTrue(welcomePage.WelcomePageIsOpen(), $"expected {nameof(welcomePage.WelcomePageIsOpen)} to return true but was false");

                Log.Information($"clicking login button -> {nameof(RegistrationNegativeWithInvalidEmailTest)} with email {invalidEmail}");
                welcomePage.ClickLoginButton();

                if (welcomePage.TheRegisterLoginModalIsVisible())
                {
                    Log.Information(@$"inserting invalid value and clicking continue on login/register popup 
                        -> {nameof(RegistrationNegativeWithInvalidEmailTest)} with email {invalidEmail}");
                    loginRegistrationModal.InsertEmail(invalidEmail);
                    loginRegistrationModal.ClickContinue();

                    Assert.IsTrue(loginRegistrationModal.ValidationTextIsVisible(), $"expected invalid email validation");
                }
                else
                {
                    Assert.IsTrue(loginSignupPage
                        .TheRegistrationSignUpPageIsOpen(), $"expected {nameof(loginSignupPage.TheRegistrationSignUpPageIsOpen)} to return true but was false");

                    Log.Information(@$"inserting invalid value and clicking continue on login/register page 
                        -> {nameof(RegistrationNegativeWithInvalidEmailTest)} with email {invalidEmail}");
                    loginSignupPage.InsertEmail(invalidEmail);
                    loginSignupPage.ClickContinueButton();

                    Assert.IsTrue(loginSignupPage.IsInvalidEmail(), $"expected email validation");
                }

                test.Pass($"Negative Test case: {nameof(RegistrationNegativeWithInvalidEmailTest)} with invalid email: {invalidEmail} passed");
            }
            catch (System.Exception e)
            {
                test.Fail(@$"Negative Test case: {nameof(RegistrationNegativeWithInvalidEmailTest)} 
                    with invalid email: {invalidEmail} 
                    failed{e.StackTrace}");

                Log.Error($"Test case: {nameof(RegistrationNegativeWithInvalidEmailTest)}. taking screenshot");
                _driverManager.TakeScreenshot(nameof(RegistrationNegativeWithInvalidEmailTest) + $"'{invalidEmail}'");

                Log.Error(@$"Negative Test case: {nameof(RegistrationNegativeWithInvalidEmailTest)} with invalid email: {invalidEmail} 
                    Failed{e.StackTrace} 
                    message -> {e.Message}");
                throw;
            }
        }


        [TestCase(" ")]
        [TestCase("P$0")]
        [TestCase("password")]
        [TestCase("12345678910")]
        public void RegistrationNegativeWithInvalidPasswordTest(string invalidPassword)
        {
            Log.Information(" ");
            Log.Information($" <<<< Starting invalid password negative registration case with password: {invalidPassword} >>>>");

            var test = _extent.CreateTest($"Registration with invalid password: {invalidPassword} ");

            try
            {
                var welcomePage = new WelcomePage(_driverManager);
                var loginRegistrationModal = new LoginRegistrationModal(_driverManager);
                var loginSignupPage = new LoginSignupPage(_driverManager);

                Log.Information($"navigating to welcome page -> {nameof(RegistrationNegativeWithInvalidPasswordTest)} with password {invalidPassword}");
                welcomePage.GoTo();
                Assert.IsTrue(welcomePage.WelcomePageIsOpen(), $"expected {nameof(welcomePage.WelcomePageIsOpen)} to return true but was false");

                Log.Information(@$"clicking loginbutton
                     -> {nameof(RegistrationNegativeWithInvalidPasswordTest)} with password {invalidPassword}");
                welcomePage.ClickLoginButton();

                if (welcomePage.TheRegisterLoginModalIsVisible())
                {
                    string currentWindowHandle = _driverManager.Driver.CurrentWindowHandle;

                    Log.Information(@$"inserting email and clicking continue on login/registration popup 
                        -> {nameof(RegistrationNegativeWithInvalidPasswordTest)} with password {invalidPassword}");
                    loginRegistrationModal.InsertEmail(PayloadGenerator.GenerateEmail());
                    loginRegistrationModal.ClickContinue();

                    var newWindowHandle = _driverManager.Driver.WindowHandles
                        .FirstOrDefault(wh => wh != currentWindowHandle);

                    if (newWindowHandle is not null)
                    {
                        Log.Information(@$"switching to password window 
                            -> {nameof(RegistrationNegativeWithInvalidPasswordTest)} with password {invalidPassword}");
                        _driverManager.Driver.SwitchTo().Window(newWindowHandle);
                    }

                    Log.Information(@$"inserting invalid password and clicking create on password window 
                            -> {nameof(RegistrationNegativeWithInvalidPasswordTest)} with password {invalidPassword}");
                    _driverManager.Driver.SwitchTo().Window(newWindowHandle);
                    loginRegistrationModal.InsertPassword(invalidPassword);
                    loginRegistrationModal.ClickCreateButton();

                    Assert.IsTrue(loginRegistrationModal.PasswordIsInvalid(), "expected validation error while inserting password");

                    Log.Information(@$"switching back to main window 
                            -> {nameof(RegistrationNegativeWithInvalidPasswordTest)} with password {invalidPassword}");
                    _driverManager.Driver.SwitchTo().Window(currentWindowHandle);
                }
                else
                {
                    Assert.IsTrue(loginSignupPage
                        .TheRegistrationSignUpPageIsOpen(), $"expected {nameof(loginSignupPage.TheRegistrationSignUpPageIsOpen)} to return true but was false");

                    Log.Information(@$"inserting email and clicking continue on login/signup page 
                            -> {nameof(RegistrationNegativeWithInvalidPasswordTest)} with password {invalidPassword}");
                    loginSignupPage.InsertEmail(PayloadGenerator.GenerateEmail());
                    loginSignupPage.ClickContinueButton();

                    Log.Information(@$"inserting invalid password and clicking create on login/signup page 
                            -> {nameof(RegistrationNegativeWithInvalidPasswordTest)} with password {invalidPassword}");
                    loginSignupPage.InsertPassword(invalidPassword);
                    loginSignupPage.ClickCreatAccountButton();

                    Assert.IsTrue(loginSignupPage.IsInvalidPassword(), "expected invalid password validation");
                }
                test.Pass($"Negative Test case: {nameof(RegistrationNegativeWithInvalidEmailTest)} with invalid password: {invalidPassword} passed");
            }
            catch (System.Exception e)
            {
                test.Fail(@$"Negative Test case: {nameof(RegistrationNegativeWithInvalidPasswordTest)} 
                    with invalid password: {invalidPassword} 
                    failed {e.StackTrace}");

                Log.Error($"Test case: {nameof(RegistrationNegativeWithInvalidPasswordTest)}. taking screenshot");
                _driverManager.TakeScreenshot(nameof(RegistrationNegativeWithInvalidPasswordTest) + $"'{invalidPassword}'");

                Log.Error(@$"Negative Test case: {nameof(RegistrationNegativeWithInvalidPasswordTest)} 
                    with invalid password: {invalidPassword} 
                    Failed {e.StackTrace} 
                    message -> {e.Message}");
                throw;
            }
        }
    }
}