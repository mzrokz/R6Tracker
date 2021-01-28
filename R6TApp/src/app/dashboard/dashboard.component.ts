import { environment } from './../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  players: any = [];

  constructor(
    private http: HttpClient
  ) { }

  ngOnInit(): void {
    this.http.get(environment.apiUrl + 'Player/GetPlayers').subscribe(res => {
      this.players = res;
    })
  }

  onPlayerClick(player, event) {
    this.http.get(environment.apiUrl + 'Player/GetGameStats?playerId=' + player.PlayerId).subscribe(res => {
      this.players = res;
    })
  }

}
