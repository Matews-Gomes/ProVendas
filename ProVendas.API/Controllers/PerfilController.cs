using Microsoft.AspNetCore.Mvc;
using ProVendas.Domain.IRepository;

namespace ProVendas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilController : ControllerBase
    {
        private readonly IPerfilRepository _perfilRepository;

        public PerfilController(IPerfilRepository perfilRepository)
        {
            _perfilRepository = perfilRepository;   
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var perfil = await _perfilRepository.GetAllAsync();
                if (perfil != null) return Ok(perfil);

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
