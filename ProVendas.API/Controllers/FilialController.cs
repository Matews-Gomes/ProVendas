using Microsoft.AspNetCore.Mvc;
using ProVendas.Data.Repository;
using ProVendas.Domain.IRepository;
using ProVendas.Domain.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProVendas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilialController : ControllerBase
    {
        private readonly IFilialRepository _filialRepository;

        public FilialController(IFilialRepository filialRepository)
        {
            _filialRepository = filialRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var filiais = await _filialRepository.GetAllAsync();
                if(filiais != null) { return Ok(filiais); }

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
                var filial = await _filialRepository.GetByIdAsync(id);

                return NotFound("Nenhum registro encontrado!");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar recuperar registro. error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(FilialModel entity)
        {
            try
            {
                if (entity != null) { await _filialRepository.AddAsync(entity); return Ok(entity); }

                return BadRequest("Erro ao tentar salvar o registro");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar salvar registro. error: {ex.Message}");
            }
        }

        // PUT api/<FilialController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FilialController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
