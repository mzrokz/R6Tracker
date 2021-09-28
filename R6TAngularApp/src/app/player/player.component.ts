import { PlayerService } from './../services/player.service';
import { Component, OnInit } from '@angular/core';
import { faAngleDown, faAngleUp, faArrowDown, faArrowUp, faCheckCircle, faCoffee, faExclamationCircle, faExternalLinkAlt, faPowerOff, faYinYang } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-player',
  templateUrl: './player.component.html',
  styleUrls: ['./player.component.scss']
})
export class PlayerComponent implements OnInit {

  players: any = [];
  playerGameStats: any = [];

  syncStart = faYinYang;
  syncSuccess = faCheckCircle;
  syncError = faExclamationCircle;
  powerOff = faPowerOff;
  link = faExternalLinkAlt;
  sortUp = faArrowUp;
  sortDown = faArrowDown;

  currentPlayerGrid: any = null;

  constructor(
    private playerService: PlayerService
  ) { }

  ngOnInit(): void {
    this.getPlayers();
  }

  public getPlayers() {
    this.playerService.getPlayers().subscribe(res => {
      this.players = res || [];
    })
  }

  getPlayerGameStats(player: any) {
    this.currentPlayerGrid = Object.assign(player);
    let playerId = player.PlayerId;
    this.playerService.getPlayerGameStats(playerId).subscribe(res => {
      this.playerGameStats = res || [];
      this.players.forEach((p: any) => {
        p.Selected = false;
      });
      player.Selected = true;
    })
  }

  syncPlayerData(player: any) {
    player.syncStart = true;
    this.playerService.syncPlayerData(player).subscribe(res => {
      // alert("done");
      player.syncSuccess = true;
      player.syncStart = false;
      player.syncError = false;

      this.playerService.getPlayer(player).subscribe(res => {
        player.RankUrl = res.RankUrl;
        player.LatestAlias = res.LatestAlias;
      });

      if (this.currentPlayerGrid && player.PlayerId == this.currentPlayerGrid.PlayerId) {
        this.getPlayerGameStats(this.currentPlayerGrid);
      }
    }, (err) => {
      // alert(err.Message);
      player.errMsg = err.Message;
      player.syncSuccess = false;
      player.syncStart = false;
      player.syncError = true;
    });
  }

  setActive(player: any) {
    this.playerService.setActive(player).subscribe(res => {
      this.getPlayers();
    });
  }

  gotoR6Tracker(player: any) {
    if (player.Url && player.Url != '') {
      window.open(player.Url);
    }
  }


  public syncAllPlayers() {
    this.players.forEach((player: any) => {
      this.syncPlayerData(player);
    });
  }

  showImgRankTooltip(d: any) {
    // console.log("showImgRankTooltip", d);
    d.showToolTip = !d.showToolTip;
  }

  setSort(player: any, type: string) {
    player.SortType = type;
    this.playerService.setSort(player).subscribe(res => {
      this.getPlayers();
    });
  }
}
