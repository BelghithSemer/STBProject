import { Component, OnInit } from '@angular/core';
import { cards } from 'app/models/cards';
import { demande } from 'app/models/demande';
import { CardsService } from 'app/services/cards.service';
import { DemandeService } from 'app/services/demande.service';
import { Chart } from 'chart.js'
@Component({
  selector: 'stat',
  templateUrl: './stat.component.html',
  styleUrls: ['./stat.component.css']
})
export class StatComponent implements OnInit {

  constructor(private cs:CardsService,private ds:DemandeService) { }
  cards:cards[];
  demandes:demande[];
  nba:number;
  nbnt:number;
  nbr:number;
  nbmaster: number;
  nbvisa: number;
  ngOnInit(): void {
  
  this.nbmaster = 0;
  this.nbvisa = 0;
  this.cs.getAllCards().subscribe((data)=>{
    this.cards = data;
    console.log(this.cards);
    this.cards.forEach(card => {
      if (card.typeDeCarte === "CARTE VISA") {
        this.nbvisa++;
      } else{
        this.nbmaster++;
      }
    });
    this.RenderChart();
  });


  this.nba=0;
  this.nbr=0;
  this.nbnt=0;
  this.ds.getData().subscribe((data)=>{
    this.demandes=data;
    console.log(this.demandes)
    this.demandes.forEach(d =>{
      console.log(d.status);
      if(d.status === "non-traitee"){
        this.nbnt++;
      }else if(d.status === "accepted"){
        this.nba++;
      }else if(d.status === "refused"){
        this.nbr++;
      }
    });
    console.log( '%d , %d , %d',this.nba,this.nbnt, this.nbr)
    this.RenderDonut()
  });
  
  
  

  }
  RenderChart(){
    new Chart("piechart", {
      type: 'bar',
      data: {
        labels: ['Carte Visa', 'MasterCard'],
        datasets: [{
          label: 'Nombre Des differentes Categorie des cartes',
          data: [this.nbvisa,this.nbmaster],
          backgroundColor: [
            'rgba(255,99,132,0.2)',
            'rgba(54,162,235,0.2)',
            'rgba(255,206,86,0.2)',          ],
          borderWidth: 1
        }]
      },
      options: {
        scales: {
          y: {
            beginAtZero: true
          }
        }
      }
    });
    

  }
  RenderDonut(){
    new Chart("doughnut", {
      type: 'doughnut',
      data : {
        labels: [
          'Acceptee',
          'Refusee',
          'Non-Traiter'
        ],
        datasets: [{
          label: 'Status des Demandes du Credit',
          data: [this.nba,this.nbr,this.nbnt],
          backgroundColor: [
            'rgb(255, 99, 132)',
            'rgb(54, 162, 235)',
            'rgb(255, 205, 86)'
          ],
          hoverOffset: 4
        }]
      },
      options: {
        scales: {
          y: {
            beginAtZero: true
          }
        }
      }
    });
  }


}
