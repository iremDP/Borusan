import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { SiparisComponent } from './pages/siparis/siparis.component';


const routes: Routes = [
  { path: 'siparis', component: SiparisComponent }
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
