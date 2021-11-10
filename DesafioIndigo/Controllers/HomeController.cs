using DesafioIndigo.Models;
using DesafioIndigo.Service;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DesafioIndigo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICepService cepService;

        //Serviço acessado por meio de Injeção de Dependencia
        public HomeController(ICepService cepService)
        {
            this.cepService = cepService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string cep)
        {
            var dados = await Task.Run(() => cepService.Get(cep));            
            return View(dados);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
