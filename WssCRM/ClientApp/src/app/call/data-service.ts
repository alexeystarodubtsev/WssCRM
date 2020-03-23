import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { isNullOrUndefined } from 'util';

@Injectable()
export class DataService {

  private url = "/api/calls";

  constructor(private http: HttpClient) {
  }
  getCall(id?: number) {
    if (isNullOrUndefined(id)) {
      id = -89765;
    }
    return this.http.get(this.url + '/' + id);

  }

}
