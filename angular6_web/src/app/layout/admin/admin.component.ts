import { Component, OnInit } from '@angular/core';
import { AdminService } from '../../core/services/admin.service';
import { DialogRef, DialogService, DialogCloseResult } from '@progress/kendo-angular-dialog';
import { Observable } from 'rxjs';
import { GridDataResult, DataStateChangeEvent } from '@progress/kendo-angular-grid';
import { State } from '@progress/kendo-data-query';
import { Admin } from '../../core/models/admin.model';
import { Router } from '@angular/router';
import { DataTransferService } from '../../core/services/data-transfer.service';
import { Globalfunction } from '../../core/global/globalfunction';
import { UnlockService } from '../../core';


@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})

export class AdminComponent implements OnInit {
  public view: Observable<GridDataResult>;
  public gridState: State = {
      sort: [],
      skip: 0,
      take: 5,
      filter: { logic: 'and', filters: []}
  };
  public editDataItem: Admin;
  public isNew: boolean;
  public stateComboData;
  public adminLevelComboData;
  public isSaved;
  public selectedItem: number;
  public globalfunction: Globalfunction;
 
  constructor(private adminService: AdminService, 
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


    const currentState = localStorage.getItem('CurrentAdminState');
    if (currentState != null) {
      this.gridState = JSON.parse(currentState);
      const stateIndex = this.gridState.filter.filters.findIndex( s => s['field'] == 'state');
      if (stateIndex >= 0) { // found
        const state = this.gridState.filter.filters[stateIndex];
        this.selectedItem = state['value'];
      }
    } else {
      localStorage.setItem('CurrentAdminState', JSON.stringify(this.gridState));
    }

    this.view = this.adminService;
    this.adminService.getAdminComboData().subscribe(x => {
      this.stateComboData = x.state;
      this.adminLevelComboData = x.adminLevel;
      localStorage.setItem('stateComboData', JSON.stringify(this.stateComboData));
      localStorage.setItem('adminLevelComboData', JSON.stringify(this.adminLevelComboData));
      this.getAllAdmin(this.gridState);
    });

  }

  public onStateChange(state: DataStateChangeEvent): void {
    this.gridState = state;
    localStorage.setItem('CurrentAdminState', JSON.stringify(this.gridState));
    this.getAllAdmin(this.gridState);
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
    localStorage.setItem('CurrentAdminState', JSON.stringify(this.gridState));
    this.getAllAdmin(this.gridState);
  }

  getAllAdmin(gridstate) {
    this.adminService.getAdminList(gridstate);
  }

  public addHandler() {
    this.editDataItem = new Admin();
    localStorage.setItem('isNewAdmin', 'true');
    localStorage.setItem('adminData', JSON.stringify(this.editDataItem));
    this.router.navigate(['apps.admin/editAdmin']);
  }

  public editHandler(dataItem) {
    this.editDataItem = dataItem;
    localStorage.setItem('isNewAdmin', 'false');
    localStorage.setItem('adminData', JSON.stringify(this.editDataItem));
    this.router.navigate(['apps.admin/editAdmin']);
  }

  public showConfirmation(dataItem) {
    const AdminName = dataItem.AdminName;
    const dialog: DialogRef = this.dialogService.open({
      title: 'Please confirm',
      content: ' Are you sure you want to delete Officer : ' + AdminName ,
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
          this.deleteAdmin(dataItem.AdminID);
        }
      }
    });
  } 

  deleteAdmin(adminID) {
    this.adminService.deleteAdmin(adminID)
      .subscribe( x => {
        if (x) {
          this.globalfunction.messageDialogBox('Delete Successfully', 'Admin');
          this.getAllAdmin(this.gridState);
        } else {
          this.globalfunction.messageDialogBox('Selected Admin can\'t delete because it is already used in Other', 'Admin');
        }
      });
  }

  unBlock(dataItem) {
    const adminID = dataItem.AdminID;
    this.unLockService.unBlock(adminID)
    .subscribe(x => {
      if (x.data == true) {
        this.globalfunction.messageDialogBox('Unblock Successfully', 'Admin');
      } else {
        this.globalfunction.messageDialogBox('Unblock Unsuccessfully', 'Admin');
      }
    });
  }

  resetPassword(dataItem) {
    const adminID = dataItem.AdminID;
    this.unLockService.resetPassword(adminID)
    .subscribe(x => {
      if (x.data) {
        this.globalfunction.messageDialogBox(`New Password is ${x.data}`, 'Admin');
      } else {
        this.globalfunction.messageDialogBox('Reset Password Unsuccessfully', 'Admin');
      }
    });
  }

}
