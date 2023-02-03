using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProVendas.APP.Models;
using System.Text;

namespace ProVendas.APP.Controllers
{
    public class ClienteController : Controller
    {
        readonly Uri baseUrl = new("https://localhost:7116/api");
        readonly HttpClient client;

        public ClienteController()
        {
            client = new HttpClient
            {
                BaseAddress = baseUrl
            };
        }
        public IActionResult Index()
        {
            List<ClienteViewModel> clientes = new();
            try
            {
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Cliente").Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    clientes = JsonConvert.DeserializeObject<List<ClienteViewModel>>(data);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar recuperar registros. error: {ex.Message}");
            }

            return View(clientes);
        }

        public IActionResult Details(int id)
        {
            ClienteViewModel clienteById = new();
            try
            {
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Cliente/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    clienteById = JsonConvert.DeserializeObject<ClienteViewModel>(data);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar recuperar registros. error: {ex.Message}");
            }
            return View(clienteById);
        }

        public IActionResult Create()
        {
            var ListTipoPessoa = ObterTipoPessoa();
            ViewBag.tipoPessoa = ListTipoPessoa;
            var ListCidades = ObterCidades();
            ViewBag.cidades = ListCidades;
            return View(new ClienteViewModel());
        }

        [HttpPost]
        public IActionResult Create(ClienteViewModel entity)
        {
            try
            {
                string data = JsonConvert.SerializeObject(entity);
                StringContent stringContent = new(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Cliente", stringContent).Result;
                if (response.IsSuccessStatusCode) return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar recuperar registros. error: {ex.Message}");
            }

            return View();
        }

        public IActionResult Edit(int id)
        {
            var ListTipoPessoa = ObterTipoPessoa();
            ViewBag.tipoPessoa = ListTipoPessoa;
            var ListCidades = ObterCidades();
            ViewBag.cidades = ListCidades;

            ClienteViewModel clienteById = new();

            try
            {
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Cliente/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    clienteById = JsonConvert.DeserializeObject<ClienteViewModel>(data);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar recuperar registros. error: {ex.Message}");
            }
            return View(clienteById);
        }

        [HttpPost]
        public IActionResult Edit(int id, ClienteViewModel entity)
        {
            try
            {
                string data = JsonConvert.SerializeObject(entity);
                StringContent stringContent = new(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/Cliente/" + entity.Id_Cliente, stringContent).Result;

                if (response.IsSuccessStatusCode) return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar recuperar registros. error: {ex.Message}");
            }

            return View(entity);
        }

        public IActionResult Delete(int id)
        {
            var ListTipoPessoa = ObterTipoPessoa();
            ViewBag.tipoPessoa = ListTipoPessoa;
            var ListCidades = ObterCidades();
            ViewBag.cidades = ListCidades;

            ClienteViewModel clienteById = new();

            try
            {
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Cliente/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    clienteById = JsonConvert.DeserializeObject<ClienteViewModel>(data);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar recuperar registros. error: {ex.Message}");
            }
            return View(clienteById);
        }

        [HttpPost]
        public ActionResult Delete(int id, ClienteViewModel entity)
        {
            try
            {
                string data = JsonConvert.SerializeObject(entity);
                StringContent stringContent = new(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "/Cliente/" + id).Result;
                if (response.IsSuccessStatusCode) return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar recuperar registro. error: {ex.Message}");
            }

            return RedirectToAction(nameof(Index));
        }


        private IEnumerable<TipoPessoaViewModel> ObterTipoPessoa()
        {
            List<TipoPessoaViewModel> tipoPessoas = new();

            try
            {
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/TipoPessoa").Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    tipoPessoas = JsonConvert.DeserializeObject<List<TipoPessoaViewModel>>(data);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar recuperar registro. error: {ex.Message}");
            }

            return tipoPessoas;
        }

        private IEnumerable<CidadeViewModel> ObterCidades()
        {
            List<CidadeViewModel> cidades = new();

            try
            {
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Cidade").Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    cidades = JsonConvert.DeserializeObject<List<CidadeViewModel>>(data);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar recuperar registro. error: {ex.Message}");
            }

            return cidades;
        }
    }
}
