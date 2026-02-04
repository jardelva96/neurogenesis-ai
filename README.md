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
## 🚀 Features

### Dashboard
- 📊 **Real-time Statistics**: Displays neuron count, cluster count, and emotion distribution
- 📈 **Interactive Charts**: 
  - Line chart showing neuron evolution over time
  - Pie chart displaying emotion distribution
- ⚡ **Stimulus Input**: Send external stimuli to influence brain behavior
- 🔄 **Cycle Control**: 
  - Manual cycle execution
  - Toggle automatic cycles on/off
  - Adjustable cycle interval (1-60 seconds)

### Monitoring & Analysis
- 📜 **Real-time Logs**: Monitor all brain events and activities
- 🕸️ **Connection Graph**: Visualize neuron relationships and connections
- 📤 **CSV Export**: Export snapshots of neurons, connections, and clusters for analysis

### Backend API
- ✅ **RESTful API**: Complete API for brain simulation control
- 🔄 **Background Service**: Automatic cycle execution
- 📊 **Status Endpoints**: Get brain state, clusters, and logs
- 🧪 **Stimulus Processing**: Emotion-based neuron responses

## 🛤️ Project Status

| Component | Status |
|-----------|--------|
| Backend | ✅ Fully Functional |
| Frontend | ✅ Functional with UI Controls |
| Documentation | 📚 Updated |
| Tests | 🚧 To be implemented |

## 📋 Recent Improvements

✅ Fixed C# nullability warnings  
✅ Added automatic brain cycle background service  
✅ Implemented cycle control API endpoints  
✅ Added cycle control UI in dashboard  
✅ Enhanced clusters endpoint to include neuron connections  
✅ Improved error handling and loading states  

## 🔭 Next Steps

🔹 Implement comprehensive unit tests  
🔹 Add more graph visualization options  
🔹 Implement training parameters and learning algorithms  
🔹 Add persistence layer (database) for brain state  
🔹 Enhance AI behavior and decision-making in neurons  

## 💡 Inspiration
Created with passion to explore the boundaries between life, neural growth, and artificial intelligence.
🧠🚀

