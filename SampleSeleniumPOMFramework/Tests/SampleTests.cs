using System;
using NUnit.Framework;
using SeleniumUtilities;
using SampleSeleniumPOMFramework.PageRepository;
using SampleSeleniumPOMFramework.Common;
using OpenQA.Selenium;
using System.Linq;
using System.Threading;

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

            
            DriverUtil.NavigateToURL(AppNameHelper.appBaseURL);
            Thread.Sleep(2000);
            NavigateTo.HotelPg.EnterLocation("Moscow, Russia");

            NavigateTo.HotelPg.SeclectNumberOfAdults("4");

            NavigateTo.HotelPg.Enter_CheckinDate("6/24/2017");
            NavigateTo.HotelPg.Enter_CheckOutDate("6/27/2017");
      
          
            NavigateTo.HotelPg.btnSearch.Click();


                       
        }

        [Test,TestCaseSource(typeof(DataSources), "SearchTerms")]
        [Description("Test case using CSV file as a Data Source")]
        [Category("Regression")]
        public void SampleDataDrivenTestUsingCSVFile(string location,string chkInDate,string chkOutDate,string adults)
        {
            DriverUtil.NavigateToURL(AppNameHelper.appBaseURL);

            Thread.Sleep(2000);

            NavigateTo.HotelPg.EnterLocation(location);

            NavigateTo.HotelPg.Enter_CheckinDate(chkInDate);

            NavigateTo.HotelPg.Enter_CheckOutDate(chkOutDate);

            NavigateTo.HotelPg.SeclectNumberOfAdults(adults);
            
            NavigateTo.HotelPg.btnSearch.Click();

        }

                
        [Test, TestCaseSource("searchCriteria")]
        [Description("Sample using locally defined DataSource")]
        [Category("ExtendedRegression")]

        public void SampleDataDrivenTestFromLocalSource(string location,string checkinDate, string checkOutDate,string adults)

        {
            DriverUtil.NavigateToURL(AppNameHelper.appBaseURL);
            Thread.Sleep(2000);

            NavigateTo.HotelPg.EnterLocation(location);

            NavigateTo.HotelPg.Enter_CheckinDate(checkinDate);

            NavigateTo.HotelPg.Enter_CheckOutDate(checkOutDate);

            NavigateTo.HotelPg.SeclectNumberOfAdults(adults);


            NavigateTo.HotelPg.btnSearch.Click();

        }
        
        static object[] searchCriteria =
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
