using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAutomation.Tests.PageObjectPattern.PageObject.HomePage
{
    public class FruitWebElement
    {
        //trabajar sobre un IwebElement
        private readonly IWebElement fruitWebElement;
        public string Name =>
        fruitWebElement.FindElement(By.TagName("h2")).Text;

        //ahora para el precio.
        public string Price => fruitWebElement.FindElement(By.TagName("p")).Text;

        //para la descripcion
        public string Description =>
        fruitWebElement.FindElements(By.TagName("p"))[1].Text;
        private IWebElement InputFieldQuantity => fruitWebElement.FindElement(By.CssSelector("[id$='Quantity']"));
        //para el boton
        private IWebElement ButtonAddToCart => fruitWebElement.FindElement(By.CssSelector("button"));

        // definimos su constructo. cuando se cree la variable fruitElement tenga un valor
        public FruitWebElement(IWebElement fruitWebElement)
        {
            this.fruitWebElement = fruitWebElement;
        }
        
        public void ClickAddToCar()=>ButtonAddToCart.Click();
        public FruitWebElement InputQuantity(int quantity)
        {
            InputFieldQuantity.Clear();
            InputFieldQuantity.SendKeys(quantity.ToString());
            return this;
        }


    }
}