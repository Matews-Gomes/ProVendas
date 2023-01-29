using Microsoft.AspNetCore.Mvc;
using ProVendas.Domain.IRepository;

namespace ProVendas.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TipoPessoaController : ControllerBase
    {
        private readonly ITipoPessoaRepository _tipoPessoaRepository;

        public TipoPessoaController(ITipoPessoaRepository tipoPessoaRepository)
        {
            _tipoPessoaRepository = tipoPessoaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var pessoa = await _tipoPessoaRepository.GetAllAsync();
                if (pessoa != null) return Ok(pessoa);

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
