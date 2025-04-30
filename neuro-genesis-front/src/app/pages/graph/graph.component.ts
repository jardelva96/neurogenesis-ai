import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common'; 
import { Node, Edge, NgxGraphModule } from '@swimlane/ngx-graph'; 
import { BrainService } from '../../services/brain.service';

@Component({
  selector: 'app-graph',
  standalone: true,
  imports: [
    CommonModule,
    NgxGraphModule
  ],
  templateUrl: './graph.component.html',
  styleUrls: ['./graph.component.css']
})
export class GraphComponent implements OnInit {

  nodes: Node[] = [];
  links: Edge[] = [];

  constructor(private brainService: BrainService) {}

  ngOnInit(): void {
    this.loadGraphData();
  }

  /**
   * Carrega os neurônios e conexões para montar o grafo.
   */
  loadGraphData(): void {
    this.brainService.getClusters().subscribe({
      next: (clusters) => {
        const nodesSet = new Set<string>();
        const linksList: Edge[] = [];

        clusters.forEach((cluster) => {
          cluster.neurons.forEach((neuron: any) => {
            nodesSet.add(neuron.id);

            if (neuron.connections && neuron.connections.length > 0) {
              neuron.connections.forEach((targetId: string) => {
                linksList.push({
                  id: `${neuron.id}-${targetId}`,
                  source: neuron.id,
                  target: targetId,
                  label: '' // opcionalmente adicionar label
                });
              });
            }
          });
        });

        this.nodes = Array.from(nodesSet).map(id => ({
          id,
          label: id
        }));

        this.links = linksList;
      },
      error: (err) => {
        console.error('Erro ao carregar o grafo:', err);
      }
    });
  }
}
