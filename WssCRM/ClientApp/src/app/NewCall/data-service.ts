import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { isNullOrUndefined } from 'util';

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

}
