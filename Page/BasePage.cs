using Automation_task.Utils;
using OpenQA.Selenium;

namespace Automation_task.Page
{
    public class BasePage
    {
        protected DriverManager _driverManager;
        public BasePage(DriverManager driverManager)
        {
            _driverManager = driverManager;
        }
        protected bool CheckIfPageOpen(By by)
        {
            var elements = _driverManager.WAitAndFindElements(by);
            return elements.Count > 0;
        }
    }
}