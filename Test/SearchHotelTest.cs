using Automation_task.Page;
using NUnit.Framework;
using Serilog;

namespace Automation_task.Test
{
    [TestFixture]
    public class SearchHotelTest : BaseTest
    {
        [Test]
        public void SearchForHotel()
        {
            Log.Information(" ");
            Log.Information($" <<<< search hotel test started >>>>");

            var test = _extent.CreateTest($"search hotel test");

            try
            {
                var welcomePage = new WelcomePage(_driverManager);
                var hotelsPage = new HotelsPage(_driverManager);
                var location = _configuration.GetSection("SearchHotel")["Location"];
                var numberofAdults = _configuration.GetSection("SearchHotel")["NumberOfAdults"];
                var numberOfChildren = _configuration.GetSection("SearchHotel")["NumberOfChildren"];
                var numberOfRooms = _configuration.GetSection("SearchHotel")["NumberOfRooms"];
                var maxPrice = _configuration.GetSection("SearchHotel")["MaxPrice"];
                var maxPricePillName = "max";
                var RatingPillName = "Rating";

                Log.Information($"navigating to welcome page -> {nameof(SearchForHotel)}");
                welcomePage.GoTo();
                Assert.IsTrue(welcomePage
                    .WelcomePageIsOpen(), $"expected {nameof(welcomePage.WelcomePageIsOpen)} to return true but was false");

                Log.Information($"inserting the location -> {nameof(SearchForHotel)}");
                welcomePage.IsertSearchText(location!);

                Log.Information($"selecting checkin and checkout date -> {nameof(SearchForHotel)}");
                welcomePage.SelectCheckInDate();
                welcomePage.SelectCheckOutDate();

                Log.Information($"inserting number of adults, children, rooms and clicking apply button -> {nameof(SearchForHotel)}");
                welcomePage.InsertNumberOfAdults(numberofAdults!);
                welcomePage.InsertNumberOfChildren(numberOfChildren!);
                welcomePage.InsertNumberOfRooms(numberOfRooms!);
                welcomePage.ClickApplyButtonOnRoomsNGuestsFilter();

                Log.Information($"clicking search button -> {nameof(SearchForHotel)}");
                welcomePage.ClickOnTheSearchButton();
                Assert.IsTrue(hotelsPage
                    .HotelsPageIsOpen(), $"expected {nameof(hotelsPage.HotelsPageIsOpen)} to return true but was false");

                Log.Information($"inserting max price and clicking apply -> {nameof(SearchForHotel)}");
                hotelsPage.ClickPriceFilter();
                hotelsPage.InsertMaxPrice(maxPrice!);
                hotelsPage.ClickApplyButton();
                Assert.IsTrue(hotelsPage.FilterIsApplied(maxPricePillName),
                    $"expected {nameof(hotelsPage.FilterIsApplied)} with parameter {maxPricePillName} to return true but was false");

                Log.Information($"picking guset rate and clicking apply -> {nameof(SearchForHotel)}");
                hotelsPage.ClickGuestRateFilter();
                hotelsPage.ClickGoodGuestRateLabel();
                hotelsPage.ClickApplyButton();
                Assert.IsTrue(hotelsPage.FilterIsApplied(RatingPillName),
                    $"expected {nameof(hotelsPage.FilterIsApplied)} with parameter {RatingPillName} to return true but was false");

                test.Pass($"Test case: {nameof(SearchForHotel)} passed");
            }
            catch (System.Exception e)
            {
                test.Fail($"Test case: {nameof(SearchForHotel)} failed\n {e.StackTrace}");

                Log.Error($"Test case: {nameof(SearchForHotel)}. taking screenshot");
                _driverManager.TakeScreenshot(nameof(SearchForHotel));

                Log.Error($"Test case: {nameof(SearchForHotel)} Failed\n {e.StackTrace} \n message -> {e.Message}");
                throw;
            }
        }
    }
}