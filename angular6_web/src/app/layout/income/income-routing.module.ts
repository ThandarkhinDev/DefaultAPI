import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { IncomeComponent } from './income.component';
import { EditIncomeComponent } from './edit-income/edit-income.component';
import { PermissionGuardService } from '../../core/services/permission-guard.service';
import { NgxPermissionsModule } from 'ngx-permissions';

const routes: Routes = [
  {path: '', component: IncomeComponent, canActivate: [PermissionGuardService] },
  {path: 'editIncome', component: EditIncomeComponent, canActivate: [PermissionGuardService] }
];

@NgModule({
  imports: [RouterModule.forChild(routes), NgxPermissionsModule.forRoot()],
  exports: [RouterModule, NgxPermissionsModule],
  providers: [ PermissionGuardService ]
})

export class IncomeRoutingModule { }
