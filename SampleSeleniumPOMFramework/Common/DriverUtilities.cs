using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using System.Configuration;
using NUnit.Framework;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Threading;
using OpenQA.Selenium.PhantomJS;
using System.Linq.Expressions;
using System.Reflection;
using System.Security;
//using NLog;
//using Protractor;

namespace SampleSeleniumPOMFramework.Common
{   
    public class DriverUtilities
    {

        
        static public string browser = ConfigurationManager.AppSettings["browser"].ToLower();


        static private bool acceptNextAlert = true;
        static public IWebDriver driver;
        
        //static public string dateTimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH. mm.ss");
        /// <summary>
        /// This method returns the current dateTimeStamp
        /// </summary>
        public static string dateTimeStamp
        {
            get
            {
                return DateTime.Now.ToString("yyyy-MM-dd HH. mm.ss");
            }
        }


        #region Driver Setup And TearDown Methods
        /// <summary>
        /// This method is uesed to launch the browser (driver) based on the browser configured in App.config
        /// </summary>
        static public void LaunchBrowser()

        {

            //verificationErrors = new StringBuilder();
            //Get browser name from the config
            switch (browser.ToLower())
            {

                case "ie":

                    InternetExplorerOptions options_IE = new InternetExplorerOptions();

                    options_IE.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                    options_IE.IgnoreZoomLevel = false;
                    driver = new InternetExplorerDriver(options_IE);

                    driver.Manage().Window.Maximize();
                   // driver.Manage().Timeouts().ImplicitlyWait = TimeSpan.FromSeconds(2);
                    driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(2));

                    break;

                case "chrome":
                    driver = new ChromeDriver();
                 //   Logger.Log.Info("Launched Chrome without any user");
                    driver.Manage().Window.Maximize();
                   // driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
                    driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(2));

                    break;

                case "firefox":
                    driver = new FirefoxDriver();
                    driver.Manage().Window.Maximize();
                    //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
                    driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(2));

                    break;

