import { Component, OnInit } from '@angular/core';
import { IncomeService } from '../../core/services/income.service';
import { ExpenseService } from '../../core/services/expense.service';
import { DialogRef, DialogService, DialogCloseResult } from '@progress/kendo-angular-dialog';
import { Observable } from 'rxjs';
import { GridDataResult, DataStateChangeEvent } from '@progress/kendo-angular-grid';
import { State } from '@progress/kendo-data-query';
import { Income } from '../../core/models/income.model';
import { Expense } from '../../core/models/expense.model';
import { Router } from '@angular/router';
import { DataTransferService } from '../../core/services/data-transfer.service';
import { Globalfunction } from '../../core/global/globalfunction';
import { UnlockService } from '../../core';


@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.scss']
})

export class ReportComponent implements OnInit {
  public value: Date;

  //#region Income
  public view: Observable<GridDataResult>;
  public gridState: State = {
      sort: [],
      skip: 0,
      take: 5,
      filter: { logic: 'and', filters: []}
  };
  public editDataItem: Income;
  public isNew: boolean;
  public stateComboData;
  public reportLevelComboData;
  public isSaved;
  public selectedItem: number;
  public globalfunction: Globalfunction;
 
  constructor(private incomeService: IncomeService, 
              private expenseService: ExpenseService, 
              private unLockService: UnlockService,
              private dialogService: DialogService,
              private dataTransferService: DataTransferService,
              private router: Router) {
                this.globalfunction  = new Globalfunction(this.dialogService);
              }
  //#endregion Income


  //#region Expense
  public view1: Observable<GridDataResult>;
  public gridState1: State = {
      sort: [],
      skip: 0,
      take: 5,
      filter: { logic: 'and', filters: []}
  };
  public editDataItem1: Expense;
  public isNew1: boolean;
  public stateComboData1;
  public expenseLevelComboData;
  public isSaved1;
  public selectedItem1: number;
  public globalfunction1: Globalfunction;
 


  //#endregion Expense
  ngOnInit() {
    //#region Income
    this.dataTransferService.currentValue.subscribe( x => {
      this.isSaved = x;
    });

      const currentState = localStorage.getItem('CurrentIncomeState');
    if (currentState != null) {
      this.gridState = JSON.parse(currentState);
      const stateIndex = this.gridState.filter.filters.findIndex( s => s['field'] == 'state');
      if (stateIndex >= 0) { // found
        const state = this.gridState.filter.filters[stateIndex];
        this.selectedItem = state['value'];
      }
    } else {
      localStorage.setItem('CurrentIncomeState', JSON.stringify(this.gridState));
    }

    this.view = this.incomeService;
    this.getAllIncome(this.gridState);
    // this.incomeService.subscribe(x => {
    //   // this.stateComboData = x.state;
    //   // this.incomeLevelComboData = x.adminLevel;
    //   // localStorage.setItem('stateComboData', JSON.stringify(this.stateComboData));
    //   // localStorage.setItem('incomeLevelComboData', JSON.stringify(this.incomeLevelComboData));
      
    // });

    //#endregion Income
  
    //#region Expense
    this.dataTransferService.currentValue.subscribe( x => {
      this.isSaved1 = x;
    });

      const currentState1 = localStorage.getItem('CurrentExpenseState');
    if (currentState1 != null) {
      this.gridState1 = JSON.parse(currentState1);
      const stateIndex1 = this.gridState1.filter.filters.findIndex( s => s['field'] == 'state');
      if (stateIndex1 >= 0) { // found
        const state1 = this.gridState1.filter.filters[stateIndex1];
        this.selectedItem1 = state1['value'];
      }
    } else {
      localStorage.setItem('CurrentExpenseState', JSON.stringify(this.gridState1));
    }

    this.view1 = this.expenseService;
    this.getAllExpense(this.gridState1);
    //#endregion Expense
  
  }


//#region Income
  public onStateChange(state: DataStateChangeEvent): void {
    this.gridState = state;
    localStorage.setItem('CurrentIncomeState', JSON.stringify(this.gridState));
    this.getAllIncome(this.gridState);
  }


  getAllIncome(gridstate) {
    this.incomeService.getIncomeList(gridstate);
  }

  // public addHandler() {
  //   this.editDataItem = new Income();
  //   localStorage.setItem('isNewIncome', 'true');
  //   localStorage.setItem('incomeData', JSON.stringify(this.editDataItem));
  //   this.router.navigate(['apps.income/editIncome']);
  // }
  public editHandler(dataItem) {
    this.editDataItem = dataItem;
    localStorage.setItem('isNewIncome', 'false');
    localStorage.setItem('incomeData', JSON.stringify(this.editDataItem));
    this.router.navigate(['apps.income/editIncome']);
  }

  public showConfirmation(dataItem) {
    const DonarName = dataItem.DonarName;
    const dialog: DialogRef = this.dialogService.open({
      title: 'Please confirm',
      content: ' Are you sure you want to delete Officer : ' + DonarName ,
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
          this.deleteIncome(dataItem.IncomeID);
        }
      }
    });
  } 
  deleteIncome(IncomeID) {
    this.incomeService.deleteIncome(IncomeID)
      .subscribe( x => {
        if (x) {
          this.globalfunction.messageDialogBox('Delete Successfully', 'Income');
          this.getAllIncome(this.gridState);
        } else {
          this.globalfunction.messageDialogBox('Selected Admin can\'t delete because it is already used in Other', 'Income');
        }
      });
  }
//#endregion Income

//#region Expense
  getAllExpense(gridstate1) {
    this.expenseService.getExpenseList(gridstate1);
  }
  
//#endregion Expense




}
