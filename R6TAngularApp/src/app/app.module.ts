import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PlayerComponent } from './player/player.component';
import { DxDataGridModule } from 'devextreme-angular';
import { DxiColumnModule } from 'devextreme-angular/ui/nested';

@NgModule({
  declarations: [
    AppComponent,
    PlayerComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    DxDataGridModule,
    DxiColumnModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
