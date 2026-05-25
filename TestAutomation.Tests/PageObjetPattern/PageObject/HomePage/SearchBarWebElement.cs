using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAutomation.Tests.PageObjectPattern.PageObject.HomePage
{
    public class SearchBarWebElement
    {
        // Variable interna para controlar el navegador de Selenium
        private readonly IWebDriver driver;

        // El constructor de la clase que recibe el driver desde el test
        public SearchBarWebElement(IWebDriver driver)
        {
            this.driver = driver;
        }

        // --- LOCALIZADORES DE ELEMENTOS WEB (PROPIEDADES DINÁMICAS) ---

        // Localiza la caja de texto (input) de búsqueda mediante su ID
        private IWebElement SearchInput => driver.FindElement(By.Id("search-input"));

        // Localiza el botón físico de buscar mediante su ID
        private IWebElement SearchButton => driver.FindElement(By.Id("search-button"));


        // --- ACCIONES DE INTERACCIÓN CON LA BARRA DE BÚSQUEDA ---

        /// <summary>
        /// Escribe un criterio en la caja de búsqueda y hace clic en el botón Search.
        /// </summary>
        /// <param name="text">El texto que se desea buscar (ej: "app")</param>
        /// <returns>Una nueva instancia de la página principal actualizada con los resultados</returns>
        /// 

        // la barra de input y boton sarch esta no sera publica
        private IWebElement InputSearchProduct => driver.FindElement(By.Id("product-search"));
        private IWebElement ButtonSearch => driver.FindElement(By.Id("search-button"));

        // acciones del input y boton search
        public SearchBarWebElement InputSearch(string termToSearch)
        {
            InputSearchProduct.Clear();
            InputSearchProduct.SendKeys(termToSearch);
            return this;
        }

        //pulsamos click sobre el boton
        public HomePageObject ClickSearch()
        {
            ButtonSearch.Click();
            return new HomePageObject(driver);
        }
        //pulsando Enter
        public HomePageObject ClickEnter()
        {
            new Actions(driver)
            .SendKeys(Keys.Enter)
            .Perform();
            return new HomePageObject(driver);
        }

        public HomePageObject ClickSearch(string text)
        {
            SearchInput.Clear();          // Borra cualquier texto previo que tenga la caja
            SearchInput.SendKeys(text);   // Escribe el texto recibido por parámetro
            SearchButton.Click();         // Hace clic físico en el botón Buscar
            return new HomePageObject(driver); // Retorna el PageObject de la página con los cambios visuales
        }

        /// <summary>
        /// Escribe un criterio en la caja de búsqueda y presiona la tecla Enter del teclado.
        /// </summary>
        /// <param name="text">El texto que se desea buscar (ej: "ape")</param>
        /// <returns>Una nueva instancia de la página principal actualizada con los resultados</returns>
        public HomePageObject EnterSearch(string text)
        {
            SearchInput.Clear();          // Limpia la caja de texto
            SearchInput.SendKeys(text);   // Escribe el texto recibido por parámetro
            SearchInput.SendKeys(Keys.Enter); // Simula la pulsación física de la tecla 'Enter'
            return new HomePageObject(driver); // Retorna el PageObject de la página con los cambios visuales
        }

        /// <summary>
        /// Limpia por completo la caja de búsqueda y presiona el botón para restablecer el catálogo.
        /// </summary>
        /// <returns>Una nueva instancia de la página principal con todas las frutas visibles de nuevo</returns>
        public HomePageObject ClearSearch()
        {
            SearchInput.Clear();          // Vacía por completo el campo de texto de búsqueda
            SearchButton.Click();         // Hace clic en buscar sin texto para quitar el filtro de la web
            return new HomePageObject(driver); // Retorna el PageObject restablecido a su estado inicial
        }
    }
}