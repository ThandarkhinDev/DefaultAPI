import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AdminComponent } from './admin.component';
import { EditAdminComponent } from './edit-admin/edit-admin.component';
import { PermissionGuardService } from '../../core/services/permission-guard.service';
import { NgxPermissionsModule } from 'ngx-permissions';

const routes: Routes = [
  {path: '', component: AdminComponent, canActivate: [PermissionGuardService] },
  {path: 'editAdmin', component: EditAdminComponent, canActivate: [PermissionGuardService] }
];

@NgModule({
  imports: [RouterModule.forChild(routes), NgxPermissionsModule.forRoot()],
  exports: [RouterModule, NgxPermissionsModule],
  providers: [ PermissionGuardService ]
})

export class AdminRoutingModule { }
