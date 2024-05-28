import { Component, OnInit } from '@angular/core';
import { cards } from 'app/models/cards';
import { CardsService } from 'app/services/cards.service';

@Component({
  selector: 'cards',
  templateUrl: './cards.component.html',
  styleUrls: ['./cards.component.css']
})
export class CardsComponent implements OnInit {

  constructor(private cs : CardsService) { }
  cards:cards[]
  
  ngOnInit(): void {
    this.cs.getAllCards().subscribe((data)=>{
      this.cards=data;
      console.log(this.cards)
    })
  }
  BlockCard(card: cards){
    console.log(card.statut)
    card.statut = "Carte Non Active";
    this.cs.update(card).subscribe(()=>{

    })
  }
  ActiverCard(card: cards){
    console.log(card.statut)
    card.statut = "Carte Active";
    this.cs.update(card).subscribe(()=>{
      
    })
  }

}
