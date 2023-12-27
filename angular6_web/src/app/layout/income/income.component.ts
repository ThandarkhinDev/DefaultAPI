import { Component, OnInit } from '@angular/core';
import { IncomeService } from '../../core/services/income.service';
import { DialogRef, DialogService, DialogCloseResult } from '@progress/kendo-angular-dialog';
import { Observable } from 'rxjs';
import { GridDataResult, DataStateChangeEvent } from '@progress/kendo-angular-grid';
import { State } from '@progress/kendo-data-query';
import { Income } from '../../core/models/income.model';
import { Router } from '@angular/router';
import { DataTransferService } from '../../core/services/data-transfer.service';
import { Globalfunction } from '../../core/global/globalfunction';
import { UnlockService } from '../../core';


@Component({
  selector: 'app-income',
  templateUrl: './income.component.html',
  styleUrls: ['./income.component.scss']
})

export class IncomeComponent implements OnInit {
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
  public incomeLevelComboData;
  public isSaved;
  public selectedItem: number;
  public globalfunction: Globalfunction;
 
  constructor(private incomeService: IncomeService, 
              private unLockService: UnlockService,
              private dialogService: DialogService,
              private dataTransferService: DataTransferService,
              private router: Router) {
                this.globalfunction  = new Globalfunction(this.dialogService);
              }

  ngOnInit() {
    // show message dialog box  # ntra
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

    // this.incomeService.getIncomeComboData().subscribe(x => {
    //   this.stateComboData = x.state;
    //   this.incomeLevelComboData = x.incomeLevel;
    //   localStorage.setItem('stateComboData', JSON.stringify(this.stateComboData));
    //   localStorage.setItem('incomeLevelComboData', JSON.stringify(this.incomeLevelComboData));
      
    // });

  }

  public onStateChange(state: DataStateChangeEvent): void {
    this.gridState = state;
    localStorage.setItem('CurrentIncomeState', JSON.stringify(this.gridState));
    this.getAllIncome(this.gridState);
  }

  public onChange(stateID: any): void {
    const stateIndex = this.gridState.filter.filters.findIndex( s => s['field'] == 'state');
    if (stateID != null) {
      const searchState = {field: 'state', operator: 'contains', value: stateID};
      if (stateIndex < 0) { // not found 
        this.gridState.filter.filters.push(searchState);
      } else {
        const state = this.gridState.filter.filters[stateIndex];
        state['value'] = stateID;
      }
    } else {
      if (stateIndex >= 0) {
        this.gridState.filter.filters.splice(stateIndex, 1);
      }
    }
    localStorage.setItem('CurrentIncomeState', JSON.stringify(this.gridState));
    this.getAllIncome(this.gridState);
  }

  getAllIncome(gridstate) {
    this.incomeService.getIncomeList(gridstate);
  }

  public addHandler() {
    this.editDataItem = new Income();
    localStorage.setItem('isNewIncome', 'true');
    localStorage.setItem('incomeData', JSON.stringify(this.editDataItem));
    this.router.navigate(['apps.income/editIncome']);
  }

  public editHandler(dataItem) {
    this.editDataItem = dataItem;
    localStorage.setItem('isNewIncome', 'false');
    localStorage.setItem('incomeData', JSON.stringify(this.editDataItem));
    this.router.navigate(['apps.income/editIncome']);
  }

  public showConfirmation(dataItem) {
    const IncomeName = dataItem.IncomeName;
    const dialog: DialogRef = this.dialogService.open({
      title: 'Please confirm',
      content: ' Are you sure you want to delete Officer : ' + IncomeName ,
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

  deleteIncome(incomeID) {
    this.incomeService.deleteIncome(incomeID)
      .subscribe( x => {
        if (x) {
          this.globalfunction.messageDialogBox('Delete Successfully', 'Income');
          this.getAllIncome(this.gridState);
        } else {
          this.globalfunction.messageDialogBox('Selected Income can\'t delete because it is already used in Other', 'Income');
        }
      });
  }

  // unBlock(dataItem) {
  //   const incomeID = dataItem.IncomeID;
  //   this.unLockService.unBlock(incomeID)
  //   .subscribe(x => {
  //     if (x.data == true) {
  //       this.globalfunction.messageDialogBox('Unblock Successfully', 'Income');
  //     } else {
  //       this.globalfunction.messageDialogBox('Unblock Unsuccessfully', 'Income');
  //     }
  //   });
  // }

  // resetPassword(dataItem) {
  //   const incomeID = dataItem.IncomeID;
  //   this.unLockService.resetPassword(incomeID)
  //   .subscribe(x => {
  //     if (x.data) {
  //       this.globalfunction.messageDialogBox(`New Password is ${x.data}`, 'Income');
  //     } else {
  //       this.globalfunction.messageDialogBox('Reset Password Unsuccessfully', 'Income');
  //     }
  //   });
  // }

}
