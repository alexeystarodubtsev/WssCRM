import { Component, OnInit, Input } from '@angular/core';
import { DataService } from './data-service';
import { Call } from '../Models/Call';
import { ActivatedRoute } from '@angular/router';
import { DateAdapter, MAT_DATE_LOCALE, MAT_DATE_FORMATS } from '@angular/material/core';
import { MomentDateAdapter, MAT_MOMENT_DATE_ADAPTER_OPTIONS } from '@angular/material-moment-adapter';
import { MY_FORMATS } from '../calls/calls.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgbdModalContent } from '../ModalWindow/ModalWindowComponent';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-call',
  templateUrl: './call.component.html',
  styleUrls: ['../NewCall/NewCallStyles.css'],
  providers: [DataService,
    {
      provide: DateAdapter,
      useClass: MomentDateAdapter,
      deps: [MAT_DATE_LOCALE, MAT_MOMENT_DATE_ADAPTER_OPTIONS]
    },

    { provide: MAT_DATE_FORMATS, useValue: MY_FORMATS },
    { provide: MAT_MOMENT_DATE_ADAPTER_OPTIONS, useValue: { useUtc: true } }]
})
export class  CallComponent implements OnInit {

  
  call: Call = new Call();
  callQuality: number = 0;
  errors: string[] = [];
  hasErrors: boolean = false;
  constructor(
    private route: ActivatedRoute,
    private dataService: DataService,
    private modalService: NgbModal
  ) { }

  
  //
  
  //constructor(private dataService: DataService) {
  //}
  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.loadCall(Number(params.get('Id')));
    });
    
  }
  updateTotalData() {
    this.call.quality = 0;
    let MaxPoints = 0;
    this.call.points.forEach(p => { this.call.quality += p.value; MaxPoints += p.maxMark; });
    if (MaxPoints != 0)
      this.call.quality = Math.round(this.call.quality * 10000 / MaxPoints) / 100;
    else
      this.call.quality = -1;
  }
  saveCall() {
    
    this.dataService.saveCall(this.call).
      subscribe(data => { },
        err => { });
  }
  loadCall(id: number) {
    this.dataService.getCall(id)
      .subscribe((data: Call) => {
        this.call = data;
        if (!data.hasDateNext) {
          this.call.dateNext = undefined;
        }
        this.updateTotalData();
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

  closeError() {

    this.hasErrors = false;
    this.errors = [];
  }

  delete() {
    const modalRef = this.modalService.open(NgbdModalContent);
    modalRef.componentInstance.question = 'Вы уверены, что хотите безвозвратно удалить звонок?';
    modalRef.result.then((result) => {
      if (result && result == 'Ok click') {
        this.dataService.delete(this.call.id).subscribe(data => {
          window.close();
        },
          err => {
            this.processError(err);

          });

      }

    })
  }
    
}

