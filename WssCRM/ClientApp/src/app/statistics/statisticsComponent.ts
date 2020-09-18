import { Component, OnInit, Input, Output } from '@angular/core';
import { DataService, DateFormat } from '../_services/';
import { MomentDateAdapter, MAT_MOMENT_DATE_ADAPTER_OPTIONS } from '@angular/material-moment-adapter';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import { Filter } from '../Models/Filter';
import { ChoseFilter } from '../Models/ChoseFilter';
import { StatisticStage } from '../Models/StatisticStage';
import { FormControl } from '@angular/forms';
import * as moment from 'moment';
import { TableWithStatistic } from '../Models/TableWithStatistic';
@Component({
  selector: 'app-statistics',
  templateUrl: './statisticsComponent.html',
  providers: [DataService, ...DateFormat]
})
export class statisticsComponent implements OnInit {
    curFlt: ChoseFilter;
    fltlist: Filter;
    DateBegin = new FormControl(moment());
    DateEnd = new FormControl(moment());
    StatisticsStages: StatisticStage[] = [];
    Tables: TableWithStatistic[] = [];
    curStage: string = "";
    constructor(private dataService: DataService) { }
    ngOnInit() {
      this.loadFilter();// загрузка данных при старте компонента
    }


  loadFilter() {
    this.dataService.getFilter()
      .subscribe((data: Filter) => {
        this.curFlt = new ChoseFilter();
        this.fltlist = data;
        //this.curFlt.company = this.fltlist.Companies[0];
        //console.log(this.fltlist);

      }
      );

  }

  getCalls() {

    this.curFlt.StartDate = this.DateBegin;
    this.curFlt.EndDate = this.DateEnd;

    this.dataService.getCallsForStatistic(this.curFlt)
      .subscribe((data: StatisticStage[]) => {
        this.StatisticsStages = data;
        this.curStage = data[0].stageName;
        this.Tables = data[0].tables;
      });

  }
  ChangeStage(s: StatisticStage) {
    this.curStage = s.stageName;
    this.Tables = s.tables;
    
  }

}
