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

        #region CT 01 - Preencher e submeter formulário completo
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
                SelecionarNotasEsportes(notaFutebol: "Ótimo", notaBaseball: "Mediano", notaeSports: "Ruim", notaRugby: "Bom").
                SelecionarIngredientesSanduiches("EggXburguer").
                PreencherCampoData(dataAtual).
                PreencherHoraAtual(horaAtual, minutoAtual).
                ClicarEmSubmit().
                VerificarMensagemDeFormularioSubmetidoComSucesso();
        }
        #endregion

        #endregion

        #region Testes Negativos

        #region CT 02 - Submeter formulário sem preencher os campos obrigatórios
        [TestMethod]
        public void NaoDeveSubmeterFormularioSemPreencherCamposObrigatorios()
        {
            _home.
                AcessarPaginaDesafio().                
                ClicarEmSubmit().
                VerificarAlertasDeCamposObrigatoriosNaoPreenchidos();
        }
        #endregion        

        #region CT 03 - Submeter formulário com email inválido
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
                SelecionarNotasEsportes(notaFutebol: "Ótimo", notaBaseball: "Mediano", notaeSports: "Ruim", notaRugby: "Bom").
                SelecionarIngredientesSanduiches("EggXburguer").
                PreencherCampoData(dataAtual).
                PreencherHoraAtual(horaAtual, minutoAtual).
                ClicarEmSubmit().
                VerificarAlertaDeEmailInvalido();
        }
        #endregion

        #region CT 04 - Submeter formulário com Data inválida
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
                SelecionarNotasEsportes(notaFutebol: "Ótimo", notaBaseball: "Mediano", notaeSports: "Ruim", notaRugby: "Bom").
                SelecionarIngredientesSanduiches("EggXburguer").
                PreencherCampoData("00/00/0000").
                PreencherHoraAtual(horaAtual, minutoAtual).
                ClicarEmSubmit().
                VerificarAlertaDeDataInvalida();
        }
        #endregion

        #region CT 05 - Submeter formulário com Hora inválida
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
                SelecionarNotasEsportes(notaFutebol: "Ótimo", notaBaseball: "Mediano", notaeSports: "Ruim", notaRugby: "Bom").
                SelecionarIngredientesSanduiches("EggXburguer").
                PreencherCampoData(dataAtual).
                PreencherCampoHoras("30").
                PreencherCampoMinutos("70").
                ClicarEmSubmit().
                VerificarAlertaDeHoraInvalida();
        }
        #endregion

        #region CT 06 - Selecionar opção "Other" sem preencher a sobremesa favorita
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
                SelecionarNotasEsportes(notaFutebol: "Ótimo", notaBaseball: "Mediano", notaeSports: "Ruim", notaRugby: "Bom").
                SelecionarIngredientesSanduiches("EggXburguer").
                PreencherCampoData(dataAtual).
                PreencherHoraAtual(horaAtual, minutoAtual).
                ClicarEmSubmit().
                VerificarAlertasDeCamposObrigatoriosNaoPreenchidos(); //CORRIGIR PARA O CASO ESPECÍFICO
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
                SelecionarNotasEsportes(notaFutebol: "Ótimo", notaBaseball: "Ruim", notaeSports: "Ruim", notaRugby: "Bom").                
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