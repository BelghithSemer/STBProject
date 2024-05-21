import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Credit } from 'app/models/Credit';
import { environment } from 'environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CreditService {
  private apiUrl: string =environment.apiUrl; 
  constructor(private http: HttpClient) { }

  public getData = (accountNumber:string) => {
    
     return this.http.get<Credit[]>(`https://localhost:7025/api/Credit/account/${accountNumber}`);

   }   

   public addCredit(c:Credit){
    return this.http.post<Credit>("https://localhost:7025/api/Credit",c);
   }
  
}
