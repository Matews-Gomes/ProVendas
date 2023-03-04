using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProVendas.APP.Models;
using System.Text;

namespace ProVendas.APP.Controllers
{
    public class UsuarioController : Controller
    {
        readonly Uri baseUrl = new("https://localhost:7116/api");
        readonly HttpClient client;

        public UsuarioController()
        {
            client = new HttpClient
            {
                BaseAddress = baseUrl
            };
        }

        public IActionResult Index()
        {
            List<UsuarioViewModel> usuarios = new();
            try
            {
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Usuario").Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    usuarios = JsonConvert.DeserializeObject<List<UsuarioViewModel>>(data);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar recuperar registros. error: {ex.Message}");
            }

            return View(usuarios);
        }

        public ActionResult Details(int id)
        {
            UsuarioViewModel usuario = new();
            try
            {
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Usuario/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    usuario = JsonConvert.DeserializeObject<UsuarioViewModel>(data);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar recuperar registros. error: {ex.Message}");
            }

            return View(usuario);
        }

        public IActionResult Create()
        {
            var ListPerfil = ObterPerfil();
            ViewBag.perfis = ListPerfil;
            var ListPessoa = ObterPessoas();
            ViewBag.Pessoas = ListPessoa;
            var Listfilial = ObterFiliais();
            ViewBag.Filiais = Listfilial;
            return View(new UsuarioViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UsuarioViewModel entity)
        {
            try
            {
                string data = JsonConvert.SerializeObject(entity);
                StringContent stringContent = new(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Usuario", stringContent).Result;
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
            var ListPerfil = ObterPerfil();
            ViewBag.perfis = ListPerfil;
            var ListPessoa = ObterPessoas();
            ViewBag.Pessoas = ListPessoa;
            var Listfilial = ObterFiliais();
            ViewBag.Filiais = Listfilial;

            UsuarioViewModel usuario = new();

            try
            {
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Usuario/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    usuario = JsonConvert.DeserializeObject<UsuarioViewModel>(data);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar recuperar registro. error: {ex.Message}");
            }

            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UsuarioViewModel entity)
        {
            try
            {
                string data = JsonConvert.SerializeObject(entity);
                StringContent stringContent = new(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/Usuario/" + entity.Usuario.Id_Pessoa, stringContent).Result;

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
            var ListPerfil = ObterPerfil();
            ViewBag.perfis = ListPerfil;
            var ListPessoa = ObterPessoas();
            ViewBag.Pessoas = ListPessoa;
            var Listfilial = ObterFiliais();
            ViewBag.Filiais = Listfilial;

            UsuarioViewModel usuario = new();

            try
            {
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Usuario/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    usuario = JsonConvert.DeserializeObject<UsuarioViewModel>(data);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar recuperar registro. error: {ex.Message}");
            }

            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, UsuarioViewModel entity)
        {
            try
            {
                string data = JsonConvert.SerializeObject(entity);
                StringContent stringContent = new(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "/Usuario/" + id).Result;
                if (response.IsSuccessStatusCode) return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar recuperar registro. error: {ex.Message}");
            }

            return RedirectToAction(nameof(Index));
        }
        
        private IEnumerable<PerfilViewModel> ObterPerfil()
        {
            List<PerfilViewModel> perfis = new();

            try
            {
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Perfil").Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    perfis = JsonConvert.DeserializeObject<List<PerfilViewModel>>(data);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar recuperar registro. error: {ex.Message}");
            }

            return perfis;
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

        private IEnumerable<FilialViewModel> ObterFiliais()
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
                 throw new Exception($"Erro ao tentar recuperar registro. error: {ex.Message}");
             }

             return filiais;
         }
    }
}
