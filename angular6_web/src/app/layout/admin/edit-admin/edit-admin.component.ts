import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AdminService } from '../../../core/services/admin.service';
import { Router } from '@angular/router';
import { DataTransferService } from '../../../core/services/data-transfer.service';
import { CustomValidator } from '../../../core/validators/custom.validator';
import { Globalfunction } from '../../../core/global/globalfunction';
import { DialogService } from '@progress/kendo-angular-dialog';

@Component({
  selector: 'app-edit-admin',
  templateUrl: './edit-admin.component.html',
  styleUrls: ['./edit-admin.component.scss']
})
export class EditAdminComponent implements OnInit {
  public globalfunction: Globalfunction;
  public minPasswordLength = localStorage.getItem('MinPasswordLength');
  editForm: FormGroup = new FormGroup({
    'AdminID': new FormControl(),
    'AdminName': new FormControl('', { validators: Validators.required, updateOn: 'blur' }),
    'LoginName': new FormControl('', { validators: Validators.required, updateOn: 'blur' }),
    'Email': new FormControl('', {validators: Validators.compose([
                              Validators.required, 
                              // tslint:disable-next-line:max-line-length
                              Validators.pattern(/^(([^<>()\[\]\.,;:\s@\"]+(\.[^<>()\[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/)
                            ]), updateOn: 'blur'}),
    'Password': new FormControl('', { validators: Validators.compose([
                                    Validators.required,
                                    CustomValidator.validatePassword
                                  ]), updateOn: 'blur' }),
    'ConfirmPassword': new FormControl('', { validators: Validators.compose([
                                    Validators.required,
                                    ]), updateOn: 'blur' }),
    'AdminLevelID': new FormControl('', { validators: Validators.required, updateOn: 'blur' }),
    'address': new FormControl('', { validators: Validators.required, updateOn: 'blur' }),
    'phone': new FormControl('', { validators: Validators.compose([Validators.required]), updateOn: 'blur' }),
    'nrc': new FormControl('', { validators: Validators.required, updateOn: 'blur' }),
    'stateid': new FormControl('', { validators: Validators.required}),
  }, (formGroup: FormGroup) => {
    return CustomValidator.matchingConfirmPasswords(formGroup);
 });

  isNewAdmin: boolean;
  adminlvls;
  states;
  isFailed = false;

  constructor(private adminService: AdminService,
              private dataTransferService: DataTransferService,
              private dialogService: DialogService,
              private router: Router) {
                this.globalfunction = new Globalfunction(dialogService);
               }

  ngOnInit() {
    this.isNewAdmin = localStorage.getItem('isNewAdmin') == 'true' ? true : false;
    let adminData = localStorage.getItem('adminData');
    const adminlvls_string = localStorage.getItem('adminLevelComboData');
    const states_string = localStorage.getItem('stateComboData');
    this.adminlvls = JSON.parse(adminlvls_string);
    this.states = JSON.parse(states_string);
    adminData = JSON.parse(adminData);
    this.editForm.reset(adminData);
    if (this.isNewAdmin) {
      this.editForm.get('Password').setValidators([Validators.required, CustomValidator.validatePassword]);
      this.editForm.get('ConfirmPassword').setValidators([Validators.required]);
    } else {
      this.editForm.get('Password').clearValidators();
      this.editForm.get('ConfirmPassword').clearValidators();
    }
    this.editForm.get('Password').updateValueAndValidity();
    this.editForm.get('ConfirmPassword').updateValueAndValidity();
  }

  saveAdmin() {
    this.adminService.checkDuplicate(this.editForm.value)
    .subscribe( x => {
      if (x[0].name === 0 && x[0].loginName === 0 && x[0].nrc === 0) {
        this.adminService.saveAdmin(this.editForm.value)
        .subscribe(x => {
          if (x > 0) {
            this.dataTransferService.isSavedAdmin(true);
            this.router.navigate(['apps.admin']);
          } else {
            this.globalfunction.messageDialogBox('Save Unsuccessfully!', 'Admin');
          }
        });
      } else {
        const message = x[0].name !== 0 ? 'Name ' : x[0].loginName !== 0 ? 'Login Name ' : x[0].nrc !== 0 ? 'NRC ' : '';
        this.globalfunction.messageDialogBox(`Duplicated : ${message}`, 'Admin');
      }
    });
  }

}
