using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using OpenQA.Selenium.Interactions;
using NLog;

namespace SampleSeleniumPOMFramework.Common
{
    /// <summary>
    /// This class has extensions to selenium webdriver's, driver, IWebelemt and IList[IWebElement] class
    /// </summary>
    static public class Extensions
    {

        /// <summary>
        /// To get text for all items in collection of specific type
        /// </summary>
        /// <param name="elementList"></param>
        /// <returns></returns>
        public static List<string> GetTextFromAllRows(this IList<IWebElement> elementList)
        {
            List<string> allRowsText = elementList.Select(x => x.Text).ToList();
            return allRowsText;
        }
        /// <summary>
        /// To get text for all rows for specified column number
        /// </summary>
        /// <param name="elementList"></param>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        public static List<string> GetAllRowValuesForColumn(this IList<IWebElement> elementList, int columnIndex)
        {
            List<string> allRowsText = elementList
                                       .Select(x => x.FindElements(By.CssSelector("td"))
                                       .ElementAt(columnIndex).Text).ToList();
            return allRowsText;
        }
        /// <summary>
        /// Get values from the selected Dropdown
        /// **This extension is not working correctly yet, need to update"
        /// </summary>
        /// <param name="elementList"></param>
        /// <returns></returns>

        public static List<string> GetValuesFromDropDown(this IList<IWebElement> elementList)

        {
            List<string> dropdownValues = elementList.Select(x => x.Text).ToList();

            return dropdownValues;

        }
        /// <summary>
        /// Selects specified value from "Select" tag dropdown
        /// </summary>
        /// <param name="element"></param>
        /// <param name="ItemValue"></param>

        public static void SelectByValueFromSelectTagDropDown(this IWebElement element, string ItemValue)
        {
            SelectElement SelectTagddl = new SelectElement(element);

            SelectTagddl.SelectByText(ItemValue);
            //loma
        }
        /// <summary>
        /// Gets all options from "select" Tag dropdown
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static List<string> GetAllOptionsFromSelectTagDropDown(this IWebElement element)
        {
            SelectElement SelectTagddl = new SelectElement(element);

            return SelectTagddl.Options.Select(x => x.Text).ToList();
        }

        /// <summary>
        /// Get a list of text values of each item in collection
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        
        public static List<string> GetTextValuesFromEachCollectionItem(this IList<IWebElement> collection)
        {
            List<string> collectionTextList = new List<string>();

            foreach (IWebElement item in collection)
            {

                collectionTextList.Add(item.Text);

            }

            return collectionTextList;
        }
        //dakhanea
        /// <summary>
        /// Gets the selected value from "Select" Tag dropdown
        /// </summary>
        /// <param name="element"></param>
        /// <param name="exceptThisString"></param>
        /// <returns></returns>
        public static string GetSelectedValueFromSelectTagDropDown(this IWebElement element,string exceptThisString="Select")
        {
            SelectElement SelectTagddl = new SelectElement(element);

            string selectedValue = SelectTagddl.AllSelectedOptions.Where(x => x.Text != exceptThisString).Single().Text;
            return selectedValue;
        }



        /// <summary>
        /// Select the value from dropdown using value from collection of dropdown values.
        /// User needs to click on dropdown before using this method
        /// </summary>
        /// <param name="elementList"></param>
        /// <param name="ItemValue"></param>
        public static void SelectItemByValuefromDrpDwn(this IList<IWebElement> elementList, string ItemValue)

        {
            elementList.Where(x => x.Text == ItemValue).Single().Click();

        }

        /// <summary>
        /// Used to click on the element from the table rows containing required text  
        /// </summary>
        /// <param name="elementList"></param>
        /// <param name="requiredText"></param>
        public static void ClickOnRowContainingText(this IList<IWebElement> elementList, string requiredText)
        {
            elementList.Where(x => x.Text.Contains(requiredText)).Single().Click();

        }
        
        /// <summary>
        /// It is used to set focus on required IWebelement
        /// </summary>
        /// <param name="element"></param>
        public static void SetFocusToElement(this IWebElement element)
        {

            var X_Cordinate = ((ILocatable)element).Coordinates.LocationInViewport.X;
            var Y_Cordinate = ((ILocatable)element).Coordinates.LocationInViewport.Y;
            DriverUtilities.ActionBuilder().MoveByOffset(X_Cordinate, Y_Cordinate)
                                      .MoveToElement(element)
                                      .Build()
                                      .Perform();
            //loma
        }

        /// <summary>
        /// It is used to set focus on required IWebelement,and then Click on it
        /// </summary>
        /// <param name="element"></param>
        public static void SetFocusToElementAndClick(this IWebElement element)
        {
            SetFocusToElement(element);
            element.ClickItem();

        }

