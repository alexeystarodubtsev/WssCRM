import { Component, OnInit } from '@angular/core';
import { DataService } from './data-service';
import { Call } from '../Models/Call';
import { Filter } from '../Models/Filter';
import { ChoseFilter } from '../Models/ChoseFilter';

@Component({
  selector: 'app-calls',
  templateUrl: './calls.component.html',
  providers: [DataService]
})
export class CallsComponent implements OnInit{

  calls: Call[];
  call: Call = new Call();
  callslist: boolean = true;
  fltlist: Filter;
  curFlt: ChoseFilter;
  constructor(private dataService: DataService) { }
  ngOnInit() {
    this.loadFilter();// загрузка данных при старте компонента
    this.curFlt = new ChoseFilter("company");
  }
  loadFilter() {
    this.dataService.getFilter()
      .subscribe((data: Filter) => {
        this.fltlist = data;
        }
      );
    
  }
  editCall(c: Call) {
    this.call = c;
    this.callslist = false;
    
  }
  getCalls() {

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
