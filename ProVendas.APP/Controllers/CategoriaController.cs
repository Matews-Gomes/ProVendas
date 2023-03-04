using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProVendas.APP.Models;
using System.Text;

namespace ProVendas.APP.Controllers
{
    public class CategoriaController : Controller
    {
        readonly Uri baseUrl = new("https://localhost:7116/api");
        readonly HttpClient client;

        public CategoriaController()
        {
            client = new HttpClient
            {
                BaseAddress = baseUrl
            };
        }

        public IActionResult Index()
        {
            List<CategoriaViewModel> categorias = new();
            try
            {
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Categoria").Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    categorias = JsonConvert.DeserializeObject<List<CategoriaViewModel>>(data);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar recuperar registros. error: {ex.Message}");
            }

            return View(categorias);
        }

        public IActionResult Details(int id)
        {
            CategoriaViewModel categoriaById = new();
            try
            {
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Categoria/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    categoriaById = JsonConvert.DeserializeObject<CategoriaViewModel>(data);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar recuperar registros. error: {ex.Message}");
            }
            return View(categoriaById);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoriaViewModel entity)
        {
            try
            {
                string data = JsonConvert.SerializeObject(entity);
                StringContent stringContent = new(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Categoria", stringContent).Result;
                if (response.IsSuccessStatusCode) return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar recuperar registros. error: {ex.Message}");
            }

            return View();
        }

        // GET: CategoriaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CategoriaController/Edit/5
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

        // GET: CategoriaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CategoriaController/Delete/5
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
    }
}
