# 🧠 NeuroGenesis AI

## Um projeto experimental de simulação de crescimento neural artificial, combinando **.NET 8** e **Angular 17**.

## 📖 Sobre o Projeto

O **NeuroGenesis AI** é uma plataforma que simula o comportamento de um cérebro virtual, permitindo:

### Nascimento, amadurecimento e conexão de neurônios.
### Formação de clusters (agrupamentos neurais).
### Estímulos externos que alteram o estado do sistema.
### Exportação de snapshots em CSV para análise posterior.

Este projeto é uma primeira etapa para a criação de um sistema **auto-adaptativo**, inspirado na **neurogênese humana**.

O **frontend** está atualmente em **fase inicial de desenvolvimento** e novas funcionalidades de visualização serão adicionadas.

---

## 🛠 Tecnologias

### Backend

- [ASP.NET Core 8](https://learn.microsoft.com/aspnet/core/)
- C# 12
- REST API
- Geração de arquivos CSV

### Frontend

- [Angular 17 Standalone](https://angular.dev/)
- Vite.js para build e HMR
- [Ngx-Charts](https://swimlane.gitbook.io/ngx-charts/)
- [Ngx-Graph](https://github.com/swimlane/ngx-graph)
- Bootstrap (planejado para futuro layout)

---

## ⚙️ Instalação

### Backend (.NET)

```bash
# Clone o repositório
git clone https://github.com/jardelva96/neurogenesis-ai.git
```
```bash

# Acesse o diretório da API
cd NeuroGenesisAI/NeuroGenesisAI.API
```

```bash

# Execute a API
dotnet run
```
A API estará disponível em:
```bash
http://localhost:5049/
```
Frontend (Angular)
```bash

# Acesse o diretório do frontend
cd neuro-genesis-front
```

```bash

# Instale as dependências
npm install
```
```bash
# Inicie o projeto
ng serve --open
```

A interface abrirá automaticamente em:
http://localhost:4200/

🧩 Estrutura de Pastas
```bash

NEUROGENESISAI/
├── neuro-genesis-front/           # Frontend Angular
├── NeuroGenesisAI/                # Núcleo do projeto (lógica do cérebro)
├── NeuroGenesisAI.API/            # Backend .NET Core (Web API)
│   ├── Controllers/
│   ├── Services/
│   ├── Entities/
│   └── Snapshots/
├── NeuroGenesisAI.VisualizerWPF/  # (Opcional) Visualizador WPF do cérebro
├── brain.json                     # Configuração inicial do cérebro artificial
├── NeuroGenesisAI.sln            # Solução do Visual Studio
└── README.md                      # Documentação do projeto

```

🚀 Funcionalidades
Dashboard: Visualiza número de neurônios, clusters e emoções.

Gráficos: Estatísticas em tempo real do crescimento neural.

Logs: Monitoramento dos eventos registrados.

Gráfico de Conexões: Relações entre neurônios.

Envio de Estímulo: Permite influenciar o cérebro.

Exportação CSV: Snapshot de neurônios, conexões e clusters.

🛤️ Status do Projeto
Backend: Funcional ✅

Frontend: Em desenvolvimento 🚧

Documentação: Em andamento 📚

Próximos Passos:

Melhorar a visualização dos gráficos

Adicionar parâmetros de treinamento

Implementar IA rudimentar nos neurônios

Feito com dedicação para explorar as fronteiras entre vida, crescimento neural e inteligência artificial! 🧠🚀
