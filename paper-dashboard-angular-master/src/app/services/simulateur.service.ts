import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreditRequest } from '../models/CreditRequest';
import { AmortizationPayment, AmortizationResult } from '../models/AmortizationPayment';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class SimulateurService {
 private apiUrl: string =environment.apiUrl; 

  constructor(private http: HttpClient) { }

  calculerAmortissement(credit: CreditRequest) {
  return this.http.post<AmortizationResult>('https://localhost:7025/api/CreditSimulator/calculateAmortization', credit);
  }
    
}