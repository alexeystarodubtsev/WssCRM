import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { isNullOrUndefined } from 'util';
import { Call } from '../Models/Call';
import { ChoseFilter } from '../Models/ChoseFilter';
import { Company } from '../Models/Company';
import { User } from '../Models/User';
import { Observable, BehaviorSubject } from 'rxjs';
import { UserWithRoles } from '../Models/userWithRoles';
import { missedCall } from '../Models/missedCall';
import { Router } from '@angular/router';
import { map } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export default class DataService {

  private urlCalls = "/api/calls";
  private urlCompanies = "/api/companies"
  private urlRoles = "/api/roles";

  private userSubject: BehaviorSubject<User>;
  public user: Observable<User>;
  constructor(private http: HttpClient, private router: Router) {

    this.userSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('user')));
    this.user = this.userSubject.asObservable();
  }
  getCall(id?: number) {
    if (isNullOrUndefined(id)) {
      id = -89765;
    }
    return this.http.get(this.urlCalls + '/' + id);

  }
  saveCall(c: Call) {
    return this.http.put(this.urlCalls, c);
  }

  deleteCall(id: number) {
    return this.http.delete(this.urlCalls + '/' + id);
  }

 
  getAllCalls(f1: ChoseFilter) {
    return this.http.post(this.urlCalls, f1);
  }
  getFilter() {
    return this.http.get(this.urlCalls + '/Flt/all');
  }

  getCompanies() {
    return this.http.get(this.urlCompanies);
  }
  getCompany(id: number) {

    return this.http.get(this.urlCompanies + '/' + id);

  }
  postCompany(c: Company) {
    return this.http.post(this.urlCompanies, c);
  }
  deleteCompany(id: number) {
    return this.http.delete(this.urlCompanies + '/' + id);
  }
  postFile(f) {
    return this.http.post(this.urlCompanies + '/processfile', f);
  }
  postCalls(files) {
    return this.http.post(this.urlCompanies + '/processcalls', files);
  }


  getUserRoles(id: string) {
    return this.http.get<UserWithRoles>(this.urlRoles + '/edit/' + id);
  }

  getMissedCalls(f1: ChoseFilter) {
    return this.http.post(this.urlCalls + '/getmissed', f1);
  }

  passOnCall(c: missedCall) {
    return this.http.put(this.urlCalls + '/passedon', c);
  }

  getMeta() {
    return this.http.get(this.urlCalls + '/Flt/restr');
  }

  gettemplpoints(c: Call) {
    return this.http.post(this.urlCalls + '/getnewcall/', c);
  }
  postCall(c: Call) {
    return this.http.post(this.urlCalls + '/newcall', c);
  }

  getCallsForStatistic(f1: ChoseFilter) {
    return this.http.post(this.urlCalls + "/getstatistics", f1);
  }

  private urlAccount = "/api/account";


  public get userValue(): User {
    return this.userSubject.value;
  }

  login(username, password) {
    console.log(username, password);
    return this.http.post<User>(`${this.urlAccount}/login`, { username, password })
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
    this.http.get(`${this.urlAccount}/logout`)
      .subscribe(
        data => {
          localStorage.removeItem('user');
          this.userSubject.next(null);
          this.router.navigate(['/account/login']);
        },
        error => {
          alert("Something went wrong...");

        });
  }

  register(user: User) {

    return this.http.post(`${this.urlAccount}/register`, user);
  }

  getUsers() {
    return this.http.get<User[]>(`${this.urlAccount}/users`);
  }

  getUserById(id: string) {
    return this.http.get<User>(`${this.urlAccount}/users/${id}`);
  }

  updateUser(id, params) {
    return this.http.put(`${this.urlAccount}/users/${id}`, params)
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

  deleteUser(id: string) {
    return this.http.delete(`${this.urlAccount}/users/${id}`)
      .pipe(map(x => {
        // auto logout if the logged in user deleted their own record
        if (id == this.userValue.id) {
          this.logout();
        }
        return x;
      }));
  }


}
