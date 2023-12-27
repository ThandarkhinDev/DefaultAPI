import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ExpenseComponent } from './expense.component';
import { EditExpenseComponent } from './edit-expense/edit-expense.component';
import { PermissionGuardService } from '../../core/services/permission-guard.service';
import { NgxPermissionsModule } from 'ngx-permissions';

const routes: Routes = [
  {path: '', component: ExpenseComponent, canActivate: [PermissionGuardService] },
  {path: 'editExpense', component: EditExpenseComponent, canActivate: [PermissionGuardService] }
];

@NgModule({
  imports: [RouterModule.forChild(routes), NgxPermissionsModule.forRoot()],
  exports: [RouterModule, NgxPermissionsModule],
  providers: [ PermissionGuardService ]
})

export class ExpenseRoutingModule { }
