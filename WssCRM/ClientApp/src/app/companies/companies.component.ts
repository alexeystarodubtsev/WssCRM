import { Component, OnInit } from '@angular/core';
import { DataService } from './data-service';
import { Company } from '../Models/Company';
import { Stage } from '../Models/Stage';
import { error } from '@angular/compiler/src/util';

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
  errors: string[] = [];
  hasErrors: boolean = false;
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
      .subscribe(data => {
        this.loadCompanies();
        this.tableMode = true;
        this.curCompany = null;
      }, err => {
        this.hasErrors = true;
        if (err.status === 400) {
          // handle validation error
          console.log(err.error);
          let validationErrorDictionary = err.error;
          
          for (var fieldName in validationErrorDictionary) {
            if (validationErrorDictionary.hasOwnProperty(fieldName)) {
              this.errors.push(validationErrorDictionary[fieldName].join());
              
            }
          }
        } else {
          this.errors.push("something went wrong!");
        }
      });
    
    
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
    console.log(c.id);
    this.dataService.getCompany(c.id)
      .subscribe((data: Company) => {
        this.curCompany = data;
        this.tableMode = false;
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
