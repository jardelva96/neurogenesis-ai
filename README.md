# 🧠 NeuroGenesis AI

An experimental project simulating artificial neural growth using **.NET 8** and **Angular 17**.

---

## 📖 About the Project

**NeuroGenesis AI** is a platform that simulates the behavior of a virtual brain, enabling:

- 🧬 Birth, maturation, and connection of neurons  
- 🧠 Formation of neural clusters  
- ⚡ External stimuli that alter the brain's state  
- 📁 Export of CSV snapshots for further analysis  

> This is the first step toward building a self-adaptive system inspired by human neurogenesis.

⚠️ The **frontend is currently in early development** and more visualization features will be added soon.

---

## 🛠 Technologies

### Backend (.NET Core 8)
- `ASP.NET Core 8`
- `C# 12`
- `REST API`
- `CSV file generation`

### Frontend (Angular)
- `Angular 17` (standalone)
- `Vite.js` (build + HMR)
- `Ngx-Charts`
- `Ngx-Graph`
- `Bootstrap` *(planned for layout)*

---

## ⚙️ Installation

### 🔧 Backend (.NET)
```bash
# Clone the repository
git clone https://github.com/jardelva96/neurogenesis-ai.git

# Go to the API directory
cd NeuroGenesisAI/NeuroGenesisAI.API

# Run the API
dotnet run
```

API available at:
👉 http://localhost:5049/

🖥️ Frontend (Angular)
```bash

# Go to the frontend folder
cd neuro-genesis-front

# Install dependencies
npm install

# Start the frontend
ng serve --open
```
Interface opens at:
👉 http://localhost:4200/

🧩 Folder Structure
```bash

NEUROGENESISAI/
├── neuro-genesis-front/           # Angular Frontend
├── NeuroGenesisAI/                # Core brain logic
├── NeuroGenesisAI.API/            # .NET Core Web API
│   ├── Controllers/
│   ├── Services/
│   ├── Entities/
│   └── Snapshots/
├── NeuroGenesisAI.VisualizerWPF/  # (Optional) WPF Brain Visualizer
├── brain.json                     # Initial brain configuration
├── NeuroGenesisAI.sln             # Visual Studio solution file
└── README.md                      # Project documentation
```
🚀 Features
📊 Dashboard: Displays neuron, cluster, and emotion count

📈 Charts: Real-time neural growth statistics

📜 Logs: Monitors brain events

🕸️ Connection Graph: Visualizes neuron relationships

✉️ Stimulus Input: Allows user interaction with the brain

📤 CSV Export: Snapshots of neurons, connections, and clusters

🛤️ Project Status

Component	Status
Backend	✅ Functional
Frontend	🚧 In development
Documentation	📚 In progress

🔭 Next Steps
🔹 Improve graph visualizations

🔹 Add training parameters

🔹 Implement basic AI behavior in neurons

💡 Inspiration
Created with passion to explore the boundaries between life, neural growth, and artificial intelligence.
🧠🚀

