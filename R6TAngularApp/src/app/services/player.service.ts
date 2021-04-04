import { environment } from './../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PlayerService {

  constructor(
    private http: HttpClient
  ) { }

  getPlayers() {
    return this.http.get<any>(environment.apiUrl + 'api/Player/GetPlayers')
  }

  getPlayerGameStats(playerId: any) {
    return this.http.get<any>(environment.apiUrl + 'api/Player/GetGameStats?playerId=' + playerId)
  }

  syncPlayerData(player: any) {
    return this.http.post<any>(environment.apiUrl + 'api/Player/SyncPlayerData', player);
  }
}