                case "phantomjs":
                    PhantomJSOptions options_PJS = new PhantomJSOptions();        
                    driver = new PhantomJSDriver();
                    driver.Manage().Window.Maximize();
                    //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
                    driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(2));


                    break;


                default:

                    InternetExplorerOptions options_IEDefault = new InternetExplorerOptions();
                    options_IEDefault.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                    options_IEDefault.IgnoreZoomLevel = true;
                    driver = new InternetExplorerDriver(options_IEDefault);
                    driver.Manage().Window.Maximize();

                    break;

            }


        }
        /// <summary>
        /// This method is uesed to launch the browser (driver) based on the browser configured in App.config
        /// </summary>
        static public void LaunchBrowserWithSpecificUser(string domainName, string userName, string passWord)

        {

      
            //Get browser name from the config
            switch (browser.ToLower())
            {

                case "ie":

                    var serviceIE = InternetExplorerDriverService.CreateDefaultService(AssemblyDirectory);
                    var passwd = new SecureString();
                    foreach (var c in passWord)
                        passwd.AppendChar(c);
                    //serviceIE.StartupLoadUserProfile = true;
                    //serviceIE.StartupDomain = domainName;
                    //serviceIE.StartupUserName = userName;
                    //serviceIE.StartupPassword = passwd;
                    InternetExplorerOptions options_IE = new InternetExplorerOptions();
                    options_IE.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                    options_IE.IgnoreZoomLevel = false;
                    driver = new InternetExplorerDriver(serviceIE, options_IE);

                    driver.Manage().Window.Maximize();
                  //  driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
                    driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(2));

                    break;

                case "chrome":
                    var serviceChrome = ChromeDriverService.CreateDefaultService(AssemblyDirectory);
                    var paswd = new SecureString();
                    foreach (var c in passWord)
                        paswd.AppendChar(c);

                    //serviceChrome.StartupLoadUserProfile = true;
                    //Logger.Log.Info("Setting user parameters for launching chrome.....");
                    //serviceChrome.StartupDomain = domainName;
                    //serviceChrome.StartupUserName = userName;
                    //serviceChrome.StartupPassword = paswd;
                    

                    var options = new ChromeOptions();
                    //Added following line to ignore Adminstrator policy pop-up
                    options.AddAdditionalCapability("useAutomationExtension", false);
                    options.AddArgument("--no-sandbox");
                    options.AddArgument("--start-maximized");

                   // Logger.Log.Info("Initiated Options for launching chrome");
                    driver = new ChromeDriver(serviceChrome, options);

                  //  Logger.Log.Info("Launched Chrome with user "+userName);


                    //driver.Manage().Timeouts().ImplicitWait= TimeSpan.FromSeconds(2);
                    driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(2));
                    break;

                case "firefox":
                    driver = new FirefoxDriver();
                    driver.Manage().Window.Maximize();
                     driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(2));
                   // driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
                    break;

                case "phantomjs":
                    PhantomJSOptions options_PJS = new PhantomJSOptions();          
                    driver = new PhantomJSDriver();
                    driver.Manage().Window.Maximize();
                    driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(2));
                   // driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
                    break;


                default:

                    InternetExplorerOptions options_IEDefault = new InternetExplorerOptions();
                    options_IEDefault.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                    options_IEDefault.IgnoreZoomLevel = true;
                    driver = new InternetExplorerDriver(options_IEDefault);
                    driver.Manage().Window.Maximize();
                    break;

            }


        }
        /// <summary>
        /// Returns Assembly directory path
        /// </summary>
        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
        /// <summary>
        /// This method is used to close the browser(driver)
        /// </summary>
        static public void Teardown()
        {
            try
            {
                
                driver.Quit();
                KillChromeSessions();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            //Assert.AreEqual("", verificationErrors.ToString());
        }

/// <summary>
/// Used to Kill Chrome sessions after TearDown as sometime driver get closed but chrome session remains typically On Windows 10
/// </summary>
       static  public void KillChromeSessions()
        {

            try
            {
                Thread.Sleep(1000);

                foreach (Process proc in Process.GetProcessesByName("chrome"))
                {


                 //   Logger.Log.Info("Killing process..... " + proc.ProcessName + " which started at  " + proc.StartTime);

                    proc.Kill();
                    Thread.Sleep(2000);
                 //   Logger.Log.Info("Killed process " + proc.ProcessName + " with process id " + proc.Id);
                }
            }
            catch (Exception ex)
            {
              //  Logger.Log.Error("Could not kill all chrome process \r\n" + ex);
                // Assert.Fail("Could not kill all chrome process");
            }

        }
        /// <summary>
        /// This method is used login with difeerent users for a website
        /// Note:This will work for chrome browser by default, for IE you might need to do some settings
        /// </summary>
        /// <param name="siteUrl"></param>
        /// <param name="domainName"></param>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        static public void LoginWithDifferentUser(string siteUrl, string domainName, string userName, string passWord)
        {

            string httptype;

            if (siteUrl.Contains("https"))
            {

                httptype = "https://";
            }
            else
            {
                httptype = "http://";
            }
            //Remove https:// or http:// from base URL 
            string hostURL = siteUrl.Replace("https://", "").Replace("http://", "");
            //  string urlTobeNavigated = httptype + HttpUtility.UrlEncode(domainName + @"\" + userName + ":" + passWord + "@" + hostURL);
            string urlTobeNavigated = httptype + domainName + "%5C" + userName + ":" + passWord + "@" + hostURL;

            //          NavigateToURL( HttpUtility.UrlEncode(urlTobeNavigated));
            NavigateToURL(urlTobeNavigated);

        }

        #region JavaScriptFunctions
        /// <summary>
        /// Returns the JavaScript executor for performing JavaScript Operations
        /// </summary>
        /// <returns></returns>
        static public IJavaScriptExecutor JsExecutor()
        {
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;

            return js;

        }
        /// <summary>
        /// Click the provided identifier using JavaScript(jQuery) click event
        /// </summary>
        /// <param name="identifier"></param>

        static public void JsClick(string identifier)
        {
            //IJavaScriptExecutor js = DriverUtil.driver as IJavaScriptExecutor;

            //js.ExecuteScript("$('" + identifier + "').trigger('click')");

            JsExecutor().ExecuteScript("$('" + identifier + "').trigger('click')");

            //js.ExecuteScript("$('.ms-commentexpand-iconouter').trigger('click')");
        }
        /// <summary>
        /// Used to scroll till end of the page
        /// </summary>
        static public void JsScrollTillEnd()
        {
            JsExecutor().ExecuteScript("window.scrollTo(0,document.body.scrollHeight)");

        }

        /// <summary>
        /// Used to scroll to top of the page
        /// </summary>
        static public void JsScrollToTop()
        {
            //JsExecutor().ExecuteScript("scroll(0, -350);");
            JsExecutor().ExecuteScript("window.scrollTo(0,-document.body.scrollHeight)");
        }

        /// <summary>
        /// Scrolls page to the specified cordinates 
        /// </summary>
        /// <param name="xCordinate"></param>
        /// <param name="yCordinate"></param>
        static public void JsScrollTillPoint(string xCordinate, string yCordinate)
        {
            JsExecutor().ExecuteScript("scroll(" + xCordinate + "," + yCordinate + ");");

        }


        #endregion

        /// <summary>
        /// This method is used to check if the specified element is present on the page
        /// </summary>
        /// <param name="by"></param>
        /// <returns>It returns True/False based on element availability</returns>

        static public bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// This method is used to check if the specified element is present on the page
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        static public bool IsElementPresent(IWebElement element)
        {
            try
            {
                SetfocusOnIWebElement(element);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
            //loma
        }

        /// <summary>
        /// This method is used to check if the Alert/Pop-up is present 
        /// </summary>
        /// <returns></returns>
        static public bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }
        /// <summary>
        /// This method is used to close the Alert/pop-up and get message from the pop-up
        /// </summary>
        /// <returns></returns>
        static public string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }

        /// <summary>
        ///  This method is used to close the Alert/pop-up  by clicking  Yes/Accept button from Alet/Pop-up
        /// </summary>

        static public void AcceptAndCloseAlert()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                //string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                //return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }

        /// <summary>
        /// This method is used to handle exceptions during test execution,this is specifically useful to restart the 
        /// driver when gets stopped/Not-Responding due to some unexpected issues
        /// </summary>
        /// <param name="exp"></param>
        static public void ManageFailure(Exception exp, string screenshotpath=@"C:\")
        {
            Console.WriteLine(exp.HResult);
            Console.WriteLine("The message is  " + exp.Message);
            Console.WriteLine("The source is  " + exp.Source);
            Console.WriteLine("The Inner Eception is  " + exp.InnerException);
            Console.WriteLine("The stack strace  is  " + exp.StackTrace);
            Console.WriteLine("Target Site is    " + exp.TargetSite);
            Console.WriteLine("Data of exception is    " + exp.Data);

            if (exp.InnerException != null)


            {
                HandleNotNullInnerException(exp,screenshotpath);
            }
            else
            {
                HandleNullInnerException(exp,screenshotpath);

            }

        }
        /// <summary>
        /// This method is used to handle exceptions having some Inner exception message
        /// </summary>
        /// <param name="exp"></param>

        static public void HandleNotNullInnerException(Exception exp, string screenshotPath)
        {
            string expInnerEx = "OpenQA.Selenium.WebDriverException: The HTTP request to the remote WebDriver server for URL";
            string expMsg = "HTTP request to the remote WebDriver server for URL";
            if (exp.InnerException.ToString().Contains(expInnerEx) || exp.Message.Contains(expMsg))
            {
                //TakeScreenshot();Commented this as it further trows exception since IEDriver won't be available for taking screenshot
                //Close Driver Error dialog
                Process.Start(@"CloseDriverError.exe");
                Thread.Sleep(2000);
                //Reinstantiate driver
                LaunchBrowser();
            }
            else
            {
                TakeScreenshot(screenshotPath);
                Console.WriteLine("Another Exception");
            }

        }
        /// <summary>
        /// This methos is used to handle the exceptions with NULL inner exception messages
        /// </summary>
        /// <param name="exp"></param>

        static public void HandleNullInnerException(Exception exp,string screenshotPath)
        {
            string expectedExpMsg = "Unable to connect to the remote server";

            if (exp.Message.Contains(expectedExpMsg))
            {
                //TakeScreenshot();Commented this as it further trows exception since IEDriver won't be available for taking screenshot
                //Close Driver Error dialog
                Process.Start(@"CloseDriverError.exe");
                Thread.Sleep(2000);
                //Reinstantiate driver
                LaunchBrowser();

            }
            else
            {
                TakeScreenshot(screenshotPath);
                Console.WriteLine("Another Exception");
                //throw exp;
            }


        }

        #endregion


        #region BasicDriverMethods like Click,Enter,gettext
        /// <summary>
        /// Switch to tab using a predicate condition
        /// </summary>
        /// <param name="predicateExp"></param>

        static public void SwitchToWindow(Expression<Func<IWebDriver, bool>> predicateExp)
        {
            var predicate = predicateExp.Compile();
            foreach (var handle in driver.WindowHandles)
            {
                driver.SwitchTo().Window(handle);
                if (predicate(driver))
                {
                    return;
                }
            }

            throw new ArgumentException(string.Format("Unable to find window with condition: '{0}'", predicateExp.Body));
        }

        /// <summary>
        /// This method is used to Navigate to URL
        /// </summary>
        /// <param name="url"></param>

        static public void NavigateToURL(string url)
        {

            driver.Navigate().GoToUrl(url);

        }
        /// <summary>
        /// This method is used to click on IWebelement by using "By" locator
        /// </summary>
        /// <param name="locator"></param>

        static public void ClickOnElement(By locator)
        {
            driver.FindElement(locator).Click();

        }

        static public IWebElement FindElement(By locator)
        {
            IWebElement element = driver.FindElement(locator);
            return element;

        }



        /// <summary>
        /// This method is used to choose the dropdown by its identifier
        /// </summary>
        /// <param name="element"></param>
        /// <returns>It returns the dropdown controler of spefied identifier which can be further used for selection of  items using various options.</returns>
        static public SelectElement Dropdown(IWebElement element)
        {
            SelectElement select = new SelectElement(element);

            return select;


        }

        static public SelectElement Dropdown(By selectDrpDwnLocator)
        {
            SelectElement select = new SelectElement(driver.FindElement(selectDrpDwnLocator));

            return select;

        }
        /// <summary>
        /// This method is used to get the text value of specified element
        /// </summary>
        /// <param name="locator"></param>
        /// <returns>Returns text of the specified element</returns>
        static public string GetText(By locator)
        {
            string txtValue = driver.FindElement(locator).Text;
            return txtValue;

        }
        /// <summary>
        /// Get value of a specific attribute
        /// </summary>
        /// <param name="locator"></param>
        /// <param name="attributeName"></param>
        /// <returns></returns>

        static public string GetAttributeValue(By locator, string attributeName)
        {
            string txtValue = driver.FindElement(locator).GetAttribute(attributeName);
            return txtValue;

            //loma
        }


        /// <summary>
        /// This method is used to enter the text in the specified element
        /// </summary>
        /// <param name="locator"></param>
        /// <param name="requiredText"></param>
        static public void EnterText(By locator, string requiredText)
        {
            driver.FindElement(locator).Clear();
            driver.FindElement(locator).SendKeys(requiredText);

        }

        #endregion


        #region ActionBuilderMethods
        /// <summary>
        /// This method is used to return Actions object for building custom keyboard/mouse actions
        /// </summary>
        /// <returns></returns>
        static public Actions ActionBuilder()
        {
            Actions ActBuilder = new Actions(driver);
            return ActBuilder;
        }
        /// <summary>
        /// This method is used to set the mouse focus to specified element
        /// </summary>
        /// <param name="element"></param>
        static public void SetfocusOnIWebElement(IWebElement element)
        {
            ActionBuilder().MoveToElement(element).Build().Perform();

        }
        /// <summary>
        /// This method is used to set the mouse focus to the element specified by identifier
        /// </summary>
        /// <param name="element"></param>
        static public void SetfocusAndClickOnIWebElement(IWebElement element)
        {
            ActionBuilder().MoveToElement(element, 0, 0).Click().Build().Perform();

        }

        /// <summary>Method to set the postion of the Mouse cursor on screen
        /// <para>Need to specify X and Y cordinates for desired mouse cursor postion. Check <see cref="System.Drawing.Point"/> for more information.</para>
        /// </summary>

        static public void MoveCursor(int Xcoordinate, int Ycoordinate)
        {

            Cursor _Cursor = new Cursor(Cursor.Current.Handle);
            Cursor.Position = new Point(Xcoordinate, Ycoordinate);
            //Cursor.Position = new Point(Cursor.Position.X - xPosition, Cursor.Position.Y - yPosition);
            //Cursor.Clip = new Rectangle(_Cursor., _Cursor.Size);
        }

        #endregion


        #region WaitAndTimeOutMethods
        /// <summary>
        /// Implicit timeout in seconds
        /// </summary>
        /// <param name="seconds"></param>

        static public void ImplicitWait(int seconds = 10)
        {

            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(seconds));
            //loma

        }


        /// <summary>
        /// This method returns the object of WebdriverWait class for using methods like Wait.Until...
        /// </summary>
        /// <returns></returns>
        static public WebDriverWait Wait()
        {
            WebDriverWait Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            return Wait;
        }

        /// <summary>Method to wait for Element to be Visible
        /// <para>You need to pass the locator and time to wait for in seconds default time is 30 seconds<see cref="WebDriverWait"/> for more information.</para>
        /// <seealso cref="ExpectedConditions"/>
        /// </summary>
        static public void WaitForElementVisible(By locator, int seconds = 30)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));

            wait.Until(ExpectedConditions.ElementIsVisible(locator));



        }
        /// <summary>
        /// This method is used to wait for element to get invisible
        /// </summary>
        /// <param name="locator"></param>
        /// <param name="seconds"></param>
        static public void WaitForElementToGetInVisible(By locator, int seconds = 30)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));

            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(locator));


        }

        /// <summary>
        /// This method is used to wait for element to be ready before clicking
        /// </summary>
        /// <param name="locator"></param>
        /// <param name="seconds"></param>
        static public void WaitForElementIsClickable(By locator, int seconds = 30)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));

            wait.Until(ExpectedConditions.ElementToBeClickable(locator));
            //loma

        }
        /// <summary>
        /// This method is used to wait for element to be ready before clicking
        /// </summary>
        /// <param name="element"></param>
        /// <param name="seconds"></param>
        static public void WaitForElementIsClickable(IWebElement element, int seconds = 30)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));

            wait.Until(ExpectedConditions.ElementToBeClickable(element));

        }

        #endregion
        /// <summary>
        /// This method is used to take a screenshot,it also returns the location of screenshot
        /// </summary>
        /// <returns name="screenshotPath"></returns>

        static public string TakeScreenshot(string screenshotBaseLocation)
        {

            //Folder Name for saving screenshots
            string screenshotFolderName = "Screenshots " + DateTime.Now.ToString("yyyy_MM_dd");

            //Directory path for saving screenshots
            //TEmporarily commenting path to make C:\ drive path default
            //string directoryPath = @"..\..\..\" + screenshotFolderName;

           

            string directoryPath = screenshotBaseLocation + screenshotFolderName;


            if (Directory.Exists(directoryPath) == false)
            {
                DirectoryInfo di = Directory.CreateDirectory(directoryPath);

            }

            //Screenshot path for returning the complete screenshot URL with its name
            string screnshotPath = Path.Combine(Directory.GetCurrentDirectory(), directoryPath + @"\");

            string tcName = TestContext.CurrentContext.Test.Name.Replace('"', '\'').Replace(";", "-").Replace("/", "_");

            int tcNameLength = TestContext.CurrentContext.Test.Name.Replace('"', '\'').Replace(";", "-").Replace("/", "_").Length;

            if (tcNameLength > 50)
            {
                tcName = tcName.Substring(0, 50) + "...";
            }


            //Take Screenshot and save it @ specified location
            #region TakeScreenshotAndSaveIt


            ITakesScreenshot screenshotDriver = driver as ITakesScreenshot;

            Screenshot screenshot = screenshotDriver.GetScreenshot();

            String fp = screnshotPath + tcName + "Screenshot" + "_" + DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss_tt") + ".png";

            //screenshot.SaveAsFile(fp, ImageFormat.Png);

            //screenshot.SaveAsFile(fp, ScreenshotImageFormat.Png);



            #endregion

            #region Return screenshot path


            string machineName = System.Environment.MachineName.ToString();
            string fpWithHostName = "\\\\" + machineName + "\\" + fp;

            var uri = new System.Uri(fpWithHostName);
            var converted = uri.AbsoluteUri.Replace("/", "\\");
            return converted;
            #endregion
        }
        

        #region CollectionRelatedMethods
        static public List<IWebElement> GetCollection(By locator)
        {

            List<IWebElement> collection = driver.FindElements(locator).ToList();
            return collection;
        }
        /// <summary>
        /// This method is used to get the similar elements to the IWebElement list
        /// </summary>
        /// <param name="locator"></param>
        /// <returns></returns>

        static public List<IWebElement> GetCollectionToList(By locator)
        {

            List<IWebElement> webElementList = driver.FindElements(locator).ToList();
            return webElementList;
        }

        /// <summary>
        /// This method is used to get all text items of the IWebElement list to the string list
        /// </summary>
        /// <param name="locator"></param>
        /// <returns></returns>


        static public List<string> GetIwebElementTextToStringList(By locator)
        {
            List<IWebElement> allitems = driver.FindElements(locator).ToList();

            List<string> allIWebElemetstostringList = GetAllItemsToStringList(allitems);
            return allIWebElemetstostringList;

        }
        /// <summary>
        /// This method is used to get a list of "Text" values for items in IWebelement list
        /// </summary>
        /// <param name="allElements"></param>
        /// <param name="textFromWhereToTrim"></param>
        /// <returns></returns>

        static public List<string> GetAllItemsToStringList(List<IWebElement> allElements, string textFromWhereToTrim = "AmolDakhane")
        {

            List<string> actualElements = new List<string>();

            for (int i = 0; i < allElements.Count(); i++)
            {

                IWebElement element = allElements[i];

                string ElementText = element.Text.Trim();
                int index = ElementText.IndexOf(textFromWhereToTrim);
                if (index > 0)
                {
                    ElementText = ElementText.Substring(0, index);
                }
                actualElements.Add(ElementText.Trim());

            }

            return actualElements;

        }

        /// <summary>
        /// This method is used to get a list of "Text" values for items in IWebelement list excluding blank values
        /// </summary>
        /// <param name="locator"></param>
        /// <param name="textFromWhereToTrim"></param>
        /// <returns></returns>
        static public List<string> GetAllItemsToStringListExcludingBlank(By locator, string textFromWhereToTrim = "AmolDakhane")
        {

            List<IWebElement> webElementList = driver.FindElements(locator).ToList();


            List<string> actualElements = new List<string>();

            for (int i = 0; i < webElementList.Count(); i++)
            {

                IWebElement element = webElementList[i];

                string ElementText = element.Text.Trim();
                if (ElementText.Length > 0)
                {
                    int index = ElementText.IndexOf(textFromWhereToTrim);
                    if (index > 0)
                    {
                        ElementText = ElementText.Substring(0, index);
                    }
                    actualElements.Add(ElementText.Trim());
                    //loma
                }
            }

            if (actualElements.Last().Length < 1)
            {
                actualElements.RemoveAt(actualElements.Count - 1);
            }

            return actualElements;
        }
        /// <summary>
        /// This method is used to get a list of "Text" values for items from collection on page
        /// </summary>
        /// <param name="locator"></param>
        /// <param name="textFromWhereToTrim"></param>
        /// <returns></returns>

        static public List<string> GetCollectionFromPageToStringList(By locator, string textFromWhereToTrim = "AmolDakhane")
        {

            List<IWebElement> webElementList = driver.FindElements(locator).ToList();


            List<string> actualElements = new List<string>();

            for (int i = 0; i < webElementList.Count(); i++)
            {

                IWebElement element = webElementList[i];

                string ElementText = element.Text.Trim();
                int index = ElementText.IndexOf(textFromWhereToTrim);
                if (index > 0)
                {
                    ElementText = ElementText.Substring(0, index);
                }
                actualElements.Add(ElementText.Trim());

            }



            return actualElements;

        }
        /// <summary>
        /// This method is used to get a list of "Text" values for items from collection on page,without last blank value if any
        /// </summary>
        /// <param name="locator"></param>
        /// <param name="textFromWhereToTrim"></param>
        /// <returns></returns>

        static public List<string> GetCollectionFromPageToStringListWit(By locator, string textFromWhereToTrim = "AmolDakhane")
        {

            List<IWebElement> webElementList = driver.FindElements(locator).ToList();


            List<string> actualElements = new List<string>();

            for (int i = 0; i < webElementList.Count(); i++)
            {

                IWebElement element = webElementList[i];

                string ElementText = element.Text.Trim();
                int index = ElementText.IndexOf(textFromWhereToTrim);
                if (index > 0)
                {
                    ElementText = ElementText.Substring(0, index);
                }
                actualElements.Add(ElementText.Trim());

            }

            if (actualElements.Last().Length < 1)
            {
                actualElements.RemoveAt(actualElements.Count - 1);
            }
            return actualElements;

        }
        /// <summary>
        /// Splits each item from string collection and adds it to string list
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="splitByCharacter"></param>
        /// <returns></returns>

        public static List<string> SplitEachItemInStringCollectionAndAddtoList(List<string> collection, string[] splitByCharacter)
        {
            List<string> splittedItemList = new List<string>();


            foreach (string item in collection)
            {
                string[] splittedTerms = item.Split(splitByCharacter, StringSplitOptions.None);

                foreach (string term in splittedTerms)
                {
                    splittedItemList.Add(term);
                }


            }
            return splittedItemList;
        }

        /// <summary>
        /// This method is used to the get all values from specific column of table 
        /// by providing <table></table> tag identifier and column index
        ///Note: Column index starts from zero
        /// </summary>
        /// <param name="tableIdentifier"></param>
        /// <param name="columnNumber"></param>
        /// <returns></returns>

        static public List<string> GetValuesOfSpecificColumnByIndexFromTable(By tableIdentifier, int columnNumber)
        {
            IWebElement tableName = driver.FindElement(tableIdentifier);
            List<IWebElement> tr_tableElements = tableName.FindElements(By.CssSelector("tbody>tr")).ToList();

            List<string> valuesFromSelectedTableColumn = new List<string>();

            foreach (IWebElement tr_element in tr_tableElements)
            {
                List<IWebElement> td_collection = tr_element.FindElements(By.CssSelector("td")).ToList();
                valuesFromSelectedTableColumn.Add(td_collection[columnNumber].Text.Trim());

            }

            return valuesFromSelectedTableColumn;

        }
        /// <summary>
        /// This method is used to the get all values from specific column of table 
        /// by providing table identifier and column index
        /// </summary>
        /// <param name="tableIdentifier"></param>
        /// <param name="columnNumber"></param>
        /// <returns></returns>
        static public List<string> GetValuesOfSpecificColumnByIndexFromTable(IWebElement tableIdentifier, int columnNumber)
        {


            List<IWebElement> tr_tableElements = tableIdentifier.FindElements(By.CssSelector("tbody>tr")).ToList();

            List<string> valuesFromSelectedTableColumn = new List<string>();

            foreach (IWebElement tr_element in tr_tableElements)
            {
                List<IWebElement> td_collection = tr_element.FindElements(By.CssSelector("td")).ToList();
                valuesFromSelectedTableColumn.Add(td_collection[columnNumber].Text.Trim());

            }

            return valuesFromSelectedTableColumn;

        }


        /// <summary>
        /// Gets values from a specified column from table, using columname from table
        /// </summary>
        /// <param name="tableIdentifier"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>

        static public List<string> GetValuesOfColumnByNameFromTable(IWebElement tableIdentifier, string columnName)
        {

            List<IWebElement> tableHeaders = tableIdentifier.FindElements(By.CssSelector("thead>tr>th")).ToList();
            int indexOfRequiredColumnName = tableHeaders.FindIndex(x => x.Text.Contains(columnName));


            List<IWebElement> tr_tableElements = tableIdentifier.FindElements(By.CssSelector("tbody>tr")).ToList();

            List<string> valuesFromSelectedTableColumn = new List<string>();

            foreach (IWebElement tr_element in tr_tableElements)
            {
                List<IWebElement> td_collection = tr_element.FindElements(By.CssSelector("td")).ToList();
                valuesFromSelectedTableColumn.Add(td_collection[indexOfRequiredColumnName].Text.Trim());

            }
            //loma

            return valuesFromSelectedTableColumn;

        }


        /// <summary>
        /// Returns the row number of required text by using common row identifier and requiredtext
        /// </summary>
        /// <param name="locatorForRows"></param>
        /// <param name="stringToBeSearched"></param>
        /// <returns></returns>
        static public int GetIndexOfRowContainingSpecificText(By locatorForRows, string stringToBeSearched)
        {
            List<IWebElement> tableRows = GetCollection(locatorForRows).ToList();
            int indexOfRequiredText = tableRows.FindIndex(x => x.Text.Contains(stringToBeSearched));
            return indexOfRequiredText;

        }
        /// <summary>
        /// Returns the row number of required text by using table identifier and requiredtext
        /// </summary>
        /// <param name="tableIdentifier"></param>
        /// <param name="stringToBeSearched"></param>
        /// <returns></returns>
        static public int GetIndexOfTableRowContainingSpecificText(By tableIdentifier, string stringToBeSearched)
        {
            IWebElement table = driver.FindElement(tableIdentifier);

            List<IWebElement> tableRows = table.FindElements(By.CssSelector("tbody>tr")).ToList();
            int indexOfRequiredText = tableRows.FindIndex(x => x.Text.Contains(stringToBeSearched));
            return indexOfRequiredText;
            //loma

        }



        /// <summary>
        /// Returns the value from specific cell of a table as per given row and column index
        /// </summary>
        /// <param name="tableIdentifier"></param>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        public static string GetValueFromSpecificCellFromTable(By tableIdentifier, int rowIndex, int columnIndex)
        {
            IWebElement table = driver.FindElement(tableIdentifier);
            return table.FindElements(By.CssSelector("tbody>tr"))[rowIndex].FindElements(By.CssSelector("td"))[columnIndex].Text;
            //loma

        }

        /// <summary>
        ///  Returns the value from specific cell of a table as per given row and column index
        /// </summary>
        /// <param name="tableIdentifier"></param>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        public static string GetValueFromSpecificCellFromTable(IWebElement tableIdentifier, int rowIndex, int columnIndex)
        {

            return tableIdentifier.FindElements(By.CssSelector("tbody>tr"))[rowIndex].FindElements(By.CssSelector("td"))[columnIndex].Text;

        }
        //loma
        ///// <summary>
        ///// Returns requested attribute of element specified in specific cell# of table
        ///// </summary>
        ///// <param name="tableIdentifier"></param>
        ///// <param name="rowIndex"></param>
        ///// <param name="columnIndex"></param>
        ///// <param name="attribute"></param>
        ///// <returns></returns>
        //public static string GetAttributeOfElementFromSpecificCellOfTable(By tableIdentifier, int rowIndex, int columnIndex, string attribute)
        //{
        //    IWebElement table = driver.FindElement(tableIdentifier);


        //    return table.FindElements(By.CssSelector("tbody>tr"))[rowIndex].FindElements(By.CssSelector("td"))[columnIndex].GetAttribute(attribute);

        //}


        /// <summary>
        /// Clicks on specific cell of a table as per given row and column index
        /// </summary>
        /// <param name="txtTOBeClicked"></param>
        public static void ClickOnCellTextFromTable(string txtTOBeClicked)
        {
            driver.FindElement(By.XPath("//tr[td[contains(text(),'" + txtTOBeClicked + "')]]")).Click();

            //DriverUtil.driver.FindElement(By.XPath("//tr[td[contains(text(),'W695')]]")).Click();
            //DriverUtil.driver.FindElement(By.CssSelector("td:contains('W695')")).Click();
        }
        /// <summary>
        /// Clicks on specific cell# from table
        /// </summary>
        /// <param name="tableIdentifier"></param>
        /// <param name="rowIndex"></param>
        /// <param name="colIndex"></param>
        public static void ClickOnSpecificCellFromTable(By tableIdentifier, int rowIndex, int colIndex)
        {
            IWebElement table = driver.FindElement(tableIdentifier);

            table.FindElement(By.XPath("//tbody/tr[" + rowIndex + "]/td[" + colIndex + "]")).Click();

           
            //loma
        }
        /// <summary>
        /// Gets the number of rows in a table on the screen, by providing the 'By' selector for table
        /// </summary>
        /// <param name="tableLocator"></param>
        /// <returns></returns>
        public static int GetNumberOfRowsInTable(By tableLocator)
        {
            int numberOfRowsInTable = driver.FindElement(tableLocator).FindElements(By.CssSelector("tbody>tr")).Count;

            return numberOfRowsInTable;

        }
        /// <summary>
        /// Gets the number of rows in a table on the screen, by providing the identifier for table
        /// </summary>
        /// <param name="tableElement"></param>
        /// <returns></returns>
        public static int GetNumberOfRowsInTable(IWebElement tableElement)
        {
            int numberOfRowsInTable = tableElement.FindElements(By.CssSelector("tbody>tr")).Count;

            return numberOfRowsInTable;

        }

      

        /// <summary>
        /// Used to switch to SharePoint dlg
        /// </summary>
        static public void SwitchToDilog()
        {
            driver.SwitchTo().Frame(driver.FindElement(By.ClassName("ms-dlgFrame")));
        }

        /// <summary>
        /// Used to drag and drop the element 
        /// </summary>
        /// <param name="sourceElement"></param>
        /// <param name="destinationElement"></param>
        static public void DragAndDrop(IWebElement sourceElement, IWebElement destinationElement)
        {
            ActionBuilder().ClickAndHold(sourceElement)
            .MoveToElement(destinationElement)
            .Release(destinationElement)
            .Build()
            .Perform();
            //loma
        }


        #endregion
        //loma
        /// <summary>
        /// This method is used to select value from Select Tag dropdown 
        /// by ignoring a Stale Element Exception
        /// </summary>
        /// <param name="Element"></param>
        /// <param name="option"></param>
        /// <param name="numberOfTries"></param>
        static public void SelectFromDrpDwnIgnoringStaleElementException(IWebElement Element, string option, int numberOfTries = 5)
        {
            bool staleElementException = true;
            int tries = 0;
            while (staleElementException = true && tries < numberOfTries)
            {
                tries = tries + 1;
                try
                {
                    Element.SelectByValueFromSelectTagDropDown(option);
                    break;
                }
                catch (StaleElementReferenceException e)
                {
                    Console.WriteLine(e.Message);
                    staleElementException = true;
                }
            }
            //loma

        }
        //loma
        /// <summary>
        /// To Select a element from list dropdown ignoring the Stale Element exception
        /// </summary>
        /// <param name="IWebElementsCollection"></param>
        /// <param name="itemName"></param>
        /// <param name="numberOfTries"></param>
        static public void SelectFromListDrpDwnIgnoringStaleElementException(IList<IWebElement> IWebElementsCollection, string itemName, int numberOfTries = 5)
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
                        foreach (IWebElement item in IWebElementsCollection)
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
            //loma

        }
        //loma
        /// <summary>
        /// Clcik on Element ignoring the Stale Element exception
        /// </summary>
        /// <param name="Element"></param>
        /// <param name="numberOfTries"></param>

        static public void ClickOnElementIgnoringStaleElementException(IWebElement Element, int numberOfTries = 5)
        {
            bool staleElementException = true;
            int tries = 0;
            while (staleElementException = true && tries < numberOfTries)
            {
                tries = tries + 1;
                try
                {
                    Element.Click();
                    break;
                }
                catch (StaleElementReferenceException e)
                {
                    Console.WriteLine(e.Message);
                    staleElementException = true;
                }
            }

        }
        //loma
        /// <summary>
        /// Clear text from textbox ignoring the Stale Element exception
        /// </summary>
        /// <param name="Element"></param>
        /// <param name="numberOfTries"></param>
        static public void ClearTextIgnoringStaleElementException(IWebElement Element, int numberOfTries = 5)
        {
            bool staleElementException = true;
            int tries = 0;
            while (staleElementException = true && tries < numberOfTries)
            {
                tries = tries + 1;
                try
                {
                    Element.Clear();
                    break;
                }
                catch (StaleElementReferenceException e)
                {
                    Console.WriteLine(e.Message);
                    staleElementException = true;
                }
            }
            //loma
        }
        //loma
        /// <summary>
        /// Enter Text in text box ignoring the Stale Element exception
        /// </summary>
        /// <param name="Element"></param>
        /// <param name="text"></param>
        /// <param name="numberOfTries"></param>
        static public void EnterTextIgnoringStaleElementException(IWebElement Element, string text, int numberOfTries = 5)
        {
            bool staleElementException = true;
            int tries = 0;
            while (staleElementException = true && tries < numberOfTries)
            {
                tries = tries + 1;
                try
                {
                    Element.SendKeys(text);
                    break;
                }
                catch (StaleElementReferenceException e)
                {
                    Console.WriteLine(e.Message);
                    staleElementException = true;
                }
            }
            //loma
        }
        //loma
        /// <summary>
        /// To select a element from List dropdown
        /// </summary>
        /// <param name="IWebElementsCollection"></param>
        /// <param name="itemName"></param>
        static public void SelectFromListDropDown(IList<IWebElement> IWebElementsCollection, string itemName)
        {
            if (itemName.Length >= 1)
            {
                foreach (IWebElement item in IWebElementsCollection)
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


        static public void ClickOnElementUsingJavaScript(IWebElement element)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript("arguments[0].click();", element);

        }


        static public void ScrollToElementUsingJavaScript(IWebElement element)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript("arguments[0].scrollIntoView(true);", element);

        }
    }


}
