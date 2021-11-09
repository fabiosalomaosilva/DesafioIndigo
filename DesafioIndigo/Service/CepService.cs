using DesafioIndigo.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DesafioIndigo.Service
{
    public class CepService : ICepService
    {
        public Consulta Get(string numeroCep)
        {
            numeroCep = numeroCep.Replace(".", "").Replace("-", "");

            var urlSearch = "https://buscacepinter.correios.com.br/app/endereco/index.php";
            var consulta = new Consulta();
            using (var driver = new ChromeDriver())
            {
                driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(3000);
                driver.Manage().Window.Minimize();
                driver.Navigate().GoToUrl(urlSearch);

                var txtCep = driver.FindElement(By.Id("endereco"));
                txtCep.SendKeys(numeroCep.ToString());

                var btnBusca = driver.FindElement(By.Id("btn_pesquisar"));
                btnBusca.Click();
                Thread.Sleep(2000);

                var dadosCep = driver
                .FindElement(By.ClassName("ctn-tabela"))
                .FindElement(By.TagName("table"))
                .FindElement(By.TagName("tbody"))
                .FindElements(By.TagName("tr"));

                var registro = dadosCep[0].FindElement(By.TagName("td"));

                consulta.Logradouro = registro.FindElement(By.XPath("//td[@data-th='Logradouro/Nome']")).Text;
                consulta.Bairro = registro.FindElement(By.XPath("//td[@data-th='Bairro/Distrito']")).Text;
                consulta.Estado = registro.FindElement(By.XPath("//td[@data-th='Localidade/UF']")).Text.Split("/")[1];
                consulta.Municipio = registro.FindElement(By.XPath("//td[@data-th='Localidade/UF']")).Text.Split("/")[0];
                consulta.NumeropCep = numeroCep;
                consulta.DataPesquisa = DateTime.Now;

                driver.Quit();
            }

            return consulta;
        }
    }
}
