import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ChoseFilter } from '../Models/ChoseFilter';
import { missedCall } from '../Models/missedCall';

@Injectable()
export class DataService {

  private url = "/api/calls";

  constructor(private http: HttpClient) {
  }
  getCalls(f1: ChoseFilter) {
    return this.http.post(this.url + '/getmissed', f1);
  }
  getFilter() {
    return this.http.get(this.url + '/Flt/all');
  }
  passOnCall(c: missedCall) {
    return this.http.put(this.url + '/passedon', c);
  }
}
