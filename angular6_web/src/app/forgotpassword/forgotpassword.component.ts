import { Component, OnInit } from '@angular/core';
import { JwtService, UnlockService } from '../core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-forgotpassword',
  templateUrl: './forgotpassword.component.html',
  styleUrls: ['./forgotpassword.component.scss']
})
export class ForgotpasswordComponent implements OnInit {

  constructor(private jwtService: JwtService,
              private unLockService: UnlockService,
              private route: ActivatedRoute) { }
  msg: String = 'Sending ...';

  ngOnInit() {
    const ID = this.route.snapshot.paramMap.get('ID'); 
    const PWD = this.route.snapshot.paramMap.get('PWD'); 
    const SALT = this.route.snapshot.paramMap.get('SALT'); 
    if (ID != '') {
    const loginType = 1;
    this.msg = 'Sending ...';	
    this.unLockService.publicResetPassword(ID, loginType, PWD, SALT)
    .subscribe( x => {
      if (x) {
        this.msg = 'Reset Successfully';
      } else {
        this.msg = 'Rest Unsuccessfully';
      }
    });
  
    }
  }

  onLoggedout() {
    localStorage.clear();
    this.jwtService.destroyToken();
}

}
