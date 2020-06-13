import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Company } from '../Models/Company';

@Injectable()
export class DataService {

  private url = "/api/companies";

  constructor(private http: HttpClient) {
  }
  getCompanies() {
    return this.http.get(this.url);
  }
  getCompany(id: number) {
    
    return this.http.get(this.url + '/' + id);

  }
  postCompany(c: Company) {
    return this.http.post(this.url, c);
  }
  delete(id: number) {
    return this.http.delete(this.url + '/' + id);
  }
  postFile(f) {
    return this.http.post(this.url + '/processfile', f);
  }

}
