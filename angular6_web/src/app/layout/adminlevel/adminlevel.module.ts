import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminlevelRoutingModule } from './adminlevel-routing.module';
import { AdminlevelComponent } from './adminlevel.component';
import { TreeviewModule } from 'ngx-treeview';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DialogModule } from '@progress/kendo-angular-dialog';
@NgModule({
  imports: [
    CommonModule,
    AdminlevelRoutingModule,
    TreeviewModule.forRoot(),
    FormsModule,
    ReactiveFormsModule,
    DialogModule
  ],
  declarations: [AdminlevelComponent]
})
export class AdminlevelModule { }
