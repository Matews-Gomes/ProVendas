using Microsoft.AspNetCore.Mvc;
using ProVendas.Domain.IRepository;
using ProVendas.Domain.Models;

namespace ProVendas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var usuarios = await _usuarioRepository.GetAllAsync();
                if (usuarios != null) return Ok(usuarios);

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
                var usuario = await _usuarioRepository.GetByIdAsync(id);
                if (usuario != null) return Ok(usuario);

                return NotFound("Nenhum registro encontrado!");

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar recuperar registro. error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(UsuarioModel entity)
        {
            try
            {

                if (entity != null) { await _usuarioRepository.AddAsync(entity); return Ok(entity); }

                return BadRequest("Erro ao tentar salvar o registro");

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar salvar registro. error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UsuarioModel entity)
        {
            try
            {
                if (id != entity.Usuario.Id_Pessoa) return this.StatusCode(StatusCodes.Status400BadRequest,
                                                            $"Error: Impossivel atualizar o registro.");

                await _usuarioRepository.UpdateAsync(entity);

                return Ok(entity);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar atualizar registro. error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                if (id <= 0) return BadRequest("Impossivel deletar o registro.");

                await _usuarioRepository.DeleteAsync(id);
                return Ok("Deletado com sucesso");

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao tentar deletar registro. error: {ex.Message}");
            }
        }
    }
}
