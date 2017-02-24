using System;
using TechTalk.SpecFlow;
using SampleSeleniumPOMFramework.Common;
using SampleSeleniumPOMFramework.PageRepository;
using SeleniumUtilities;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;

namespace SampleSeleniumPOMFramework.SpecFlow.StepDefinitions
{


    [Binding]
    public class SearchHotelSteps
    {
        [BeforeFeature]
        static public void Setup()
        {
            DriverUtil.Setup();

        }

        [AfterFeature]
        static public void TearDown()
        {
            DriverUtil.Teardown();

        }



        [Given(@"Enter the destination")]
        public void GivenEnterTheDestination()
        {
            DriverUtil.NavigateToURL(AppNameHelper.appBaseURL);
          
            NavigateTo.HotelPg.EnterLocation("Moscow, Russia");
            
        }

        [Given(@"Enter the Chekin and Checkout Date")]
        public void GivenEnterTheChekinAndCheckoutDate()
        {
            
            NavigateTo.HotelPg.Enter_CheckinDate("6/24/2017");
            NavigateTo.HotelPg.Enter_CheckOutDate("6/27/2017");
            
        }
        
        [When(@"I click on Search button")]
        public void WhenIClickOnSearchButton()
        {
            NavigateTo.HotelPg.btnSearch.Click();
        }
        
        [Then(@"the options for entered destination should be displayed on the screen")]
        public void ThenTheOptionsForEnteredDestinationShouldBeDisplayedOnTheScreen()
        {
            string actualLocation = DriverUtil.GetText(By.CssSelector("span[class='captext ellipsis go-right']"));
            Assert.AreEqual("Moscow, Russia", actualLocation);
        }
    }
}
