import { MoneyFlow } from "../money-flow/money_flow";
import { Wallet } from "../wallet/wallet";

export class BudgetAccount {
    id: String;
    createdDate: Date;
    userId: String;
    moneyFlows: MoneyFlow[];
    wallets: Wallet[];
}