import { Component, OnInit, Input } from '@angular/core';
import { DataService } from './data-service';
import { Call } from '../Models/Call';
import { ActivatedRoute } from '@angular/router';
import { Filter } from '../Models/Filter';
import { DateAdapter, MAT_DATE_LOCALE, MAT_DATE_FORMATS } from '@angular/material/core';
import { MomentDateAdapter, MAT_MOMENT_DATE_ADAPTER_OPTIONS } from '@angular/material-moment-adapter';
import { MY_FORMATS } from '../calls/calls.component';
import * as _moment from 'moment';
import { FormControl, Validators } from '@angular/forms';
// tslint:disable-next-line:no-duplicate-imports

const moment = _moment;
@Component({
  selector: 'app-NewCall',
  templateUrl: './NewCallComponent.html',
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
export class NewCallComponent implements OnInit {

  minDate = new Date();
  maxDate = new Date();
  call: Call = new Call();
  fltlist: Filter = new Filter();
  //PointFormControl = new FormControl('', [
  //  Validators.required,
  //  Validators.pattern("dd"),
  //]);


  constructor(
    private route: ActivatedRoute,
    private dataService: DataService
  ) { }


  ngOnInit() {
    //this.route.paramMap.subscribe(params => {
    //  this.loadCall(Number(params.get('Id')));
    //});
    this.loadMeta();
  }

  getNewCall() {

    console.log("dd");
    if (this.call.stage.id != null && this.call.stage.id != 0) {
      this.dataService.getCall(this.call.company.id, this.call.stage.id)
        .subscribe((data: Call) => {
          this.call = data;
          this.updateTotalData();
        });
    }
  }

  saveCall() {

  }
  updateTotalData() {
    this.call.quality = 0;
    let MaxPoints = 0;
    this.call.points.forEach(p => { this.call.quality += p.value; MaxPoints += p.maxMark; });
    if (MaxPoints != 0)
      this.call.quality /= MaxPoints;
    else
      this.call.quality = -1;
  }
  loadMeta() {
    this.dataService.getMeta()
      .subscribe((data: Filter) => {
        this.fltlist = data;
      });

  }

}

