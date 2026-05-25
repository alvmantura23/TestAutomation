using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAutomation.Tests.PageObjectPattern.Helpers;
using TestAutomation.Tests.PageObjectPattern.Models;
using TestAutomation.Tests.PageObjetPattern.Helpers;
using TestAutomation.Tests.PageObjectPattern.PageObject.ShoppingCart;

namespace TestAutomation.Tests.PageObjectPattern.PageObject.HomePage

{
    public class HomePageObject
    {
        private readonly IWebDriver driver;

        public HomePageObject(IWebDriver driver)
        {
            this.driver = driver;
        }

        private List<IWebElement> DisplayedFruits =>
            driver.FindElements(By.ClassName("fruit"))
            .Where(fruit => fruit.Displayed)
            .ToList();

        public PageBarWebElement PageNavegation =>
            new PageBarWebElement(driver);

        public IList<FruitWebElement> DisplayedFruitWebElements()
        {
            return FruitHelper.Parse(DisplayedFruits);
        }

        public IList<FruitModel> DisplayedFruitModel() =>
            FruitHelper.Parse(DisplayedFruitWebElements());

        public SearchBarWebElement SearchBar =>
            new SearchBarWebElement(driver);

        // =========================
        // CARRITO DE COMPRAS
        // =========================

        private IWebElement ShoppingCartIcon =>
            driver.FindElement(By.Id("cart-icon"));

        // ESTE ES EL NUEVO METODO
        public ShoppingCartPageObject ClickShoppingCartIcon()
        {
            ShoppingCartIcon.Click();
            return new ShoppingCartPageObject(driver);
        }

        public bool IsShoppingCartIconNumberOfItems(int number)
        {
            try
            {
                WaitHelper.WaitForCondition(() =>
                    int.Parse(ShoppingCartIcon.Text).Equals(number));

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}