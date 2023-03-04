using Microsoft.AspNetCore.Mvc;
using ProVendas.Data.Repository;
using ProVendas.Domain.IRepository;
using ProVendas.Domain.Models;

namespace ProVendas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaRepository _categoriaRepository;
        public CategoriaController(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var categorias = await _categoriaRepository.GetAllAsync();
                if(categorias != null) return Ok(categorias);

                return NotFound("Nenhum registro encontrado!");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                                         $"Erro ao tentar recuperar registros. error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var categoriaById = await _categoriaRepository.GetByIdAsync(id);
                if (categoriaById != null) return Ok(categoriaById);

                return NotFound("Nenhum registro encontrado!");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                                         $"Erro ao tentar recuperar registros. error: {ex.Message}");
            }           
        }

        [HttpPost]
        public async Task<IActionResult> Post(CategoriaModel entity)
        {
            try
            {

                if (entity != null) { await _categoriaRepository.AddAsync(entity); return Ok(entity); }

                return BadRequest("Erro ao tentar salvar o registro");

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar salvar registro. error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
