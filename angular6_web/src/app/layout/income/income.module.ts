import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { IncomeRoutingModule } from './income-routing.module';
import { IncomeComponent } from './income.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DialogModule } from '@progress/kendo-angular-dialog';
import { GridModule } from '@progress/kendo-angular-grid';
import { DropDownListModule } from '@progress/kendo-angular-dropdowns';
import { NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';
import { EditIncomeComponent } from './edit-income/edit-income.component';

@NgModule({
  imports: [
    GridModule,
    CommonModule,
    IncomeRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    DialogModule,
    DropDownListModule,
    NgbAlertModule.forRoot(),
  ],
  declarations: [IncomeComponent, EditIncomeComponent],

})
export class IncomeModule { }
