import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { BehaviorSubject } from 'rxjs';
import { GridDataResult } from '@progress/kendo-angular-grid';

@Injectable({
  providedIn: 'root'
})
export class ExpenseService extends BehaviorSubject<GridDataResult> {

  public loading: boolean;
  constructor(private apiservice: ApiService) {
    super(null);
  }

  getExpenseList(girdState: any) {
    this.loading = true;
    this.apiservice.fetchgrid_postJson('/expense/GetAllExpenses/', girdState)
    .subscribe(x => {
      super.next(x);
      this.loading = false;
    });
  }
  
  getExpenseComboData() {
    return this.apiservice.get('/expense/GetExpenseComboData');
  }

  checkDuplicate(expenseSet) {
    return this.apiservice.postJson('/expense/checkDuplicateExpense/', expenseSet);
  }

  saveExpense(expenseSet) {
    return this.apiservice.postJson('/expense/AddExpenseSetup/', expenseSet);
  }

  deleteExpense(expenseID) {
    return this.apiservice.delete('/expense/DeleteExpense1/' + expenseID);
  }
}
