import { PlayerService } from './../services/player.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-player',
  templateUrl: './player.component.html',
  styleUrls: ['./player.component.scss']
})
export class PlayerComponent implements OnInit {

  players: any = [];
  playerGameStats: any = [];

  constructor(
    private playerService: PlayerService
  ) { }

  ngOnInit(): void {
    this.getPlayers();
  }

  getPlayers() {
    this.playerService.getPlayers().subscribe(res => {
      this.players = res || [];
    })
  }

  getPlayerGameStats(player: any) {
    let playerId = player.PlayerId;
    this.playerService.getPlayerGameStats(playerId).subscribe(res => {
      this.playerGameStats = res || [];
    })
  }

}
