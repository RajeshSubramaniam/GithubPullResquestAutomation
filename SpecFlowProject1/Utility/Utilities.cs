using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Text;

namespace SpecFlowProject1.Utility
{
    public class Utilities
    {
        public static IWebDriver browser;
        public static IWebElement element;
       
        public void LaunchWebDriver(string uri)
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            browser = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),options);
            browser.Navigate().GoToUrl(uri);
         
        }

        public void Click(string locator)
        {
            WaitForElement();
            element =  browser.FindElement(By.XPath(locator));
            element.Click();
        }

        public void ClickUsingElementName(string locator, string name)
        {
            try
            {
                string updateLocator = string.Format(locator, name);
                element = browser.FindElement(By.XPath(updateLocator));
                element.Click();
            }
            catch (Exception e) { }
        }

        public string GetElementText(string locator)
        {
            string elementText = "";
            element = browser.FindElement(By.XPath(locator));
            elementText = element.Text;
            if (elementText == "")
                elementText = element.GetAttribute("innerText").TrimStart().TrimEnd();
            
            return elementText;
        }

        public Boolean CheckIfElementIsEnabled(string locator)
        {
            bool enabled = true;
            try {
                element = browser.FindElement(By.XPath(locator));
                if (!element.Enabled)
                { enabled = false; }
            }
            catch (NoSuchElementException e) { enabled = false; }
            return enabled;
        }

        public Boolean CheckIfElementExists(string locator)
        {
            bool exists = true;
            List<IWebElement> elementList = new List<IWebElement>();
            elementList.AddRange(browser.FindElements(By.XPath(locator)));

            if (elementList.Count == 0)
            {
                exists = false;
            }
            return exists;
        }

        public void CloseBrowser()
        {
            browser.Quit();
        }

        public void WaitForElement()
        {
            browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }
        public void Navigate(string uri)
        {
            browser.Navigate().GoToUrl(uri);
        }

        public void login(string uname, string pwd, string sigin)
        {
            try
            {
               // string unametestdata = ConfigurationManager.AppSettings["uname"].ToString();
               // string pwdtestdata = ConfigurationManager.AppSettings["pwd"].ToString();
                byte[] decodeByte = Convert.FromBase64String("TXljI3dvcmtzQDEyMw==");
                string decodedStr = Encoding.UTF8.GetString(decodeByte);

                element = browser.FindElement(By.XPath(uname));
                element.SendKeys("rajesh90.it@gmail.com");
                element = browser.FindElement(By.XPath(pwd));
                element.SendKeys(decodedStr);
                element = browser.FindElement(By.XPath(sigin));
                element.SendKeys(Keys.Enter);
            }
            catch (Exception e) { }
        }
    }
    public static class WebDriverExtensions
    {
        public static IWebElement FindElement(this IWebDriver driver, By by, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv => drv.FindElement(by));
            }
            return driver.FindElement(by);
        }
    }
}
