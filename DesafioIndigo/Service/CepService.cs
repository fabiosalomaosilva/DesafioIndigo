using DesafioIndigo.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DesafioIndigo.Service
{
    public class CepService : ICepService
    {
        private readonly IStoreService store;

        //Serviço acessado por meio de Injeção de Dependencia
        public CepService(IStoreService store)
        {
            this.store = store;
        }

        //Para utilização da consulta no site dos correios, foi utilizado o Seleniun WebDriver e o ChromeDriver
        public async Task<Consulta> Get(string numeroCep)
        {
            numeroCep = numeroCep.Replace(".", "").Replace("-", "");
            var consulta = new Consulta();
            try
            {
                var urlSearch = "https://buscacepinter.correios.com.br/app/endereco/index.php";
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

                    //Caso o CEP não exista na base de dados dos correios, será enviada a consulta para o Banco de Dados com o erro
                    if (dadosCep.Count == 0)
                    {
                        consulta.NumeroCep = numeroCep;
                        consulta.Erro = "CEP não encontrado.";
                        consulta.DataPesquisa = DateTime.Now;
                        await store.SaveAsync(consulta);

                        return consulta;
                    }

                    var registro = dadosCep[0].FindElement(By.TagName("td"));

                    consulta.Logradouro = registro.FindElement(By.XPath("//td[@data-th='Logradouro/Nome']")).Text;
                    consulta.Bairro = registro.FindElement(By.XPath("//td[@data-th='Bairro/Distrito']")).Text;
                    consulta.Estado = registro.FindElement(By.XPath("//td[@data-th='Localidade/UF']")).Text.Split("/")[1].UfToEstado();
                    consulta.Municipio = registro.FindElement(By.XPath("//td[@data-th='Localidade/UF']")).Text.Split("/")[0];
                    consulta.NumeroCep = numeroCep;
                    consulta.DataPesquisa = DateTime.Now;

                    driver.Quit();
                }
                await store.SaveAsync(consulta);
                return consulta;
            }
            catch (Exception ex)
            {
                consulta.NumeroCep = numeroCep;
                consulta.Erro = ex.Message;
                consulta.DataPesquisa = DateTime.Now;
                await store.SaveAsync(consulta);

                return consulta;
            }
           
        }
    }
}
