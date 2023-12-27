import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { NgxPermissionsService } from 'ngx-permissions';

@Injectable({
  providedIn: 'root'
})

export class PermissionGuardService implements CanActivate {

  constructor(private router: Router, private permissionService: NgxPermissionsService) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | Observable<boolean> | Promise<boolean> {
    const menulist = JSON.parse(localStorage.getItem('menuList'));
    
    const routObj = this.FilterJsonRegExp(menulist, 'ControllerName', state.url.substring(1));
    if (routObj.length > 0 && routObj[0].Permission != null) {
      this.permissionService.loadPermissions(routObj[0].Permission.split(','));
      return true;
    } else {
      this.router.navigate(['/access-denied']);
      return false;
    }
  }
   // json filter 
   FilterJsonRegExp(jsonobj: any, field: string, value: string  ) {
    return jsonobj.filter(
    function(jsonobj) {
      const fieldregx = new RegExp(jsonobj[field] + '$');
      const matchposition = value.search(fieldregx);
      return matchposition == 0;
    });
  }
}
