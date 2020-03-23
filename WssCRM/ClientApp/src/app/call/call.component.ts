import { Component, OnInit, Input } from '@angular/core';
import { DataService } from './data-service';
import { Call } from '../Models/Call';

@Component({
  selector: 'app-call',
  templateUrl: './call.component.html',
  providers: [DataService]
})
export class CallComponent implements OnInit {
  
  @Input() call: Call = new Call();
  constructor(private dataService: DataService) { }
  ngOnInit() {
    console.log(this.call);
    this.loadCall(this.call.id);
    
  }
  loadCall(id: number) {
    this.dataService.getCall(id)
      .subscribe((data: Call) => {
        this.call = data
      });
  }
    
}

