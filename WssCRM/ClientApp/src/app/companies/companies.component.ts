import { Component, OnInit } from '@angular/core';
import { DataService } from './data-service';
import { Company } from '../Models/Company';

@Component({
  selector: 'app-companies',
  templateUrl: './companies.component.html',
  providers: [DataService]
})
export class CompaniesComponent implements OnInit {

  Companies: Company[];
  constructor(private dataService: DataService) { }
  ngOnInit() {
    this.loadCompanies();    // загрузка данных при старте компонента  
  }
  loadCompanies() {
    this.dataService.getCompanies()
      .subscribe((data: Company[]) => this.Companies = data);
  }
}
