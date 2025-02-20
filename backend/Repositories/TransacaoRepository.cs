//Repositories.TransacaoRepository.cs
using backend.Models;

namespace backend.Repositories
{
    /// <summary>
    /// Repositório para gerenciar operações relacionadas a transações.
    /// </summary>
    public class TransacaoRepository
    {
        // Lista em memória para armazenar as transações
        private readonly List<Transacao> _transacoes = new();

        // Contador para gerar IDs únicos para as transações
        private int _nextId = 1;

        /// <summary>
        /// Retorna a lista de todas as transações cadastradas.
        /// </summary>
        /// <returns>Lista de transações.</returns>
        public List<Transacao> Listar() => _transacoes;

        /// <summary>
        /// Cria uma nova transação.
        /// </summary>
        /// <param name="transacao">Dados da transação a ser criada.</param>
        /// <param name="usuario">Usuário associado à transação.</param>
        /// <returns>Transação criada ou null se a transação for inválida.</returns>
        public Transacao? Criar(Transacao transacao, Usuario usuario)
        {
            // Valida se o usuário é menor de idade e tenta registrar uma receita
            if (usuario.Idade < 18 && transacao.Tipo.ToLower() == "receita")
            {
                return null; // Menores de idade só podem registrar despesas
            }

            // Atribui um ID único à transação e a adiciona à lista
            transacao.Id = _nextId++;
            _transacoes.Add(transacao);

            return transacao; // Retorna a transação criada
        }

        /// <summary>
        /// Retorna a lista de transações associadas a um usuário específico.
        /// </summary>
        /// <param name="usuarioId">ID do usuário.</param>
        /// <returns>Lista de transações do usuário.</returns>
        public List<Transacao> ListarPorUsuario(int usuarioId) =>
            _transacoes.Where(t => t.UsuarioId == usuarioId).ToList();

        /// <summary>
        /// Remove todas as transações associadas a um usuário.
        /// </summary>
        /// <param name="usuarioId">ID do usuário.</param>
        /// <returns>Número de transações removidas.</returns>
        public int RemoverPorUsuario(int usuarioId)
        {
            // Filtra as transações do usuário
            var transacoes = _transacoes.Where(t => t.UsuarioId == usuarioId).ToList();

            // Se não houver transações, retorna 0
            if (transacoes.Count == 0)
            {
                return 0;
            }

            // Remove cada transação da lista
            foreach (var transacao in transacoes)
            {
                _transacoes.Remove(transacao);
            }

            // Retorna o número de transações removidas
            return transacoes.Count;
        }
    }
}