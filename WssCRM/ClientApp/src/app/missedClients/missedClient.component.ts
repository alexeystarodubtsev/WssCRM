import { Component, OnInit } from '@angular/core';
import { DataService } from './data-service';
import { Call } from '../Models/Call';
import { Filter } from '../Models/Filter';
import { ChoseFilter } from '../Models/ChoseFilter';
import { Router } from '@angular/router';
import * as _moment from 'moment';
import { FormControl } from '@angular/forms';
import { PartialCalls } from '../Models/PartialCalls';
import { missedCall } from '../Models/missedCall';
import { PartialMissedCalls } from '../Models/PartialMissedCalls';
// tslint:disable-next-line:no-duplicate-imports


@Component({
  selector: 'app-missedClients',
  templateUrl: './calls.component.html',
  styleUrls: ['./calls.component.css'],
  providers: [DataService]
})
export class missedClientComponent implements OnInit{

  calls: missedCall[];
  call: missedCall = new missedCall();
  callslist: boolean = true;
  fltlist: Filter = new Filter();
  curFlt: ChoseFilter = new ChoseFilter();
  pageSize: number = 0;
  processCalls: boolean = false;
  constructor(private router: Router, private dataService: DataService) { }
  ngOnInit() {
    this.loadFilter();// загрузка данных при старте компонента

    
  }
  loadFilter() {
    this.dataService.getFilter()
      .subscribe((data: Filter) => {
        this.curFlt = new ChoseFilter();
        this.fltlist = data;
        
        }
      );
    
  }
  processCall(c: missedCall) {
    this.processCalls = true;
    this.dataService.passOnCall(c)
      .subscribe((data => {
        this.processCalls = false;
      }),
      err => { }
        );
  }
 
  getCalls() {
    this.processCalls = true;
    this.dataService.getCalls(this.curFlt)
      .subscribe((data: PartialMissedCalls) => {
        this.processCalls = false;
        this.calls = data.calls;
        this.pageSize = data.pageSize;
      });
    
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
