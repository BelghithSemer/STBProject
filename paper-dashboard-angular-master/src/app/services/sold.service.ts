import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { sold } from 'app/models/sold';

@Injectable({
  providedIn: 'root'
})
export class SoldService {
  private apiUrl = 'https://localhost:7025/api/Sold'; 

  constructor(private http: HttpClient) { }
 
  public getAccounts = (accountNumber:string) =>{
    return this.http.get<sold[]>(`https://localhost:7025/api/Sold?accountNumber=${accountNumber}`);
 }
 public getData = (accountNumber:string) => {
    
  return this.http.get<sold[]>(`https://localhost:7025/api/Sold?accountNumber=${accountNumber}`);

} 
 }
