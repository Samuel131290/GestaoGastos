//components.TransacaoList.tsx
import { useEffect, useState } from "react";
import api from "../services/api";

// Interface para representar uma transação
interface Transacao {
  id: number;
  descricao: string;
  valor: number;
  tipo: string; // "Receita" ou "Despesa"
  usuarioId: number;
}

// Interface para representar o resumo de transações de um usuário
interface ResumoTransacao {
  usuarioId: number;
  nome: string;
  totalReceitas: number;
  totalDespesas: number;
  saldo: number;
  transacoes: Transacao[];
}

/**
 * Componente para exibir a lista de transações e resumos financeiros.
 */
export function TransacaoList() {
  // Estados para armazenar os resumos de transações e totais gerais
  const [resumoTransacoes, setResumoTransacoes] = useState<ResumoTransacao[]>([]);
  const [totalReceitasGeral, setTotalReceitasGeral] = useState<number>(0);
  const [totalDespesasGeral, setTotalDespesasGeral] = useState<number>(0);
  const [saldoLiquidoGeral, setSaldoLiquidoGeral] = useState<number>(0);

  // Efeito para buscar as transações ao carregar o componente
  useEffect(() => {
    buscarTransacoes();
  }, []);

  /**
   * Busca as transações da API e calcula os resumos por usuário e totais gerais.
   */
  const buscarTransacoes = async () => {
    try {
      // Busca todas as transações
      const response = await api.get("/transacoes");
      const transacoes: Transacao[] = response.data;

      // Objeto para agrupar transações por usuário
      const resumo: Record<number, ResumoTransacao> = {};
      let totalReceitas = 0;
      let totalDespesas = 0;

      // Processa cada transação
      transacoes.forEach((transacao) => {
        // Inicializa o resumo do usuário se ainda não existir
        if (!resumo[transacao.usuarioId]) {
          resumo[transacao.usuarioId] = {
            usuarioId: transacao.usuarioId,
            nome: "",
            totalReceitas: 0,
            totalDespesas: 0,
            saldo: 0,
            transacoes: [],
          };
        }

        const usuarioResumo = resumo[transacao.usuarioId];

        // Atualiza os totais de receitas e despesas
        if (transacao.tipo === "Receita") {
          usuarioResumo.totalReceitas += transacao.valor;
          totalReceitas += transacao.valor;
        } else if (transacao.tipo === "Despesa") {
          usuarioResumo.totalDespesas += transacao.valor;
          totalDespesas += transacao.valor;
        }

        // Adiciona a transação ao resumo do usuário
        usuarioResumo.transacoes.push(transacao);
        usuarioResumo.saldo = usuarioResumo.totalReceitas - usuarioResumo.totalDespesas;
      });

      // Busca os usuários para obter os nomes
      const usuariosResponse = await api.get("/usuarios");
      const usuarios = usuariosResponse.data;

      // Atribui os nomes aos resumos
      usuarios.forEach((usuario: { id: number; nome: string }) => {
        if (resumo[usuario.id]) {
          resumo[usuario.id].nome = usuario.nome;
        }
      });

      // Atualiza os estados com os dados processados
      setTotalReceitasGeral(totalReceitas);
      setTotalDespesasGeral(totalDespesas);
      setSaldoLiquidoGeral(totalReceitas - totalDespesas);
      setResumoTransacoes(Object.values(resumo));
    } catch (error) {
      console.error("Erro ao buscar transações:", error);
    }
  };

  return (
    <div className="resumo">
      <h2>Resumo de Transações por Usuário</h2>
      <ul>
        {resumoTransacoes.length > 0 ? (
          // Exibe as transações agrupadas por usuário
          resumoTransacoes.map((resumo) => (
            <li key={resumo.usuarioId}>
              <h3>{resumo.nome}</h3>
              <ul>
                {resumo.transacoes.map((transacao) => (
                  <li key={transacao.id}>
                    Descrição: {transacao.descricao} | Valor: R$ {transacao.valor.toFixed(2)} | Tipo: {transacao.tipo}
                  </li>
                ))}
              </ul>
            </li>
          ))
        ) : (
          <p>Nenhuma transação cadastrada.</p>
        )}
      </ul>

      <h2>Total Por Usuário</h2>
      <div className="total-usuario">
        <ul>
          {resumoTransacoes.length > 0 ? (
            // Exibe os totais de receitas, despesas e saldo por usuário
            resumoTransacoes.map((resumo) => (
              <li key={resumo.usuarioId}>
                <h3>{resumo.nome}</h3>
                <ul>
                  Receita Total: R$ {resumo.totalReceitas.toFixed(2)} | Despesa Total: R$ {resumo.totalDespesas.toFixed(2)} | Saldo: R$ {resumo.saldo.toFixed(2)}
                </ul>
              </li>
            ))
          ) : (
            <p>Nenhuma transação cadastrada.</p>
          )}
        </ul>
      </div>

      <h2>Total Geral</h2>
      <div className="total-geral">
        <ul>
          {/* Exibe os totais gerais de receitas, despesas e saldo líquido */}
          <p>Total de Receitas: R$ {totalReceitasGeral.toFixed(2)}</p>
          <p>Total de Despesas: R$ {totalDespesasGeral.toFixed(2)}</p>
          <p>Saldo Líquido: R$ {saldoLiquidoGeral.toFixed(2)}</p>
        </ul>
      </div>
    </div>
  );
}