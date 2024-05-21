import { HttpClient } from '@angular/common/http';
import { Component,OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { demande } from 'app/models/demande';
import { DemandeService } from 'app/services/demande.service';
import { Router } from 'express';



declare var google: any;

@Component({
    moduleId: module.id,
    selector: 'demandeCredit-cmp',
    templateUrl: 'demandeCredit.component.html',

})
export class demandeCredit {
  firstFormGroup: FormGroup;
  secondFormGroup: FormGroup;
  thirdFormGroup: FormGroup
  isLinear = false;
  demande : demande;
  selectedFiles: File[] = [];

  

 
  constructor(private formBuilder: FormBuilder,private DemandeService: DemandeService) {

    this.demande = {
      id: 1,
      firstName: "string",
      lastName: "string",
      currentResidentialAddress: "string",
      phoneNumber: "string",
      dateOfBirth: new Date(),
      salarity: 0,
      maritalStatus: "string",
      job: "string",
      seniority: 0,
      contract: "string",
      otherIncomes: 0,
      repaymentYrears: 0,
      loanAmountRequested: 0,
      loanPurpose: "string",
      accountNumber: "2123456789",
      files: [],
      userid: 1,
      Status : 'non-traitee'
    };
  }
 
  ngOnInit() {
    console.log('Initializing form groups');
     this.firstFormGroup = this.formBuilder.group({
       nom: ['', Validators.required],
       prenom: ['', Validators.required],
       date: ['', Validators.required],
       adresse: ['', Validators.required],
       numero: ['', Validators.required],
       etat: ['', Validators.required],
      
       
     });

     this.secondFormGroup = this.formBuilder.group({
       profession: ['', Validators.required],
       anciennete: ['', Validators.required],
       contrat: ['', Validators.required],
       salaire: ['', Validators.required],
       revenus: ['', Validators.required]
     });

    

     this.thirdFormGroup = this.formBuilder.group({
      montant: ['', Validators.required],
      annee: ['', Validators.required],
      raison: ['', Validators.required],
      AttachmentFileName: ['', Validators.required],
      files: ['', Validators.required]


    });
    console.log(this.firstFormGroup);

  }
  onFileChange(event: any) {
    if (event.target.files.length > 0) {
      this.selectedFiles = Array.from(event.target.files);
    }
  }
  submit(){
    const formData = new FormData();
/*
    this.selectedFiles.forEach((file, index) => {
      formData.append(`files`, file, file.name);
    });

    Object.keys(this.firstFormGroup.controls).forEach(key => {
      formData.append(key, this.firstFormGroup.get(key)?.value);
    })
    Object.keys(this.thirdFormGroup.controls).forEach(key => {
      if (key != 'files') {
        formData.append(key, this.thirdFormGroup.get(key)?.value);
      } 
    });*/
    formData.append('AccountNumber','123456789');
    formData.append('firstName', 'semer');
    formData.append('lastName', 'belghith');
    formData.append('currentResidentialAddress', 'address');
    formData.append('phoneNumber', '20230154');
    formData.append('maritalStatus', 'single');

    formData.append('job', this.secondFormGroup.get('profession')?.value);
    formData.append('seniority', this.secondFormGroup.get('anciennete')?.value);
    formData.append('contract', this.secondFormGroup.get('contrat')?.value);
    formData.append('salarity', this.secondFormGroup.get('salaire')?.value);
    formData.append('otherIncomes', this.secondFormGroup.get('revenus')?.value);

    formData.append('loanAmountRequested', this.thirdFormGroup.get('montant')?.value);
    formData.append('repaymentYrears', this.thirdFormGroup.get('annee')?.value);
    formData.append('loanPurpose', this.thirdFormGroup.get('raison')?.value);

    this.selectedFiles.forEach((file, index) => {
      formData.append('files', file, file.name);
    });

    formData.append('userid', '1'); // Assuming you set userid elsewhere
    formData.append('status', 'non-traitee');
    console.log(formData.getAll)
    this.DemandeService.add(formData).subscribe(
      response => {
        console.log('Credit application submitted successfully', response);
        alert("Votre Demande a ete enregistrer ")
        this.ngOnInit();
      },
      error => {
        console.error('Error submitting credit application', error);
      }
    );


  }

  onSubmit() {
    // Mise à jour des données de la demande avec les valeurs du formulaire
    this.demande.job = this.secondFormGroup.get('profession').value;
    this.demande.seniority = this.secondFormGroup.get('anciennete').value;
    this.demande.contract = this.secondFormGroup.get('contrat').value;
    this.demande.salarity = this.secondFormGroup.get('salaire').value;
    this.demande.otherIncomes = this.secondFormGroup.get('revenus').value;
    this.demande.loanAmountRequested = this.thirdFormGroup.get('montant').value;
    this.demande.repaymentYrears = this.thirdFormGroup.get('annee').value;
    this.demande.loanPurpose = this.thirdFormGroup.get('raison').value;
    this.demande.files = this.thirdFormGroup.get('files').value;
    
    // Soumission de la demande
    /*this.DemandeService.add(this.demande).subscribe(
       {
         next: (data) => {
           // Message de succès
           alert('Votre formulaire a été soumis avec succès!');
           console.log(data)
           
           // Réinitialisation du formulaire ou redirection selon votre logique d'application
           this.ngOnInit();
         },
         error: (error) => {
           // Message d'erreur
           alert('Une erreur est survenue lors de la soumission du formulaire. Veuillez vérifier vos données et réessayer.');
           console.error('Erreur lors de la soumission du formulaire:', error);
         }
       }
    );*/

    /// uploading files 
    /*console.log("start uploading ")
    let files = this.thirdFormGroup.get('files').value
    if(files){
      for(let i = 0; i<files.length ; i++){
        this.DemandeService.UploadFiles(this.demande)
      }
    }*/
   }
  }   