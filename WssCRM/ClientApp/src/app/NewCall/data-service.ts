import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { isNullOrUndefined } from 'util';
import { Call } from '../Models/Call';

@Injectable()
export class DataService {

  private url = "/api/calls";

  constructor(private http: HttpClient) {
  }
  getMeta() {
    return this.http.get(this.url + '/Flt/restr');
  }

  gettemplpoints(c: Call) {
    return this.http.post(this.url + '/getnewcall/', c);
  }
  postCall(c: Call) {
    return this.http.post(this.url + '/newcall', c);
  }

}
