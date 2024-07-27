using System;
using Automation_task.Utils;
using OpenQA.Selenium;

namespace Automation_task.Page
{
    public class WelcomePage : BasePage
    {
        public WelcomePage(DriverManager driverManager) : base(driverManager)
        {
        }

        public static By ProfileAvatarLocator => By.XPath("//button[@data-testid='header-profile-menu-desktop']/span/span[contains(@class,'blue')]");
        public static By ToastLoginLocator => By.ClassName("login-toast_toastRoot__kZJap");
        public static By ProfileButtonLocator => By.CssSelector("[data-testid='profile-menu-account-settings']");
        public static By LogoutButtonLocator => By.CssSelector("[data-testid=header-logout]");
        public static By LoginButtonLocator => By.XPath("//button/span/span[text()='Log in']");
        public static By LoginOrRegistrationModule => By.XPath("//div[contains(@class,'login-modal_container__Ud0IE')]");
        public static By SearchInputLocator => By.Id("input-auto-complete");
        public static By CalendarCheckInLocator => By.CssSelector("[data-testid='search-form-calendar-checkin-value']");
        public static By CalendarCheckInThisWeekendLabelLocator => By.CssSelector("[data-testid='thisWeekend-index-label']");
        public static By CalendarCheckoutLocator => By.CssSelector("[data-testid='search-form-calendar-checkout-value']");
        public static By CalendarCheckOutNextWeekendLabelLocator => By.CssSelector("[data-testid='nextWeekend-index-label']");
        public static By GuestsNRoomsLocator => By.CssSelector("[data-testid='search-form-guest-selector-value']");
        public static By AdultNumberInputLocator => By.XPath("//input[@data-testid='adults-amount']");
        public static By ChildrenNumberLabelLocator => By.XPath("//label[contains(text(),'Children')]");
        public static By ChildrenNumberInputLocator => By.CssSelector("[data-testid='children-amount']");
        public static By RoomLabelLocator => By.XPath("//label[contains(text(),'Room')]");
        public static By RoomInputLocator => By.CssSelector("[data-testid='rooms-amount']");
        public static By ApplyButtonLocator => By.CssSelector("[data-testid='guest-selector-apply']");
        public static By SearchButtonLocator => By.XPath("//div[@data-testid='search-form']/button[@data-testid='search-button-with-loader']");
        public static By WlecomePageUniqueElementLocator => By.Id("advertiser-bar-headline");


        public bool WelcomePageIsOpen()
        {
            return base.CheckIfPageOpen(WlecomePageUniqueElementLocator);
        }

        public void GoTo()
        {
            _driverManager.GoTo("https://www.trivago.com/", () => _driverManager.WAitAndFindElement(By.CssSelector("[data-testid=homepage-seo-about]")));
            _driverManager.WaitUntilPageLoadsCompletely();
        }

        public void ClickLoginButton()
        {
            var element = _driverManager.WAitAndFindElement(LoginButtonLocator);
            element.Click();

        }

        public void ClickLogoutButton()
        {
            var profileAvatarElement = _driverManager.WAitAndFindElement(ProfileAvatarLocator);
            _driverManager.Actions.MoveToElement(profileAvatarElement).Perform();
            var logOutButton = _driverManager.WAitAndFindElement(LogoutButtonLocator);
            _driverManager.Actions.MoveToElement(logOutButton).Click().Perform();
        }

        public bool IsLogedIn()
        {
            var elements = _driverManager.WAitAndFindElements(ToastLoginLocator);
            return elements.Count > 0;
        }

        public bool TheRegisterLoginModalIsVisible()
        {
            _driverManager.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
            var elements = _driverManager.Driver.FindElements(LoginOrRegistrationModule);

            return elements.Count > 0;
        }

        public void ClickProfileButton()
        {
            var profileAvatarElement = _driverManager.WAitAndFindElement(ProfileAvatarLocator);
            _driverManager.Actions.MoveToElement(profileAvatarElement).Perform();
            var element = _driverManager.WAitAndFindElement(ProfileButtonLocator);
            element.Click();
        }

        public void IsertSearchText(string keyWord)
        {
            var element = _driverManager.WAitAndFindElement(SearchInputLocator);
            element.Clear();
            element.SendKeys(keyWord);
            element.SendKeys(Keys.Enter);
        }

        public void SelectCheckInDate()
        {
            _driverManager.WAitAndFindElement(CalendarCheckInLocator).Click();
            var label = _driverManager.WAitAndFindElement(CalendarCheckInThisWeekendLabelLocator);
            label.Click();
        }

        public void SelectCheckOutDate()
        {
            _driverManager.WAitAndFindElement(CalendarCheckoutLocator).Click();
            var label = _driverManager.WAitAndFindElement(CalendarCheckOutNextWeekendLabelLocator);
            label.Click();
        }

        public void ClickGuestsNRooms()
        {
            _driverManager.WAitAndFindElement(GuestsNRoomsLocator).Click();
        }

        public void InsertNumberOfAdults(string numberOfAdult)
        {
            var input = _driverManager.WAitAndFindElement(AdultNumberInputLocator);
            input.Clear();
            input.SendKeys(Keys.Backspace);
            input.SendKeys(numberOfAdult);
        }

        public void InsertNumberOfChildren(string numberofChildren)
        {
            var input = _driverManager.WAitAndFindElement(ChildrenNumberInputLocator);
            input.Clear();
            input.SendKeys(Keys.Backspace);
            input.SendKeys(numberofChildren);
        }

        public void InsertNumberOfRooms(string numberOfRoomes)
        {
            var input = _driverManager.WAitAndFindElement(RoomInputLocator);
            input.Clear();
            input.SendKeys(Keys.Backspace);
            input.SendKeys(numberOfRoomes);
        }

        public void ClickApplyButtonOnRoomsNGuestsFilter()
        {
            var button = _driverManager.WAitAndFindElement(ApplyButtonLocator);
            button.Click();
        }

        public void ClickOnTheSearchButton()
        {
            var button = _driverManager.WAitAndFindElement(SearchButtonLocator);
            button.Click();
        }

    }

}