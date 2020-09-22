import { Component, OnInit, Input } from '@angular/core';
import { DataService, DateFormat } from '../_services/';
import { Call } from '../Models/Call';
import { ActivatedRoute } from '@angular/router';
import { Filter } from '../Models/Filter';
import { DateAdapter, MAT_DATE_LOCALE, MAT_DATE_FORMATS, ErrorStateMatcher } from '@angular/material/core';
import { MomentDateAdapter, MAT_MOMENT_DATE_ADAPTER_OPTIONS } from '@angular/material-moment-adapter';
import { isNullOrUndefined } from 'util';
import { FormControl, Validators, NgForm, FormGroupDirective } from '@angular/forms';
import { Stage } from '../Models/Stage';


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
  providers: [DataService, ...DateFormat]
})
export class NewCallComponent implements OnInit {

  minDate = new Date();
  maxDate = new Date();
  call: Call = new Call();
  fltlist: Filter = new Filter();
  stage: Stage = new Stage();
  fltStages: Stage[] = [];
  loading: boolean = true;
  loadingData: boolean = false;
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

    this.loadingData = true;
      this.dataService.gettemplpoints(this.call)
        .subscribe((data: Call) => {
          this.loadingData = false;
          this.call.stages = data.stages;
          this.call.correctioncolor = data.correctioncolor;
          if (isNullOrUndefined(this.call.date)) {
            this.call.date = data.date;
          }
          if (isNullOrUndefined(this.call.duration)) {
            this.call.duration = data.duration;
          }
          this.updateTotalData();
        });
    
  }
  Addstage() {
    this.stage = new Stage();
    this.fltStages = this.call.company.stages.filter(item => this.call.stages.every(instage => instage != item));
  }
  addstagetolist() {
    this.call.stages.push(this.stage);
    this.stage = null;
    this.fltStages = this.call.company.stages.filter(item => this.call.stages.every(instage => instage != item));
    
  }
  deletestage(s: Stage) {
    this.call.stages = this.call.stages.filter(item => item != s);
    this.fltStages = this.call.company.stages.filter(item => this.call.stages.every(instage => instage != item));
  }
  saveCall() {
    this.dataService.postCall(this.call)
      .subscribe(data => {
      });
  }
  updateTotalData() {
    this.call.quality = 0;
    let MaxPoints = 0;
    this.call.stages.forEach(s => { s.points.forEach(p => { this.call.quality += p.value; MaxPoints += p.maxMark; }) })
    if (MaxPoints != 0)
      this.call.quality = Math.round(this.call.quality * 10000 / MaxPoints) / 100;
    else
      this.call.quality = -1;
  }
  loadMeta() {
    this.dataService.getMeta()
      .subscribe((data: Filter) => {
        this.fltlist = data;
        this.loading = false;
      });

  }

}

