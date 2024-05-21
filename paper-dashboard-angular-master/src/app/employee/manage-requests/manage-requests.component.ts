import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Credit } from 'app/models/Credit';
import { MailRequest } from 'app/models/MailRequest';
import { demande } from 'app/models/demande';
import { users } from 'app/models/users';
import { CreditService } from 'app/services/Credit.service';
import { DemandeService } from 'app/services/demande.service';
import { UsersService } from 'app/services/users.service';

@Component({
  selector: 'app-manage-requests',
  templateUrl: './manage-requests.component.html',
  styleUrls: ['./manage-requests.component.scss']
})
export class ManageRequestsComponent implements OnInit {
user : users;
demandes:demande[]=[]
demande:demande;
credits:Credit[]=[]
capacity:number;
credit!: Credit;
mail!:MailRequest;
  constructor(private usersService: UsersService,private demandeService: DemandeService, private creditService:CreditService ) {
    this.user = { id :0,
      name:"string",
      lastName:"string",
      date :new Date(),
      idRef  :0,
      cin  :0,
      phoneNumber  :0,
      agency :"string",
      adsress :"string",
      email :"string",
      role :"string",
      accountNumber :"",
  } 
}
  ngOnInit(): void {
    console.log("start");
      this.demandeService.getData().subscribe((data) => {
        this.demandes = data; 
        console.log(this.demandes);
    });

  
  }
  getFileUrl(filePath: string): string {
    return `https://localhost:7025/api/CreditApplications/filename?fullFilePath=${encodeURIComponent(filePath)}`;
  }
  SetUser(d:demande){
    this.demande = d;
    this.usersService.getData(d.accountNumber).subscribe((data)=> {
      console.log(data);
      this.user=data;

    })
    this.creditService.getData(d.accountNumber).subscribe((data)=>{
      console.log(data);
      this.credits=data;
      this.capacity = 0;
      for(let c of this.credits){
        this.capacity+=c.totalDue;
      }
      this.capacity = ((d.salarity/100)*40)-this.capacity;
      console.log(this.capacity)
    });
    
  }

  UpdateDemande(res:string){
      this.demande.Status = res;
      console.log(this.demande)
      this.demandeService.UpdateDemande(this.demande).subscribe(()=>{
        console.log("succes")
      });
      this.credit = {
        id:undefined,
        accountNumber: this.demande.accountNumber,
        date: new Date(),
        totalIssued: this.demande.loanAmountRequested,
        TotalRedeemed: 0,
        balance: this.demande.loanAmountRequested,
        totalDue: 0,
        capacity: 0,
        NumberOfMonthsToRepay:12
      }
      console.log(this.credit)
      this.creditService.addCredit(this.credit).subscribe(()=>{
        console.log("Credit Added");
      })
      this.mail = {
        ToEmail:"semer.belghith@esprit.tn",
        Subject:"Test",
        Body:'Je suis un email de test !!!! '
      }
      this.demandeService.SendMail(this.mail).subscribe(()=>{
        console.log("Email sent");
      })
    
  }


}
