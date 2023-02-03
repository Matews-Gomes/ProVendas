using Microsoft.AspNetCore.Mvc;
using ProVendas.Domain.IRepository;
using ProVendas.Domain.Models;

namespace ProVendas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var clientes = await _clienteRepository.GetAllAsync();
                if(clientes != null) {return Ok(clientes);}

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
                var clienteById = await _clienteRepository.GetByIdAsync(id);
                if (clienteById != null) { return Ok(clienteById); }

                return NotFound("Nenhum registro encontrado!");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                                        $"Erro ao tentar recuperar registros. error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(ClienteModel entity)
        {
            try
            {

                if (entity != null) { await _clienteRepository.AddAsync(entity); return Ok(entity); }

                return BadRequest("Erro ao tentar salvar o registro");

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar salvar registro. error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ClienteModel entity)
        {
            try
            {
                if (id != entity.Id_Cliente) return this.StatusCode(StatusCodes.Status400BadRequest,
                                                            $"Error: Impossivel atualizar o registro.");

                await _clienteRepository.UpdateAsync(entity);

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

                await _clienteRepository.DeleteAsync(id);
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
