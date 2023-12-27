import { Injectable } from '@angular/core';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class AdminlevelService {

  constructor(private apiService: ApiService) { }

  getAdminLevel(ID) {
    return this.apiService.get('/adminlevel/GetAdminLevel/' + ID);
  }  

  getAdminLevelMenu(ID) {
    return this.apiService.get('/adminlevel/GetAdminLevelMenu/' + ID);
  }  

  checkDuplicate(adminLevelSet) {
    const map = new Map();
    map.set('formDataObj', adminLevelSet);
    return this.apiService.postJson('/adminlevel/checkDuplicate', adminLevelSet);
  }

  addAdminLevel(adminLevelSet) {
    return this.apiService.postJson('/adminlevel/AddAdminLevel', adminLevelSet);
  }

  addAdminLevelMenu(adminLevelID, adminMenuList) {
    return this.apiService.postJson('/adminlevel/AddAdminLevelMenu/' + adminLevelID, {adminMenuList: adminMenuList});
  }

  deleteAdminLevel(adminLevelID) {
    return this.apiService.delete('/adminlevel/DeleteAdminLevel/' + adminLevelID);
  }
}
