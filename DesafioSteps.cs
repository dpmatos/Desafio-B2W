using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using TechTalk.SpecFlow;
using Desafio.Utils;
using System.IO;
using System.Reflection;

namespace Desafio
{
    [Binding]
    public class DesafioSteps
    {
        private readonly HomePageObject _home;
        private readonly PageObject _pageObject;
        readonly string dataAtual = DateTime.Now.ToShortDateString();
        readonly string horaAtual = DateTime.Now.Hour.ToString();
        readonly string minutoAtual = DateTime.Now.Minute.ToString();        

        IWebDriver _driver;
        [BeforeScenario]
        public void SetupTest()
        {
            IWebDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
        }

        [AfterScenario]
        public void TeardownTest()
        {
            _driver.Quit();
            _driver.Dispose();
            _home.tearDown();
        }

        [Given(@"que acessei a página do formulário")]
        public void DadoQueAcesseiAPaginaDoFormulario()
        {
            _home.AcessarPaginaDesafio();
        }
        
        [Given(@"preenchi todos os campos")]
        public void DadoPreenchiTodosOsCampos(Table table)
        {
            var dictionary = TableExtensions.ToDictionary(table);
            var nomeCompleto = dictionary["nomeCompleto"];
            var email = dictionary["email"];
            var data = dictionary["data"];
            var horas = dictionary["horas"];
            var minutos = dictionary["minutos"];
            var sobremesa = dictionary["sobremesa"];
            var cor = dictionary["cor"];
            var comida = dictionary["comida"];
            var notaAnimal = dictionary["notaAnimal"];
            var notaFutebol = dictionary["notaFutebol"];
            var notaBaseball = dictionary["notaBaseball"];
            var notaeSports = dictionary["notaeSports"];
            var notaRugby = dictionary["notaRugby"];
            var tipoSanduiche = dictionary["tipoSanduiche"];

            _pageObject.
                PreencherCampoNomeCompleto(dictionary["nomeCompleto"]).
                PreencherCampoEmail(dictionary["email"]).
                SelecionarCorFavorita(dictionary["cor"]).
                SelecionarSobremesaFavorita(dictionary["sobremesa"]).
                SelecionarComidaFavorita(dictionary["comida"]).
                SelecionarNotaAnimais(dictionary["notaAnimal"]).
                SelecionarNotasEsportes(notaFutebol: dictionary["notaFutebol"], notaBaseball: dictionary["notaBaseball"], notaeSports: dictionary["notaeSports"], notaRugby: dictionary["notaRugby"]).
                SelecionarIngredientesSanduiches(dictionary["tipoSanduiche"]).
                PreencherCampoData(dataAtual).
                PreencherHoraAtual(horaAtual, minutoAtual);
        }

        [Given(@"não preenchi o nome da Sobremesa")]
        public void DadoNaoPreenchiONomeDaSobremesa()
        {
            _pageObject.PreencherOutraSobremesa("");
        }


        [When(@"clicar em salvar")]
        public void QuandoClicarEmSalvar()
        {
            _pageObject.ClicarEmSubmit();
        }
        
        [Then(@"Mensagem de sucesso deve ser exibida\.")]
        public void EntaoMensagemDeSucessoDeveSerExibida_()
        {
            _pageObject.VerificarMensagemDeFormularioSubmetidoComSucesso();
        }

        [Then(@"Mensagens de erro de campos obrigatórios não preenchidos devem ser exibidas\.")]
        public void EntaoMensagensDeErroDeCamposObrigatoriosNaoPreenchidosDevemSerExibidas()
        {
            _pageObject.VerificarAlertasDeCamposObrigatoriosNaoPreenchidos();
        }

        [Then(@"Mensagem de erro de email inválido deve ser exibida\.")]
        public void EntaoMensagemDeErroDeEmailInvalidoDeveSerExibida_()
        {
            _pageObject.VerificarAlertaDeEmailInvalido();
        }

        [Then(@"Mensagem de erro de data inválida deve ser exibida\.")]
        public void EntaoMensagemDeErroDeDataInvalidaDeveSerExibida_()
        {
            _pageObject.VerificarAlertaDeDataInvalida();
        }

        [Then(@"Mensagem de erro de hora inválida deve ser exibida\.")]
        public void EntaoMensagemDeErroDeHoraInvalidaDeveSerExibida_()
        {
            _pageObject.VerificarAlertaDeHoraInvalida();
        }

        [Then(@"Mensagens de erro de mesma nota escolhida para esportes diferentes deve ser exibida\.")]
        public void EntaoMensagensDeErroDeMesmaNotaEscolhidaParaEsportesDiferentesDeveSerExibida_()
        {
            _pageObject.VerificarAlertaDeMaisDeUmaRespostaNaMesmaColuna();
        }

    }
}
