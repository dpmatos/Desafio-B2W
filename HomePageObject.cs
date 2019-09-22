using System;

using OpenQA.Selenium;

namespace Desafio
{
    class HomePageObject : ShareActionsAndElements
    {
        public HomePageObject(IWebDriver driver)
            : base(driver)
        {
        }                      

        #region Planos

        internal PageObject AcessarPaginaDesafio()
        {
            ClicarNoLink("https://docs.google.com/forms/d/e/1FAIpQLSfWfPcADbvEPrGDePWhY-agioR1TAyFZTW-hwNTucN28-VACg/viewform");
            return new PageObject(Driver);
        }
        #endregion
    }
}
