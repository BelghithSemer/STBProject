import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AmortizationPayment, AmortizationResult } from 'app/models/AmortizationPayment';
import { CreditRequest } from 'app/models/CreditRequest';
import { SimulateurService } from 'app/services/simulateur.service';

@Component({
    selector: 'simulateur-cmp',
    moduleId: module.id,
    templateUrl: 'simulateur.component.html'
})

export class IconsComponent{
amortissement!:AmortizationResult;
Payments:AmortizationPayment[] = [];
showTable: boolean = false;

simulateurform:FormGroup;
Credit!:CreditRequest 
constructor(private simulateurService: SimulateurService,sf:FormBuilder) {
  this.simulateurform=sf.group({
    montant:['' ,Validators.required],
    taux:['' ,Validators.required],
    periode:['' ,Validators.required],
    duree:['' ,Validators.required]
  });
  this.Credit = {
    id:0,
    CreditAmount : 0,
    CreditDuration : 0,
    Periodicity : "",
    InterestRate : 0
  };
  this.amortissement= {
    id:0,
    payments : []
  }
}

calculerAmortissement(){
  this.Credit.CreditAmount=this.simulateurform.get("montant")?.value;
  this.Credit.InterestRate=this.simulateurform.get("taux")?.value;
  this.Credit.Periodicity=this.simulateurform.get("periode")?.value;
  this.Credit.CreditDuration=this.simulateurform.get("duree")?.value;

console.log("start");
  this.simulateurService.calculerAmortissement(this.Credit).subscribe(
    (data) => {
      this.amortissement = data;

      this.Payments = this.amortissement.payments;
      this.showTable = true;
    },
    (error) => {
      console.error('Error fetching amortissement:', error);
    }
  )

function calculerAmortissement() {
    throw new Error('Function not implemented.');
}
}
}
