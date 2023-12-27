import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable ,  BehaviorSubject ,  ReplaySubject } from 'rxjs';

import { ApiService } from './api.service';
import { JwtService } from './jwt.service';
import { User } from '../models';
import { map ,  distinctUntilChanged } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class UserService {
  private currentUserSubject = new BehaviorSubject<User>({} as User);
  public currentUser = this.currentUserSubject.asObservable().pipe(distinctUntilChanged());

  private isAuthenticatedSubject = new ReplaySubject<boolean>(1);
  public isAuthenticated = this.isAuthenticatedSubject.asObservable();

  constructor (
    private apiService: ApiService,
    private jwtService: JwtService
  ) {}

  // Verify JWT in localstorage with server & load user's info.
  // This runs once on application startup.
  populate() {
    // If JWT detected, attempt to get & store user's info
    if (this.jwtService.getToken()) {
      this.apiService.get('/user')
      .subscribe(
        data => this.setAuth(data.user),
        err => this.purgeAuth()
      );
    } else {
      // Remove any potential remnants of previous auth states
      this.purgeAuth();
    }
  }

  setAuth(user: any) {
    // Save JWT sent from server in localstorage
    this.jwtService.saveToken(user.access_token);
    // Set current user data into observable
    this.currentUserSubject.next(user);
    // Set isAuthenticated to true
    this.isAuthenticatedSubject.next(true);
  }

  purgeAuth() {
    // Remove JWT from localstorage
    this.jwtService.destroyToken();
    // Set current user to an empty object
    this.currentUserSubject.next({} as User);
    // Set auth status to false
    this.isAuthenticatedSubject.next(false);
  }

  attemptAuth(type, credentials): Observable<any> {

    //const body = `grant_type=password&LoginType=1&username=${credentials.username}&password=${credentials.password}&`;
    const body = {
      "LoginType": "1",
      "username": credentials.username,
      "password": credentials.password,
      "grant_type": "password"
    }
    return this.apiService.postJson('/token', body)
      .pipe(map(
      data => {
        localStorage.setItem('authorizationData', data.access_token); // save access token
        localStorage.setItem('userName', data.displayName);
        localStorage.setItem('loginCookie', 'loginCookie live');
        localStorage.setItem('UserID', data.UserID);
        localStorage.setItem('LoginType', data.LoginType);
        localStorage.setItem('LoginUserImage', data.userImage);
        localStorage.setItem('MinPasswordLength', data.PWDLength);
        
        this.setAuth(data);
        return <any>data;
      }
    ));
  }

  getUserMenu(userLevelID): Observable<any> {
    return this.apiService.get('/menu/GetAdminLevelMenuData/' + userLevelID).pipe(
      map(response => {
        localStorage.setItem('menuList', JSON.stringify(response.data));
        localStorage.setItem('isLoggedin', 'true');
      }));
  }

  getCurrentUser(): User {
    return this.currentUserSubject.value;
  }
}
