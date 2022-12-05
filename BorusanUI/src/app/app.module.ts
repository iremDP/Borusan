import { NgModule} from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SiparisComponent } from './pages/siparis/siparis.component';
import { ListeleSiparisComponent } from './pages/siparis/listele-siparis/listele-siparis.component';
import { GuncelleSiparisComponent } from './pages/siparis/guncelle-siparis/guncelle-siparis.component';
import { SiparisServis } from './services/SiparisServis';
import{HttpClientModule} from '@angular/common/http';
import {FormsModule,ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatTableModule} from '@angular/material/table';
import {MatDialogModule} from '@angular/material/dialog';
import {MatListModule} from '@angular/material/list';
import {MatSelectModule} from '@angular/material/select';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatButtonModule} from '@angular/material/button';
import {MatCheckboxModule} from '@angular/material/checkbox'
import {MatRadioModule} from '@angular/material/radio';


@NgModule({
  declarations: [
    AppComponent,
    SiparisComponent,
    ListeleSiparisComponent,
    GuncelleSiparisComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    MatTableModule,
    MatDialogModule,
    MatListModule,
    MatSelectModule,
    MatFormFieldModule,
    MatButtonModule,
    MatCheckboxModule,
    MatRadioModule
  ],
  providers: [SiparisServis],
  bootstrap: [AppComponent]
})
export class AppModule { }
