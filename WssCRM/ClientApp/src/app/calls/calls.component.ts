import { Component, OnInit } from '@angular/core';
import { DataService, DateFormat } from '../_services/';
import { Call } from '../Models/Call';
import { Filter } from '../Models/Filter';
import { ChoseFilter } from '../Models/ChoseFilter';
import { Router } from '@angular/router';
import * as _moment from 'moment';
import { PartialCalls } from '../Models/PartialCalls';
// tslint:disable-next-line:no-duplicate-imports

@Component({
  selector: 'app-calls',
  templateUrl: './calls.component.html',
  styleUrls: ['./calls.component.css'],
  providers: [DataService, ...DateFormat]
})
export class CallsComponent implements OnInit{

  calls: Call[];
  call: Call = new Call();
  callslist: boolean = true;
  fltlist: Filter = new Filter();
  curFlt: ChoseFilter = new ChoseFilter();
  
  
  pageSize: number = 0;
  loading: boolean = false; 
  //choseDate: Date; [(ngModel)]="choseDate"
  constructor(private router: Router, private dataService: DataService) { }
  ngOnInit() {
    
  }




  editCall(c: Call) {
    this.call = c;
    this.callslist = false;
    
  }
  getCalls() {
    
    this.dataService.getAllCalls(this.curFlt)
      .subscribe((data: PartialCalls) => {
        this.calls = data.calls;
        this.pageSize = data.pageSize;
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
