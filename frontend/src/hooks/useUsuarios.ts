//hooks.useUsuarios.ts
import { useEffect, useState } from "react";
import api from "../services/api";

// Interface para representar um usuário
interface Usuario {
  id: number;
  nome: string;
  idade: number;
}

/**
 * Hook personalizado para buscar e gerenciar a lista de usuários.
 * @returns {Object} Um objeto contendo a lista de usuários.
 */
export function useUsuarios() {
  // Estado para armazenar a lista de usuários
  const [usuarios, setUsuarios] = useState<Usuario[]>([]);

  // Efeito para buscar os usuários ao carregar o componente que utiliza este hook
  useEffect(() => {
    // Faz uma requisição GET para obter a lista de usuários
    api.get("/usuarios")
      .then((response) => {
        // Atualiza o estado com os usuários obtidos da API
        setUsuarios(response.data);
      })
      .catch((error) => {
        // Exibe um erro no console caso a requisição falhe
        console.error("Erro ao buscar usuarios:", error);
      });
  }, []); // O array vazio garante que o efeito só seja executado uma vez, ao montar o componente

  // Retorna a lista de usuários
  return { usuarios };
}