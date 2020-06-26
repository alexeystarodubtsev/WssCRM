import { Component, OnInit } from '@angular/core';
import { DataService } from './data-service';
import { Call } from '../Models/Call';
import { Filter } from '../Models/Filter';
import { ChoseFilter } from '../Models/ChoseFilter';
import { Router } from '@angular/router';
import { MomentDateAdapter, MAT_MOMENT_DATE_ADAPTER_OPTIONS } from '@angular/material-moment-adapter';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import * as _moment from 'moment';
import { FormControl } from '@angular/forms';
import { PartialCalls } from '../Models/PartialCalls';
// tslint:disable-next-line:no-duplicate-imports

const moment =  _moment;

export const MY_FORMATS = {
  
  
  parse: {
    dateInput: 'DD.MM.YYYY',
  },
  display: {
    dateInput: 'DD.MM.YYYY',
    monthYearLabel: 'MMM YYYY',
    dateA11yLabel: 'LL',
    monthYearA11yLabel: 'MMMM YYYY',
  }
};
@Component({
  selector: 'app-calls',
  templateUrl: './calls.component.html',
  styleUrls: ['./calls.component.css'],
  providers: [DataService,
    {
    provide: DateAdapter,
    useClass: MomentDateAdapter,
    deps: [MAT_DATE_LOCALE, MAT_MOMENT_DATE_ADAPTER_OPTIONS]
    },

    { provide: MAT_DATE_FORMATS, useValue: MY_FORMATS },
    { provide: MAT_MOMENT_DATE_ADAPTER_OPTIONS, useValue: { useUtc: true } }
  ]
})
export class CallsComponent implements OnInit{

  calls: Call[];
  call: Call = new Call();
  callslist: boolean = true;
  fltlist: Filter = new Filter();
  curFlt: ChoseFilter = new ChoseFilter();
  DateBegin = new FormControl(moment());
  DateEnd = new FormControl(moment());
  pageSize: number = 0;
  //choseDate: Date; [(ngModel)]="choseDate"
  constructor(private router: Router, private dataService: DataService) { }
  ngOnInit() {
    this.loadFilter();// загрузка данных при старте компонента

    
  }
  loadFilter() {
    this.dataService.getFilter()
      .subscribe((data: Filter) => {
        this.curFlt = new ChoseFilter();
        this.fltlist = data;
        //this.curFlt.company = this.fltlist.Companies[0];
        //console.log(this.fltlist);
        
        }
      );
    
  }

  

  editCall(c: Call) {
    this.call = c;
    this.callslist = false;
    
  }
  getCalls() {

    this.curFlt.StartDate = this.DateBegin;
    this.curFlt.EndDate = this.DateEnd;

    this.dataService.getCalls(this.curFlt)
      .subscribe((data: PartialCalls) => {
        this.calls = data.calls;
        this.pageSize = data.pageSize;
        console.log(this.pageSize);
      });
    
  }
  returnCalls() {
    this.callslist = true;
  }
  OpenCall(id: number) {
    const url = this.router.serializeUrl(
      this.router.createUrlTree(["/call/" + id])
    );

    window.open(url, '_blank');
  
  }

  pageChanged() {
    this.getCalls();
  }
}
