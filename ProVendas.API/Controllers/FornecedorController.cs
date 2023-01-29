using Microsoft.AspNetCore.Mvc;
using ProVendas.Data.Repository;
using ProVendas.Domain.IRepository;
using ProVendas.Domain.Models;

namespace ProVendas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FornecedorController : ControllerBase
    {
        private readonly IFornecedorRepository _fornecedorRepository;

        public FornecedorController(IFornecedorRepository fornecedorRepository)
        {
            _fornecedorRepository = fornecedorRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var fornecedores =  await _fornecedorRepository.GetAllAsync();
                if(fornecedores != null) return Ok(fornecedores);

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
                var fornecedor = await _fornecedorRepository.GetByIdAsync(id);
                if (fornecedor != null) return Ok(fornecedor);

                return NotFound("Nenhum registro encontrado!");

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar recuperar registro. error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(FornecedorModel entity)
        {
            try
            {

                if (entity != null) { await _fornecedorRepository.AddAsync(entity); return Ok(entity); }

                return BadRequest("Erro ao tentar salvar o registro");

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Erro ao tentar salvar registro. error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, FornecedorModel entity)
        {
            try
            {
                if(id != entity.Id_Fornecedor) return this.StatusCode(StatusCodes.Status400BadRequest,
                                                            $"Error: Impossivel atualizar o registro.");

                await _fornecedorRepository.UpdateAsync(entity);

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

                await _fornecedorRepository.DeleteAsync(id);
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
