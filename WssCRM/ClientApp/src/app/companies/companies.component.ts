import { Component, OnInit } from '@angular/core';
import { DataService } from './data-service';
import { Company } from '../Models/Company';

@Component({
  selector: 'app-companies',
  templateUrl: './companies.component.html',
  providers: [DataService]
})
export class CompaniesComponent implements OnInit {

  companies: Company[];
  curCompany: Company;
  curmanager: string = "";
  ModeNewManager: boolean = false;
  tableMode: boolean = true;
  constructor(private dataService: DataService) { }
  ngOnInit() {
    this.loadCompanies();    // загрузка данных при старте компонента  
  }
  loadCompanies() {
    this.dataService.getCompanies()
      .subscribe((data: Company[]) => {
        this.companies = data;
        console.log(this.companies);
      });
  }
  newCompany() {
    this.tableMode = false;
    this.curCompany = new Company();
    this.curCompany.managers.push("Игорь");
  }
  newManager() {
    this.curmanager = "";
    this.ModeNewManager = true;
  }
  addManager() {
    if (this.curmanager != "") {
      this.curCompany.managers.push(this.curmanager)
    }
    this.ModeNewManager = false;
  }
  returnCompanies() {
    this.tableMode = true;
  }
}
