import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common'; 
import { BrainService, LogEntry } from '../../services/brain.service';

@Component({
  selector: 'app-logs',
  standalone: true,
  imports: [
    CommonModule
  ],
  templateUrl: './logs.component.html',
  styleUrls: ['./logs.component.css']
})
export class LogsComponent implements OnInit {

  logs: LogEntry[] = [];
  loading: boolean = false;
  errorMessage: string = '';

  constructor(private brainService: BrainService) {}

  ngOnInit(): void {
    this.loadLogs();
  }

  /**
   * Carrega todos os logs do cérebro.
   */
  loadLogs(): void {
    this.loading = true;
    this.errorMessage = '';

    this.brainService.getLogs().subscribe({
      next: (data) => {
        this.logs = (data ?? []).reverse(); // ✅ Proteção extra se data vier undefined
        this.loading = false;
      },
      error: (err) => {
        console.error('Erro ao carregar logs:', err);
        this.errorMessage = 'Erro ao carregar logs.';
        this.loading = false;
      }
    });
  }
}
