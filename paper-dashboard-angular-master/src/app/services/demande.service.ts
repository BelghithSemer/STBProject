import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { demande } from 'app/models/demande';
import { environment } from 'environments/environment';
import { MailRequest } from 'app/models/MailRequest';

@Injectable({
 providedIn: 'root'
})
   export class DemandeService {
      private apiUrl: string = environment.apiUrl; 
      constructor(private http: HttpClient) { }
    
      addDemande(data: demande){
         console.log(demande);
         return this.http.post("https://localhost:7025/api/CreditApplications/adddemande", data);
      }

      public add = (demande : FormData) => {
         console.log(demande);
         return this.http.post("https://localhost:7025/api/CreditApplications/adddemande",demande);
      }
      public getData = () => {
    
         return this.http.get<demande[]>("https://localhost:7025/api/CreditApplications/GetAll");
    
       }  

      public UpdateDemande(d : demande){
         return this.http.put(`https://localhost:7025/api/CreditApplications/${d.id}`, d);
      }

      public SendMail(m: MailRequest){
         return this.http.post("https://localhost:7025/api/CreditApplications/SendMail",m);
      }

      public UploadFiles(id:number, file : File){
         const formData = new FormData();
         formData.append('file',file);
         const headers = new HttpHeaders().append('Content-Disposition', 'multipart/form-data');
         return this.http.post(`https://localhost:7025/api/CreditApplications/UploadFiles/${id}`, formData,{headers});
      }

     }