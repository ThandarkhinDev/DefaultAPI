import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ExpenseRoutingModule } from './expense-routing.module';
import { ExpenseComponent } from './expense.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DialogModule } from '@progress/kendo-angular-dialog';
import { GridModule } from '@progress/kendo-angular-grid';
import { DropDownListModule } from '@progress/kendo-angular-dropdowns';
import { NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';
import { EditExpenseComponent } from './edit-expense/edit-expense.component';

@NgModule({
  imports: [
    GridModule,
    CommonModule,
    ExpenseRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    DialogModule,
    DropDownListModule,
    NgbAlertModule.forRoot(),
  ],
  declarations: [ExpenseComponent, EditExpenseComponent],

})
export class ExpenseModule { }
