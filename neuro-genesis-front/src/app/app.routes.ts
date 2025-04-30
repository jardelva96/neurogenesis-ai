import { Routes } from '@angular/router';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { ClustersComponent } from './pages/clusters/clusters.component';
import { LogsComponent } from './pages/logs/logs.component';
import { GraphComponent } from './pages/graph/graph.component';

export const routes: Routes = [
  { path: '', loadComponent: () => import('./pages/dashboard/dashboard.component').then(m => m.DashboardComponent) },
  { path: 'clusters', loadComponent: () => import('./pages/clusters/clusters.component').then(m => m.ClustersComponent) },
  { path: 'logs', loadComponent: () => import('./pages/logs/logs.component').then(m => m.LogsComponent) },
  { path: 'graph', loadComponent: () => import('./pages/graph/graph.component').then(m => m.GraphComponent) },
];
