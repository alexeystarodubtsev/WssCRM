import { Component, OnInit, Input } from '@angular/core';
import { DataService } from './data-service';
import { Call } from '../Models/Call';
import { ActivatedRoute } from '@angular/router';
import { Filter } from '../Models/Filter';
import { DateAdapter, MAT_DATE_LOCALE, MAT_DATE_FORMATS, ErrorStateMatcher } from '@angular/material/core';
import { MomentDateAdapter, MAT_MOMENT_DATE_ADAPTER_OPTIONS } from '@angular/material-moment-adapter';
import { MY_FORMATS } from '../calls/calls.component';
import { isNullOrUndefined } from 'util';
import { FormControl, Validators, NgForm, FormGroupDirective } from '@angular/forms';


export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid/* && (control.dirty || control.touched || isSubmitted)*/);
  }
}

@Component({
  selector: 'app-NewCall',
  templateUrl: './NewCallComponent.html',
  styleUrls: ['./NewCallStyles.css'],
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

  NameFormControl = new FormControl('',
    [Validators.required]
  );
  ManagerFormControl = new FormControl('',
    [Validators.required]
  )
  DateFormControl = new FormControl('',
    [Validators.required]
  );
  matcher = new MyErrorStateMatcher();

  constructor(
    private route: ActivatedRoute,
    private dataService: DataService
  ) { }


  ngOnInit() {
    this.loadMeta();
  }

  getNewCall() {
    
    if (this.call.stage.id != null && this.call.stage.id != 0) {
      this.dataService.getCall(this.call.company.id, this.call.stage.id)
        .subscribe((data: Call) => {
          this.call.points = data.points;
          if (isNullOrUndefined(this.call.date)) {
            this.call.date = data.date;
          }
          if (isNullOrUndefined(this.call.duration)) {
            this.call.duration = data.duration;
          }
          this.updateTotalData();
        });
    }
  }

  saveCall() {
    this.dataService.postCall(this.call)
      .subscribe(data => {
      });
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

