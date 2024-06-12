import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BudgetAccountComponent } from './budget.account/budget.account.component';
import { MoneyFlowComponent } from './money.flow/money.flow.component';
import { WalletComponent } from './wallet/wallet.component';
import { BudgetHomeComponent } from './budget.home/budget.home.component';
import { RouterModule } from '@angular/router';
import { AuthGuard } from 'src/app/guards/auth.guard';



@NgModule({
  declarations: [
    BudgetAccountComponent,
    MoneyFlowComponent,
    WalletComponent,
    BudgetHomeComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([
      { path: "", component: BudgetHomeComponent, canActivate: [AuthGuard] },
      { path: "budget-account", component: BudgetAccountComponent, canActivate: [AuthGuard] },
      { path: "money-flow", component: MoneyFlowComponent, canActivate: [AuthGuard] },
      { path: "wallet", component: WalletComponent, canActivate: [AuthGuard] },
    ]),
  ]
})
export class BudgetModule { }
