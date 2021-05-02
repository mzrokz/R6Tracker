import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';

import { DxButtonModule, DxDataGridModule, DxPopupModule, DxTooltipModule } from 'devextreme-angular';
import { DxiColumnModule } from 'devextreme-angular/ui/nested';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

import { AppComponent } from './app.component';
import { PlayerComponent } from './player/player.component';
import { AddPlayerComponent } from './player/add-player/add-player.component';
import { FormsModule } from '@angular/forms';


const DxiModules = [
  // DX -------------------
  DxDataGridModule,
  DxButtonModule,
  DxPopupModule,
  DxTooltipModule,
  // DXI -------------------
  DxiColumnModule
]
@NgModule({
  declarations: [
    AppComponent,
    PlayerComponent,
    AddPlayerComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ...DxiModules,
    FontAwesomeModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
