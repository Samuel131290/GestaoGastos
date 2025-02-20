//Controllers.UsuarioController.cs
using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.Repositories;

namespace backend.Controllers
{
    // Define o controlador para a gestão de usuários
    [ApiController]
    [Route("api/usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioRepository _usuarioRepo;
        private readonly TransacaoRepository _transacaoRepo;

        // Injeção de dependência dos repositórios para acessar dados de usuários e transações
        public UsuarioController(UsuarioRepository usuarioRepo, TransacaoRepository transacaoRepo)
        {
            _usuarioRepo = usuarioRepo;
            _transacaoRepo = transacaoRepo;
        }

        /// <summary>
        /// Lista todos os usuários cadastrados no sistema.
        /// </summary>
        /// <returns>Lista de todos os usuários com status 200.</returns>
        /// <response code="200">Retorna a lista de usuários com sucesso.</response>
        [HttpGet]
        [ProducesResponseType(typeof(Usuario), StatusCodes.Status200OK)]  // Tipo de resposta 200
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // Tipo de resposta 500
        public IActionResult Listar()
        {
            // Chama o repositório de usuários para obter todos os usuários
            var usuarios = _usuarioRepo.Listar();
            // Retorna a lista de usuários com o status 200
            return Ok(usuarios);
        }

        /// <summary>
        /// Cria um novo usuário no sistema.
        /// </summary>
        /// <param name="usuario">Objeto contendo os dados do usuário a ser criado.</param>
        /// <returns>Usuário criado com status 201.</returns>
        /// <response code="201">Usuário criado com sucesso.</response>
        /// <response code="400">Se os dados do usuário forem inválidos.</response>
        [HttpPost]
        [ProducesResponseType(typeof(Usuario), StatusCodes.Status201Created)]  // Tipo de resposta 201
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Tipo de resposta 400
        public IActionResult Criar([FromBody] Usuario usuario)
        {
            // Valida se o nome do usuário é válido (apenas letras, espaços e caracteres acentuados)
            if (usuario == null || string.IsNullOrWhiteSpace(usuario.Nome) || !System.Text.RegularExpressions.Regex.IsMatch(usuario.Nome, @"^[a-zA-ZÀ-ÿ\s]+$"))
            {
                return BadRequest("Nome inválido. Use apenas letras, espaços e caracteres acentuados.");
            }

            // Valida se a idade do usuário é positiva
            if (usuario.Idade <= 0)
            {
                return BadRequest("Idade inválida.");
            }

            // Cria o novo usuário e o salva no banco de dados
            var novaUsuario = _usuarioRepo.Criar(usuario);
            // Retorna o status 201 e o novo usuário criado
            return CreatedAtAction(nameof(Listar), new { id = novaUsuario.Id }, novaUsuario);
        }

        /// <summary>
        /// Remove um usuário do sistema e suas transações associadas.
        /// </summary>
        /// <param name="id">ID do usuário a ser removido.</param>
        /// <returns>Mensagem de sucesso ou erro se o usuário não for encontrado.</returns>
        /// <response code="200">Usuário e suas transações removidos com sucesso.</response>
        /// <response code="404">Se o usuário não for encontrado.</response>
        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            // Remove todas as transações associadas ao usuário
            _transacaoRepo.RemoverPorUsuario(id);

            // Remove o usuário do sistema
            var sucesso = _usuarioRepo.Remover(id);
            if (!sucesso)
            {
                // Retorna erro 404 se o usuário não for encontrado
                return NotFound("Usuário não encontrado.");
            }

            // Retorna uma mensagem de sucesso caso o usuário e suas transações sejam removidos
            return Ok("Usuário e suas transações foram excluídos com sucesso!");
        }
    }
}
