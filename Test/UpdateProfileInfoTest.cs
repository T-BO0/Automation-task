using System;
using System.Linq;
using Automation_task.Page;
using NUnit.Framework;
using Serilog;

namespace Automation_task.Test
{
    [TestFixture]
    public class UpdateProfileInfoTest : BaseTest
    {

        [SetUp]
        public void Setup()
        {
            Log.Information("  ");
            Log.Information("<<<< Starting update profile info tests [setup] >>>>");

            try
            {
                var welcomePage = new WelcomePage(_driverManager);
                var loginRegistrationModal = new LoginRegistrationModal(_driverManager);
                var loginSignupPage = new LoginSignupPage(_driverManager);
                var profilePage = new ProfilePage(_driverManager);
                var correctEmail = _configuration["CorrectEmail"];
                var correctPassword = _configuration["CorrectPassword"];

                Log.Information($"navigating to welcome page -> {nameof(UpdateProfileInfoTest.Setup)}");
                welcomePage.GoTo();
                Assert.IsTrue(welcomePage.WelcomePageIsOpen());

                Log.Information($"clicking login button -> {nameof(UpdateProfileInfoTest.Setup)}");
                welcomePage.ClickLoginButton();

                if (welcomePage.TheRegisterLoginModalIsVisible())
                {
                    string currentWindowHandle = _driverManager.Driver.CurrentWindowHandle;

                    Log.Information($"inserting email and clicking continue on login/signup popup -> {nameof(UpdateProfileInfoTest.Setup)}");
                    loginRegistrationModal.InsertEmail(correctEmail!);
                    loginRegistrationModal.ClickContinue();

                    var newWindowHandle = _driverManager.Driver.WindowHandles
                        .FirstOrDefault(wh => wh != currentWindowHandle);

                    if (newWindowHandle is not null)
                    {
                        Log.Information($"switching to password window -> {nameof(UpdateProfileInfoTest.Setup)}");
                        _driverManager.Driver.SwitchTo().Window(newWindowHandle);
                    }

                    Log.Information($"inserting password and clicking login on password window -> {nameof(UpdateProfileInfoTest.Setup)}");
                    loginRegistrationModal.InsertPassword(correctPassword!);
                    loginRegistrationModal.ClickLoginButton();

                    Log.Information($"switching back to main window -> {nameof(UpdateProfileInfoTest.Setup)}");
                    _driverManager.Driver.SwitchTo().Window(currentWindowHandle);
                }
                else
                {
                    Assert.IsTrue(loginSignupPage.TheRegistrationSignUpPageIsOpen());

                    Log.Information($"inserting email and clicking continue on login/signup page -> {nameof(UpdateProfileInfoTest.Setup)}");
                    loginSignupPage.InsertEmail(correctEmail!);
                    loginSignupPage.ClickContinueButton();

                    Log.Information($"inserting password and clicking login on login page -> {nameof(UpdateProfileInfoTest.Setup)}");
                    loginSignupPage.InsertPassword(correctPassword!);
                    loginSignupPage.ClickLoginButton();
                }
                Assert.IsTrue(welcomePage.IsLogedIn());

                Log.Information($"navigating to profile page-> {nameof(UpdateProfileInfoTest.Setup)}");
                profilePage.GoTo();
                Assert.IsTrue(profilePage.TheProfilePageIsOpen());
            }
            catch (Exception e)
            {
                Log.Error($"Tests : {nameof(UpdateProfileInfoTest)} setup Failed\n {e.StackTrace}\n message -> {e.Message}");
                throw;
            }
        }

        [TearDown]
        public void TearDown()
        {
            WelcomePage welcomePage = new(_driverManager);

            Log.Information($"navigating to welcome page -> {nameof(UpdateProfileInfoTest.TearDown)}");
            welcomePage.GoTo();
            Assert.IsTrue(welcomePage.WelcomePageIsOpen());

            Log.Information($"Logout -> {nameof(UpdateProfileInfoTest.TearDown)}");
            welcomePage.ClickLogoutButton();
        }

