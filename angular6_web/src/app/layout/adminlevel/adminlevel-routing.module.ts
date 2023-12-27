import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AdminlevelComponent } from './adminlevel.component';
import { PermissionGuardService } from '../../core/services/permission-guard.service';
import { NgxPermissionsModule } from 'ngx-permissions';

const routes: Routes = [
  {path: '', component: AdminlevelComponent , canActivate: [PermissionGuardService]}
];

@NgModule({
  imports: [RouterModule.forChild(routes), NgxPermissionsModule.forRoot()],
  exports: [RouterModule, NgxPermissionsModule],
  providers: [ PermissionGuardService ]
})
export class AdminlevelRoutingModule { }
