using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;

namespace Desafio
{

    public static class DriverManager
    {
        public static SeleniumDriver TipoDriver => (SeleniumDriver)Enum.Parse(typeof(SeleniumDriver), ConfigurationManager.AppSettings["selenium:driver"]);
        public static bool IsRemote => TipoDriver.ToString().Contains("Remote");
        public static bool Is_x64 { get; } = (ConfigurationManager.AppSettings["selenium:browsermode"] ?? "").ToLower().Split(',').Contains("x64");
        public static bool InPrivateMode { get; } = (ConfigurationManager.AppSettings["selenium:browsermode"] ?? "").ToLower().Split(',').Contains("private");
        public static bool ReuseDriver { get; } = (ConfigurationManager.AppSettings["selenium:reusedriver"] ?? "").ToLower() == "true";
        public static Uri UrlBase { get; set; } = new Uri(ConfigurationManager.AppSettings["Url"]);        

        public static Uri ApiUrlBase { get; set; }

        public static Rectangle Window { get; } = ParseWindow();
        

        private static object lockObj = new Object();
        private static string CaminhoDriver =>
            string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["selenium:driverpath"]) ?
             @"Desafio\bin\Debug\netcoreapp2.2" :
            ConfigurationManager.AppSettings["selenium:driverpath"].Trim();

