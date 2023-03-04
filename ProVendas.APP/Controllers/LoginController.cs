using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProVendas.APP.Models;
using System.Text;

namespace ProVendas.APP.Controllers
{
    public class LoginController : Controller
    {

        readonly Uri baseUrl = new("https://localhost:7116/api");
        readonly HttpClient client;

        public LoginController()
        {
            client = new HttpClient
            {
                BaseAddress = baseUrl
            };
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(UsuarioViewModel entity)
        {
            try
            {
                string data = JsonConvert.SerializeObject(entity);
                StringContent stringContent = new(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Login", stringContent).Result;
                if (response.IsSuccessStatusCode) return RedirectToAction("Index", "Home");
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar recuperar registro. error: {ex.Message}");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
