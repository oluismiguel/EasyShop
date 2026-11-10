# EasyShop – Persistência Poliglota (SQL + NoSQL) em .NET MAUI

Protótipo de mini e-commerce que usa **dois paradigmas de banco de dados locais** no mesmo aplicativo Android:

| Domínio | Paradigma | Tecnologia | Por quê |
|---|---|---|---|
| Catálogo de Produtos | NoSQL (documentos BSON) | **LiteDB** | Atributos dinâmicos (cor, tamanho, voltagem, RAM…) sem alterar schema |
| Cadastro de Clientes | SQL (relacional) | **SQLite** (`sqlite-net-pcl`) | Schema fixo, `NOT NULL`, `UNIQUE` no e-mail e consistência estrita |

## 👥 Integrantes

- Luis Miguel Vitor Miranda da Silva
- João Pedro
- Lucca

> ⚠️ Antes de entregar, substitua "João Pedro" e "Lucca" pelos **nomes completos** (sobrenomes incluídos), conforme exigido pela atividade.

## 🏗️ Arquitetura em camadas

```
EasyShop/
├── Models/            Customer (SQL) e Product (NoSQL)
├── Data/              CAMADA DE DADOS – só CRUD
│   ├── CustomerRepository.cs   → SQLite
│   └── ProductRepository.cs    → LiteDB
├── Services/          CAMADA DE NEGÓCIO – validações e orquestração
│   ├── CustomerService.cs
│   └── ProductService.cs
├── ViewModels/        CAMADA DE APRESENTAÇÃO – estado e comandos (MVVM)
├── Views/             CAMADA DE APRESENTAÇÃO – XAML (abas Produtos e Clientes)
├── Platforms/Android/ Arquivos de plataforma
└── MauiProgram.cs     Injeção de dependência
```

Fluxo: `View → ViewModel → Service → Repository → Banco`. A UI nunca acessa os bancos diretamente.

### Regras de negócio
- **Clientes:** nome completo (nome + sobrenome), e-mail válido e único.
- **Produtos:** nome obrigatório, preço maior que zero (aceita `129,90` ou `129.90`), categoria opcional e atributos livres no formato `Chave=Valor; Chave=Valor`.

### Exemplos de atributos dinâmicos (NoSQL)
- Camisa: `Tamanho=M; Cor=Azul; Material=Algodão`
- Notebook: `Processador=i7; RAM=16GB; Garantia=24 meses`
- Liquidificador: `Voltagem=220V; Potência=900W`

Todos convivem na mesma coleção, sem nenhuma coluna nula ou migração de schema.

## ▶️ Como executar

**Pré-requisitos**
- .NET 10 SDK
- Workload MAUI: `dotnet workload install maui` (ou `maui-android`)
- Android SDK + um emulador Android (via Android Studio ou Visual Studio 2022)

**Pelo terminal**
```bash
cd EasyShop
dotnet restore
dotnet build -t:Run -f net10.0-android
```
(com o emulador já iniciado)

**Pelo Visual Studio 2022:** abra a pasta/projeto `EasyShop.csproj`, escolha o emulador Android como alvo e pressione **F5**.

## 🧪 Roteiro de demonstração (vídeo)
1. Aba **Produtos**: cadastrar uma camisa e um notebook com atributos diferentes e mostrar a listagem.
2. Aba **Clientes**: cadastrar um cliente válido.
3. Tentar cadastrar o mesmo e-mail novamente e um e-mail inválido (validação SQL/negócio).
4. Fechar e reabrir o app para mostrar a persistência local.

## 📦 Dependências (NuGet)
`sqlite-net-pcl`, `SQLitePCLRaw.bundle_green`, `LiteDB`, `CommunityToolkit.Mvvm`

## 📤 Entrega
- Repositório público no GitHub com este código e README.
- Link do vídeo (YouTube/Google Drive) com demonstração no emulador Android.
