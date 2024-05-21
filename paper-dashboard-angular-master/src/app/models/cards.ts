
export class cards {
    id!:number
    statut!:string
    typeDeCarte!:string
    libelle!:string
    solde!:number
    accountNumber!:number
    plafond!: number
    usedPercentage:number = 0;

    get GetusedPercentage(): number {
        this.usedPercentage = (this.solde / this.plafond) * 100;
        return this.usedPercentage;
    }


}