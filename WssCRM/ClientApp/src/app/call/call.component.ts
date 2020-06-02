import { Component, OnInit, Input } from '@angular/core';
import { DataService } from './data-service';
import { Call } from '../Models/Call';
import { ActivatedRoute } from '@angular/router';
import { DateAdapter, MAT_DATE_LOCALE, MAT_DATE_FORMATS } from '@angular/material/core';
import { MomentDateAdapter, MAT_MOMENT_DATE_ADAPTER_OPTIONS } from '@angular/material-moment-adapter';
import { MY_FORMATS } from '../calls/calls.component';

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
      console.log(Number(params.get('Id')));
      this.loadCall(Number(params.get('Id')));
    });
    
  }
  updateTotalData()
  {
    this.callQuality = 0;
    this.call.points.forEach(p => { this.callQuality += p.value;  });
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
        console.log(this.call);
        this.updateTotalData();
      });
    
  }
    
}

