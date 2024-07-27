using Automation_task.Utils;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace Automation_task.Page
{
    public class HotelsPage : BasePage
    {
        public HotelsPage(DriverManager driverManager) : base(driverManager)
        {
        }
        public static By PriceFilterLocator => By.XPath("//button[@name='budget']/strong[contains(text(),'Price')]");
        public static By MaxPriceInputLocator => By.CssSelector("[data-testid='price-filter-value-max']");
        public static By ApplyButtonLocator => By.CssSelector("[data-testid='filters-popover-apply-button']");
        public static By GuestRateFilterLocator => By.CssSelector("[name='guest_rating_filters']");
        public static By GoodGuestRateLabelLocator => By.CssSelector("[data-testid='Good-rating-label']");
        public static By CalendarCloseButton => By.CssSelector("[data-testid='calendar-button-close']");
        public static By HotelResultsLocator => By.CssSelector("[data-testid='result-list-ready']");

        public void ClickPriceFilter()
        {
            var element = _driverManager.WAitAndFindElement(PriceFilterLocator);
            var closeElements = _driverManager.WAitAndFindElements(CalendarCloseButton);
            if (closeElements.Count > 0)
                closeElements[closeElements.Count - 1].Click();
            element.Click();
        }

        public bool HotelsPageIsOpen()
        {
            return CheckIfPageOpen(HotelResultsLocator);
        }

        public void ClickGoodGuestRateLabel()
        {
            var element = _driverManager.WAitAndFindElement(GoodGuestRateLabelLocator);
            element.Click();
        }

        public void ClickGuestRateFilter()
        {
            var element = _driverManager.WAitAndFindElement(GuestRateFilterLocator);
            element.Click();
        }

        public void InsertMaxPrice(string maxPrice)
        {
            var input = _driverManager.WAitAndFindElement(MaxPriceInputLocator);
            input.SendKeys(maxPrice);
        }

        public void ClickApplyButton()
        {
            var element = _driverManager.WAitAndFindElement(ApplyButtonLocator);
            element.Click();
        }

        public bool FilterIsApplied(string filterName)
        {
            var el = _driverManager.WebDriverWait.Until(ExpectedConditions.ElementIsVisible(By.XPath($"//li[@data-testid='filter-pill']//span[contains(text(),'{filterName}')]")));
            return el.Text.Contains(filterName);
        }
    }
}