//components.UsuarioList.tsx
import { useEffect, useState } from "react";
import api from "../services/api";

// Interface para representar um usuário
interface Usuario {
  id: number;
  nome: string;
  idade: number;
}

// Interface para as propriedades do componente
interface UsuarioListProps {
  onUsuarioExcluido: () => void; // Callback chamado após excluir um usuário
}

/**
 * Componente para exibir a lista de usuários e permitir a exclusão.
 */
export function UsuarioList({ onUsuarioExcluido }: UsuarioListProps) {
  // Estado para armazenar a lista de usuários
  const [usuarios, setUsuarios] = useState<Usuario[]>([]);

  // Efeito para buscar os usuários ao carregar o componente
  useEffect(() => {
    buscarUsuarios();
  }, []);

  /**
   * Busca a lista de usuários da API.
   */
  const buscarUsuarios = async () => {
    try {
      const response = await api.get("/usuarios");
      setUsuarios(response.data); // Atualiza o estado com os usuários obtidos
    } catch (error) {
      console.error("Erro ao buscar usuário!:", error);
    }
  };

  /**
   * Exclui um usuário pelo ID.
   * @param {number} id - ID do usuário a ser excluído.
   */
  const excluirUsuario = async (id: number) => {
    try {
      // Envia uma requisição para excluir o usuário
      await api.delete(`/usuarios/${id}`);

      // Atualiza a lista de usuários, removendo o usuário excluído
      setUsuarios((prevUsuarios) => prevUsuarios.filter((usuario) => usuario.id !== id));

      // Chama o callback para notificar que um usuário foi excluído
      onUsuarioExcluido();

      // Exibe uma mensagem de sucesso
      alert("Usuário Excluído com Sucesso!");
    } catch (error) {
      // Exibe uma mensagem de erro e log no console
      alert("Erro ao excluir usuário.");
      console.error("Erro ao excluir usuário:", error);
    }
  };

  return (
    <div>
      <h2>Lista de Usuários</h2>
      <ul>
        {usuarios.length > 0 ? (
          // Exibe a lista de usuários
          usuarios.map((usuario) => (
            <li key={usuario.id}>
              <div>
                <span>ID: {usuario.id}</span>
                <p>
                  {usuario.nome} - {usuario.idade} anos
                </p>
              </div>
              {/* Botão para excluir o usuário */}
              <button onClick={() => excluirUsuario(usuario.id)}>Excluir</button>
            </li>
          ))
        ) : (
          // Mensagem exibida quando não há usuários cadastrados
          <p>Nenhum usuário cadastrado.</p>
        )}
      </ul>
    </div>
  );
}