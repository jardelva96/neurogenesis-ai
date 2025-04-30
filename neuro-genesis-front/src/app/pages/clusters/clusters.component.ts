import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common'; // Necessário para *ngIf, *ngFor
import { BrainService, ClusterStatus } from '../../services/brain.service';

@Component({
  selector: 'app-clusters',
  standalone: true,
  imports: [
    CommonModule
  ],
  templateUrl: './clusters.component.html',
  styleUrls: ['./clusters.component.css']
})
export class ClustersComponent implements OnInit {

  clusters: ClusterStatus[] = [];
  loading: boolean = false;
  errorMessage: string = '';

  constructor(private brainService: BrainService) {}

  ngOnInit(): void {
    this.loadClusters();
  }

  /**
   * Carrega a lista de clusters do cérebro.
   */
  loadClusters(): void {
    this.loading = true;
    this.errorMessage = '';

    this.brainService.getClusters().subscribe({
      next: (data) => {
        this.clusters = data;
        this.loading = false;
      },
      error: (err) => {
        console.error('Erro ao carregar clusters', err);
        this.errorMessage = 'Erro ao carregar clusters.';
        this.loading = false;
      }
    });
  }
}
