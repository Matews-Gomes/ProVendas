using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ProVendas.APP.Models;
using System.Collections.Generic;
using System.Text;

namespace ProVendas.APP.Controllers
{
    public class ProdutoController : Controller
    {
        readonly Uri baseUrl = new("https://localhost:7116/api");
        readonly HttpClient client;

        public ProdutoController()
        {
            client = new HttpClient
            {
                BaseAddress = baseUrl
            };
        }

        public IActionResult Index()
        {
            List<ProdutoViewModel> produtos = new();

            try
            {
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Produto").Result;

                if (response.IsSuccessStatusCode)
                {
                    string data =  response.Content.ReadAsStringAsync().Result;
                    produtos = JsonConvert.DeserializeObject<List<ProdutoViewModel>>(data);
                }               
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar recuperar registros. error: {ex.Message}");
            }

            return View(produtos);
        }

        public IActionResult Details(int id)
        {
            ProdutoViewModel produto = new();

            try
            {
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Produto/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    produto = JsonConvert.DeserializeObject<ProdutoViewModel>(data);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar recuperar registro. error: {ex.Message}");
            }
            return View(produto);
        }

        public IActionResult Create()
        {
            var ListFornecedores =  ObterFornecedores();
            ViewBag.fornecedores = ListFornecedores;
            var ListCategorias = ObterCategorias();
            ViewBag.categorias = ListCategorias;
            return View(new ProdutoViewModel());
        }

        [HttpPost]
        public IActionResult Create(ProdutoViewModel entity)
        {
            try
            {
                if (entity.ImagemUpload != null)
                {
                    var imgPrefixo = Guid.NewGuid() + "_";
                    UploadArquivo(entity.ImagemUpload, imgPrefixo);

                    entity.Ds_Imagem = imgPrefixo + entity.ImagemUpload.FileName;
                }

                string data = JsonConvert.SerializeObject(entity);
                StringContent stringContent = new(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Produto", stringContent).Result;
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
            var Listfornecedores = ObterFornecedores();
            ViewBag.fornecedores = Listfornecedores;
            var ListCategorias = ObterCategorias();
            ViewBag.categorias = ListCategorias;

            ProdutoViewModel produto = new();

            try
            {               
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Produto/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    produto = JsonConvert.DeserializeObject<ProdutoViewModel>(data);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar recuperar registro. error: {ex.Message}");
            }

            return View(produto);
        }

        [HttpPost]
        public IActionResult Edit(ProdutoViewModel entity)
        {        
            try
            {
                var produtoAtualizacao = ObterProduto(entity.Id_Produto);

                if (entity.ImagemUpload != null)
                {                        
                    var imgPrefixo = Guid.NewGuid() + "_";

                    UploadArquivo(entity.ImagemUpload, imgPrefixo);

                    produtoAtualizacao.Ds_Imagem = imgPrefixo + entity.ImagemUpload.FileName;
                    entity.Ds_Imagem = produtoAtualizacao.Ds_Imagem;
                }

                string data = JsonConvert.SerializeObject(entity);
                StringContent stringContent = new(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/Produto/" + entity.Id_Produto, stringContent).Result;
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
            ProdutoViewModel produto = new();

            try
            {
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Produto/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    produto = JsonConvert.DeserializeObject<ProdutoViewModel>(data);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar recuperar registro. error: {ex.Message}");
            }
            return View(produto);
        }

        [HttpPost]
        public IActionResult Delete(int id, ProdutoViewModel entity)
        {
            try
            {
                string data = JsonConvert.SerializeObject(entity);
                StringContent stringContent = new(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "/Produto/" + id).Result;
                if (response.IsSuccessStatusCode) return RedirectToAction(nameof(Index));
                
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar recuperar registro. error: {ex.Message}");
            }

            return RedirectToAction(nameof(Index));
        }

        private bool UploadArquivo(IFormFile arquivo, string imgPrefixo)
        {
            if (arquivo.Length <= 0) return false;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imgPrefixo + arquivo.FileName);

            if (System.IO.File.Exists(path))
            {
                ModelState.AddModelError(string.Empty, "Já existe um arquivo com este nome!");
                return false;
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                arquivo.CopyTo(stream);
            }

            return true;
        }

        private IEnumerable<FornecedorViewModel> ObterFornecedores()
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
                throw new Exception($"Erro ao tentar recuperar registro. error: {ex.Message}");
            }

            return fornecedores;
        }

        private IEnumerable<CategoriaViewModel> ObterCategorias()
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
                throw new Exception($"Erro ao tentar recuperar registro. error: {ex.Message}");
            }

            return categorias;
        }

        private ProdutoViewModel ObterProduto(int id)
        {
            ProdutoViewModel produto = new();

            try
            {
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Produto/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    produto = JsonConvert.DeserializeObject<ProdutoViewModel>(data);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar recuperar registro. error: {ex.Message}");
            }
            return produto;
        }

    }
}
