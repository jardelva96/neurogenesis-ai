import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

// Importação dos componentes
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { ClustersComponent } from './pages/clusters/clusters.component';
import { LogsComponent } from './pages/logs/logs.component';
import { GraphComponent } from './pages/graph/graph.component'; // 👈 Novo componente!

const routes: Routes = [
  { path: '', component: DashboardComponent },        // Página inicial
  { path: 'clusters', component: ClustersComponent },  // Visualizar clusters
  { path: 'logs', component: LogsComponent },          // Visualizar logs
  { path: 'graph', component: GraphComponent },        // Novo: Visualizar grafo neural
  // { path: '**', redirectTo: '' } // Futuro: redirecionar páginas inválidas
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
