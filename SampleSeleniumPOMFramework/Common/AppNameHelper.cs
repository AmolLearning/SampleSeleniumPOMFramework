using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.IE;
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


namespace SampleSeleniumPOMFramework.Common
{
     public static class AppNameHelper
     {
        static public string appBaseURL
        {
            get
            {
                return ConfigurationManager.AppSettings["Environment"];
            }
        }





     }
}
