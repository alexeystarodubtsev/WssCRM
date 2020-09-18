import { Component, OnInit, Input, Output } from '@angular/core';
import { DataService } from '../_services'
import { Filter } from "../Models/Filter";
import { ChoseFilter } from "../Models/ChoseFilter";

@Component({
  selector: 'Search-Panel',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css'],
  providers: [DataService
  ]
})
export class SearchComponent implements OnInit {

  @Input() curFlt: ChoseFilter;
  
  loading: boolean = true;

  fltlist: Filter = new Filter();

  ngOnInit() {
    this.loadFilter();// загрузка данных при старте компонента

  }
  loadFilter() {
    this.dataService.getFilter()
      .subscribe((data: Filter) => {
        this.fltlist = data;
        //this.curFlt.company = this.fltlist.Companies[0];
        //console.log(this.fltlist);
        this.loading = false;
      }
      );

  }


  constructor(private dataService: DataService) {

  }
}
