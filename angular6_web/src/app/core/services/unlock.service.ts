import { Injectable } from '@angular/core';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class UnlockService {

  constructor(private apiservice: ApiService) { }

  checkOldPassword(oldPassword) {
    return this.apiservice.postJson('/admin/checkPassword', {oldPassword: oldPassword});
  }

  changePassword(oldPassword, newPassword) {
    return this.apiservice.postJson('/admin/PassChange', {oldPassword: oldPassword, newPassword: newPassword});

  }

  unBlock(adminID) {
    return this.apiservice.get('/admin/unBlock/' + adminID);
  }

  resetPassword(adminID) {
    return this.apiservice.get('/admin/ResetPassword/' + adminID);
  }

  publicResetPassword(ID, loginType, PWD, SALT) {
    const obj = {ID: ID,loginType: loginType, PWD: PWD, SALT : SALT};
   return this.apiservice.postJson('/public/resetpassword', obj);
  }
 
  forgetPassword(loginName, loginType) {
    return this.apiservice.postJson('/public/ForgotPassword', {loginName:loginName,loginType: loginType});
  }

}
