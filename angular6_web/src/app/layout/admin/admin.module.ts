import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminRoutingModule } from './admin-routing.module';
import { AdminComponent } from './admin.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DialogModule } from '@progress/kendo-angular-dialog';
import { GridModule } from '@progress/kendo-angular-grid';
import { EditAdminComponent } from './edit-admin/edit-admin.component';
import { DropDownListModule } from '@progress/kendo-angular-dropdowns';
import { NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  imports: [
    GridModule,
    CommonModule,
    AdminRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    DialogModule,
    DropDownListModule,
    NgbAlertModule.forRoot(),
  ],
  declarations: [AdminComponent, EditAdminComponent],

})
export class AdminModule { }
