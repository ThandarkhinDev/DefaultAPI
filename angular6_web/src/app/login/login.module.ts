import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LoginRoutingModule } from './login-routing.module';
import { LoginComponent } from './login.component';
import { FormsModule } from '@angular/forms';
import { DialogModule } from '@progress/kendo-angular-dialog';


@NgModule({
    imports: [CommonModule, LoginRoutingModule, FormsModule, DialogModule],
    declarations: [LoginComponent]
})
export class LoginModule {}
