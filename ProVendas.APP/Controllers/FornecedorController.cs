using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProVendas.APP.Models;
using System.Text;

namespace ProVendas.APP.Controllers
{
    public class FornecedorController : Controller
    {
        readonly Uri baseUrl = new("https://localhost:7116/api");
        readonly HttpClient client;

        public FornecedorController()
        {
            client = new HttpClient
            {
                BaseAddress = baseUrl
            };            
        }

        public IActionResult Index()
        {
            List<FornecedorViewModel> fornecedores = new();
            try
            {
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Fornecedor").Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    fornecedores = JsonConvert.DeserializeObject<List<FornecedorViewModel>>(data);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar recuperar registros. error: {ex.Message}");
            }

            return View(fornecedores);

        }

        public IActionResult Details(int id)
        {
            FornecedorViewModel fornecedor = new();
            try
            {
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Fornecedor/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    fornecedor = JsonConvert.DeserializeObject<FornecedorViewModel>(data);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar recuperar registros. error: {ex.Message}");
            }

            return View(fornecedor);
        }

        public IActionResult Create()
        {
            var ListTipoPessoa = ObterTipoPessoa();
            ViewBag.tipoPessoa = ListTipoPessoa;
            return View(new FornecedorViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FornecedorViewModel entity)
        {
            try
            {
                string data = JsonConvert.SerializeObject(entity);
                StringContent stringContent = new(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Fornecedor", stringContent).Result;
                if(response.IsSuccessStatusCode) return RedirectToAction(nameof(Index));               
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

            FornecedorViewModel fornecedor = new();

            try
            {
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Fornecedor/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    fornecedor = JsonConvert.DeserializeObject<FornecedorViewModel>(data);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar recuperar registro. error: {ex.Message}");
            }

            return View(fornecedor);
        }

        [HttpPost]
        public IActionResult Edit(FornecedorViewModel entity)
        {
            try
            {
                string data = JsonConvert.SerializeObject(entity);
                StringContent stringContent = new(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/Fornecedor/" + entity.Id_Fornecedor, stringContent).Result;

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

            FornecedorViewModel fornecedor = new();

            try
            {
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Fornecedor/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    fornecedor = JsonConvert.DeserializeObject<FornecedorViewModel>(data);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar recuperar registro. error: {ex.Message}");
            }


            return View(fornecedor);
        }

        [HttpPost]
        public IActionResult Delete(int id, FornecedorViewModel entity)
        {
            try
            {                
                string data = JsonConvert.SerializeObject(entity);
                StringContent stringContent = new(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "/Fornecedor/" + id).Result;
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
    }
}
