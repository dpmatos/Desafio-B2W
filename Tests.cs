using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;

namespace Desafio
{
    [TestClass]
    public class Tests : ShareActionsAndElements
    {
        #region construtor

        public Tests(IWebDriver driver)
                : base(driver)
        {
        }
        #endregion 

        private HomePageObject _home;
        readonly string dataAtual = DateTime.Now.ToShortDateString();
        readonly string horaAtual = DateTime.Now.Hour.ToString();
        readonly string minutoAtual = DateTime.Now.Minute.ToString();

        #region Testes Positivos

        #region CT 01 - Preencher e submeter formul�rio completo
        [TestMethod]
        public void DevePreencherESubmeterFormularioCompleto()
        {
            _home.
                AcessarPaginaDesafio().
                PreencherCampoNomeCompleto("Daniel Passos de Matos").
                PreencherCampoEmail("dpmatos1@gmail.com").
                SelecionarCorFavorita("Vermelho").
                //SelecionarSobremesaFavorita(sobremesa: "Other", outraSobremesa: "Torta").
                SelecionarComidaFavorita("Carnes").
                SelecionarNotaAnimais("10").
                SelecionarNotasEsportes(notaFutebol: "�timo", notaBaseball: "Mediano", notaeSports: "Ruim", notaRugby: "Bom").
                SelecionarIngredientesSanduiches("EggXburguer").
                PreencherCampoData(dataAtual).
                PreencherHoraAtual(horaAtual, minutoAtual).
                ClicarEmSubmit().
                VerificarMensagemDeFormularioSubmetidoComSucesso();
        }
        #endregion

        #endregion

        #region Testes Negativos

        #region CT 02 - Submeter formul�rio sem preencher os campos obrigat�rios
        [TestMethod]
        public void NaoDeveSubmeterFormularioSemPreencherCamposObrigatorios()
        {
            _home.
                AcessarPaginaDesafio().                
                ClicarEmSubmit().
                VerificarAlertasDeCamposObrigatoriosNaoPreenchidos();
        }
        #endregion        

        #region CT 03 - Submeter formul�rio com email inv�lido
        [TestMethod]
        public void NaoDeveSubmeterFormularioComEmailInvalido()
        {
            _home.
                AcessarPaginaDesafio().
                PreencherCampoNomeCompleto("Daniel Passos de Matos").
                PreencherCampoEmail("a.b").
                SelecionarCorFavorita("Vermelho").
                //SelecionarSobremesaFavorita(sobremesa: "Other", outraSobremesa: "Torta").
                SelecionarComidaFavorita("Carnes").
                SelecionarNotaAnimais("10").
                SelecionarNotasEsportes(notaFutebol: "�timo", notaBaseball: "Mediano", notaeSports: "Ruim", notaRugby: "Bom").
                SelecionarIngredientesSanduiches("EggXburguer").
                PreencherCampoData(dataAtual).
                PreencherHoraAtual(horaAtual, minutoAtual).
                ClicarEmSubmit().
                VerificarAlertaDeEmailInvalido();
        }
        #endregion

        #region CT 04 - Submeter formul�rio com Data inv�lida
        [TestMethod]
        public void NaoDeveSubmeterFormularioComDataInvalida()
        {
            _home.
                AcessarPaginaDesafio().
                PreencherCampoNomeCompleto("Daniel Passos de Matos").
                PreencherCampoEmail("dpmatos1@gmail.com").
                SelecionarCorFavorita("Vermelho").
                //SelecionarSobremesaFavorita(sobremesa: "Other", outraSobremesa: "Torta").
                SelecionarComidaFavorita("Carnes").
                SelecionarNotaAnimais("10").
                SelecionarNotasEsportes(notaFutebol: "�timo", notaBaseball: "Mediano", notaeSports: "Ruim", notaRugby: "Bom").
                SelecionarIngredientesSanduiches("EggXburguer").
                PreencherCampoData("00/00/0000").
                PreencherHoraAtual(horaAtual, minutoAtual).
                ClicarEmSubmit().
                VerificarAlertaDeDataInvalida();
        }
        #endregion

