import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { transaction } from 'app/models/transaction';
import { environment } from 'environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {
  private apiUrl: string =environment.apiUrl; 
  constructor(private http: HttpClient) { }

  public getData = (accountNumber:string) => {
    
    return this.http.get<transaction[]>(`https://localhost:7025/api/Transaction/account/${accountNumber}`);
   }   
  public update = (transaction:any) => {
    return this.http.put('https://localhost:7025/api/Transaction/',transaction );
  }
 
  public delete = (transaction:any) => {
    return this.http.delete('https://localhost:7025/api/Transaction/',transaction );
  }
 
}
 
  
