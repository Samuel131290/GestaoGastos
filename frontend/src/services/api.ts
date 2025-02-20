//services.api.ts
import axios from "axios";

/**
 * Configuração da instância do Axios para fazer requisições HTTP.
 * A baseURL é definida como o endereço da API backend.
 */
const api = axios.create({
  baseURL: "http://localhost:5043/api", // URL base da API
});

// Exporta a instância configurada do Axios para ser usada em outros arquivos
export default api;