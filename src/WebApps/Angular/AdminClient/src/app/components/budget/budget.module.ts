import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MoneyAccountComponent } from './money.account/money.account.component';
import { MoneyFlowComponent } from './money.flow/money.flow.component';
import { WalletComponent } from './wallet/wallet.component';
import { BudgetHomeComponent } from './budget.home/budget.home.component';



@NgModule({
  declarations: [
    MoneyAccountComponent,
    MoneyFlowComponent,
    WalletComponent,
    BudgetHomeComponent
  ],
  imports: [
    CommonModule
  ]
})
export class BudgetModule { }
