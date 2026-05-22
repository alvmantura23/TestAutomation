using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestAutomation.Tests.Selectores
{
    public class SelectoresTests
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
        [TearDown]
        public void TearDown()
        {
            driver.Close(); //cierra la pestaña del navegador , solo cierra la ventana actual.
        }

        [Test]
        public void GetEachOfTheElements()
        {
            driver.FindElement(By.Id("SelectorsWeb")).Click();

            driver.FindElement(By.Id("myId")).Text.Should().Be("Element 1");

            driver.FindElement(By.ClassName("className")).Text.Should().Be("Element 2");

            //FORMAS DE SELECCIONAR EL ELEMENTO 3
            //opcion de la lista para el tercer elemento. el FindElements
            driver.FindElements(By.Id("myId"))[1].Text.Should().Be("Element 3");
            //opcion buscarlo dentro de la seccion div name elements
            var elementsSecction = driver.FindElement(By.Name("elements")); //filtra la seccion
            elementsSecction.FindElement(By.Id("myId")).Text.Should().Be("Element 3"); // dentro de la seccion filtrada busca por id

            //opcion usando directamente el selector de CSS
            driver.FindElement(By.CssSelector("[name='elements'] #myId")).Text.Should().Be("Element 3");

            //para el elemnet4, lo buscaremos por nombre
            driver.FindElement(By.Name("myName")).Text.Should().Be("Element 4");

            //para el element 5, por CSS 
            driver.FindElement(By.CssSelector("div [style='color:magenta']")).Text.Should().Be("Element 5");
            // para element 5 usando Xpath: esto es arriesgado pues depende de como se escriba el texto.
            driver.FindElement(By.XPath("//*[contains(text(),'Element 5')]")).Text.Should().Be("Element 5");

            //para ubicar el element 6 por CSS
            driver.FindElement(By.CssSelector("[autotestid='Element6']")).Text.Should().Be("Element 6");

            //para ubicar el element 7
            var divElementsSection = driver.FindElements(By.CssSelector("[name='elements'] div")); //para obtener la lista de div
            divElementsSection[5].Text.Should().Be("Element 7");
            divElementsSection[6].Text.Should().Be("Element 8");

            //para ubicar Home1 y Home2
            var homeButtons = driver.FindElements(By.CssSelector("[name='refs'] div>a")); //para obtener la lista home buttons
            homeButtons[0].Text.Should().Be("Home1");
            homeButtons[1].Text.Should().Be("Home2");

            //para ubicar los botones click me 1 y click me 2
            var home1 = homeButtons[0];
            var button2 =
                driver.FindElement(RelativeBy.WithLocator(By.CssSelector("button")).RightOf(home1));
            button2.Text.Should().Be("Click me 2");

            //para btener el valor de la tabla de usuarios inactivos Sandra
            var interativetable = driver.FindElements(By.ClassName("styled-table"))[1]; //nos retorna la tabla de usuarios inactivos, toma el 2do
            var inactiveUser = interativetable.FindElements(By.CssSelector("tbody tr")); //nos retorna 2 elementos
            Console.WriteLine(inactiveUser[1].Text);
        }

    }
}

