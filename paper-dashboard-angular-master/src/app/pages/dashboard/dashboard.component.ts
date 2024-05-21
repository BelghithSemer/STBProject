import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Credit } from 'app/models/Credit';
import { cards } from 'app/models/cards';
import { sold } from 'app/models/sold';
import { transaction } from 'app/models/transaction';
import { users } from 'app/models/users';
import { CreditService } from 'app/services/Credit.service';
import { CardsService } from 'app/services/cards.service';
import { SoldService } from 'app/services/sold.service';
import { TransactionService } from 'app/services/transaction.service';
import { UsersService } from 'app/services/users.service';
import Chart from 'chart.js';
import { setDefaultResultOrder } from 'dns';

@Component({
    selector: 'dashboard-cmp',
    moduleId: module.id,
    templateUrl: 'dashboard.component.html'
})
export class DashboardComponent implements OnInit {
 transactions: transaction[] = [];
 credits: Credit[];
 accounts: sold[] = [];
 cards:cards[] = [];
  user : users;
 constructor(
    private transactionService: TransactionService,
    private creditService: CreditService,
    private soldService: SoldService,
    private cardService: CardsService
    
 ) {}

 ngOnInit() {
  // retrieving user from sessionStorage 
  this.user = JSON.parse(sessionStorage.getItem('user'));
  console.log(this.user.accountNumber);

  this.loadTransactions();
  this.loadCredits();
  this.loadSolds();
  this.loadCards();
  

}

loadTransactions() {
  this.transactionService.getData(this.user.accountNumber).subscribe((data) => {
    this.transactions = data; 
    console.log(this.transactions)
  });
}

loadCredits() {
  this.creditService.getData(this.user.accountNumber).subscribe((data) => {
    this.credits = data;
    console.log(this.credits);
  });
}
loadSolds() {
  this.soldService.getData(this.user.accountNumber).subscribe(
     (data) => {
       this.accounts=data;
       console.log(this.accounts);
     }
  );
 }
 loadCards() {
  this.cardService.getData(this.user.accountNumber).subscribe(
     (data) => {
       this.cards=data;
       
       console.log(this.cards);
       this.cards.forEach((e) => {
        e.usedPercentage = (e.solde/e.plafond) *100
       })
     }
     
  );

 }
}



