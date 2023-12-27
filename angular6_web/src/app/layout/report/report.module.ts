import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ReportRoutingModule } from './report-routing.module';
import { ReportComponent } from './report.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DialogModule } from '@progress/kendo-angular-dialog';
import { GridModule } from '@progress/kendo-angular-grid';
import { DropDownListModule } from '@progress/kendo-angular-dropdowns';
import { NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';

import { IntlModule } from '@progress/kendo-angular-intl';
import { DateInputsModule } from '@progress/kendo-angular-dateinputs';

@NgModule({
  imports: [
    GridModule,
    CommonModule,
    ReportRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    DialogModule,
    DropDownListModule,
    IntlModule,
    DateInputsModule,
    NgbAlertModule.forRoot(),
  ],
  declarations: [ReportComponent],

})
export class ReportModule { }
