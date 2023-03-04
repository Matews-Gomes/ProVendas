using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProVendas.Data.Repository;
using ProVendas.Domain.IRepository;
using ProVendas.Domain.Models;

namespace ProVendas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private readonly IPessoaRepository _pessoaRepository;

        public PessoaController(IPessoaRepository pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var pessoas = await _pessoaRepository.GetAllAsync();
                if (pessoas != null) return Ok(pessoas);

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
