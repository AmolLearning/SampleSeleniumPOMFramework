using System;
using NUnit.Framework;
using SampleSeleniumPOMFramework.PageRepository;
using SampleSeleniumPOMFramework.Common;
using OpenQA.Selenium;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;

namespace SampleSeleniumPOMFramework
{
    [TestFixture]
    public class TechTest
    {
        [TestFixtureSetUp]
        //[SetUp]
        public void Setup()
        {
            DriverUtil.LaunchBrowser();
            
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

        



        [Test, TestCaseSource("Categories")]
        [Description("Test case using List as Data Source")]
        [Category("Regression")]
        public void SampleDataDrivenFromList(string category)
        {

            Console.WriteLine($"This is test {category}");

        }


        [Test, TestCaseSource("searchCriteriaFromLocal")]
        [Description("Sample using locally defined DataSource")]
        [Category("ExtendedRegression")]

        public void SampleDataDrivenTestFromLocalSource(string location,string checkinDate, string checkOutDate,string adults)

        {
          
           
        }


        static  List<string> Categories = new List<string> { "Cat1", "Cat2" };

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
