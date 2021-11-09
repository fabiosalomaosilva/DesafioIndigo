using DesafioIndigo.Models;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DesafioIndigo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string cep)
        {
            var urlSearch = "https://buscacepinter.correios.com.br/app/endereco/index.php";

            var driver = new ChromeDriver();
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(3000);
            driver.Manage().Window.Minimize();
            driver.Navigate().GoToUrl(urlSearch);

            var txtCep = driver.FindElement(By.Id("endereco"));
            txtCep.SendKeys(cep);
            //Thread.Sleep(2000);
            var btnBusca = driver.FindElement(By.Id("btn_pesquisar"));
            btnBusca.Click();
            //Thread.Sleep(2000);


            var dadosCep = driver
            .FindElement(By.ClassName("ctn-tabela"))
            .FindElement(By.TagName("table"))
            .FindElement(By.TagName("tbody"))
            .FindElements(By.TagName("tr"));

            var registro = dadosCep[0].FindElement(By.TagName("td"));

            var text = registro.FindElement(By.XPath("//td[@data-th='Logradouro/Nome']"));

            ViewData["Cep"] = text.Text;
            driver.Quit();
            driver = null;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
