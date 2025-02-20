//components.UsuarioForm.tsx
import { useState } from "react";
import api from "../services/api";

/**
 * Componente para cadastrar um novo usuário.
 * @param {Function} onUsuarioAdicionado - Callback chamado após adicionar um usuário.
 */
export function UsuarioForm({ onUsuarioAdicionado }: { onUsuarioAdicionado: () => void }) {
  // Estados para armazenar o nome e a idade do usuário
  const [nome, setNome] = useState("");
  const [idade, setIdade] = useState("");

  /**
   * Valida e atualiza o nome do usuário.
   * @param {React.ChangeEvent<HTMLInputElement>} e - Evento de mudança no campo de nome.
   */
  const handleNomeChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const valor = e.target.value;
    // Valida se o nome contém apenas letras, espaços e caracteres acentuados
    if (/^[a-zA-ZÀ-ÿ\s]*$/.test(valor)) {
      setNome(valor);
    }
  };

  /**
   * Envia os dados do usuário para a API.
   * @param {React.FormEvent} e - Evento de submissão do formulário.
   */
  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    // Valida se o nome é válido (apenas letras, espaços e caracteres acentuados)
    const nomeValido = /^[a-zA-ZÀ-ÿ\s]+$/.test(nome);
    if (!nomeValido) {
      alert("Nome inválido. Use apenas letras, espaços e caracteres acentuados.");
      return;
    }

    // Valida se os campos estão preenchidos
    if (!nome || !idade) {
      alert("Preencha todos os campos!");
      return;
    }

    try {
      // Envia os dados do usuário para a API
      await api.post("/usuarios", { nome, idade: Number(idade) });

      // Limpa os campos do formulário
      setNome("");
      setIdade("");

      // Chama o callback para atualizar a lista de usuários
      onUsuarioAdicionado();

      // Exibe uma mensagem de sucesso
      alert("Usuário Cadastrado com Sucesso!");
    } catch (error) {
      // Exibe uma mensagem de erro no console
      console.error("Erro ao Cadastrar Usuário!:", error);
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <h2>Cadastro de Usuários</h2>
      <input
        type="text"
        placeholder="Nome"
        value={nome}
        onChange={handleNomeChange}
      />
      <input
        type="number"
        placeholder="Idade"
        value={idade}
        onChange={(e) => setIdade(e.target.value)}
      />
      <button type="submit">Cadastrar</button>
    </form>
  );
}