        [Test]
        public void TestUpdateProfileInfo()
        {
            Log.Information("  ");
            Log.Information("<<<< Starting update profile info positive >>>>");

            var test = _extent.CreateTest("Update profile info positive");

            try
            {
                var profilePage = new ProfilePage(_driverManager);
                var firstNmae = _configuration.GetSection("UpdateProfileInfoTest")["ValidFristName"];
                var lastName = _configuration.GetSection("UpdateProfileInfoTest")["ValidLastName"];

                Log.Information($"inserting new name and lastname, and clicking update button -> {TestUpdateProfileInfo}");
                profilePage.InsertNewFirstName(firstNmae!);
                profilePage.InsertNewLastName(lastName!);
                profilePage.ClickUpdatePersonalInfoButton();

                Assert.IsTrue(profilePage
                    .TheProfileinfoIsUpdated(), $"expected {nameof(profilePage.TheProfileinfoIsUpdated)} to return true but was false");

                test.Pass($"Test case: {nameof(TestUpdateProfileInfo)} passed");
            }
            catch (Exception e)
            {
                test.Fail($"Test case: {nameof(TestUpdateProfileInfo)} failed\n {e.StackTrace}");

                Log.Error($"Test case: {nameof(TestUpdateProfileInfo)}. taking screenshot");
                _driverManager.TakeScreenshot(nameof(TestUpdateProfileInfo));

                Log.Error($"Test case: {nameof(TestUpdateProfileInfo)} Failed\n {e.StackTrace} \n message -> {e.Message}");
                throw;
            }
        }

        [TestCase("Test_FirstName")]
        public void TestUpdateProfileInfoWithInvalidFirstNameSgouldShowvalidation(string invalidFirstname)
        {
            Log.Information("  ");
            Log.Information($"<<<< Starting update profile info negative with invalid name: {invalidFirstname} >>>>");

            var test = _extent.CreateTest("Update profile info negative");
            try
            {
                var profilePage = new ProfilePage(_driverManager);
                var lastName = _configuration.GetSection("UpdateProfileInfoTest")["ValidLastName"];

                Log.Information(@$"inserting invalid name and valid lastname, and clicking update button
                    -> {nameof(TestUpdateProfileInfoWithInvalidFirstNameSgouldShowvalidation)} with firstname {invalidFirstname}");
                profilePage.InsertNewFirstName(invalidFirstname);
                profilePage.InsertNewLastName(lastName!);
                profilePage.ClickUpdatePersonalInfoButton();

                Assert.IsTrue(profilePage.TheValidationErrorisVisible(), "validation does not accure");

                test.Pass(@$"Test case: {nameof(TestUpdateProfileInfoWithInvalidFirstNameSgouldShowvalidation)} 
                    with invalid first Name {invalidFirstname} passed");
            }
            catch (Exception e)
            {
                test.Fail($"Test case: {nameof(TestUpdateProfileInfoWithInvalidFirstNameSgouldShowvalidation)} failed\n {e.StackTrace}");

                Log.Error($"Test case: {nameof(TestUpdateProfileInfoWithInvalidFirstNameSgouldShowvalidation)}. taking screenshot");
                _driverManager.TakeScreenshot(nameof(TestUpdateProfileInfoWithInvalidFirstNameSgouldShowvalidation));

                Log.Error(@$"Test case: {nameof(TestUpdateProfileInfoWithInvalidFirstNameSgouldShowvalidation)} 
                    Failed {e.StackTrace} 
                    message -> {e.Message}");
                throw;
            }
        }


        [TestCase("Test_LastName")]
        public void TestUpdateProfileInfoWithInvalidLastNameShouldShowvalidation(string invalidLastName)
        {
            Log.Information("  ");
            Log.Information($"<<<< Starting update profile info negative with invalid last name: {invalidLastName} >>>>");

            var test = _extent.CreateTest("Update profile info negative invalid name");
            try
            {
                var profilePage = new ProfilePage(_driverManager);
                var firstName = _configuration.GetSection("UpdateProfileInfoTest")["ValidFristName"];

                Log.Information(@$"inserting valid name, invalid lastname and clicking update button
                     -> {nameof(TestUpdateProfileInfoWithInvalidLastNameShouldShowvalidation)} with lastname {invalidLastName}");
                profilePage.InsertNewFirstName(firstName!);
                profilePage.InsertNewLastName(invalidLastName);
                profilePage.ClickUpdatePersonalInfoButton();

                Assert.IsTrue(profilePage.TheValidationErrorisVisible(), "validation does not accure");

                test.Pass(@$"Test case: {nameof(TestUpdateProfileInfoWithInvalidLastNameShouldShowvalidation)} 
                    with invalid last Name {invalidLastName} passed");
            }
            catch (Exception e)
            {
                test.Fail($"Test case: {nameof(TestUpdateProfileInfoWithInvalidLastNameShouldShowvalidation)} failed\n {e.StackTrace}");

                Log.Error($"Test case: {nameof(TestUpdateProfileInfoWithInvalidLastNameShouldShowvalidation)}. taking screenshot");
                _driverManager.TakeScreenshot(nameof(TestUpdateProfileInfoWithInvalidLastNameShouldShowvalidation));

                Log.Error(@$"Test case: {nameof(TestUpdateProfileInfoWithInvalidLastNameShouldShowvalidation)} 
                    Failed {e.StackTrace} 
                    message -> {e.Message}");
                throw;
            }
        }
    }
}