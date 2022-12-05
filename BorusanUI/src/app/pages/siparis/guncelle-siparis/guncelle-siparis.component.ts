import { Component, OnInit ,Input, Inject} from '@angular/core';
import { Siparis } from 'src/app/models/Siparis';
import { SiparisServis } from 'src/app/services/SiparisServis';
import { SiparisDurums } from 'src/app/models/enum/SiparisDurums';
import {MatDialog, MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';

@Component({
  selector: 'app-guncelle-siparis',
  templateUrl: './guncelle-siparis.component.html',
  styleUrls: ['./guncelle-siparis.component.css']
})
export class GuncelleSiparisComponent implements OnInit {

  selectedStatus :number = 0;

  constructor(private servis: SiparisServis,
       public dialogRef: MatDialogRef<GuncelleSiparisComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Siparis) { 
 

  }
  ngOnInit(): void {
  }



  guncelleClick(): void {
    this.data.siparisDurum = +this.selectedStatus;
    this.servis.guncelleSiparisStatu(this.data).subscribe((data)=> {
    }    ,   
     (error) => { 
      console.error(error)

    });

    this.dialogRef.close();
  }


}
