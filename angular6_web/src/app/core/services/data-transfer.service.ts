import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Admin } from '../models/admin.model';
import { Income } from '../models/income.model';
import { Expense } from '../models/expense.model';

@Injectable({
  providedIn: 'root'
})
export class DataTransferService {

  private isSaved = new BehaviorSubject(false);
  currentValue = this.isSaved.asObservable();

  constructor() { }
  
  isSavedAdmin(isSaved: boolean) {
    this.isSaved.next(isSaved);
  }

  // isSavedIncome(isSaved: boolean) {
  //   this.isSaved.next(isSaved);
  // }

}
