using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;

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
                
                //string query = string.Format("return document.evaluate(\"{0} \", document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;", locator);
                //IJavaScriptExecutor jsExe = (IJavaScriptExecutor)browser;
                //IWebElement element = (IWebElement)jsExe.ExecuteScript(query);

            var element = browser.FindElements(By.XPath(locator));

            if (element.Count == 0)
                {
                    exists = false;
                }
                return exists;
            
        }

        public void CloseBrowser()
        {
            if (browser!=null)
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

        public RestResponse GetResponseViaAPICall(string Uri, string authToken)
        {
            try
            {

                var client = new RestClient(Uri);
                var request = new RestRequest(Method.GET);
                request.AddHeader("apikey", authToken);
                var response = client.Execute(request);
                return (RestResponse)response;
            }
            catch (Exception e) { return null; }
        }

       
        public string ExtractProjectPath()
        {
            string wokringDirecory = Environment.CurrentDirectory;
            string projectDictionary = Directory.GetParent(wokringDirecory).Parent.FullName;
            projectDictionary = projectDictionary.Replace(@"\bin", "")+"\\TestData";
            return projectDictionary;
        }

        public string FetchData(string input)
        {
            string outputData = "";
            XmlDocument docToRead = new XmlDocument();
            string fileToLoad = ExtractProjectPath() + "\\Data.xml";
            docToRead.Load(fileToLoad);

            XmlNode node = docToRead.DocumentElement.SelectSingleNode("/root");
            foreach (XmlNode n in docToRead.DocumentElement.ChildNodes)
            {
                if (n.Name == input)
                {
                    outputData = n.InnerText;
                    break;
                }
            }
            return outputData;
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
