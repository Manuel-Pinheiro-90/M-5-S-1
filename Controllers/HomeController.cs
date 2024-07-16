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
        private readonly IAggiornamentoSpedizioneService _aggiornamentoSpedizioneService;

        public HomeController(ILogger<HomeController> logger, IClienteService clienteService, ISpedizioneService spedizioneService, IAggiornamentoSpedizioneService aggiornamentoSpedizioneService)
        {
            _logger = logger;
            _clienteService = clienteService;
            _spedizioneService = spedizioneService;
            _aggiornamentoSpedizioneService = aggiornamentoSpedizioneService;
        }

        [Authorize]
        public IActionResult Index()
        {
            var spedizioni = _spedizioneService.GetSpedizioniInConsegnaOggi();
            return View(spedizioni);
        }

        public IActionResult DettagliSpedizione(int id)
        {
            var spedizione = _spedizioneService.GetSpedizioneById(id);
            if (spedizione == null)
            {
                return NotFound();
            }

            var aggiornamenti = _aggiornamentoSpedizioneService.GetAggiornamentiBySpedizioneId(id);
            var viewModel = new DettagliSpedizioneViewModel
            {
                Spedizione = spedizione,
                Aggiornamenti = aggiornamenti
            };

            return View(viewModel);
        }

        public IActionResult VerificaSpedizione()
        {
            return View();
        }

        [HttpPost]
        public IActionResult VerificaSpedizione(string codiceFiscale, string numeroSpedizione)
        {
            var cliente = _clienteService.GetClienteByCodiceFiscale(codiceFiscale);
            if (cliente == null)
            {
                ViewBag.ErrorMessage = "Codice fiscale non valido.";
                return View();
            }

            var spedizione = _spedizioneService.GetSpedizioneByNumeroSpedizione(numeroSpedizione);
            if (spedizione == null || spedizione.ClienteId != cliente.IdCliente)
            {
                ViewBag.ErrorMessage = "Spedizione non trovata o non associata al codice fiscale fornito.";
                return View();
            }

            var aggiornamenti = _aggiornamentoSpedizioneService.GetAggiornamentiBySpedizioneId(spedizione.IdSpedizione);
            var viewModel = new DettagliSpedizioneViewModel
            {
                Spedizione = spedizione,
                Aggiornamenti = aggiornamenti
            };

            return View("DettagliSpedizione", viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