        /// <summary>
        /// It is used to execute any custome JavaScript/jquery
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="script"></param>
        /// <returns></returns>

        public static object ExecuteJs(this IWebDriver driver, string script)
        {
            return ((IJavaScriptExecutor)driver).ExecuteScript(script);
        }
        //dakhanea
        /// <summary>
        /// This extension clicks element ignoring stale element exception 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="numberOfTries"></param>

        static public void ClickItem(this IWebElement element,int numberOfTries=10)
        {
            bool staleElementException = true;
            int tries = 0;
            while (staleElementException = true && tries < numberOfTries)
            {
                tries = tries + 1;
                try
                {                
                    element.Click();
                    break;
                }
                catch (StaleElementReferenceException e)
                {
                    Console.WriteLine(e.Message);
                    staleElementException = true;
                }
              
            }

        }
        /// <summary>
        /// Returns the text attribute of the element ignoring stale element exception
        /// </summary>
        /// <param name="element"></param>
        /// <param name="numberOfTries"></param>
        /// <returns></returns>

        static public string GetText(this IWebElement element, int numberOfTries = 10)
        {
            string elementText="initializedText";
            bool staleElementException = true;
            int tries = 0;
            while (staleElementException = true && tries < numberOfTries)
            {
                tries = tries + 1;
                try
                {
                    elementText = element.Text;
                 
                    break;
                }
                catch (StaleElementReferenceException e)
                {
                    Console.WriteLine(e.Message);
                    staleElementException = true;
                }
            }
            return elementText;
            //loma
        }

        /// <summary>
        ///  Returns the value of required attribute of the element ignoring stale element exception
        /// </summary>
        /// <param name="element"></param>
        /// <param name="attributeName"></param>
        /// <param name="numberOfTries"></param>
        /// <returns></returns>
        static public string GetAttributeValue(this IWebElement element,string attributeName="value", int numberOfTries = 10)
        {
            string elementText = "initializedText";
            bool staleElementException = true;
            int tries = 0;
            while (staleElementException = true && tries < numberOfTries)
            {
                tries = tries + 1;
                try
                {
                    elementText = element.GetAttribute(attributeName);

                    break;
                }
                catch (StaleElementReferenceException e)
                {
                    Console.WriteLine(e.Message);
                    staleElementException = true;
                }
                catch(ElementNotVisibleException e)
                {
                     Console.WriteLine(e.Message);
                    staleElementException = true;
                }
            }
            return elementText;
        }
        /// <summary>
        /// Enter text in stages, many times selenium fails to enter long text in a text box, this method solves that issue
        /// </summary>
        /// <param name="element"></param>
        /// <param name="textToBeEntered"></param>

        static public void EnterTextInSteps(this IWebElement element, string textToBeEntered)
        {
        
                try
                {
                foreach (var character in textToBeEntered)
                {
                    element.SendKeys(character.ToString());

                  //  Logger.Log.Info("Entered " + character);
                    Thread.Sleep(100);
                }
                                
                }
                catch (Exception )
                {
                throw;
                }
         }

        

        /// <summary>
        /// Enter the the text ignoring Stale element exception
        /// </summary>
        /// <param name="element"></param>
        /// <param name="textToBeEntered"></param>
        /// <param name="numberOfTries"></param>
        /// dakhanea
        static public void EnterText(this IWebElement element, string textToBeEntered, int numberOfTries = 10)
        {
            bool staleElementException = true;
            int tries = 0;
            while (staleElementException = true && tries < numberOfTries)
            {
                tries = tries + 1;
                try
                {
                    element.SendKeys(textToBeEntered);
                    break;
                }
                catch (StaleElementReferenceException e)
                {
                    Console.WriteLine(e.Message);
                    staleElementException = true;
                }
            }

        }
        /// <summary>
        /// Used to Scroll to the Iwebelement
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="element"></param>
        public static void ScrollToElement(this IWebDriver driver, IWebElement element)
        {
            if (element == null) return;

            var elemPosY = element.Location.Y;
            var elemPosX = element.Location.X;
            ((IJavaScriptExecutor)driver).ExecuteScript($"window.scroll({elemPosX}, {elemPosY});");
            Thread.Sleep(TimeSpan.FromSeconds(2));
        }

        public static void ScrollToElement(this IWebElement element)
        {
            Actions act = new Actions(DriverUtilities.driver);

            act.MoveToElement(element).Perform();


        }
        public static void ScrollToElementAndClickOnIt(this IWebElement element)
        {
            try
            {
                ScrollToElement(element);
                element.Click();
            }
            catch (Exception)
            {
                Logger.Log.Error("Failed to scroll to the element");
                throw;
            }

        }





    }
}
