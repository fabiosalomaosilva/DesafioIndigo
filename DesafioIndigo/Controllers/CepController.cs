using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DesafioIndigo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CepController : ControllerBase
    {
        [HttpGet("{cep}")]
        public ActionResult Get(string cep)
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
            Thread.Sleep(2000);


            var dadosCep = driver
            .FindElement(By.ClassName("ctn-tabela"))
            .FindElement(By.TagName("table"))
            .FindElement(By.TagName("tbody"))
            .FindElements(By.TagName("tr"));

            var registro = dadosCep[0].FindElement(By.TagName("td"));

            var text = registro.FindElement(By.XPath("//td[@data-th='Logradouro/Nome']")).Text;

            driver.Quit();
            driver = null;
            return Ok(text);
        }
    }
}
