import { Component, OnInit } from '@angular/core';
import { DataService } from './data-service';
import { Call } from '../Models/Call';
import { Filter } from '../Models/Filter';
import { ChoseFilter } from '../Models/ChoseFilter';

import { MomentDateAdapter, MAT_MOMENT_DATE_ADAPTER_OPTIONS } from '@angular/material-moment-adapter';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import * as _moment from 'moment';
import { FormControl } from '@angular/forms';
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
  },
};
@Component({
  selector: 'app-calls',
  templateUrl: './calls.component.html',
  providers: [DataService,
    {
    provide: DateAdapter,
    useClass: MomentDateAdapter,
    deps: [MAT_DATE_LOCALE, MAT_MOMENT_DATE_ADAPTER_OPTIONS]
    },

    { provide: MAT_DATE_FORMATS, useValue: MY_FORMATS }]
})
export class CallsComponent implements OnInit{

  calls: Call[];
  call: Call = new Call();
  callslist: boolean = true;
  fltlist: Filter = new Filter();
  curFlt: ChoseFilter;
  DateBegin = new FormControl(moment());
  DateEnd = new FormControl(moment());
  //choseDate: Date; [(ngModel)]="choseDate"
  constructor(private dataService: DataService) { }
  ngOnInit() {
    this.loadFilter();// загрузка данных при старте компонента

    
  }
  loadFilter() {
    this.dataService.getFilter()
      .subscribe((data: Filter) => {
        this.fltlist = data;
        this.curFlt = this.fltlist.pointsFilter[0];
        

        }
      );
    
  }
  editCall(c: Call) {
    this.call = c;
    this.callslist = false;
    
  }
  getCalls() {
    console.log(this.DateBegin);
    this.dataService.getCalls(this.curFlt)
      .subscribe((data: Call[]) => {
        this.calls = data;
        for (var i = 0; i < this.calls.length; i++) {
          this.calls[i].TrueDate = new Date(this.calls[i].date);
        }
      });
    
  }
  returnCalls() {
    this.callslist = true;
  }
}
