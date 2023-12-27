import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { routerTransition } from '../router.animations';
import { Errors, UserService, UnlockService } from '../core';
import { FormGroup, FormControl } from '@angular/forms';
import { Globalfunction } from '../core/global/globalfunction';
import { DialogService } from '@progress/kendo-angular-dialog';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss'],
    animations: [routerTransition()]
})
export class LoginComponent implements OnInit {
    public globalFunction: Globalfunction;
    constructor(private route: ActivatedRoute,
                private router: Router,
                private userService: UserService,
                private unlockService: UnlockService,
                private dialogService: DialogService
            ) {
                this.globalFunction = new Globalfunction(dialogService);
            }
    requireName = false;
    authType: String = '';
    title: String = '';
    errors: Errors = {errors: {}};
    isSubmitting = false;
    authForm: FormGroup;
    loginData: any = {};
    ngOnInit() {
        this.route.url.subscribe(data => {
            // Get the last piece of the URL (it's either 'login' or 'register')
            // this.authType = data[data.length - 1].path;
            // Set a title for the page accordingly
            this.title = (this.authType === 'login') ? 'Sign in' : 'Sign up';
            // add form control for username if this is the register page
            if (this.authType === 'register') {
              this.authForm.addControl('username', new FormControl());
            }
          });
    }

    onLoggedin() {
        this.isSubmitting = true;
        this.errors = {errors: {}};
        const credentials = this.loginData; // this.authForm.value;
        this.userService
        .attemptAuth(this.authType, credentials)
        .subscribe(
            data => {
                this.userService.getUserMenu(data.userLevelID).subscribe(
                    res => this.router.navigateByUrl('/')
                );
            },
            err => {
                this.errors = err;
                this.isSubmitting = false;
            }
        );
    }

    forgetPassword() {
        const loginName = this.loginData.username;
        if (loginName != null && loginName != '') {
            this.requireName = false;
            const loginType = 1;
            this.unlockService.forgetPassword(loginName, loginType)
            .subscribe( x => {
                if (x) {
                    this.globalFunction.messageDialogBox('Your password has been sent! Please check your email!', 'Forget Password');
                } else {
                    this.globalFunction.messageDialogBox('The login name you entered is not there in our database.', 'Forget Password');
                }
            });
        } else {
            this.requireName = true;
        }
    }
}
