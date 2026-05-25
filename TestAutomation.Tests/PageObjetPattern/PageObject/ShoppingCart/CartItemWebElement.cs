using OpenQA.Selenium;
using System;
using System.Linq;

namespace TestAutomation.Tests.PageObjectPattern.PageObject.ShoppingCart
{
    public class CartItemWebElement
    {
        private readonly IWebElement item;

        public CartItemWebElement(IWebElement item)
        {
            this.item = item;
        }

        private IWebElement ButtonRemove =>
            item.FindElement(By.TagName("button"));

        private IWebElement InputFieldQuantity =>
            item.FindElement(By.TagName("input"));

        public string GetText()
        {
            return item.Text
                .Replace("Remove", "")
                .Trim()
                .Split('\n')[0];
        }

        public int GetQuantity() =>
            int.Parse(InputFieldQuantity.GetAttribute("value"));

        public decimal GetPrice()
        {
            var text = GetText();

            var priceText = text
                .Split(' ')[1]
                .Replace("€/Kg", "");

            return decimal.Parse(priceText);
        }

        public decimal GetTotalPrice() =>
            GetPrice() * GetQuantity();

        public void ClickButtonRemove() =>
            ButtonRemove.Click();

        public void InputQuantity(int quantity)
        {
            InputFieldQuantity.Clear();
            InputFieldQuantity.SendKeys(quantity.ToString());
            InputFieldQuantity.SendKeys(Keys.Enter);
        }
    }
}