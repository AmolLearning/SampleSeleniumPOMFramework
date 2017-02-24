using System;
using NUnit.Framework;
using SeleniumUtilities;
using SampleSeleniumPOMFramework.PageRepository;
using SampleSeleniumPOMFramework.Common;
using OpenQA.Selenium;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace SampleSeleniumPOMFramework
{
    [TestFixture]
    public class SampleTests
    {
        [TestFixtureSetUp]
        //[SetUp]
        public void Setup()
        {
            DriverUtil.Setup();
            
        }

        [TestFixtureTearDown]
        //[TearDown]
        public void TearDown()
        {
            DriverUtil.Teardown();
      
        }

        [Test]
        [Description("Simple test")]
        [Category("Smoke")]
        public void SampleBasicTest()
        {

            //Navigate to hotel site
        
            DriverUtil.NavigateToURL(AppNameHelper.appBaseURL);
          
            //Enter Location
            NavigateTo.HotelPg.EnterLocation("Moscow, Russia");
         
            //Select Number of adults
            NavigateTo.HotelPg.SeclectNumberOfAdults("4");

            //Enter Checkin Date
            NavigateTo.HotelPg.Enter_CheckinDate("6/24/2017");
            //Enter Checkout Date
            NavigateTo.HotelPg.Enter_CheckOutDate("6/27/2017");

            //Click On search button
            NavigateTo.HotelPg.btnSearch.Click();
            string actualLocation = DriverUtil.GetText(By.CssSelector("span[class='captext ellipsis go-right']"));
            Assert.AreEqual("Moscow, Russia", actualLocation);
           

                       
        }

        [Test,TestCaseSource(typeof(DataSources), "SearchTermsFromCSV")]
        [Description("Test case using CSV file as a Data Source")]
        [Category("Regression")]
        public void SampleDataDrivenTestUsingCSVFile(string location,string chkInDate,string chkOutDate,string adults)
        {
            //Navigate to hotel site
            DriverUtil.NavigateToURL(AppNameHelper.appBaseURL);
            //Enter Location
            NavigateTo.HotelPg.EnterLocation(location);
            //Enter Checkin Date
            NavigateTo.HotelPg.Enter_CheckinDate(chkInDate);
            //Enter Checkout date
            NavigateTo.HotelPg.Enter_CheckOutDate(chkOutDate);
            //Select Number of adults
            NavigateTo.HotelPg.SeclectNumberOfAdults(adults);
            //Click On Search button
            NavigateTo.HotelPg.btnSearch.Click();
            //Read the location from results
            string actualLocation = DriverUtil.GetText(By.CssSelector("span[class='captext ellipsis go-right']"));
            //Verify location 
            Assert.AreEqual(location, actualLocation);

        }

                
        [Test, TestCaseSource("searchCriteriaFromLocal")]
        [Description("Sample using locally defined DataSource")]
        [Category("ExtendedRegression")]

        public void SampleDataDrivenTestFromLocalSource(string location,string checkinDate, string checkOutDate,string adults)

        {
            //Navigate to hotel site
            DriverUtil.NavigateToURL(AppNameHelper.appBaseURL);
            //Enter Location
            NavigateTo.HotelPg.EnterLocation(location);
            //Enter Checkin Date
            NavigateTo.HotelPg.Enter_CheckinDate(checkinDate);
            //Enter Checkout date
            NavigateTo.HotelPg.Enter_CheckOutDate(checkOutDate);
            //Select Number of adults
            NavigateTo.HotelPg.SeclectNumberOfAdults(adults);
            //Click On Search button
            NavigateTo.HotelPg.btnSearch.Click();
            //Read the location from results
            string actualLocation = DriverUtil.GetText(By.CssSelector("span[class='captext ellipsis go-right']"));
            //Verify location 
            Assert.AreEqual(location, actualLocation);
        }
        
        static object[] searchCriteriaFromLocal =
        {
            new object[] { "Delhi City, India", "6/24/2017","6/28/2017","1"},
            new object[] { "Vienna, Austria", "7/24/2017","7/28/2017","2" },
            new object[] { "Paris, France", "8/24/2017","8/28/2017","3" },
            new object[] { "Zurich, Switzerland", "9/24/2017","9/28/2017","4"},
            new object[] { "New York City, New York, United States", "10/24/2017","10/28/2017","5" },
            new object[] { "Vancouver, Canada", "11/24/2017","11/28/2017","1"},

        };




    }
}
