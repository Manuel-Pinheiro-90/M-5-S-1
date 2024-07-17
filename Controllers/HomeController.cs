using M_5_S_1.Models;

using M_5_S_1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace M_5_S_1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IClienteService _clienteService;
        private readonly ISpedizioneService _spedizioneService;
        

        public HomeController(ILogger<HomeController> logger, IClienteService clienteService, ISpedizioneService spedizioneService)
        {
            _logger = logger;
            _clienteService = clienteService;
            _spedizioneService = spedizioneService;
           
        }





        [Authorize]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult VerificaSpedizione(string codiceFiscalePartitaIVA, string numeroIdentificativo)
        {
            var aggiornamenti = _spedizioneService.VerificaAggiornamentoSpedizione(codiceFiscalePartitaIVA, numeroIdentificativo);
            if (aggiornamenti == null || !aggiornamenti.Any())
            {
                ViewBag.ErrorMessage = "Spedizione non trovata o non associata al codice fiscale/partita IVA fornito.";
                return View("Index");
            }

            var viewModel = new DettagliSpedizioneViewModel
            {
                Aggiornamenti = aggiornamenti
            };

            return View("Index", viewModel);
        }
        public IActionResult DettagliSpedizione(int id)
        {
            var spedizione = _spedizioneService.GetSpedizioneById(id);
            if (spedizione == null)
            {
                return NotFound();
            }

          
            var viewModel = new DettagliSpedizioneViewModel
            {
                Spedizione = spedizione,
               
            };

            return View(viewModel);
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
