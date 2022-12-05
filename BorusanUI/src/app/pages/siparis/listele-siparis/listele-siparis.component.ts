import { Component, OnInit } from '@angular/core';
import { Siparis } from 'src/app/models/Siparis';
import { SiparisServis } from 'src/app/services/SiparisServis';
import {MatDialog} from '@angular/material/dialog';
import { GuncelleSiparisComponent } from '../guncelle-siparis/guncelle-siparis.component';
import { SelectionModel } from '@angular/cdk/collections';
import { SiparisDurums } from 'src/app/models/enum/SiparisDurums';
import { MiktarBirims } from 'src/app/models/enum/MiktarBirims';
import { AgirlikBirims } from 'src/app/models/enum/AgirlikBirims';

@Component({
  selector: 'app-listele-siparis',
  templateUrl: './listele-siparis.component.html',
  styleUrls: ['./listele-siparis.component.css']
})
export class ListeleSiparisComponent implements OnInit {

  constructor(private servis: SiparisServis,
    public dialog: MatDialog) { }

  SiparisList: Siparis [] = [];
  selectedItem :Siparis = new Siparis();
  displayedColumns: string[] = ['seciniz','musteriSiparisNo', 'cikisAdresi', 'varisAdresi', 'miktar','miktarBirim','agirlik','agirlikBirim','malzemeKodu','not','siparisDurum'];
  
  ngOnInit(): void {
    this.getirSiparisListesi()
  }

  duzenleClick() {
  
      this.dialog.open(GuncelleSiparisComponent,{data:this.selectedItem});
   
  }

  getirSiparisListesi() {
    this.servis.getirSiparisListesi().subscribe((data)=> {
      this.SiparisList = data;
    }    ,   
     (error) => { 
      console.error(error)

    });
  }

  kapatClick() {
    this.getirSiparisListesi();
  }

 getirSiparisDurum(value : number) {
switch(value)
{
  case SiparisDurums.None:
    return '';

  case SiparisDurums.DagitimMerkezinde:
    return 'Dağıtım Merkezinde';

  case SiparisDurums.DagitimaCikti:
    return 'Dağıtıma Çıktı';

  case SiparisDurums.SiparisAlindi:
    return 'Sipariş Alındı';

  case SiparisDurums.TeslimEdildi:
    return 'Teslim Edildi';
  
  case SiparisDurums.TeslimEdilemedi:
    return 'Teslim Edilemedi';
  
    case SiparisDurums.YolaCikti:
    return 'Yola Çıktı';

}

    return SiparisDurums[value];
  }

  getirMiktarBirim(value : number) {
    return MiktarBirims[value];
  }

  getirAgirlikBirim(value : number) {
    return AgirlikBirims[value];
  }



}
