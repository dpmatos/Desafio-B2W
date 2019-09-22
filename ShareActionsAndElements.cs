using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections;
using System.IO;
using System.Linq;
using OpenQA.Selenium.Interactions;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace Desafio
{
    public abstract class ShareActionsAndElements
    {
        #region Construtor        

        public ShareActionsAndElements(IWebDriver driver)
        {
            if (driver == null)
                throw new ArgumentNullException("driver");

            this.driver = driver;
        }

        #endregion

        #region Atributos
        
        public string url => DriverManager.UrlBase.ToString();

        [Obsolete("Para testes de selenium grid este caminho não funcionará")]
        //Path padrão solution + instancia webdriver
        public static string pathExecucao = new FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).Directory.FullName;

        private IWebDriver driver = null;
        protected IWebDriver Driver
        {
            get
            {
                if (driver == null)
                    driver = DriverManager.ResolverDriver();
                return driver;
            }
        }
        #endregion        

        #region Métodos
        
        public void AbrirBrowser()
        {
            Driver.Url = url;
            //_driver.Manage().Window.Maximize();
        }
        
        protected IWebElement ClickJS(By @by)
        {
            var element = FindBy(@by);

            IJavaScriptExecutor executor = (IJavaScriptExecutor)Driver;
            executor.ExecuteScript("arguments[0].click();", element);

            return element;
        }
        
        protected IWebElement FindBy(By @by)
        {
            var elem = FindBy(@by, ExpectedConditions.ElementExists(@by));
            return elem;
        }               

        private TResult FindBy<TResult>(By @by, Func<IWebDriver, TResult> expectation)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));

            //O IF a seguir garante que estamos encontrando o elemento correto. 
            //Se vier mais de um elemento estourar exception.
            if (!typeof(IEnumerable).IsAssignableFrom(typeof(TResult)) && !expectation.Method.Name.Contains("Invisibility"))
            {
                var elems = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(@by))
                    .Where(x => x.Displayed).ToArray();
                // verificar uma solucao para planos e beneficios
                //if (elems.Length > 1)
                //    Assert.Fail("Mais de um elemento encontrado para o critério {0}", @by);
            }

            try
            {
                var elem = wait.Until(expectation);
                return elem;
            }
            catch (Exception)
            {
                Assert.Fail("O elemento não foi encontrado: {0}", @by);
                throw;
            }
        }
        
        protected IWebElement FindByAndClick(By @by)
        {
            var elem = FindBy(@by, ExpectedConditions.ElementToBeClickable(@by));
            Actions action = new Actions(this.driver);
            action.MoveToElement(elem).Click().Perform();

            return elem;
        }

        public void SelecionaElementoPorTexto(By @by, string txtElement)
        {
            var elem = FindBy(@by);
            //var elem = FindByAndClick(@by);
            new SelectElement(elem).SelectByText(txtElement);
        }

        protected void SendkeysBy(By @by, string text)
        {
            FindBy(by).SendKeys(text);
        }

        protected void ClicarNoLink(string link)
        {
            if (string.IsNullOrWhiteSpace(link))
                throw new ArgumentNullException(nameof(link));
            if (link.StartsWith("/"))
                link = link.Substring(1);
            var @by = By.CssSelector(string.Format("a[href='{0}']", link));
            //var element = FindBy(@by, ExpectedConditions.ElementToBeClickable(@by));
            //var href = element.GetAttribute("href");

            //element.Click();
            //element.SendKeys(Keys.Enter);
            ClickJS(@by);

            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditions.UrlContains(link));
        }

        public void setUp()
        {
            AbrirBrowser();
        }

        public void Deslogar()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("localStorage.clear()");
        }

        public void tearDown()
        {
            Deslogar();
            DriverManager.FinalizarDriverSeNaoReutilizavel(Driver);
        }

        #endregion
    }    
}
