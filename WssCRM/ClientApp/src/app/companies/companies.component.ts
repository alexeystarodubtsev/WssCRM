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
  providers: [DataService
  ]
})
export class CompaniesComponent implements OnInit {

  companies: Company[];
  curCompany: Company = new Company();
  curStage: Stage;
  Attachments: File[] = [];
  curmanager: Manager = new Manager();
  ModeNewManager: boolean = false;
  tableMode: boolean = true;
  StageMode: boolean = false;
  caption: string;
  errors: string[] = [];
  hasErrors: boolean = false;
  processFile: boolean = false;
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
    console.log(this.curCompany);
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

  deleteMan(m: Manager) {
    const modalRef = this.modalService.open(NgbdModalContent);
    modalRef.componentInstance.question = 'Вы уверены, что хотите удалить менеджера?';
    modalRef.result.then((result) => {
      if (result && result == 'Ok click') {
        m.deleted = true;
        
      }

    })
  }

  deleteStage(s: Stage) {
    const modalRef = this.modalService.open(NgbdModalContent);
    modalRef.componentInstance.question = 'Вы уверены, что хотите удалить этап?';
    modalRef.result.then((result) => {
      if (result && result == 'Ok click') {
        s.deleted = true;

      }

    })
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
    console.log(this.curCompany.managers);
    this.ModeNewManager = false;
  }
  OpenCompany(c: Company) {
    console.log(c.id);
    this.dataService.getCompany(c.id)
      .subscribe((data: Company) => {
        this.curCompany = data;
        this.tableMode = false;
        this.editCaption();
      });

    
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
    this.curStage = new Stage();
  }
  returnCompanies() {
    this.tableMode = true;
    this.curCompany = new Company();
    this.caption = "";
    this.curStage = new Stage();
    this.curmanager = new Manager();
    this.Attachments = [];
  }

  chooseXLS(event) {
    this.Attachments = event.target.files;
    
  }

  uploadXLS() {
    this.processFile = true;
    const formData = new FormData();
    formData.append(this.Attachments[0].name, this.Attachments[0]);
    this.dataService.postFile(formData).subscribe((data: Company) => {
      this.curCompany.stages = data.stages;
      this.processFile = false;
      this.Attachments = [];
    });
  }


}
