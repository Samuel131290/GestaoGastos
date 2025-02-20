//components.TransacaoForm.tsx
import { useState, useEffect } from "react";
import api from "../services/api";

/**
 * Componente para cadastrar transações.
 * @param {Function} onTransacaoAdicionada - Callback chamado após adicionar uma transação.
 */
export function TransacaoForm({ onTransacaoAdicionada }: { onTransacaoAdicionada: () => void }) {
  const [descricao, setDescricao] = useState("");
  const [valor, setValor] = useState("");
  const [tipo, setTipo] = useState("Despesa");
  const [usuarioId, setUsuarioId] = useState("");
  const [idadeUsuario, setIdadeUsuario] = useState<number | null>(null);

  // Validação em tempo real para a descrição (apenas letras e espaços)
  const handleDescricaoChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const campo = e.target.value;
    if (/^[a-zA-ZÀ-ÿ\s]*$/.test(campo)) {
      setDescricao(campo);
    }
  };

  // Verifica a idade do usuário ao selecionar um ID
  useEffect(() => {
    if (usuarioId) {
      const verificarIdade = async () => {
        try {
          const response = await api.get(`/usuarios/${usuarioId}`);
          setIdadeUsuario(response.data.idade);
        } catch (error) {
          console.error("Erro ao buscar usuario:", error);
        }
      };

      verificarIdade();
    }
  }, [usuarioId]);

  // Envia os dados da transação para o backend
  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    const valorNumerico = Number(valor);
    if (!descricao || isNaN(valorNumerico) || !usuarioId) {
      return alert("Preencha todos os campos corretamente!");
    }

    // Valida se o usuário é menor de idade e tenta registrar uma receita
    if (idadeUsuario !== null && idadeUsuario < 18 && tipo === "Receita") {
      alert("Menores de idade só podem registrar despesas.");
      return;
    }

    try {
      await api.post("/transacoes", {
        descricao,
        valor: valorNumerico,
        tipo,
        usuarioId: Number(usuarioId),
      });

      // Limpa o formulário e atualiza a lista de transações
      setDescricao("");
      setValor("");
      setUsuarioId("");
      setIdadeUsuario(null);
      onTransacaoAdicionada();
      alert("Transação Cadastrada com Sucesso!");
    } catch (error) {
      alert("Erro ao cadastrar transação.");
      console.error("Erro ao cadastrar transação:", error);
    }
  };

  return (
    <form onSubmit={handleSubmit} className="form">
      <h2>Cadastro de Transações</h2>
      <input
        type="text"
        placeholder="Descrição"
        value={descricao}
        onChange={handleDescricaoChange}
      />
      <input
        type="number"
        placeholder="Valor"
        value={valor}
        onChange={(e) => setValor(e.target.value)}
      />
      <select value={tipo} onChange={(e) => setTipo(e.target.value)}>
        <option value="Despesa">Despesa</option>
        <option value="Receita">Receita</option>
      </select>
      <input
        type="number"
        placeholder="ID do Usuario"
        value={usuarioId}
        onChange={(e) => {
          const value = e.target.value;
          if (/^\d*$/.test(value)) {
            setUsuarioId(value);
          }
        }}
      />
      <button type="submit">Cadastrar</button>
    </form>
  );
}