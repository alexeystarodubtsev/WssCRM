import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ChoseFilter } from '../Models/ChoseFilter';
import { missedCall } from '../Models/missedCall';
import { AccountService } from '../Authentication/account.service';
import { Observable } from 'rxjs';
import { User } from '../Models/User';
import { UserWithRoles } from '../Models/userWithRoles';

@Injectable()
export class DataService {

  private url = "/api/roles";

  constructor(private http: HttpClient, private account: AccountService) {
  }
  getUsers(): Observable<User[]> {
    return this.account.getAll();
  }
  getUserRoles(id: string) {
    return this.http.get<UserWithRoles>(this.url + '/edit/' + id);
  }
}
