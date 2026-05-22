using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAutomation.Tests.PageObjectPattern.Models;
using TestAutomation.Tests.PageObjectPattern.PageObject.HomePage;

namespace TestAutomation.Tests.PageObjectPattern.Helpers
{
    public static class FruitHelper
    {
        public static IList<FruitWebElement> Parse(IList<IWebElement> fruits)
        {
            return fruits.Select(fruit => new FruitWebElement(fruit)).ToList();
        }

        //metodo que nos retorme el fruitModel
        public static IList<FruitModel> Parse(IList<FruitWebElement> fruits)
        {
            return fruits.Select(fruit => Parse(fruit)).ToList();
        }

        //metodo para convertir y separar el precio y convertir a decimal
        public static FruitModel Parse(FruitWebElement element)
        {
            var price = decimal.Parse(element.Price.Split(' ')[0]);
            return new FruitModel(element.Name, price, element.Description);
        }
    }
}