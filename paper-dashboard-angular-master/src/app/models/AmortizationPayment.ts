export class AmortizationPayment{
    id!:number
    paymentNumber!:number
    date!:any
    principalPaid!:number
    interestPaid!:number
    remainingBalance!:number

}
export class  AmortizationResult{
    id!:number
    payments!:Array<AmortizationPayment>
}