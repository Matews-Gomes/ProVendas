using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProVendas.APP.Models;
using System.Text;

namespace ProVendas.APP.Controllers
{
    public class FilialController : Controller
    {
        readonly Uri baseUrl = new("https://localhost:7116/api");
        readonly HttpClient client;

        public FilialController()
        {
            client = new HttpClient
            {
                BaseAddress = baseUrl
            };
        }

        public IActionResult Index()
        {
            List<FilialViewModel> filiais = new();
            try
            {
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Filial").Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    filiais = JsonConvert.DeserializeObject<List<FilialViewModel>>(data);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar recuperar registros. error: {ex.Message}");
            }

            return View(filiais);
        }

        public ActionResult Details(int id)
        {
            FilialViewModel filial = new();
            try
            {
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Filial/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    filial = JsonConvert.DeserializeObject<FilialViewModel>(data);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar recuperar registros. error: {ex.Message}");
            }

            return View(filial);
        }

        public ActionResult Create()
        {
            var ListPessoa = ObterPessoas();
            ViewBag.Pessoas = ListPessoa;           
            return View(new FilialViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FilialViewModel entity)
        {
            try
            {
                string data = JsonConvert.SerializeObject(entity);
                StringContent stringContent = new(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Filial", stringContent).Result;
                if (response.IsSuccessStatusCode) return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar recuperar registros. error: {ex.Message}");
            }

            return View();
        }

        // GET: FilialController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FilialController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FilialController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FilialController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private IEnumerable<PessoaViewModel> ObterPessoas()
        {
            List<PessoaViewModel> pessoas = new();

            try
            {
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Pessoa").Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    pessoas = JsonConvert.DeserializeObject<List<PessoaViewModel>>(data);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar recuperar registro. error: {ex.Message}");
            }

            return pessoas;
        }
    }
}