        #region CT 05 - Submeter formul�rio com Hora inv�lida
        [TestMethod]
        public void NaoDeveSubmeterFormularioComHoraInvalida()
        {
            _home.
                AcessarPaginaDesafio().
                PreencherCampoNomeCompleto("Daniel Passos de Matos").
                PreencherCampoEmail("dpmatos1@gmail.com").
                SelecionarCorFavorita("Vermelho").
                //SelecionarSobremesaFavorita(sobremesa: "Other", outraSobremesa: "Torta").
                SelecionarComidaFavorita("Carnes").
                SelecionarNotaAnimais("10").
                SelecionarNotasEsportes(notaFutebol: "�timo", notaBaseball: "Mediano", notaeSports: "Ruim", notaRugby: "Bom").
                SelecionarIngredientesSanduiches("EggXburguer").
                PreencherCampoData(dataAtual).
                PreencherCampoHoras("30").
                PreencherCampoMinutos("70").
                ClicarEmSubmit().
                VerificarAlertaDeHoraInvalida();
        }
        #endregion

        #region CT 06 - Selecionar op��o "Other" sem preencher a sobremesa favorita
        [TestMethod]
        public void NaoDeveSubmeterFormularioComOutraOpcaoDeSobremesaNaoPreenchida()
        {
            _home.
                AcessarPaginaDesafio().
                PreencherCampoNomeCompleto("Daniel Passos de Matos").
                PreencherCampoEmail("dpmatos1@gmail.com").
                SelecionarCorFavorita("Vermelho").
                //SelecionarSobremesaFavorita(sobremesa: "Other", outraSobremesa: "").
                SelecionarComidaFavorita("Carnes").
                SelecionarNotaAnimais("10").
                SelecionarNotasEsportes(notaFutebol: "�timo", notaBaseball: "Mediano", notaeSports: "Ruim", notaRugby: "Bom").
                SelecionarIngredientesSanduiches("EggXburguer").
                PreencherCampoData(dataAtual).
                PreencherHoraAtual(horaAtual, minutoAtual).
                ClicarEmSubmit().
                VerificarAlertasDeCamposObrigatoriosNaoPreenchidos(); //CORRIGIR PARA O CASO ESPEC�FICO
        }
        #endregion

        #region CT 07 - Selecionar a mesma nota para dois Esportes diferentes
        [TestMethod]
        public void NaoDeveSubmeterFormularioComMesmaNotaParaDoisEsportesDiferentes()
        {
            _home.
                AcessarPaginaDesafio().
                PreencherCampoNomeCompleto("Daniel Passos de Matos").
                PreencherCampoEmail("dpmatos1@gmail.com").
                SelecionarCorFavorita("Vermelho").
                //SelecionarSobremesaFavorita(sobremesa: "Bolo", outraSobremesa: "").
                SelecionarComidaFavorita("Carnes").
                SelecionarNotaAnimais("10").
                SelecionarNotasEsportes(notaFutebol: "�timo", notaBaseball: "Ruim", notaeSports: "Ruim", notaRugby: "Bom").                
                SelecionarIngredientesSanduiches("EggXburguer").
                PreencherCampoData(dataAtual).
                PreencherHoraAtual(horaAtual, minutoAtual).
                ClicarEmSubmit().
                VerificarAlertaDeMaisDeUmaRespostaNaMesmaColuna();
        }
        #endregion
        
        #endregion

        #region Initialize and Cleanup

        [TestInitialize]
        public void SetUp()
        {            
            _home = new HomePageObject(Driver);
            _home.setUp();
        }

        [TestCleanup]
        public void TearDown()
        {
            _home.tearDown();
        }

        #endregion
    }
}