        private static Uri HubUri => new Uri(
            string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["selenium:huburi"]) ?
                @"http://" + GetFQDN() + ":4444/wd/hub" :
            ConfigurationManager.AppSettings["selenium:huburi"].Trim());


        public static DirectoryInfo RaizProjeto => new DirectoryInfo(Path.Combine(System.Reflection.Assembly.GetExecutingAssembly().Location, @"..\..\..\..\"));

        private static string CaminhoDriverLocal => Path.Combine(RaizProjeto.FullName, CaminhoDriver);

        public static IWebDriver ResolverDriver()
        {
            lock (lockObj)
            {
                IWebDriver driver = null;
                try
                {
                    if (IsRemote || !ReuseDriver)
                    {
                        //se for remoto (grid) ou não desejar reusar o driver entre testes,
                        //sempre criar um driver novo. 
                        //vai servir para testes paralelos. 
                        driver = CriarDriver();
                        driverInstances.Add(driver);
                    }
                    else
                    {
                        //se for reusar driver, durante todo o TestRun teremos apenas um 
                        //driver
                        if (driverInstances.Count != 1)
                        {
                            FinalizarDriver();
                            driver = CriarDriver();
                            driverInstances.Add(driver);
                        }
                        else
                        {
                            driver = driverInstances[0];
                            //GerenciarJanelaNavegador(driver);
                        }
                    }
                    return driver;
                }
                catch (WebDriverException)
                {
                    try
                    {
                        if (driver != null)
                            driver.Quit();
                    }
                    catch
                    {
                        /*falhar silenciosamente mesmo, se não conseguir matar, só tentar matar por garantia. */
                    }
                    throw;
                }
            }
        }

        private static bool falhouPorTimeOut;
        private static IWebDriver CriarDriver()
        {
            if (falhouPorTimeOut)
                Assert.Inconclusive("O driver do selenium falhou por timeout na execução anterior.");
            try
            {

                /*
                Limpar o proxy do webrequest para o selenium grid conectar diretamente.
                */
                HttpWebRequest.DefaultWebProxy = null;
                //---

                IWebDriver drv = null;

                var startTime = DateTime.Now;
                Trace.WriteLine("Iniciando driver selenium: " + TipoDriver.ToString());
                
                switch (TipoDriver)
                {
                    case SeleniumDriver.Firefox:
                        drv = new FirefoxDriver();
                        break;
                    
                    case SeleniumDriver.Chrome:
                        var chrome = new ChromeOptions();
                        chrome.AddArgument("-incognito"); //para não usar cache, nem guardar historico
                        chrome.AddArgument("--start-maximized");
                        chrome.AddArgument("--ignore-certificate-errors");
                        if (Convert.ToBoolean(ConfigurationManager.AppSettings["chrome:headless"]))
                        {
                            chrome.AddArgument("--headless");
                            chrome.AddArgument("--disable-gpu");
                        }

                        Trace.WriteLine(chrome.ToCapabilities(), nameof(SeleniumDriver.Chrome));
                        drv = new ChromeDriver(CaminhoDriverLocal, chrome);
                        break;
                    case SeleniumDriver.Edge:
                        var edge = new EdgeOptions();

                        Trace.WriteLine(edge.ToCapabilities(), nameof(SeleniumDriver.Edge));
                        drv = new EdgeDriver(CaminhoDriverLocal, edge);
                        break;                    

                    //case SeleniumDriver.RemoteChrome:

                    //    var remoteChrome = DesiredCapabilities.Chrome();
                    //    remoteChrome.IsJavaScriptEnabled = true;                        
                                                
                    //    Trace.WriteLine(remoteChrome.ToString(), nameof(SeleniumDriver.RemoteChrome));
                    //    drv = new RemoteWebDriver(HubUri, remoteChrome);
                    //    break;

                    default:
                        throw new NotImplementedException();
                }

                Trace.WriteLine("Driver iniciado. [" + Math.Round((DateTime.Now - startTime).TotalSeconds, 2) + "s]");
                return drv;
            }
            catch (WebDriverException ex)
            {
                if (ex.Message.Contains("timed out after"))
                {
                    falhouPorTimeOut = true;
                    Assert.Inconclusive("Driver de selenium falhou por timeout. Verifique a disponibilidade do hub.\r\n{0}\r\n----\r\n{1}", ex.Message, ex.InnerException);
                }
                throw;
            }
        }

        private static void GerenciarJanelaNavegador(IWebDriver drv)
        {
            if (Window.IsEmpty)
                drv.Manage().Window.Maximize();           

            else
            {
                drv.Manage().Window.Size = Window.Size;
                drv.Manage().Window.Position = Window.Location;
            }
        }

        private static IList<IWebDriver> driverInstances = new List<IWebDriver>();



        private static string GetFQDN()
        {
            string domainName = IPGlobalProperties.GetIPGlobalProperties().DomainName;
            string hostName = Dns.GetHostName();

            domainName = "." + domainName;
            if (!hostName.EndsWith(domainName))  // if hostname does not already include domain name
            {
                hostName += domainName;   // add the domain name part
            }

            return hostName;                    // return the fully qualified name
        }


        private static Rectangle ParseWindow()
        {
            var config = ConfigurationManager.AppSettings["selenium:window"];
            if (string.IsNullOrWhiteSpace(config))
                return Rectangle.Empty;
            var pos = config.Split(';', ',').Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => Convert.ToInt32(x)).ToArray();
            if (pos.Length < 4)
                return Rectangle.Empty;
            else
                return new Rectangle(pos[0], pos[1], pos[2], pos[3]);
        }

        /// <summary>
        /// Verifica se deve finalizar ou reusar o driver.
        /// </summary>
        /// <param name="_driver"></param>
        public static void FinalizarDriverSeNaoReutilizavel(IWebDriver _driver)
        {
            //somente finalizar se o driver não for reutilizável.
            if (IsRemote || !ReuseDriver)
                FinalizarDriver(_driver);
        }

        /// <summary>
        /// Finaliza todos os drivers abertos nesse TestRun
        /// </summary>
        public static void FinalizarDriver()
        {
            for (var i = driverInstances.Count - 1; i >= 0; i--)
            {
                FinalizarDriver(driverInstances[i]);
            }
        }

        //Finaliza o driver atual
        public static void FinalizarDriver(IWebDriver driver)
        {
            lock (lockObj)
            {
                if (driver != null)
                {
                    try
                    {
                        driver.Close();
                        driver.Quit(); Trace.WriteLine("Driver finalizado.");
                        driver.Dispose();

                    }
                    catch (Exception) {/* nada a fazer*/ }

                    if (driverInstances.Contains(driver))
                        driverInstances.Remove(driver); //remover por instancia
                }
            }
        }
    }

    public enum SeleniumDriver
    {
        InternetExplorer,
        Chrome,
        Edge,
        Firefox,
        RemoteInternetExplorer,
        RemoteChrome,
        RemoteEdge,
        RemoteFirefox,
    }
}
