import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { AdminlevelService, TreeserviceService } from '../../core';
// tslint:disable-next-line:max-line-length
import { TreeviewItem, TreeviewConfig, TreeviewComponent, DownlineTreeviewItem, TreeviewHelper, TreeviewEventParser, OrderDownlineTreeviewEventParser } from 'ngx-treeview';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { DialogRef, DialogCloseResult, DialogService } from '@progress/kendo-angular-dialog';
import { isNil, remove, reverse } from 'lodash';

@Component({
  selector: 'app-adminlevel',
  templateUrl: './adminlevel.component.html',
  styleUrls: ['./adminlevel.component.scss'],
  providers: [
    { provide: TreeviewEventParser, useClass: OrderDownlineTreeviewEventParser },
]
})
export class AdminlevelComponent implements OnInit {
  @ViewChild(TreeviewComponent) treeviewComponent: TreeviewComponent; 
  idList: number[];
  adminLevelSets;
  adminLevelSet = {};
  items: TreeviewItem[];
  values: number;
  config = TreeviewConfig.create({
    hasAllCheckBox: false,
    hasFilter: false,
    hasCollapseExpand: false,
    decoupleChildFromParent: false,
    maxHeight: 190,
  });
  
  editForm: FormGroup = new FormGroup({
    'AdminLevelID' : new FormControl(0),
    'AdminLevel': new FormControl('', {validators: Validators.required, updateOn: 'blur'}),
    'restricted_iplist': new FormControl(),
    'Description': new FormControl(),
    'Remark': new FormControl(),
  });

  constructor(private adminLevelService: AdminlevelService, 
              private treeviewService: TreeserviceService, 
              private dialogService: DialogService) { }

  ngOnInit() {
    this.editForm.reset();
    this.adminLevelService.getAdminLevel(0)
    .subscribe(x => {
      this.adminLevelSets = x.data;
      this.adminLevelService.getAdminLevelMenu(0)
      .subscribe(x =>  {
        this.items =  [new TreeviewItem(this.treeviewService.createSingleDataTreeView(x.data, 0, []))]; 
      });
    });
  }

  selectedAdminLevel(ID) {
    this.adminLevelService.getAdminLevel(ID)
    .subscribe(x => {
              this.editForm.controls['AdminLevel'].enable();
              this.adminLevelSet = x.data[0];
              this.editForm.reset(x.data[0]);
              this.adminLevelService.getAdminLevelMenu(ID) 
                .subscribe( x => {
                  this.items =  [new TreeviewItem(this.treeviewService.createSingleDataTreeView(x.data, 0, []))];
                });
            });
  }

  // new admin level 
  newAdminLevel() {
    this.editForm.reset();
    this.adminLevelService.getAdminLevelMenu(0) 
    .subscribe( x => {
                this.items =  [new TreeviewItem(this.treeviewService.createSingleDataTreeView(x.data, 0, []))];
              });
  }

  saveAdminLevel() {
    this.adminLevelService.checkDuplicate(this.editForm.value)
    .subscribe(x => { 
      if (x[0].AdminLevel == '0') {
        this.adminLevelService.addAdminLevel(this.editForm.value)
          .subscribe(x => {
            if (x > 0) {
              this.adminLevelService.addAdminLevelMenu(x, this.idList.toString())
              .subscribe( x => {
                this.successMessagePopup('Save Successfully!');
                this.ngOnInit();
              });
            } else {
              this.successMessagePopup('Save Unsuccessfully!');
            }
          }); 
        } else {
          this.successMessagePopup('Duplicated!');
        }
    });

  }

  // select tree view items
  onSelectedChange(downlineItems: DownlineTreeviewItem[]) {
    this.idList = [0];
    downlineItems.forEach(downlineItem => {
        const item = downlineItem.item;
        const value = item.value;
        const texts = [item.text];
        let parent = downlineItem.parent;
        while (!isNil(parent)) {
            const id = parent.item.value;
            if (this.idList.findIndex(x => x == id) === -1) {
              this.idList.push(id);
            }
            parent = parent.parent;
        }
        if (this.idList.findIndex(x => x == value) === -1) {
          this.idList.push(value);
        }
    });
  }

  public deleteConfirm() {
    const adminLevelName = this.editForm.value.AdminLevel;
    const dialog: DialogRef = this.dialogService.open({
      title: 'Please confirm',
      content: ' Are you sure you want to delete admin level Name : ' + adminLevelName ,
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
              this.deleteAdminLevel();
            }
        }
    });
  }

  public successMessagePopup(message: string) {
    const dialog: DialogRef = this.dialogService.open({
      title: 'Admin level',
      content: message,
      actions: [
          { text: 'Ok', primary: true }
      ]  ,
      width: 300    
    });
 
    dialog.result.subscribe((result) => {
        if (result instanceof DialogCloseResult) {
            console.log('close');
        } else {
        }
    });
  }

  deleteAdminLevel() {
    this.adminLevelService.deleteAdminLevel(this.editForm.value.AdminLevelID)
     .subscribe( x => this.ngOnInit());
  }

}
