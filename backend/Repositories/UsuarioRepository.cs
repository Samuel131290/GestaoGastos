//Repositories.UsuarioRepository.cs
using backend.Models;

namespace backend.Repositories
{
    /// <summary>
    /// Repositório para gerenciar operações relacionadas a usuários.
    /// </summary>
    public class UsuarioRepository
    {
        // Lista em memória para armazenar os usuários
        private readonly List<Usuario> _usuarios = new();

        // Contador para gerar IDs únicos para os usuários
        private int _proximoId = 1;

        // Repositório de transações para gerenciar operações relacionadas
        private readonly TransacaoRepository _transacaoRepo;

        /// <summary>
        /// Inicializa uma nova instância do repositório de usuários.
        /// </summary>
        /// <param name="transacaoRepo">Repositório de transações para operações relacionadas.</param>
        public UsuarioRepository(TransacaoRepository transacaoRepo)
        {
            _transacaoRepo = transacaoRepo;
        }

        /// <summary>
        /// Retorna a lista de todos os usuários cadastrados.
        /// </summary>
        /// <returns>Lista de usuários.</returns>
        public List<Usuario> Listar()
        {
            return _usuarios;
        }

        /// <summary>
        /// Cria um novo usuário.
        /// </summary>
        /// <param name="usuario">Dados do usuário a ser criado.</param>
        /// <returns>Usuário criado.</returns>
        public Usuario Criar(Usuario usuario)
        {
            // Atribui um ID único ao usuário e o adiciona à lista
            usuario.Id = _proximoId++;
            _usuarios.Add(usuario);

            return usuario; // Retorna o usuário criado
        }

        /// <summary>
        /// Obtém um usuário pelo seu ID.
        /// </summary>
        /// <param name="id">ID do usuário.</param>
        /// <returns>Usuário encontrado ou null se não existir.</returns>
        public Usuario? ObterPorId(int id)
        {
            return _usuarios.FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// Remove um usuário e suas transações associadas.
        /// </summary>
        /// <param name="id">ID do usuário a ser removido.</param>
        /// <returns>True se o usuário foi removido com sucesso; caso contrário, false.</returns>
        public bool Remover(int id)
        {
            // Obtém o usuário pelo ID
            var usuario = ObterPorId(id);

            // Se o usuário não for encontrado, retorna false
            if (usuario == null)
            {
                return false;
            }

            // Remove todas as transações associadas ao usuário
            _transacaoRepo.RemoverPorUsuario(id);

            // Remove o usuário da lista
            return _usuarios.Remove(usuario);
        }
    }
}