using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using OpenQA.Selenium;
using FluentAssertions;  // para el By.
using System.Diagnostics; // para el Stopwatch

namespace TestAutomation.Tests.Inicio
{
    [TestFixture]
    public class TestBasico
    {
        #pragma warning disable NUnit1032
        IWebDriver driver; // para definir el driver, es una variable global para que pueda ser utilizada en todos los métodos de prueba
        [SetUp]
        public void SetUp()
        {
            // aqui se pueden colocar las acciones que se deben ejecutar antes de cada test, como por ejemplo, abrir el navegador, navegar a la pagina web, etc.
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize(); // sentencia para maximizar navegador
            driver.Url = "https://curso.testautomation.es"; // para navegar a la pagina web que vamos a testear
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3); // esta indica a Selenium que cuando intente encontrar un elemento, espere hasta 3 segundos antes de lanzar una excepción de que el elemento no se encuentra, esto es útil para manejar elementos que pueden tardar en cargar o aparecer en la página.
        }
        [TearDown]
        public void TearDown()
        {
            driver.Quit(); // para liberar los recursos del navegador, cierra todas las ventanas abiertas por el driver y termina el proceso del driver.
        }
        [Test]
        public void TestBasicWebPage()
        {
            // NORMAL LOAD WEBSITE
            var normalLoadWeb = driver.FindElement(By.Id("NormalWeb")); // para ubicar por id el elemento de la pagina
            normalLoadWeb.Click(); // para hacer click en el elemento

            var titulo = driver.FindElement(By.CssSelector("h1")); // para obtener el elemento que tiene el tag h1
            titulo.Text.Should().Be("Normal load website"); // para obtener el texto del elemento y compararlo con el valor esperado, en este caso "Normal load website"
        }

        [Test]
        public void TestSlowLoadWebPage()
        {
            // SLOW LOAD WEBSITE
            var slowLoadWeb = driver.FindElement(By.Id("SlowLoadWeb")); // para ubicar por id el elemento de la pagina
            slowLoadWeb.Click();
            var titulo = driver.FindElement(By.Id("title")); // para obtener el elemento que tiene el id "title", en este caso es el titulo de la pagina lenta.
            titulo.Text.Should().Be("Slow load website");
        }

        [Test]
        public void TestSlowLoadTextWebPage()
        {
            var driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = "https://curso.testautomation.es";

            var slowLoadTextWeb = driver.FindElement(By.Id("SlowSpeedTextWeb")); // para ubicar por id el elemento de la pagina
            slowLoadTextWeb.Click();
            var titulo = driver.FindElement(By.Id("title"));
            WaitForCondition(() => IsTextElement(titulo, "Slow load website")); // es una expresion lambda
        }

        private bool IsTextElement(IWebElement element, string expectedText)
        {
            return element.Text.Equals(expectedText);
        }

        private void WaitForCondition(Func<bool> condition, int msTimeout = 4000)
        {
            // este codigo es muy util para controlar las esperas en los test, ya que permite esperar hasta que se cumpla una condicion, en este caso, la condicion es una funcion que devuelve un booleano, y el tiempo de espera es de 4 segundos por defecto.
            var stopWatch = new Stopwatch(); // Definimos una variable de tipo Stopwatch

            stopWatch.Start();

            Exception? ex;
            do
            {
                try
                {
                    ex = null;
                    if (condition())
                    {
                        return;
                    }
                }
                catch (Exception e)
                {
                    ex = e;
                }
            } while (stopWatch.ElapsedMilliseconds < msTimeout);

            stopWatch.Stop();

            if (ex != null)
            {
                throw new TimeoutException("Error executing the condition", ex);
            }
            throw new TimeoutException("Error the condition was false", ex); // si la condicion es falsa siempre
        }
    }
}