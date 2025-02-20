//Controllers.TransacaoController.cs;
using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.Repositories;

namespace backend.Controllers
{
    // Define o controlador para a gestão de transações
    [ApiController]
    [Route("api/transacoes")]
    public class TransacaoController : ControllerBase
    {
        private readonly UsuarioRepository _usuarioRepo;
        private readonly TransacaoRepository _transacaoRepo;

        // Injeção de dependência dos repositórios para acessar dados de usuários e transações
        public TransacaoController(UsuarioRepository usuarioRepo, TransacaoRepository transacaoRepo)
        {
            _usuarioRepo = usuarioRepo;
            _transacaoRepo = transacaoRepo;
        }

        /// <summary>
        /// Lista todas as transações registradas no sistema.
        /// </summary>
        /// <returns>Lista de todas as transações com status 200.</returns>
        /// <response code="200">Retorna a lista de transações com sucesso.</response>
        /// <response code="500">Erro interno no servidor ao tentar recuperar as transações.</response>
        [HttpGet]
        [ProducesResponseType(typeof(Transacao), StatusCodes.Status200OK)]  // Tipo de resposta 200
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // Tipo de resposta 500
        public IActionResult Listar()
        {
            // Chama o repositório de transações para obter todas as transações
            return Ok(_transacaoRepo.Listar());
        }

        /// <summary>
        /// Cria uma nova transação no sistema.
        /// </summary>
        /// <param name="transacao">Objeto contendo os dados da transação a ser criada.</param>
        /// <returns>A transação criada com status 201.</returns>
        /// <response code="201">Transação criada com sucesso.</response>
        /// <response code="400">Dados inválidos, como valores de transação menores ou iguais a zero, ou usuário não encontrado.</response>
        [HttpPost]
        [ProducesResponseType(typeof(Transacao), StatusCodes.Status201Created)]  // Tipo de resposta 201
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Tipo de resposta 400
        public IActionResult Criar([FromBody] Transacao transacao)
        {
            // Verifica se o usuário associado à transação existe
            var usuario = _usuarioRepo.ObterPorId(transacao.UsuarioId);
            if (usuario == null)
            {
                // Retorna erro 400 se o usuário não for encontrado
                return BadRequest("Usuário não encontrado.");
            }

            // Verifica regra de negócio: menores de idade só podem registrar despesas
            if (usuario.Idade < 18 && transacao.Tipo == "Receita")
            {
                return BadRequest("Menores de idade só podem registrar despesas.");
            }

            // Valida se o valor da transação é maior que zero
            if (transacao.Valor <= 0)
            {
                return BadRequest("O valor da transação deve ser maior que zero.");
            }

            // Cria a nova transação e a salva no banco de dados
            var novaTransacao = _transacaoRepo.Criar(transacao, usuario);
            // Retorna a transação criada com o status 201 e um link para visualizar a lista de transações
            return CreatedAtAction(nameof(Listar), novaTransacao);
        }

        /// <summary>
        /// Lista todas as transações de um usuário específico.
        /// </summary>
        /// <param name="usuarioId">ID do usuário para o qual as transações serão listadas.</param>
        /// <returns>Lista de transações associadas ao usuário.</returns>
        /// <response code="200">Retorna as transações do usuário com sucesso.</response>
        /// <response code="404">Se o usuário não for encontrado.</response>
        [HttpGet("usuario/{usuarioId}")]
        [ProducesResponseType(typeof(IEnumerable<Transacao>), StatusCodes.Status200OK)]  // Tipo de resposta 200
        [ProducesResponseType(StatusCodes.Status404NotFound)] // Tipo de resposta 404
        public IActionResult ListarPorUsuario(int usuarioId)
        {
            // Tenta obter o usuário pelo ID
            var usuario = _usuarioRepo.ObterPorId(usuarioId);
            if (usuario == null)
            {
                // Retorna erro 404 se o usuário não for encontrado
                return NotFound("Usuário não encontrado.");
            }

            // Retorna as transações associadas ao usuário
            return Ok(_transacaoRepo.ListarPorUsuario(usuarioId));
        }
    }
}
