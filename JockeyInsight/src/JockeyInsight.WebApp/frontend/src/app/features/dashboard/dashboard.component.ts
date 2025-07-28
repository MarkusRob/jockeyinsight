import {Component} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule} from "@angular/forms";
import {JockeyPerformance} from "../../core/models/jockey-performance.interface";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'dashboard',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent {


  horses: string[] = [];
  selectedHorse: string = '';
  jockeyStats: JockeyPerformance[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.loadHorses();
  }

  loadHorses() {
    // Fetch add this to a service,
    this.http.get<string[]>('/api/v1/horses').subscribe({
      next: (horses) => {
        this.horses = horses;
        if (horses.length > 0) {
          this.selectedHorse = horses[0];
          this.loadJockeyStats();
        }
      }
    });
  }

  loadJockeyStats() {
    if (!this.selectedHorse) {
      this.jockeyStats = [];
      return;
    }
    const trimmedHorseName = this.selectedHorse.split(' ')[0];
    const uri = `/api/v1/insight/jockey_performance?horse=${encodeURIComponent(trimmedHorseName)}`;
    this.http.get<JockeyPerformance[]>(uri).subscribe({
      next: (stats) => {
        this.jockeyStats = stats;
      },
      error: () => {
        this.jockeyStats = [];
      }
    });
  }

  getRowClass(stat: JockeyPerformance) {
    if (stat.winRate > 50) return 'table-success';
    if (stat.winRate >= 25) return 'table-warning';
    return 'table-danger';
  }
}
