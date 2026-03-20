# Avaliação - Full Stack .NET + Vue.js

Olá! Muito obrigado por participar da avaliação técnica para integrar a equipe de desenvolvimento da Top Solutions.

## 📋 Sobre o Projeto

Este é um desafio técnico para avaliação de candidatos Full Stack (.NET + Vue.js).

O projeto consiste em um sistema simplificado de Gestão de Fornecedores, composto por uma API em .NET 10 e uma aplicação Front-end base em Vue.js com TailwindCSS.

⚠️ **Estado atual (API):** Toda a lógica da API, incluindo o acesso ao banco e as validações, está implementada e aglomerada diretamente no arquivo principal (`Program.cs`), sem Controllers, Services, Repositories ou qualquer separação de responsabilidades. Sua tarefa no back-end é refatorar esse código.

⚠️ **Estado atual (Front-end):** Existe um projeto Vue.js básico já configurado com TailwindCSS. No momento, o arquivo `App.vue` apenas realiza um GET na rota `/api/fornecedores` (conectando na API local) e exibe o JSON puro na tela. Sua tarefa é transformar isso numa interface web completa e amigável.

---

## 🚀 Setup do Projeto

### Back-end (API)

**1. Crie um Fork do repositório e clone para sua máquina**

**2. Baixe e instale o SDK do .NET 10 (se ainda não tiver instalado)**

🔗 https://dotnet.microsoft.com/pt-br/download

**3. Restaure as dependências do .NET (Se necessário)**
```bash
dotnet restore
```

**4. Configure o Banco de Dados**

O projeto está preparado para receber uma string de conexão. Nós iremos fornecer os dados de um banco de dados para a sua avaliação. Você deverá incluir a conexão no seu arquivo `appsettings.json` (ou `appsettings.Development.json`) para utilizar a base fornecida, adicionando algo semelhante ao exemplo abaixo:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Initial Catalog=dbFornecedores;Persist Security Info=False;User ID=user;Password=password;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=true;"
}
```

**5. Inicie o servidor da API**
```bash
dotnet run
```
A API estará disponível localmente em `https://localhost:7277`. (Verifique se porta corresponde, caso contrário troque-a)
💡 **Dica:** Você pode testar e visualizar todos os endpoints interativamente acessando a documentação no Scalar em `https://localhost:7277/scalar`.

---

### Front-end (Vue.js)

**1. ** Baixe e instale o Node.js v24 LTS (se ainda não tiver instalado)**

🔗 https://nodejs.org/pt-br/download

**1. Navegue até a pasta do projeto front-end e instale as dependências**
```bash
npm install
```

**2. Inicie o servidor de desenvolvimento**
```bash
npm run dev
```

---

## 📊 Estrutura do Banco de Dados

🏭 **Fornecedores (fornecedores)**

| Campo | Tipo | Descrição |
| --- | --- | --- |
| `id` | int | Chave primária (Autoincremento) |
| `NomeFornecedor` | varchar(200) | Nome do fornecedor (Obrigatório) |
| `Cnpj` | string | CNPJ da empresa |
| `Endereco` | string | Endereço formatado |
| `Cep` | string | CEP do endereço |
| `DataAberturaEmpresa` | datetime | Data de fundação/abertura |
| `DataInclusao` | datetime | Data/hora de cadastro no sistema |

---

## 📡 Endpoints Disponíveis (API Atual)

| Método | Endpoint | Descrição |
| --- | --- | --- |
| `GET` | `/api/fornecedores` | Listar todos os fornecedores |
| `POST` | `/api/fornecedores` | Criar um novo fornecedor |
| `GET` | `/api/fornecedores/{id}` | Buscar um fornecedor específico por ID |
| `PUT` | `/api/fornecedores/{id}` | Atualizar dados de um fornecedor existente |
| `DELETE` | `/api/fornecedores/{id}` | Excluir um fornecedor |

---

## 🎯 Sua Tarefa

O código atual funciona, mas carece gravemente de boas arquiteturas e interfaces de usuário consolidadas. Seu desafio é refatorar aplicando boas práticas e montar o frontend.

### ✅ O que você deve fazer (Obrigatório):

**No Back-end (.NET 10):**
1. **Refatorar o `Program.cs` para uma Arquitetura em Camadas (Layered Architecture):**
   - **Controllers (Apresentação):** Extrair a lógica das rotas para Controllers dedicados.
   - **Services (Regras de Negócio):** Isolar as validações (ex: tamanho do nome) em classes de Serviço.
   - **Repositories (Acesso a Dados):** Retirar toda menção direta ao banco de dados dos endpoints e criar classes de Repositório limitadas a consulta e persistência.
   - **Models (Entidades):** Deixar a entidade bem definida isoladamente.

**No Front-end (Vue.js):**
1. **Criar o formulário e a Interface CRUD:** Construir o formulário de cadastro, funcionalidade de exibir os dados retornados no `App.vue` de forma organizada (listagem) e recursos de exclusão/edição utilizando exclusivamente o **TailwindCSS** para estilização.
2. **Integração de CNPJ Externa:** Ao preencher o campo **CNPJ** no formulário, a aplicação Front-end deve consumir uma API de CNPJ pública (como a **ReceitaWS**, **BrasilAPI** ou similar) e **preencher automaticamente** os demais campos no formulário (Nome do fornecedor, Endereço, CEP, Data de Abertura).
3. Persistir e ler os dados de Fornecedores integrando com a sua API .NET em `localhost:7277`.

---

## ⭐ Diferenciais (opcional)

- Utilização do padrão **Form Requests / ViewModels / DTOs** (Data Transfer Objects) para transitar os dados no Back-end em vez das instâncias diretas das Entidades.
- Implementação de tratamento de erros ou Middlewares de Exceções globais com retornos padronizados.
- Feedback visual excelente no Front-end (estados de _loading_ nos botões, caixas de alerta ou "toasts" para sucessos/erros).
- Melhorias na validação visual HTML.
- Qualquer outra melhoria que você julgue pertinente.

**Critérios de avaliação:**
- ✅ Organização e separação de responsabilidades em camadas (.NET).
- ✅ Injeção de dependência e aplicação dos preceitos SOLID.
- ✅ Qualidade da interação e interface consumindo as duas APIs (Interna e Externa/ReceitaWS).
- ✅ Cuidado e detalhe visual usando TailwindCSS.

---

## 🛠️ Tecnologias

- .NET 10 (WebAPI)
- Entity Framework Core
- Vue.js 3
- TailwindCSS

---

## 📬 Instruções de Entrega

1. **Faça um Fork** — Crie um fork deste repositório para a sua conta pessoal no GitHub.
2. **Desenvolva** — Implemente a solução realizando os commits diretamente no seu fork.
3. **Envie** — Disponibilize o link do seu repositório finalizado para a nossa equipe avaliar.

**⏰ Prazo:** Você tem **7 dias** a partir do recebimento deste enunciado para concluir e entregar o desafio.

Boa sorte e bom desenvolvimento! 🚀
