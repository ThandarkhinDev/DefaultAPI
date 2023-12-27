import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { IncomeService } from '../../../core/services/income.service';
import { Router } from '@angular/router';
import { DataTransferService } from '../../../core/services/data-transfer.service';
import { CustomValidator } from '../../../core/validators/custom.validator';
import { Globalfunction } from '../../../core/global/globalfunction';
import { DialogService } from '@progress/kendo-angular-dialog';

@Component({
  selector: 'app-edit-income',
  templateUrl: './edit-income.component.html',
  styleUrls: ['./edit-income.component.scss']
})
export class EditIncomeComponent implements OnInit {
  public globalfunction: Globalfunction;
  // public minPasswordLength = localStorage.getItem('MinPasswordLength');
  editForm: FormGroup = new FormGroup({
    'IncomeID': new FormControl(),
    'DonarName': new FormControl('', { validators: Validators.required, updateOn: 'blur' }),
    'Amount': new FormControl('', { validators: Validators.required}),
    // 'Email': new FormControl('', {validators: Validators.compose([
    //                           Validators.required, 
    //                           // tslint:disable-next-line:max-line-length
    //                           Validators.pattern(/^(([^<>()\[\]\.,;:\s@\"]+(\.[^<>()\[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/)
    //                         ]), updateOn: 'blur'}),
    // 'Password': new FormControl('', { validators: Validators.compose([
    //                                 Validators.required,
    //                                 CustomValidator.validatePassword
    //                               ]), updateOn: 'blur' }),
    // 'ConfirmPassword': new FormControl('', { validators: Validators.compose([
    //                                 Validators.required,
    //                                 ]), updateOn: 'blur' }),
    // 'AdminLevelID': new FormControl('', { validators: Validators.required, updateOn: 'blur' }),
    // 'address': new FormControl('', { validators: Validators.required, updateOn: 'blur' }),
    // 'phone': new FormControl('', { validators: Validators.compose([Validators.required]), updateOn: 'blur' }),
    // 'nrc': new FormControl('', { validators: Validators.required, updateOn: 'blur' }),
    // 'stateid': new FormControl('', { validators: Validators.required}),
  }
  , (formGroup: FormGroup) => {
    return CustomValidator.matchingConfirmPasswords(formGroup);
 }
);

  isNewIncome: boolean;
  incomelvls;
  states;
  isFailed = false;

  constructor(private incomeService: IncomeService,
              private dataTransferService: DataTransferService,
              private dialogService: DialogService,
              private router: Router) {
                this.globalfunction = new Globalfunction(dialogService);
               }

  ngOnInit() {
    this.isNewIncome = localStorage.getItem('isNewIncome') == 'true' ? true : false;
    let incomeData = localStorage.getItem('incomeData');
    // const adminlvls_string = localStorage.getItem('adminLevelComboData');
    // const states_string = localStorage.getItem('stateComboData');
    // this.incomelvls = JSON.parse(adminlvls_string);
    // this.states = JSON.parse(states_string);
    incomeData = JSON.parse(incomeData);
    this.editForm.reset(incomeData);
    // if (this.isNewIncome) {
    //   this.editForm.get('Password').setValidators([Validators.required, CustomValidator.validatePassword]);
    //   this.editForm.get('ConfirmPassword').setValidators([Validators.required]);
    // } else {
    //   this.editForm.get('Password').clearValidators();
    //   this.editForm.get('ConfirmPassword').clearValidators();
    // }
    // this.editForm.get('Password').updateValueAndValidity();
    // this.editForm.get('ConfirmPassword').updateValueAndValidity();
  }

  //#region originalSaveAdmin
  // saveAdmin() {
  //   this.incomeService.checkDuplicate(this.editForm.value)
  //   .subscribe( x => {
  //     if (x[0].name === 0 && x[0].loginName === 0 && x[0].nrc === 0) {

  //       this.incomeService.saveIncome(this.editForm.value)
  //       .subscribe(x => {
  //         if (x > 0) {
  //           this.dataTransferService.isSavedAdmin(true);
  //           this.router.navigate(['apps.income']);
  //         } else {
  //           this.globalfunction.messageDialogBox('Save Unsuccessfully!', 'Income');
  //         }
  //       });
  //     } else {
  //       const message = x[0].name !== 0 ? 'Name ' : x[0].loginName !== 0 ? 'Login Name ' : x[0].nrc !== 0 ? 'NRC ' : '';
  //       this.globalfunction.messageDialogBox(`Duplicated : ${message}`, 'Income');
  //     }
  //   });
  // }
  //#endregion originalSaveAdmin
 
    saveIncome() {
    
        this.incomeService.saveIncome(this.editForm.value)
        .subscribe(x => {
          if (x > 0) {
            this.dataTransferService.isSavedAdmin(true);
            this.router.navigate(['apps.income']);
          } else {
            this.globalfunction.messageDialogBox('Save Unsuccessfully!', 'Income');
          }
        });
  }

}
