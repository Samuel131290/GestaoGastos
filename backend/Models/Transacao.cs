//Models.Transacao.cs;
namespace backend.Models
{
    /// <summary>
    /// Representa uma transação financeira.
    /// </summary>
    public class Transacao
    {
        /// <summary>
        /// Identificador único da transação.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Descrição da transação.
        /// </summary>
        public string Descricao { get; set; } = string.Empty;

        /// <summary>
        /// Valor monetário da transação.
        /// </summary>
        public decimal Valor { get; set; }

        /// <summary>
        /// Tipo da transação (receita ou despesa).
        /// Valor padrão: "despesa".
        /// </summary>
        public string Tipo { get; set; } = "despesa";

        /// <summary>
        /// Identificador do usuário associado à transação.
        /// </summary>
        public int UsuarioId { get; set; }
    }
}