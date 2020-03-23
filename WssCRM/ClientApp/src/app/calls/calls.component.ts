import { Component, OnInit } from '@angular/core';
import { DataService } from './data-service';
import { Call } from '../Models/Call';

@Component({
  selector: 'app-calls',
  templateUrl: './calls.component.html',
  providers: [DataService]
})
export class CallsComponent implements OnInit{

  calls: Call[];
  call: Call = new Call();
  callslist: boolean = true;
  selectCompany: string = "Все компании";
  
  constructor(private dataService: DataService) { }
  ngOnInit() {
    this.loadCalls();    // загрузка данных при старте компонента  
  }
  loadCalls() {
    this.dataService.getCalls()
      .subscribe((data: Call[]) => {
        this.calls = data;
        for (var i = 0; i < this.calls.length; i++) {
          this.calls[i].TrueDate = new Date(this.calls[i].date);
          console.log(this.calls[i].TrueDate);
        }
      });
    
  }
  editCall(c: Call) {
    this.call = c;
    this.callslist = false;
    
  }
}
