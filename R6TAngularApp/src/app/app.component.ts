import { Component, Output, ViewChild } from '@angular/core';
import { PlayerComponent } from './player/player.component';

@Component({
	selector: 'app-root',
	templateUrl: './app.component.html',
	styleUrls: ['./app.component.scss'],
})
export class AppComponent {
	title = 'R6TAngularApp';

	@ViewChild(PlayerComponent)
	playerView!: PlayerComponent;

	constructor() {}

	onPlayerAdd() {
		this.playerView.getPlayers();
	}

	syncAllPlayers() {
		this.playerView.syncAllPlayers();
	}
}
