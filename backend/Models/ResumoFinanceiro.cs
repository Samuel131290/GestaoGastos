//Models.ResumoFinanceiro.cs;
namespace backend.Models
{
    /// <summary>
    /// Representa o resumo financeiro de um usuário.
    /// </summary>
    public class ResumoFinanceiro
    {        
        /// <summary>
        /// Identificador único do usuário.
        /// </summary>
        public int UsuarioId { get; set; }

        /// <summary>
        /// Nome do usuário.
        /// </summary>
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// Total de receitas usuário.
        /// </summary>
        public decimal TotalReceitas { get; set; }

        /// <summary>
        /// Total de despesas usuário.
        /// </summary>
        public decimal TotalDespesas { get; set; }

        /// <summary>
        /// Saldo total do usuário.
        /// </summary>
        public decimal Saldo => TotalReceitas - TotalDespesas;
    }

    /// <summary>
    /// Representa o resumo financeiro geral de todos os usuários.
    /// </summary>
    public class ResumoGeral
    {

        /// <summary>
        /// Representa o usuário cadastrado no sistema.
        /// </summary>
        public List<ResumoFinanceiro> Usuario { get; set; } = new();

        /// <summary>
        /// Total de receitas do sistema.
        /// </summary>
        public decimal TotalReceitas { get; set; }

        /// <summary>
        /// Total de despesas do sistema.
        /// </summary>
        public decimal TotalDespesas { get; set; }

        /// <summary>
        /// Saldo líquido calculado (total receitas - total despesas)
        /// </summary>
        public decimal SaldoLiquido => TotalReceitas - TotalDespesas;
    }
}