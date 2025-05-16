# Controle de Gastos Pessoais

O Controle de Gastos Pessoais é uma aplicação web desenvolvida para ajudar usuários a gerenciar suas finanças pessoais. 
Com ela, é possível cadastrar usuários, registrar transações (receitas e despesas) e visualizar um resumo financeiro detalhado, 
tanto por usuário quanto de forma geral.

---

## Funcionalidades

### Backend
- **Cadastro de Usuários**:
  - Cadastre usuários com nome e idade.
  - Validação de nome (apenas letras, espaços e caracteres acentuados).
  - Validação de idade (deve ser maior que zero).
- **Cadastro de Transações**:
  - Registre transações do tipo "Receita" ou "Despesa".
  - Validação de valor (deve ser maior que zero).
  - Restrição para menores de idade: só podem registrar despesas.
- **Resumo Financeiro**:
  - Visualize o resumo financeiro por usuário, com totais de receitas, despesas e saldo.
  - Visualize o resumo financeiro geral, com totais consolidados de receitas, despesas e saldo líquido.
- **Exclusão de Usuários**:
  - Remova usuários e todas as suas transações associadas.

### Frontend
- **Interface Intuitiva**:
  - Formulários para cadastro de usuários e transações.
  - Listagem de usuários e transações.
  - Resumo financeiro detalhado.
- **Validações em Tempo Real**:
  - Validação de campos como nome, idade e valor.
  - Feedback visual para o usuário em caso de erros.
- **Atualização Automática**:
  - A lista de usuários e transações é atualizada automaticamente após cadastros ou exclusões.

---

## Tecnologias Utilizadas

### Backend
- **ASP.NET Core**: Framework para desenvolvimento de APIs RESTful.
- **C#**: Linguagem de programação utilizada no backend.
- **Injeção de Dependência**: Para gerenciar repositórios e serviços.
- **Swagger**: Documentação automática da API (disponível em ambiente de desenvolvimento).

### Frontend
- **React**: Biblioteca JavaScript para construção de interfaces de usuário.
- **TypeScript**: Adiciona tipagem estática ao JavaScript, melhorando a qualidade do código.
- **Axios**: Cliente HTTP para consumir a API backend.
- **CSS**: Estilização básica dos componentes.

---

## COMO EXECUTAR O PROGRAMA?

### Pré-requisitos
- **.NET SDK** (versão 6.0 ou superior): Para executar o backend.
- **Node.js** (versão 16 ou superior): Para executar o frontend.
- **NPM** ou **Yarn**: Gerenciadores de pacotes para instalar as dependências do frontend.

---

### Passo a Passo

### 1. Backend:

- **Abra o terminal e navegue até a pasta do backend**:
  - cd backend

- **Restaure as dependências e execute o projeto**:
  - dotnet restore
  - dotnet run

  - O backend estará disponível em http://localhost:5043.


- **Caso necessário, instale o pacote do Cors, com o seguinte comando**:
  - dotnet add package Microsoft.AspNetCore.Cors

---

### 2. Documentação:

- **Acessar a documentação do programa**:
  - A documentação estará disponível em http://localhost:5043/swagger/index.html


- **Caso necessário, no terminal do backend, instale o Swagger via NuGet, com o seguinte comando**:
  - dotnet add package Swashbuckle.AspNetCore

---

### 3. Frontend:

- **Abra um novo terminal e navegue até a pasta do frontend**:
  - cd frontend

- **Execute o projeto**:
  - npm run dev

- **Acesse a Aplicação**:
  - Abra o navegador e acesse http://localhost:5173.


- **Instale as dependências (caso a pasta node_modules não existir)**:
  - npm install

- **Caso necessário, instale as demais dependências (apenas caso o programa apresentar erros de dependências)**:
  - npm install axios
  - npm install react react-dom react-scripts
  - npm install react-router-dom
  - npm install eslint --save-dev
  - npm install styled-components

---
