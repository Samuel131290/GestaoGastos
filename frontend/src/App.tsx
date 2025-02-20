// Importa os componentes do formulário e listas de Usuário e Transação
import { UsuarioForm } from "./components/UsuarioForm";
import { UsuarioList } from "./components/UsuarioList";
import { TransacaoForm } from "./components/TransacaoForm";
import { TransacaoList } from "./components/TransacaoList";
import { useState } from "react";

function App() {
  // Estado para rastrear se as transações foram atualizadas
  const [transacoesAtualizadas, setTransacoesAtualizadas] = useState(false);

  // Função chamada quando um usuário é excluído
  // Alterna o estado para forçar a atualização da lista de transações
  const handleUsuarioExcluido = () => {
    setTransacoesAtualizadas((prev) => !prev);
  };

  return (
    <div>
      <h1>Controle de Gastos</h1>
      
      {/* Formulário para adicionar um novo usuário */}
      {/* Recarrega a página após a adição para refletir mudanças */}
      <UsuarioForm onUsuarioAdicionado={() => window.location.reload()} />
      
      {/* Lista de usuários existentes */}
      {/* Chama handleUsuarioExcluido ao excluir um usuário para atualizar as transações */}
      <UsuarioList onUsuarioExcluido={handleUsuarioExcluido} />
      
      {/* Formulário para adicionar uma nova transação */}
      {/* Recarrega a página após a adição para refletir mudanças */}
      <TransacaoForm onTransacaoAdicionada={() => window.location.reload()} />
      
      {/* Lista de transações */}
      {/* A chave (key) depende do estado transacoesAtualizadas, forçando re-renderização quando alterado */}
      <TransacaoList key={transacoesAtualizadas.toString()} />
    </div>
  );
}

export default App;
