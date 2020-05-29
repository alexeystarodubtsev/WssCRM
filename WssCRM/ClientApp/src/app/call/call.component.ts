import { Component, OnInit, Input } from '@angular/core';
import { DataService } from './data-service';
import { Call } from '../Models/Call';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-call',
  templateUrl: './call.component.html',
  providers: [DataService]
})
export class  CallComponent implements OnInit {

  product;
  call: Call;
  callQuality: number = 0;
  constructor(
    private route: ActivatedRoute,
    private dataService: DataService
  ) { }

  
  //
  
  //constructor(private dataService: DataService) {
  //}
  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.loadCall(Number(params.get('Id')));
    });
    
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

