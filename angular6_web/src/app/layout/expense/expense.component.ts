import { Component, OnInit } from '@angular/core';
import { ExpenseService } from '../../core/services/expense.service';
import { DialogRef, DialogService, DialogCloseResult } from '@progress/kendo-angular-dialog';
import { Observable } from 'rxjs';
import { GridDataResult, DataStateChangeEvent } from '@progress/kendo-angular-grid';
import { State } from '@progress/kendo-data-query';
import { Expense } from '../../core/models/expense.model';
import { Router } from '@angular/router';
import { DataTransferService } from '../../core/services/data-transfer.service';
import { Globalfunction } from '../../core/global/globalfunction';
import { UnlockService } from '../../core';


@Component({
  selector: 'app-expense',
  templateUrl: './expense.component.html',
  styleUrls: ['./expense.component.scss']
})

export class ExpenseComponent implements OnInit {
  public view: Observable<GridDataResult>;
  public gridState: State = {
      sort: [],
      skip: 0,
      take: 5,
      filter: { logic: 'and', filters: []}
  };
  public editDataItem: Expense;
  public isNew: boolean;
  public stateComboData;
  public expenseLevelComboData;
  public isSaved;
  public selectedItem: number;
  public globalfunction: Globalfunction;
 
  constructor(private expenseService: ExpenseService, 
              private unLockService: UnlockService,
              private dialogService: DialogService,
              private dataTransferService: DataTransferService,
              private router: Router) {
                this.globalfunction  = new Globalfunction(this.dialogService);
              }

  ngOnInit() {
    
    this.dataTransferService.currentValue.subscribe( x => {
      this.isSaved = x;
    });

      const currentState = localStorage.getItem('CurrentExpenseState');
    if (currentState != null) {
      this.gridState = JSON.parse(currentState);
      const stateIndex = this.gridState.filter.filters.findIndex( s => s['field'] == 'state');
      if (stateIndex >= 0) { // found
        const state = this.gridState.filter.filters[stateIndex];
        this.selectedItem = state['value'];
      }
    } else {
      localStorage.setItem('CurrentExpenseState', JSON.stringify(this.gridState));
    }

    this.view = this.expenseService;
    this.getAllExpense(this.gridState);
    // this.incomeService.subscribe(x => {
    //   // this.stateComboData = x.state;
    //   // this.incomeLevelComboData = x.adminLevel;
    //   // localStorage.setItem('stateComboData', JSON.stringify(this.stateComboData));
    //   // localStorage.setItem('incomeLevelComboData', JSON.stringify(this.incomeLevelComboData));
      
    // });
  }
  
  public onStateChange(state: DataStateChangeEvent): void {
    this.gridState = state;
    localStorage.setItem('CurrentExpenseState', JSON.stringify(this.gridState));
    this.getAllExpense(this.gridState);
  }

  getAllExpense(gridstate) {
    this.expenseService.getExpenseList(gridstate);
  }

  public addHandler() {
    this.editDataItem = new Expense();
    localStorage.setItem('isNewExpense', 'true');
    localStorage.setItem('expenseData', JSON.stringify(this.editDataItem));
    this.router.navigate(['apps.expense/editExpense']);
  }
  public editHandler(dataItem) {
    this.editDataItem = dataItem;
    localStorage.setItem('isNewExpense', 'false');
    localStorage.setItem('expenseData', JSON.stringify(this.editDataItem));
    this.router.navigate(['apps.expense/editExpense']);
  }

  public showConfirmation(dataItem) {
    const ParticularName = dataItem.ParticularName;
    const dialog: DialogRef = this.dialogService.open({
      title: 'Please confirm',
      content: ' Are you sure you want to delete Officer : ' + ParticularName ,
      actions: [
          { text: 'No' },
          { text: 'Yes', primary: true }
      ]      
    });
   
    dialog.result.subscribe((result) => {
      if (result instanceof DialogCloseResult) {
        console.log('close');
      } else {
        if (result.text == 'Yes') {
          this.deleteExpense(dataItem.ExpenseID);
        }
      }
    });
  } 
  deleteExpense(ExpenseID) {
    this.expenseService.deleteExpense(ExpenseID)
      .subscribe( x => {
        if (x) {
          this.globalfunction.messageDialogBox('Delete Successfully', 'Expense');
          this.getAllExpense(this.gridState);
        } else {
          this.globalfunction.messageDialogBox('Selected Admin can\'t delete because it is already used in Other', 'Expense');
        }
      });
  }

}
