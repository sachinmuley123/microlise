using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    [TestFixture]
    public class SearchKeyword
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;
        
        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            baseURL = "http://www.microlise.com/";
            verificationErrors = new StringBuilder();
        }
        
        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }
        
        [Test]
        public void TheSearchKeywordTest()
        {
            driver.Navigate().GoToUrl("http://www.microlise.com/resources/");
            Assert.AreEqual("Resources - Microlise", driver.Title);
            driver.FindElement(By.LinkText("Resources")).Click();
            Assert.AreEqual("Resources - Microlise", driver.Title);
            driver.FindElement(By.Id("searchbox")).Clear();
            driver.FindElement(By.Id("searchbox")).SendKeys("TeleMatic");
            driver.FindElement(By.CssSelector("div.columns > #searchsubmit")).Click();
            Assert.AreEqual("Search for \"TeleMatic\" | Microlise", driver.Title);
            driver.FindElement(By.LinkText("Should Drivers Have Direct Access to Their Telematics Performance Data?")).Click();
            Assert.IsTrue(Regex.IsMatch(driver.Title, "^exact:Should Drivers Have Direct Access to Their Telematics Performance Data[\\s\\S] - Microlise$"));
        }
        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        
        private bool IsAlertPresent()
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
        
        private string CloseAlertAndGetItsText() {
            try {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert) {
                    alert.Accept();
                } else {
                    alert.Dismiss();
                }
                return alertText;
            } finally {
                acceptNextAlert = true;
            }
        }
    }
}
