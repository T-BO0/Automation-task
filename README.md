# trivago Automation Task
## Technologies 

- [.NET SDK 6](https://dotnet.microsoft.com/download) (for running NUnit tests)
- [Selenium WebDriver](https://www.selenium.dev/downloads/) (for browser automation)
- [ExtentReports](https://www.extentreports.com/) (for reporting)
- [NUnit](https://nunit.org/) (for testing framework)

## Installation and Usage

1. **Clone the Repository**

   ```bash
   # clone repor
   git clone https://github.com/T-BO0/Automation-task.git

   # navigate to the project file
   cd Automation-task

   # install dependencies
   dotnet restore

   #run tests
   dotnet test
   ```
2. **Change Browser**
   ```C#
      # in base test pass BrowserType enum (by default it is Chrome)
   ...
   _driverManager = new DriverManager();
   ...
   ```
3. **Run tests in Parallel**
   
   if you want to run the test in parallel you need to use [Parallelizable] and specify the scope. 

   example:
   ```C#
      # TestFixture specifies that it is a test class (it is optional)
   [TestFixture,Parallelizable(ParallelScope.Fixtures)]
   public class BaseTest
   {
   ...
   }
   ```
   by default Parallelizable attribute is added. if you want to run in sequential mode, just remove that attribute.
4. **Reporting**

   the project uses ExtentReports for reporting, which generates HTML file in the Reports folder + Screenshots if test fails.
