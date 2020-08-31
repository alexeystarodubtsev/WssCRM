import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { User } from '../Models/User';

@Injectable({ providedIn: 'root' })
//@Injectable()
export class AccountService {
  private userSubject: BehaviorSubject<User>;
  public user: Observable<User>;
  private url = "/api/account";
  constructor(
    private router: Router,
    private http: HttpClient
  ) {
    this.userSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('user')));
    this.user = this.userSubject.asObservable();
  }

  public get userValue(): User {
    return this.userSubject.value;
  }

  login(username, password) {
    console.log(username, password);
    return this.http.post<User>(`${this.url}/login`, { username, password })
      .pipe(map(user => {
        // store user details and jwt token in local storage to keep user logged in between page refreshes
        localStorage.setItem('user', JSON.stringify(user));
        this.userSubject.next(user);
        return user;
      }));
  }

  logout() {
    // remove user from local storage and set current user to null
    //localStorage.removeItem('user');
    //this.http.get(`${this.url}/logout`);
    //this.userSubject.next(null);
    //this.router.navigate(['/account/login']);
    this.http.get(`${this.url}/logout`)
      .subscribe(
      data => {
          localStorage.removeItem('user');
          this.userSubject.next(null);
          this.router.navigate(['/account/login']);
        },
        error => {
          alert("error111");

        });
  }

  register(user: User) {
    
    return this.http.post(`${this.url}/register`, user);
  }

  getAll() {
    return this.http.get<User[]>(`${this.url}/users`);
  }

  getById(id: string) {
    return this.http.get<User>(`${this.url}/users/${id}`);
  }

  update(id, params) {
    return this.http.put(`${this.url}/users/${id}`, params)
      .pipe(map(x => {
        // update stored user if the logged in user updated their own record
        if (id == this.userValue.id) {
          // update local storage
          const user = { ...this.userValue, ...params };
          localStorage.setItem('user', JSON.stringify(user));

          // publish updated user to subscribers
          this.userSubject.next(user);
        }
        return x;
      }));
  }

  delete(id: string) {
    return this.http.delete(`${this.url}/users/${id}`)
      .pipe(map(x => {
        // auto logout if the logged in user deleted their own record
        if (id == this.userValue.id) {
          this.logout();
        }
        return x;
      }));
  }
}
