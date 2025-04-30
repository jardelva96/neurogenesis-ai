import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { Subscription, interval } from 'rxjs';
import { saveAs } from 'file-saver';

import { BrainService, BrainStatus, LogEntry } from '../../services/brain.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    NgxChartsModule
  ],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit, OnDestroy {

  brainStatus?: BrainStatus;
  stimulusInput: string = '';

  neuronData: any[] = [];      
  neuronHistory: any[] = [];   
  cycleCounter: number = 0;    
  logs: LogEntry[] = [];       

  loading: boolean = false;
  errorMessage: string = '';

  private refreshSubscription?: Subscription;

  constructor(private brainService: BrainService) {}

  ngOnInit(): void {
    this.loadBrainStatus();
    this.loadLogs();

    this.refreshSubscription = interval(5000).subscribe(() => {
      this.loadBrainStatus();
      this.loadLogs();
    });
  }

  ngOnDestroy(): void {
    this.refreshSubscription?.unsubscribe();
  }

  loadBrainStatus(): void {
    this.loading = true;
    this.errorMessage = '';

    this.brainService.getBrainStatus().subscribe({
      next: (status) => {
        this.brainStatus = status;
        this.prepareNeuronData();
        this.updateNeuronHistory();
        this.loading = false;
      },
      error: (err) => {
        console.error('Erro ao buscar status do cérebro:', err);
        this.errorMessage = 'Erro ao carregar status do cérebro.';
        this.loading = false;
      }
    });
  }

  prepareNeuronData(): void {
    if (!this.brainStatus) return;

    this.neuronData = Object.entries(this.brainStatus.emotions || {}).map(([emotion, count]) => ({
      name: emotion,
      value: count
    }));
  }

  updateNeuronHistory(): void {
    if (!this.brainStatus) return;

    this.cycleCounter++;
    this.neuronHistory.push({
      name: `Ciclo ${this.cycleCounter}`,
      value: this.brainStatus.neuronCount
    });

    if (this.neuronHistory.length > 20) {
      this.neuronHistory.shift();
    }
  }

  sendStimulus(): void {
    const stimulus = this.stimulusInput.trim();
    if (!stimulus) return;

    this.brainService.sendStimulus(stimulus).subscribe({
      next: () => {
        console.log('Estímulo enviado com sucesso:', stimulus);
        this.stimulusInput = '';
        this.loadBrainStatus();
        this.loadLogs();
      },
      error: (err) => {
        console.error('Erro ao enviar estímulo:', err);
        this.errorMessage = 'Erro ao enviar estímulo.';
      }
    });
  }

  loadLogs(): void {
    this.brainService.getLogs().subscribe({
      next: (data) => {
        this.logs = data.reverse();
      },
      error: (err) => {
        console.error('Erro ao carregar logs:', err);
      }
    });
  }

  emotionEntries(): [string, number][] {
    return this.brainStatus?.emotions ? Object.entries(this.brainStatus.emotions) : [];
  }

  downloadCsv(type: 'neurons' | 'connections' | 'clusters'): void {
    const url = `http://localhost:5049/brain/snapshot/${type}`;

    fetch(url)
      .then(response => response.blob())
      .then(blob => {
        saveAs(blob, `${type}.csv`);
      })
      .catch(err => {
        console.error('Erro ao baixar CSV:', err);
      });
  }
}
