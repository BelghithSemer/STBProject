import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Credit } from 'app/models/Credit';
import { cards } from 'app/models/cards';
import { environment } from 'environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CardsService {
  private apiUrl: string =environment.apiUrl; 
  constructor(private http: HttpClient) { }

  public getData = (accountNumber:string) => {
    
     return this.http.get<cards[]>(`https://localhost:7025/api/CreditCards?accountNumber=${accountNumber}`);

   }
   public getAllCards() {
    return this.http.get<cards[]>('https://localhost:7025/api/CreditCards/all');
   }   

  
  
}