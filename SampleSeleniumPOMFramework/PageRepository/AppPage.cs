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
     public class AppPage
    {
        public IWebDriver _driver;
        public AppPage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(_driver, this);
        }


        [FindsBy(How = How.CssSelector, Using = "input[title^='Search']")]
        public IWebElement txtGlobalSearch;

        [FindsBy(How = How.ClassName, Using = "ms-srch-sb-searchLink")]
        public IWebElement imgSearch;

        public void ClickOnSearch()
        {
            imgSearch.Click();
        }


    }
}
