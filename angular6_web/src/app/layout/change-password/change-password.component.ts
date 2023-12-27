import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { CustomValidator } from '../../core';
import { Router } from '@angular/router';
import { UnlockService } from '../../core/services/unlock.service';
import { Globalfunction } from '../../core/global/globalfunction';
import { DialogService } from '@progress/kendo-angular-dialog';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.scss']
})
export class ChangePasswordComponent implements OnInit {
 
  editForm: FormGroup = new FormGroup({
    'oldPassword': new FormControl('', {validators: Validators.required, updateOn: 'blur'}),
    'Password': new FormControl('', { validators: Validators.compose([
      Validators.required,
      CustomValidator.validatePassword
    ]), updateOn: 'blur' }),
    'ConfirmPassword': new FormControl('', {validators: Validators.required, updateOn: 'blur'})
  }, (formGroup: FormGroup) => {
    return CustomValidator.matchingConfirmPasswords(formGroup);
  });

  public minPasswordLength = localStorage.getItem('MinPasswordLength');
  public globalFunction: Globalfunction;
  constructor(private router: Router,
              private dialogService: DialogService,
              private unlockService: UnlockService) { 
                this.globalFunction = new Globalfunction(dialogService);
              }

  ngOnInit() {
  }

  
   cancel() {
    this.router.navigate(['/dashboard']);
   }
}
