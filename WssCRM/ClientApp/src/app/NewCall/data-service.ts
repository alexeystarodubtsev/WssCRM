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

  getCall(idCompany: number, idStage: number) {
    return this.http.get(this.url + '/newcall/' + idCompany + '/' + idStage);
  }
  postCall(c: Call) {
    return this.http.post(this.url + '/newcall', c);
  }

}
