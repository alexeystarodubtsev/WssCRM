import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class DataService {

  private url = "/api/companies";

  constructor(private http: HttpClient) {
  }
  getCompanies() {
    return this.http.get(this.url);
  }

}
