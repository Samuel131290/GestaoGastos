//Controllers.ResumoController.cs;
using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.Repositories;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/resumo")]
    public class ResumoController : ControllerBase
    {
        private readonly UsuarioRepository _usuarioRepo;
        private readonly TransacaoRepository _transacaoRepo;

        // Injeção de dependência dos repositórios
        public ResumoController(UsuarioRepository usuarioRepo, TransacaoRepository transacaoRepo)
        {
            _usuarioRepo = usuarioRepo;
            _transacaoRepo = transacaoRepo;
        }

        /// <summary>
        /// Obtém o resumo financeiro de todos os usuários.
        /// </summary>
        /// <returns>Resumo financeiro geral e por usuário.</returns>
        /// <response code="200">Retorna o resumo financeiro com sucesso.</response>
        /// <response code="500">Erro interno no servidor.</response>
        [HttpGet]
        [ProducesResponseType(typeof(ResumoGeral), StatusCodes.Status200OK)] // Documenta o tipo de retorno para status 200
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // Documenta o status 500
        public IActionResult ObterResumoFinanceiro()
        {
            try
            {
                var usuarios = _usuarioRepo.Listar();
                var resumoUsuarios = new List<ResumoFinanceiro>();

                decimal totalReceitasGeral = 0;
                decimal totalDespesasGeral = 0;

                // Calcula o resumo financeiro para cada usuário
                foreach (var usuario in usuarios)
                {
                    var transacoes = _transacaoRepo.ListarPorUsuario(usuario.Id);
                    decimal totalReceitas = transacoes.Where(t => t.Tipo == "receita").Sum(t => t.Valor);
                    decimal totalDespesas = transacoes.Where(t => t.Tipo == "despesa").Sum(t => t.Valor);

                    resumoUsuarios.Add(new ResumoFinanceiro
                    {
                        UsuarioId = usuario.Id,
                        Nome = usuario.Nome,
                        TotalReceitas = totalReceitas,
                        TotalDespesas = totalDespesas
                    });

                    // Acumula totais gerais
                    totalReceitasGeral += totalReceitas;
                    totalDespesasGeral += totalDespesas;
                }

                // Cria o resumo geral
                var resumoGeral = new ResumoGeral
                {
                    Usuario = resumoUsuarios,
                    TotalReceitas = totalReceitasGeral,
                    TotalDespesas = totalDespesasGeral
                };

                return Ok(resumoGeral);
            }
            catch (Exception)
            {
                // Retorna um erro 500 em caso de exceção
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Erro interno no servidor." });
            }
        }
    }
}