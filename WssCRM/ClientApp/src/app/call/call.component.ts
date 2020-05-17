import { Component, OnInit, Input } from '@angular/core';
import { DataService } from './data-service';
import { Call } from '../Models/Call';

@Component({
  selector: 'app-call',
  templateUrl: './call.component.html',
  providers: [DataService]
})
export class  CallComponent implements OnInit {
  
  @Input() call: Call = new Call();
  callQuality: number = 0;
  
  constructor(private dataService: DataService) {
  }
  ngOnInit() {
    this.loadCall(this.call.id);
  }
  updateTotalData()
  {
    this.callQuality = 0;
    this.call.points.forEach(p => { this.callQuality += p.value;  });
  }
  loadCall(id: number) {
    this.dataService.getCall(id)
      .subscribe((data: Call) => {
        this.call = data;
        this.updateTotalData();
      });
    
  }
    
}

