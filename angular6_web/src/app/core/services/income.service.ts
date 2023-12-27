import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { BehaviorSubject } from 'rxjs';
import { GridDataResult } from '@progress/kendo-angular-grid';

@Injectable({
  providedIn: 'root'
})
export class IncomeService extends BehaviorSubject<GridDataResult> {

  public loading: boolean;
  constructor(private apiservice: ApiService) {
    super(null);
  }

  getIncomeList(girdState: any) {
    this.loading = true;
    this.apiservice.fetchgrid_postJson('/income/GetAllIncomes/', girdState)
    .subscribe(x => {
      super.next(x);
      this.loading = false;
    });
  }
  
  getIncomeComboData() {
    return this.apiservice.get('/income/GetIncomeComboData');
  }

  checkDuplicate(incomeSet) {
    return this.apiservice.postJson('/income/checkDuplicate/', incomeSet);
  }

  saveIncome(incomeSet) {
    return this.apiservice.postJson('/income/AddIncomeSetup/', incomeSet);
  }

  deleteIncome(incomeID) {
    return this.apiservice.delete('/income/DeleteIncome1/' + incomeID) ;
  }
}
