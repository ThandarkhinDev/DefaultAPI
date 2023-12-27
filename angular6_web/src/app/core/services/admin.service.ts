import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { BehaviorSubject } from 'rxjs';
import { GridDataResult } from '@progress/kendo-angular-grid';

@Injectable({
  providedIn: 'root'
})
export class AdminService extends BehaviorSubject<GridDataResult> {

  public loading: boolean;
  constructor(private apiservice: ApiService) {
    super(null);
  }

  getAdminList(girdState: any) {
    this.loading = true;
    this.apiservice.fetchgrid_postJson('/admin/GetAdminList/', girdState)
    .subscribe(x => {
      super.next(x);
      this.loading = false;
    });
  }
  
  getAdminComboData() {
    return this.apiservice.get('/admin/GetAdminComboData');
  }

  checkDuplicate(adminSet) {
    return this.apiservice.postJson('/admin/checkDuplicate/', adminSet);
  }

  saveAdmin(adminSet) {
    return this.apiservice.postJson('/admin/AddAdminSetup/', adminSet);
  }

  deleteAdmin(adminID) {
    return this.apiservice.delete('/admin/DeleteAdminSetup/' + adminID);
  }
}
