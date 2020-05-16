import { Component, OnInit } from '@angular/core';
import { DataService } from './data-service';
import { Company } from '../Models/Company';
import { Stage } from '../Models/Stage';

@Component({
  selector: 'app-companies',
  templateUrl: './companies.component.html',
  providers: [DataService]
})
export class CompaniesComponent implements OnInit {

  companies: Company[];
  curCompany: Company;
  curStage: Stage;
  curmanager: string = "";
  ModeNewManager: boolean = false;
  tableMode: boolean = true;
  StageMode: boolean = false;
  caption: string;

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
    this.companies.push(this.curCompany);
    this.curCompany.managers.push("Игорь");
    this.caption = "Новая компания";

  }
  saveCompany() {
    this.dataService.postCompany(this.curCompany)
      .subscribe(data => this.loadCompanies());
    this.curCompany = null;
    this.tableMode = true;
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
  OpenCompany(c: Company) {
    this.tableMode = false;
    console.log(c.id);
    this.dataService.getCompany(c.id)
      .subscribe((data: Company) => {
      this.curCompany = data;
        this.editCaption();});
    
  }
  editCaption() {
    if (this.curCompany.name != "") {
      this.caption = this.curCompany.name;
    }
    

  }
  newStage() {
    this.curStage = new Stage();
    this.curCompany.stages.push(this.curStage);
    this.StageMode = true;
  }
  editStage(s : Stage) {
    this.curStage = s;
    this.StageMode = true;
  }
  returnToCompany() {
    this.StageMode = false;
  }
  returnCompanies() {
    this.tableMode = true;
  }
}
