import { Injectable } from '@angular/core';
import { HttpClient, HttpEvent, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
@Injectable({
    providedIn: 'root'
  })
  export class SiparisServis {
    readonly apiUrl = 'https://localhost:44337/api/Siparis';
  
    constructor(private http: HttpClient) { }
  
    // Department
    getirSiparisListesi(): Observable<any[]> {
      return this.http.get<any[]>(this.apiUrl + '/GetirSiparisListesi');
    }
  
    guncelleSiparisStatu(dept: any): Observable<any> {
      const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
      return this.http.post<any>(this.apiUrl + '/GuncelleSiparisStatu', dept, httpOptions);
    }

  
  }