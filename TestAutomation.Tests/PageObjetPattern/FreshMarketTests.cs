using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Xml.Linq;
using TestAutomation.Tests.PageObjectPattern.Helpers;
using TestAutomation.Tests.PageObjectPattern.Models;
using TestAutomation.Tests.PageObjectPattern.PageObject.HomePage;
using TestAutomation.Tests.PageObjectPattern.PageObject.ShoppingCart;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TestAutomation.Tests.PageObjectPattern
{
    [TestFixture]
    public class FreshMarketTests
    {
        #pragma warning disable NUnit1032
        IWebDriver driver;

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            driver.Url = "https://curso.testautomation.es/FruitVegetablesShopWeb/index.html";
        }
        [TearDown]
        public void TearDown()
        {
            driver.Close(); //cierra la pestaña del navegador , solo cierra la ventana actual.
        }

        /// <summary>
        /// Verify that the provided fruits are displayed correctly into the shop.
        /// Please check that the content of all fruits are correct.
        /// </summary>
        [Test]
        public void VerifyThatFruitsAreCorrectlyDisplayed()
        {
            var expectedFruits = new List<FruitModel>
            {
                new FruitModel("Apple", 2.50m, "Crispy and delicious apples from the orchard."),
                new FruitModel("Banana", 1.00m, "Sweet and ripe bananas for a healthy snack."),
                new FruitModel("Orange", 1.50m, "Fresh and juicy oranges for a Vitamin C boost."),
                new FruitModel("Pear", 2.00m, "Sweet and juicy pears for a delightful taste."),
                new FruitModel("Strawberry", 3.00m, "Red and juicy strawberries for a sweet treat."),
                new FruitModel("Carrot", 1.20m, "Fresh and crunchy carrots for a healthy snack."),
                new FruitModel("Grape", 2.80m, "Sweet and delicious grapes for a refreshing taste."),
                new FruitModel("Watermelon", 0.80m, "Juicy and refreshing watermelon for hot days."),
                new FruitModel("Cherry", 2.70m, "Sweet and vibrant cherries for a delightful taste."),
                new FruitModel("Pumpkin", 1.80m, "Fresh and hearty pumpkin for a variety of recipes."),
                new FruitModel("Broccoli", 1.80m, "Fresh and nutritious broccoli for a healthy diet."),
                new FruitModel("Pineapple", 3.00m, "Sweet and tropical pineapples for a refreshing snack."),
                new FruitModel("Cucumber", 0.80m, "Crisp and refreshing cucumbers for salads and snacks."),
                new FruitModel("Potato", 1.20m, "Versatile and delicious potatoes for various dishes."),
                new FruitModel("Lemon", 2.00m, "Zesty and tangy lemons for cooking and beverages."),
                new FruitModel("Onion", 1.50m, "Flavorful and aromatic onions for cooking."),
                new FruitModel("Peach", 2.20m, "Sweet and juicy peaches for a delightful summer treat."),
                new FruitModel("Cabbage", 1.30m, "Crisp and crunchy cabbage for salads and coleslaw."),
                new FruitModel("Grapefruit", 2.40m, "Tangy and refreshing grapefruits for a healthy start."),
                new FruitModel("Kiwi", 3.20m, "Green and tangy kiwis for a tropical twist."),
                new FruitModel("Tomato", 1.60m, "Plump and juicy tomatoes for salads and sauces."),
                new FruitModel("Cantaloupe", 1.90m, "Sweet and aromatic cantaloupes for a refreshing treat."),
                new FruitModel("Avocado", 2.80m, "Creamy and nutritious avocados for salads and guacamole."),
                new FruitModel("Mango", 2.70m, "Exotic and sweet mangoes for a tropical delight."),
                new FruitModel("Raspberry", 3.50m, "Delicate and flavorful raspberries for desserts and snacking."),
                new FruitModel("Pomegranate", 4.00m, "Juicy and antioxidant-rich pomegranates for health-conscious individuals."),
                new FruitModel("Blackberry", 2.80m, "Sweet and juicy blackberries for desserts and smoothies."),
                new FruitModel("Cranberry", 3.20m, "Tart and antioxidant-packed cranberries for holiday dishes.")
            };

            var result = new List<FruitModel>();

            var homePage = new HomePageObject(driver); // se obtiene la pagin donde estan las frutas.

            result.AddRange(homePage.DisplayedFruitModel()); //con esto se obtienen 12 frutas de la page y se inserta }

            //para los otros rangos de frutas
            result.AddRange(homePage.PageNavegation.ClickButtonPage2().DisplayedFruitModel());
            result.AddRange(homePage.PageNavegation.ClickButtonPage3().DisplayedFruitModel());
            //para comprar los valores cargados de la pagina contra lo que tenemos:
            result.Should().BeEquivalentTo(expectedFruits);

            var displayedFruits = homePage.DisplayedFruitWebElements(); //con esto se obtienen 12 frutas de la page
            var displayedOfDisplayedFruits = displayedFruits.Count();//la catidad (12frutas) se ponen a una variable 
        }
        //IMPLEMENTACION DE 9.

        //Resumen
        //Nos implementar el siguiente Test:
        
        //1. Buscar ‘app’ pulsar Search, button y verique que solo Apple y Pineapple se nuestran
        //2. Limpiar el Search, pulsar el botón Search, y verificar que 12 frutas y vegetales se muestran
        //3. Buscar ‘ape’ pulsando la tecla ‘Enter’, y verificar que 2 frutas son mostradas Grape y GrapeFruit
        [Test]
        public void SearchTests()
        {
            var homePage = new HomePageObject(driver); // no retorna la pagina
            var foundFruits = homePage.SearchBar.InputSearch("app").ClickSearch().DisplayedFruitModel();
            foundFruits.Count.Should().Be(2); //segun la condicion debe retornar solo 2

            //para obtener los nombre
            var foundFruitsName = foundFruits.Select(fruit => fruit.Name).ToList();
            var expectFruitNames = new[] {"Pineapple", "Apple"};
            foundFruitsName.Should().BeEquivalentTo(expectFruitNames);   //compara los valores

            //para el test 2:
            homePage.SearchBar
            .InputSearch(string.Empty)
            .ClickSearch()
            .DisplayedFruitWebElements()
            .Count.Should().Be(12);

            //par el 3. que es similar al 1.
            foundFruits = homePage.SearchBar.InputSearch("ape").ClickEnter().DisplayedFruitModel();
            expectFruitNames = new[] { "Grape", "Grapefruit"};
            foundFruits.Select(fruit=>fruit.Name).Should().BeEquivalentTo(expectFruitNames);
        }

        [Test]
        public void ShoppingCartTest()
        {
            //tarea 1. verificar que el icon de arriba es 0
            var homePage = new HomePageObject(driver);
            homePage.IsShoppingCartIconNumberOfItems(0).Should().BeTrue();

            var expectedFruitsInCart = new List <FruitModel>(); //define la lista de productos.
            var DisplayedFruits = ()=> homePage.DisplayedFruitWebElements();

            // Tarea 2: + 10apple, 6 bananas, 5 Avocado 1 Pomegranete.. vericar el icon de shopping = 4
            expectedFruitsInCart.Add(AddItemToCart(DisplayedFruits(), "Apple", 10));

            expectedFruitsInCart.Add(AddItemToCart(DisplayedFruits(), "Banana", 6));

            homePage.PageNavegation.ClickButtonPage2();

            expectedFruitsInCart.Add(AddItemToCart(DisplayedFruits(), "Avocado", 5));

            homePage.PageNavegation.ClickButtonPage3();

            expectedFruitsInCart.Add(AddItemToCart(DisplayedFruits(), "Pomegranate", 1));

            //para verificar si el carro tiene el numero 4
            homePage.IsShoppingCartIconNumberOfItems(4).Should().BeTrue();

            //Test 3: Abrir el carro, verificar que tiene 4 elementos y sus valores son  correctos
            var cart = homePage.ClickShoppingCartIcon();

            cart.CartItemWebElements.Count().Should().Be(4);
            var item = () => cart.CartItemWebElements;
            for (var i = 0; i < 4; i++)
            {
                var fruit = expectedFruitsInCart[i];
                item().ElementAt(i).GetText().Should().Be($"{fruit.Name} {fruit.Price} €/Kg");

                fruit.Quantity.Should().Be(item().ElementAt(i).GetQuantity());
            }

            //para porbar que los totales son iguales
            var totalPrice = cart.GetTotalPrice();
            var TotalPriceFromItems = cart.GetTotalPriceFromItems();
            cart.GetTotalPrice().Should().Be(cart.GetTotalPriceFromItems());








            //borrar la granada
            item().ElementAt(3).ClickButtonRemove();

            //el numero del icon de carro es 3
            homePage.IsShoppingCartIconNumberOfItems(3).Should().BeTrue();

            //se actualiza bananas a 3
            item().ElementAt(1).InputQuantity(3);

            totalPrice = cart.GetTotalPrice();
            TotalPriceFromItems = cart.GetTotalPriceFromItems();

            //se verifica total son iguales
            cart.GetTotalPrice().Should().Be(cart.GetTotalPriceFromItems());

            //clic sobre boton Close
            cart.ClickButtonClose();










            FruitModel AddItemToCart(IList<FruitWebElement> displayedFruits, string fruitName, int quantity)
            {
                var fruitWebElement = displayedFruits.Single(fruit => fruit.Name.Equals(fruitName));
                fruitWebElement
                    .InputQuantity(quantity)
                    .ClickAddToCar();
                var fruitModel = FruitHelper.Parse(fruitWebElement);
                fruitModel.Quantity = quantity;
                return fruitModel;
            }
        } // cierra ShoppingCartTest

        // AQUI AFUERA DEL TEST
        private FruitModel AddItemToCart(
            IList<FruitWebElement> displayedFruits,
            string fruitName,
            int quantity)
        {
            var fruitWebElement =
                displayedFruits.Single(fruit => fruit.Name.Equals(fruitName));

            fruitWebElement
                .InputQuantity(quantity)
                .ClickAddToCar();

            var fruitModel = FruitHelper.Parse(fruitWebElement);

            fruitModel.Quantity = quantity;

            return fruitModel;
        }

    } // cierra la clase FreshMarketTests
} // cierra el namespace
