using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.IE;
using SampleSeleniumPOMFramework.Common;
using System.Threading;

namespace SampleSeleniumPOMFramework.PageRepository
{
     public class HotelsPage
    {
        public IWebDriver _driver;
        public HotelsPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(_driver, this);
        }
            
        [FindsBy(How = How.CssSelector, Using = "div[class='easy-autocomplete']>input")]
        public IWebElement listLocation;

        [FindsBy(How = How.CssSelector, Using = "div[id='dpean1']>div>input")]
        public IWebElement txtCheckinDate;

        [FindsBy(How = How.CssSelector, Using = "div[id='dpd2']>div>input")]
        public IWebElement txtCheckOut;
        
        [FindsBy(How = How.Id, Using = "adults")]
        public IWebElement drpAdults;

        [FindsBy(How = How.Name, Using = "child")]
        public IWebElement drpChild;


        [FindsBy(How = How.CssSelector, Using = "button[type='submit']")]
        public IWebElement btnSearch;


        [FindsBy(How = How.CssSelector, Using = "div.eac-item")]
        public IList<IWebElement> DropDownOptionsLocationList { get; set; }

        public List<string> GetResultPgSections()
        {
            //List<string> resulPageSections = GlobalMethods.GetIwebElementTexttoStringList(By.XPath("//ul/li/h2[@class='ms-displayInline']"));
            List<string> resulPageSections = DriverUtil.GetIwebElementTextToStringList(By.CssSelector("ul>li>h2[class='ms-displayInline']"));
            return resulPageSections;
         }
        public void EnterLocation(string locationName)
        {
            if (locationName.Length >= 1)
            {
                DriverUtil.ClickOnElementIgnoringStaleElementException(listLocation);
                //listLocation.Click();
                Thread.Sleep(2000);
                DriverUtil.ClearTextIgnoringStaleElementException(listLocation);
                //listLocation.Clear();
                DriverUtil.EnterTextIgnoringStaleElementException(listLocation, locationName.Substring(0, 4));
                //listLocation.SendKeys(locationName.Substring(0, 4));
                SelectFromDrpDwnIgnoringStaleElementException(locationName);
                //SelectFromDropDownList(locationName);
                      

            }

        }

        public void Enter_CheckinDate(string checkinDate)
        {
            txtCheckinDate.Clear();
            txtCheckinDate.SendKeys(checkinDate);
            txtCheckinDate.SendKeys(OpenQA.Selenium.Keys.Tab);
        }
        public void Enter_CheckOutDate(string checkinDate)
        {
            txtCheckOut.Clear();
            txtCheckOut.SendKeys(checkinDate);
            txtCheckinDate.SendKeys(OpenQA.Selenium.Keys.Tab);
        }

        public void SeclectNumberOfAdults(string number)
        {
            drpAdults.SelectByValueFromSelectTagDropDown(number);

        }

        public void SeclectNumberOfChilds(string number)
        {
           drpChild.SelectByValueFromSelectTagDropDown(number);

        }



        public List<string> getRefinerValues()
        {
            List<string> refinerList = DriverUtil.GetIwebElementTextToStringList(By.ClassName("ms-ref-refinername"));
            return refinerList;
        }



        public void SelectFromDrpDwnIgnoringStaleElementException(string itemName, int numberOfTries = 5)
        {
            bool staleElementException = true;
            int tries = 0;
            while (staleElementException = true && tries < numberOfTries)
            {
                tries = tries + 1;
                try
                {
                    if (itemName.Length >= 1)
                    {
                        foreach (IWebElement item in DropDownOptionsLocationList)
                        {

                            if (item.Text == itemName)
                            {
                                Thread.Sleep(1500);
                                item.Click();
                                break;
                            }
                        }
                    }
                    break;
                }
                catch (StaleElementReferenceException e)
                {
                    Console.WriteLine(e.Message);
                    staleElementException = true;
                }
            }

        }

        public void SelectFromDropDownList(string itemName)
        {
            if (itemName.Length >= 1)
            {
                foreach (IWebElement item in DropDownOptionsLocationList)
                {

                    if (item.Text == itemName)
                    {
                        Thread.Sleep(1500);
                        item.Click();
                        break;
                    }
                }
            }
        }

        public void SelectFromItemDropDownContatiningItem(string itemName)
        {
            if (itemName.Length >= 1)
            {
                foreach (IWebElement item in DropDownOptionsLocationList)
                {

                    if (item.Text.Contains(itemName))
                    {
                        Thread.Sleep(1500);
                        item.Click();
                        break;

                    }
                }
            }
        }

    }
}
