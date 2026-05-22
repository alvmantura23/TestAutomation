using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using FluentAssertions;
using System;
namespace TestAutomation.Tests.Frame
{
    public class FrameTests
    {
        #pragma warning disable NUnit1032
        IWebDriver driver;

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            driver.Url = "https://curso.testautomation.es";
        }

        [Test]
        public void FrameTest()
        {
            driver.FindElement(By.Id("DifferentFrames")).Click(); 
            
            driver.FindElement(By.CssSelector("button")).Click();

            driver.SwitchTo().Frame(0);

            var webElement = driver.FindElement(By.CssSelector("h2")).Text;

            driver.SwitchTo().DefaultContent();

            driver.SwitchTo().Frame(1);
            
            var webElementLeft = driver.FindElement(By.CssSelector("h2")).Text; // Agregar .Text
        }

        [TearDown]
        public void TearDownTest()
        {
            driver.Quit();
        }
    }
}

