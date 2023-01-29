using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProVendas.Domain.IRepository;

namespace ProVendas.API.Controllers
{
    

    [Route("api/[controller]")]
    [ApiController]
    public class EstadoController : ControllerBase
    {
        private readonly IEstadoRepository _estadoRepository;

        public EstadoController(IEstadoRepository estadoRepository)
        {
            _estadoRepository = estadoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get() 
        {
            try
            {
                var estados = await _estadoRepository.GetAllAsync();
                if(estados != null) return Ok(estados);

                return NotFound("Nenhum registro encontrado!");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                                        $"Erro ao tentar recuperar registros. error: {ex.Message}");
            }
        }
    }
}
