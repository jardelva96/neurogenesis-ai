import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

/**
 * Representa um log do cérebro.
 */
export interface LogEntry {
  timestamp: string;
  message: string;
}

/**
 * Representa o status geral do cérebro.
 */
export interface BrainStatus {
  neuronCount: number;
  clusterCount: number;
  emotions: { [emotion: string]: number };
}

/**
 * Representa um cluster e seus neurônios.
 */
export interface ClusterStatus {
  name: string;
  neurons: { id: string; maturityLevel: number; connections: string[] }[];
}

/**
 * Representa a configuração do ciclo automático.
 */
export interface CycleConfig {
  enabled: boolean;
  intervalMs: number;
}

@Injectable({
  providedIn: 'root'
})
export class BrainService {
  private apiUrl = 'http://localhost:5049/brain'; // Endereço da API do backend

  constructor(private http: HttpClient) { }

  /**
   * Envia um estímulo para o cérebro.
   */
  sendStimulus(stimulus: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/stimulate`, { stimulus });
  }

  /**
   * Consulta o status atual do cérebro (neurônios, clusters, emoções).
   */
  getBrainStatus(): Observable<BrainStatus> {
    return this.http.get<BrainStatus>(`${this.apiUrl}/status`);
  }

  /**
   * Consulta todos os clusters e seus neurônios.
   */
  getClusters(): Observable<ClusterStatus[]> {
    return this.http.get<ClusterStatus[]>(`${this.apiUrl}/clusters`);
  }

  /**
   * Consulta os logs de eventos do cérebro.
   */
  getLogs(): Observable<LogEntry[]> {
    return this.http.get<LogEntry[]>(`${this.apiUrl}/logs`);
  }

  /**
   * Executa um ciclo manual de simulação.
   */
  executeCycle(): Observable<any> {
    return this.http.post(`${this.apiUrl}/cycle`, {});
  }

  /**
   * Obtém a configuração do ciclo automático.
   */
  getCycleConfig(): Observable<CycleConfig> {
    return this.http.get<CycleConfig>(`${this.apiUrl}/cycle/config`);
  }

  /**
   * Configura o ciclo automático.
   */
  setCycleConfig(enabled?: boolean, intervalMs?: number): Observable<any> {
    return this.http.post(`${this.apiUrl}/cycle/config`, { enabled, intervalMs });
  }
}
