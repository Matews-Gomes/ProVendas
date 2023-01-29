using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProVendas.Domain.IRepository;

namespace ProVendas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CidadeController : ControllerBase
    {
        private readonly ICidadeRepository _cidadeRepository;

        public CidadeController(ICidadeRepository cidadeRepository)
        {
            _cidadeRepository = cidadeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var cidades = await _cidadeRepository.GetAllAsync();
                if(cidades != null) return Ok(cidades);

                return NotFound("Nenhum registo encontrado!");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                                        $"Erro ao tentar recuperar registros. error: {ex.Message}");
            }
        }
    }
}
