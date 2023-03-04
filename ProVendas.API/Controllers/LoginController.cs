using Microsoft.AspNetCore.Mvc;
using ProVendas.Domain.IRepository;
using ProVendas.Domain.Models;

namespace ProVendas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public LoginController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost]
        public IActionResult Login(UsuarioModel entity)
        {

            var usuario = entity.Ds_Usuario;
            var senha = entity.Ds_Senha;

            try
            {
                var found = _usuarioRepository.Login(usuario, senha);

                if (found)
                {
                    return Ok(entity);
                }
                else
                {
                    return NotFound("Usuário ou senha incorrretos");
                }
                

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar salvar registro. error: {ex.Message}");
            }
        }

    }
}
