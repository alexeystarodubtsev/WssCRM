import { Component, OnInit } from '@angular/core';
import { DataService } from './data-service';
import { Company } from '../Models/Company';
import { Stage } from '../Models/Stage';
import { error } from '@angular/compiler/src/util';
import { Manager } from '../Models/Manager';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgbdModalContent } from '../ModalWindow/ModalWindowComponent';

@Component({
  selector: 'app-companies',
  templateUrl: './companies.component.html',
  providers: [DataService]
})
export class CompaniesComponent implements OnInit {

  companies: Company[];
  curCompany: Company = new Company();
  curStage: Stage;
  curmanager: Manager = new Manager();
  ModeNewManager: boolean = false;
  tableMode: boolean = true;
  StageMode: boolean = false;
  caption: string;
  errors: string[] = [];
  hasErrors: boolean = false;
  constructor(private dataService: DataService, private modalService: NgbModal) { }
  ngOnInit() {
    this.curCompany.name = "";
    this.loadCompanies();    // загрузка данных при старте компонента  
  }
  loadCompanies() {
    this.dataService.getCompanies()
      .subscribe((data: Company[]) => {
        this.companies = data;
      });
  }
  newCompany() {
    this.tableMode = false;
    this.curCompany = new Company();
    //this.companies.push(this.curCompany);
    this.caption = "Новая компания";

  }
  closeError() {

    this.hasErrors = false;
    this.errors = [];
  }
  saveCompany() {
    this.dataService.postCompany(this.curCompany)
      .subscribe(data => {
        this.loadCompanies();
        this.tableMode = true;
        this.curCompany = null;
      }, err => {
        this.processError(err);
      });
    
    
  }
  processError(err) {
    this.hasErrors = true;
    if (err.status === 400) {
      let validationErrorDictionary = err.error;

      for (var fieldName in validationErrorDictionary) {
        if (validationErrorDictionary.hasOwnProperty(fieldName)) {
          this.errors.push(validationErrorDictionary[fieldName].join());

        }
      }
    } else {
      this.errors.push("something went wrong!");
    }
    setTimeout(() => {
      this.hasErrors = false;
      this.errors = [];
    }, 10000);
  }

  delete() {
    const modalRef = this.modalService.open(NgbdModalContent);
    modalRef.componentInstance.question = 'Вы уверены, что хотите безвозвратно удалить компанию со всеми звонками?';
    modalRef.result.then((result) => {
      if (result && result == 'Ok click') {
        this.dataService.delete(this.curCompany.id).subscribe(data => {
          console.log(data);
          this.loadCompanies();
          this.tableMode = true;
        },
          err => {
            this.processError(err);

          });
        
      }

    })
  }
  newManager() {
    this.curmanager = new Manager();
    this.ModeNewManager = true;
  }
  addManager() {
    if (this.curmanager.name != "") {
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
