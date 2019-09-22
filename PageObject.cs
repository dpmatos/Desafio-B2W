using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;

namespace Desafio
{
    public class PageObject : ShareActionsAndElements
    {
        #region construtor

        public PageObject(IWebDriver driver)
            : base(driver)
        {            
        }
        #endregion        

        #region Métodos de interação com os elementos da tela

        internal PageObject AcessarPaginaDesafio()
        {
            Driver.Navigate().GoToUrl("https://docs.google.com/forms/d/e/1FAIpQLSfWfPcADbvEPrGDePWhY-agioR1TAyFZTW-hwNTucN28-VACg/viewform");
            return this;
        }

        internal PageObject PreencherCampoNomeCompleto(string nomeCompleto)
        {            
            SendkeysBy(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[1]/div/div[2]/div/div[1]/div/div[1]/input"), nomeCompleto);
            return this;
        }        

        internal PageObject PreencherCampoEmail(string email)
        {            
            SendkeysBy(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[2]/div/div[2]/div/div[1]/div/div[1]/input"), email);
            return this;
        }

        internal PageObject PreencherCampoData(string data)
        {            
            SendkeysBy(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[9]/div/div[2]/div/div[2]/div[1]/div/div[1]/input"), data);
            return this;
        }

        internal PageObject PreencherCampoHoras(string horas)
        {            
            SendkeysBy(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[10]/div/div[2]/div[1]/div[2]/div[1]/div/div[1]/input"), horas);
            return this;
        }

        internal PageObject PreencherCampoMinutos(string minutos)
        {            
            SendkeysBy(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[10]/div/div[2]/div[3]/div/div[1]/div/div[1]/input"), minutos);
            return this;
        }

        internal PageObject PreencherOutraSobremesa(string sobremesa)
        {            
            SendkeysBy(By.CssSelector(".quantumWizTextinputSimpleinputInput"), sobremesa);
            //SendkeysBy(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[4]/div/div[2]/div[4]/div/div/div/div/div/div[1]/input"), sobremesa);
            return this;
        }

        internal PageObject SelecionarAMPM()
        {            
            int hora = DateTime.Now.Hour;

            if(hora < 12)
                FindByAndClick(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[10]/div/div[2]/div[4]/div[2]/div[1]")); //Opção AM
            else
                FindByAndClick(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[10]/div/div[2]/div[4]/div[2]/div[2]")); //Opção PM

            return this;
        }

        internal PageObject PreencherHoraAtual(string horas, string minutos)
        {
            PreencherCampoHoras(horas);
            PreencherCampoMinutos(minutos);
            SelecionarAMPM();

            return this;
        }

        internal PageObject SelecionarCorFavorita(string cor)
        {
            if(cor == "Verde")
                FindByAndClick(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[3]/div/div[2]/div/span/div/div[1]/label/div/div[1]/div[3]/div/div"));

            if (cor == "Vermelho")
                FindByAndClick(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[3]/div/div[2]/div/span/div/div[3]/label/div/div[1]/div[3]/div"));

            if (cor == "Azul")
                FindByAndClick(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[3]/div/div[2]/div/span/div/div[3]/label/div/div[1]/div[3]/div"));

            return this;            
        }

        internal PageObject SelecionarSobremesaFavorita(string sobremesa)
        {
            if (sobremesa == "Sorvete")
                FindByAndClick(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[4]/div/div[2]/div[1]/div/label/div/div[1]/div[2]"));

            if (sobremesa == "Bolo")
                FindByAndClick(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[4]/div/div[2]/div[2]/div/label/div/div[1]/div[2]"));

            if (sobremesa == "Fruta")
                FindByAndClick(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[4]/div/div[2]/div[4]/div/div/label/div/div[1]/div[2]"));

            if (sobremesa == "Other")
            {
                FindByAndClick(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[4]/div/div[2]/div[4]/div/div/label/div/div[1]/div[2]"));                
            }

            return this;
        }

        internal PageObject SelecionarComidaFavorita(string comida)
        {
            //VERIFICAR SE FUNCIONA COM SELEÇÃO OU CLICK

            if (comida == "Todas")
                //SelecionaElementoPorTexto(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[5]/div/div[2]/div[2]/div[3]/div"), opcao);
                FindByAndClick(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[5]/div/div[2]/div[2]/div[3]/div"));

            if (comida == "Carnes")
                //SelecionaElementoPorTexto(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[5]/div/div[2]/div[1]/div[1]/div[4]"), opcao);
                FindByAndClick(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[5]/div/div[2]/div[1]/div[1]/div[4]"));

            if (comida == "Legumes")
                //SelecionaElementoPorTexto(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[5]/div/div[2]/div[1]/div[1]/div[5]"), opcao);
                FindByAndClick(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[5]/div/div[2]/div[1]/div[1]/div[5]"));            

            if (comida == "Massas")
                //SelecionaElementoPorTexto(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[5]/div/div[2]/div[1]/div[1]/div[6]"), opcao);
                FindByAndClick(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[5]/div/div[2]/div[1]/div[1]/div[6]"));

            if (comida == "Choose")
                //SelecionaElementoPorTexto(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[5]/div/div[2]/div[1]/div[1]/div[1]/span"), opcao);
                FindByAndClick(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[5]/div/div[2]/div[1]/div[1]/div[1]/span")); //Avaliar click sobre span

            return this;
        }

        internal PageObject SelecionarNotaAnimais(string notaAnimal)
        {
            if (notaAnimal == "1")
                FindByAndClick(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[6]/div/div[2]/div/span/div/label[1]/div[2]/div/div[3]/div/div"));

            if (notaAnimal == "2")
                FindByAndClick(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[6]/div/div[2]/div/span/div/label[2]/div[2]/div/div[3]/div/div"));

            if (notaAnimal == "3")
                FindByAndClick(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[6]/div/div[2]/div/span/div/label[3]/div[2]/div/div[3]/div/div"));

            if (notaAnimal == "4")
                FindByAndClick(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[6]/div/div[2]/div/span/div/label[4]/div[2]/div/div[3]/div/div"));

            if (notaAnimal == "5")
                FindByAndClick(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[6]/div/div[2]/div/span/div/label[5]/div[2]/div/div[3]/div/div"));

            if (notaAnimal == "6")
                FindByAndClick(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[6]/div/div[2]/div/span/div/label[6]/div[2]/div/div[3]/div/div"));

            if (notaAnimal == "7")
                FindByAndClick(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[6]/div/div[2]/div/span/div/label[7]/div[2]/div/div[3]/div/div"));

            if (notaAnimal == "8")
                FindByAndClick(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[6]/div/div[2]/div/span/div/label[8]/div[2]/div/div[3]/div/div"));

            if (notaAnimal == "9")
                FindByAndClick(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[6]/div/div[2]/div/span/div/label[9]/div[2]/div/div[3]/div/div"));

            if (notaAnimal == "10")
                FindByAndClick(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[6]/div/div[2]/div/span/div/label[10]/div[2]/div/div[3]/div"));

            return this;
        }

        internal PageObject SelecionarNotasEsportes(string notaFutebol, string notaBaseball, string notaeSports, string notaRugby)
        {
            if (notaFutebol == "Ruim")
                FindByAndClick(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[7]/div/div[2]/div/div[1]/div/div[2]/span/div[2]/div/div[3]/div/div"));

            if (notaFutebol == "Mediano")
                FindByAndClick(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[7]/div/div[2]/div/div[1]/div/div[2]/span/div[3]/div/div[3]/div/div"));

            if (notaFutebol == "Bom")
                FindByAndClick(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[7]/div/div[2]/div/div[1]/div/div[2]/span/div[4]/div/div[3]/div/div"));

            if (notaFutebol == "Ótimo")
                FindByAndClick(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[7]/div/div[2]/div/div[1]/div/div[4]/span/div[5]/div/div[3]/div/div"));

            if (notaBaseball == "Ruim")
                FindByAndClick(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[7]/div/div[2]/div/div[1]/div/div[3]/span/div[2]/div/div[3]/div/div"));

            if (notaBaseball == "Mediano")
                FindByAndClick(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[7]/div/div[2]/div/div[1]/div/div[3]/span/div[3]/div/div[3]/div/div"));

            if (notaBaseball == "Bom")
                FindByAndClick(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[7]/div/div[2]/div/div[1]/div/div[3]/span/div[4]/div/div[3]/div/div"));

            if (notaBaseball == "Ótimo")
                FindByAndClick(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[7]/div/div[2]/div/div[1]/div/div[3]/span/div[5]/div/div[3]/div/div"));

            if (notaeSports == "Ruim")
                FindByAndClick(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[7]/div/div[2]/div/div[1]/div/div[2]/span/div[2]/div/div[3]/div/div"));

            if (notaeSports == "Mediano")
                FindByAndClick(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[7]/div/div[2]/div/div[1]/div/div[4]/span/div[3]/div/div[3]/div/div"));

            if (notaeSports == "Bom")
                FindByAndClick(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[7]/div/div[2]/div/div[1]/div/div[4]/span/div[4]/div/div[3]/div/div"));

            if (notaeSports == "Ótimo")
                FindByAndClick(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[7]/div/div[2]/div/div[1]/div/div[4]/span/div[5]/div/div[3]/div/div"));          

            if (notaRugby == "Ruim")
                FindByAndClick(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[7]/div/div[2]/div/div[1]/div/div[5]/span/div[2]/div/div[3]/div/div"));

            if (notaRugby == "Mediano")
                FindByAndClick(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[7]/div/div[2]/div/div[1]/div/div[5]/span/div[3]/div/div[3]/div/div"));

            if (notaRugby == "Bom")
                FindByAndClick(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[7]/div/div[2]/div/div[1]/div/div[5]/span/div[4]/div/div[3]/div/div"));

            if (notaRugby == "Ótimo")
                FindByAndClick(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[7]/div/div[2]/div/div[1]/div/div[5]/span/div[5]/div/div[3]/div/div"));

            return this;
        }        

        internal PageObject SelecionarIngredientesSanduiches(string tipoSanduiche)
        {
            if (tipoSanduiche == "Vegetariano")
            {
                FindByAndClick(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[8]/div/div[2]/div/div[1]/div/div[2]/label[1]/div/div/div[3]")); //Pão
                FindByAndClick(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[8]/div/div[2]/div/div[1]/div/div[2]/label[5]/div/div/div[3] ")); //Salada
            }

            if (tipoSanduiche == "Hamburguer")
            {
                FindByAndClick(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[8]/div/div[2]/div/div[1]/div/div[4]/label[1]/div/div/div[2]")); //Pão
                FindByAndClick(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[8]/div/div[2]/div/div[1]/div/div[4]/label[2]/div/div/div[2]")); //Carne
            }

            if (tipoSanduiche == "Xburguer")
            {
                FindByAndClick(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[8]/div/div[2]/div/div[1]/div/div[5]/label[1]/div/div/div[2]")); //Pão
                FindByAndClick(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[8]/div/div[2]/div/div[1]/div/div[5]/label[2]/div/div/div[2]")); //Carne
                FindByAndClick(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[8]/div/div[2]/div/div[1]/div/div[5]/label[3]/div/div/div[2]")); //Queijo
            }

            if (tipoSanduiche == "EggXburguer")
            {
                FindByAndClick(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[8]/div/div[2]/div/div[1]/div/div[3]/label[1]/div/div/div[2]")); //Pão
                FindByAndClick(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[8]/div/div[2]/div/div[1]/div/div[2]/label[2]/div/div/div[2]")); //Carne
                FindByAndClick(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[8]/div/div[2]/div/div[1]/div/div[2]/label[3]/div/div/div[2]")); //Queijo
                FindByAndClick(By.XPath("//*[@id='mG61Hd']/div/div[2]/div[2]/div[8]/div/div[2]/div/div[1]/div/div[2]/label[4]/div/div/div[2]")); //Ovo
            }

            return this;
        }

        internal PageObject ClicarEmSubmit()
        {
            FindByAndClick(By.CssSelector(".quantumWizButtonPaperbuttonFocusOverlay"));
            return this;
        }

        #endregion       

        #region Verificar Alertas

        internal PageObject VerificarMensagemDeFormularioSubmetidoComSucesso()
        {
            string mensagem = FindBy(By.CssSelector(".freebirdFormviewerViewResponseConfirmationMessage")).Text;
            Assert.AreEqual("Your response has been recorded.", mensagem, true);

            return this;
        }

        internal PageObject VerificarAlertasDeCamposObrigatoriosNaoPreenchidos()
        {
            string alertaNome = FindBy(By.Id("i.err.338029540")).Text;
            string alertaEmail = FindBy(By.Id("i.err.2081699196")).Text;
            string alertaCor = FindBy(By.Id("i.err.2125875329")).Text;
            string alertaSobremesa = FindBy(By.Id("i.err.1449888689")).Text;
            string alertaComida = FindBy(By.Id("i.err.1171906890")).Text;
            string alertaAnimais = FindBy(By.Id("i.err.1157512433")).Text;
            string alertaEsportes = FindBy(By.Id("i.err.1693046790")).Text;
            string alertaSanduiches = FindBy(By.Id("i.err.456499868")).Text;
            string alertaData = FindBy(By.Id("i.err.1229910557")).Text;
            string alertaHora = FindBy(By.Id("i.err.1229910557")).Text;

            string mensagem1 = "This is a required question";            

            Assert.AreEqual(mensagem1, alertaNome, true);
            Assert.AreEqual(mensagem1, alertaEmail, true);
            Assert.AreEqual(mensagem1, alertaCor, true);
            Assert.AreEqual(mensagem1, alertaSobremesa, true);
            Assert.AreEqual(mensagem1, alertaComida, true);
            Assert.AreEqual(mensagem1, alertaAnimais, true);
            Assert.AreEqual(mensagem1, alertaData, true);
            Assert.AreEqual(mensagem1, alertaHora, true);

            string mensagem2 = "This question requires at least one response per row";

            Assert.AreEqual(mensagem2, alertaEsportes, true);
            Assert.AreEqual(mensagem2, alertaSanduiches, true);

            return this;
        }

        internal PageObject VerificarAlertaDeEmailInvalido()
        {
            string alertaEmail = FindBy(By.Id("i.err.2081699196")).Text;            
            string mensagem = "Esse e-mail não parece válido";

            Assert.AreEqual(mensagem, alertaEmail, true);            

            return this;
        }

        internal PageObject VerificarAlertaDeDataInvalida()
        {
            string alertaData = FindBy(By.Id("i.err.1672509407")).Text;
            string mensagem = "Invalid Date";

            Assert.AreEqual(mensagem, alertaData, true);

            return this;
        }

        internal PageObject VerificarAlertaDeHoraInvalida()
        {
            string alertaHora = FindBy(By.Id("i.err.1229910557")).Text;
            string mensagem = "Invalid time";

            Assert.AreEqual(mensagem, alertaHora, true);

            return this;
        }

        internal PageObject VerificarAlertaDeMaisDeUmaRespostaNaMesmaColuna()
        {
            string alertaEsportes = FindBy(By.Id("i.err.338029540")).Text;            
            string mensagem = "Please don't select more than one response per column";

            Assert.AreEqual(mensagem, alertaEsportes, true);            

            return this;
        }

        #endregion
    }
}
