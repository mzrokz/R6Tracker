import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { PlayerService } from 'src/app/services/player.service';

@Component({
  selector: 'app-add-player',
  templateUrl: './add-player.component.html',
  styleUrls: ['./add-player.component.scss']
})
export class AddPlayerComponent implements OnInit {

  @Output("onPlayerAdd") onPlayerAdd = new EventEmitter();

  popupVisible = false;
  player: any = {};

  constructor(
    private playerService: PlayerService
  ) { }

  ngOnInit(): void {
  }

  addPlayer() {
    this.playerService.addPlayer(this.player).subscribe(res => {
      if (res && res.PlayerId) {
        this.onPlayerAdd.emit('');
        this.popupVisible = false;
      }
    }, err => {
      if (err && err.error) {
        if (err.error.Message) {
          alert(err.error.Message);
        }
      }
    })
  }
}
