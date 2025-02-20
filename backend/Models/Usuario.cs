//Models.Usuario.cs;
namespace backend.Models
{
    /// <summary>
    /// Representa um usuário do sistema.
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// Identificador único do usuário.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome do usuário.
        /// </summary>
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// Idade do usuário.
        /// </summary>
        public int Idade { get; set; }

        /// <summary>
        /// Lista de transações associadas ao usuário.
        /// </summary>
        public List<Transacao> Transacoes { get; set; } = new();
    }
}