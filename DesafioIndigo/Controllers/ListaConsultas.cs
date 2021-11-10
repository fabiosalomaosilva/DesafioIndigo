using DesafioIndigo.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioIndigo.Controllers
{
    public class ListaConsultas : Controller
    {
        private readonly IStoreService _storeService;
        
        //Serviço acessado por meio de Injeção de Dependencia
        public ListaConsultas(IStoreService storeService)
        {
            _storeService = storeService;
        }
        public async Task<IActionResult> Index()
        {
            var lista = await _storeService.GatAllAsync();
            return View(lista);
        }
    }
}
