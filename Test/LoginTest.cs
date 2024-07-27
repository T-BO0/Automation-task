using System.Linq;
using Automation_task.Page;
using NUnit.Framework;
using Serilog;

namespace Automation_task.Test
{
    [TestFixture]
    public class LoginTest : BaseTest
    {
        [Test]
        public void TestLoginPositive()
        {
            Log.Information("  ");
            Log.Information("<<<< Starting positive Login Test >>>>");

            var test = _extent.CreateTest("Login positive");

            try
            {
                var welcomePage = new WelcomePage(_driverManager);
                var loginRegistrationModal = new LoginRegistrationModal(_driverManager);
                var loginSignupPage = new LoginSignupPage(_driverManager);
                var CorrectEmail = _configuration["CorrectEmail"];
                var correctPassword = _configuration["CorrectPassword"];

                Log.Information($"going to welcome page -> {nameof(TestLoginPositive)}");
                welcomePage.GoTo();
                Assert.IsTrue(welcomePage.WelcomePageIsOpen(), $"expected {nameof(welcomePage.WelcomePageIsOpen)} to returne true but was false");


                Log.Information($"clicking the login button -> {nameof(TestLoginPositive)}");
                welcomePage.ClickLoginButton();

                if (welcomePage.TheRegisterLoginModalIsVisible())
                {
                    string currentWindowHandle = _driverManager.Driver.CurrentWindowHandle;

                    Log.Information($"inserting email and clicking continue on popup -> {nameof(TestLoginPositive)}");
                    loginRegistrationModal.InsertEmail(CorrectEmail!);
                    loginRegistrationModal.ClickContinue();

                    var newWindowHandle = _driverManager.Driver.WindowHandles
                        .FirstOrDefault(wh => wh != currentWindowHandle);

                    if (newWindowHandle is not null)
                    {
                        Log.Information($"switching to password window from loginregistration popup -> {nameof(TestLoginPositive)}");
                        _driverManager.Driver.SwitchTo().Window(newWindowHandle);
                    }

                    Log.Information($"inserting passsword and clicking Login button on password window -> {nameof(TestLoginPositive)}");
                    loginRegistrationModal.InsertPassword(correctPassword!);
                    loginRegistrationModal.ClickLoginButton();

                    Log.Information($"switching back to main window -> {nameof(TestLoginPositive)}");
                    _driverManager.Driver.SwitchTo().Window(currentWindowHandle);
                }
                else
                {
                    Assert.IsTrue(loginSignupPage
                        .TheRegistrationSignUpPageIsOpen(), $"login/registration page is not open -> {nameof(TestLoginPositive)}");

                    Log.Information($"inserting email and clicking continue on loginregistration page -> {nameof(TestLoginPositive)}");
                    loginSignupPage.InsertEmail(CorrectEmail!);
                    loginSignupPage.ClickContinueButton();

                    Log.Information($"inserting password and clicking login on login page -> {nameof(TestLoginPositive)}");
                    loginSignupPage.InsertPassword(correctPassword!);
                    loginSignupPage.ClickLoginButton();
                }

                Assert.IsTrue(welcomePage.IsLogedIn(), $"expected to be logedin -> {nameof(TestLoginPositive)}");

                Log.Information($"logout -> {nameof(TestLoginPositive)}");
                welcomePage.ClickLogoutButton();

                test.Pass($"Test case: {nameof(TestLoginPositive)} passed");
            }
            catch (System.Exception e)
            {
                test.Fail($"Test case: {nameof(TestLoginPositive)} failed\n {e.StackTrace}");

                Log.Error($"Test case: {nameof(TestLoginPositive)}. taking screenshot");
                _driverManager.TakeScreenshot(nameof(TestLoginPositive));

                Log.Error($"Test case: {nameof(TestLoginPositive)} Failed\n {e.StackTrace} \n message -> {e.Message}");
                throw;
            }
        }
    }
}