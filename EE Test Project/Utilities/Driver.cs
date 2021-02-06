using System;
using System.Diagnostics;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace EE_Test_Project.Utilities
{
    public static class Driver
    {
        //These values come from config file
        public static string BaseUrl;
        public static int DefaultTimeout;

        public static IWebDriver TheDriver;
        public static WebDriverWait TheWait;
                
        /// <summary>
        /// Creates new static WebDriver and WebDriverWait in Driver class
        /// </summary>
        /// <param name="selectedBrowser">the browser to create WebDriver for</param>
        public static void OpenBrowser()
        {           
            TheDriver = new ChromeDriver();
            TheDriver.Manage().Window.Maximize();
                                
            TheWait = new WebDriverWait(TheDriver, TimeSpan.FromSeconds(DefaultTimeout));
        }

        /// <summary>
        /// Navigates to the given URL
        /// </summary>
        /// <param name="targetURL">URL to navigate to</param>
        public static void NavigateTo(string targetURL)
        {
            TheDriver.Navigate().GoToUrl(targetURL);
        }

        /// <summary>
        /// Returns the config value for the given key
        /// </summary>
        /// <param name="key"></param>
        /// <returns>key to get value for</returns>
        public static string GetValueFromConfigKey(string key)
        {
            var settings = ConfigHelper.GetConfig();
            return settings[key];
        }

        /// <summary>
        /// Returns the value of DefaultTimeoutSeconds from config file
        /// </summary>
        /// <returns></returns>
        public static int GetTimeouSeconds()
        {
            var time = GetValueFromConfigKey("DefaultTimeoutSeconds");
            return int.Parse(time);
        }

        
        /// <summary>
        /// Returns value of BaseUrl from config file
        /// </summary>
        /// <returns></returns>
        public static string GetBaseUrl()
        {
            var baseUrl = GetValueFromConfigKey("BaseUrl");
            return baseUrl;
        }

        /// <summary>
        /// Returns value of BasePageTitle from config file
        /// </summary>
        /// <returns></returns>
        public static string GetBasePageTitle()
        {
            var basePageTitle = GetValueFromConfigKey("BasePageTitle");
            return basePageTitle;
        }

        /// <summary>
        /// Compare given title to the title of page
        /// </summary>
        /// <param name="expectedTitle">expected page title</param>
        public static void CheckPageTitle(string expectedTitle)
        {
           try
            {
                TheWait.Until(ExpectedConditions.TitleIs(expectedTitle));
            }
            catch (Exception e)
            {
                throw new Exception($"Expected title " + expectedTitle + "  not found. " + TheDriver.Title + "found instead" + e);
            }
        }
     

        /// <summary>
        /// Quits the current driver
        /// </summary>
        public static void ShutDown()
        {
            TheDriver.Quit();
        }

    }
}
