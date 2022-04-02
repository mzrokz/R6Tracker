import { Component, EventEmitter, Input, Output } from '@angular/core';

import {
	faArrowDown,
	faArrowUp,
	faCheckCircle,
	faExclamationCircle,
	faExternalLinkAlt,
	faPowerOff,
	faYinYang,
} from '@fortawesome/free-solid-svg-icons';
import { PlayerService } from 'src/app/services/player.service';

@Component({
	selector: 'app-player-card',
	templateUrl: './player-card.component.html',
	styleUrls: ['./player-card.component.scss'],
})
export class PlayerCardComponent {
	@Input() player: any = null;

	@Output() getPlayersEvent: EventEmitter<void> = new EventEmitter();
	@Output() getPlayerStatsEvent: EventEmitter<any> = new EventEmitter();
	@Output() syncPlayerEvent: EventEmitter<any> = new EventEmitter();

	syncStart = faYinYang;
	syncSuccess = faCheckCircle;
	syncError = faExclamationCircle;
	powerOff = faPowerOff;
	link = faExternalLinkAlt;
	sortUp = faArrowUp;
	sortDown = faArrowDown;

	constructor(private playerService: PlayerService) {}

	setActive() {
		this.playerService.setActive(this.player).subscribe((res) => {
			this.getPlayersEvent.emit();
		});
	}

	gotoR6Tracker() {
		if (this.player.Url && this.player.Url != '') {
			window.open(this.player.Url);
		}
	}

	setSort(type: string) {
		this.player.SortType = type;
		this.playerService.setSort(this.player).subscribe((res) => {
			this.getPlayersEvent.emit();
		});
	}

	getPlayerGameStats() {
		this.getPlayerStatsEvent.emit(this.player);
	}

	syncPlayerData() {
		this.syncPlayerEvent.emit(this.player);
	}
}
