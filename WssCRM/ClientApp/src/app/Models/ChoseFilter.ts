import { Company } from "./Company";
import { Stage } from "./Stage";
import { Manager } from "./Manager";
import { FormControl } from "@angular/forms";
import * as _moment from 'moment';
const moment = _moment;

export class ChoseFilter {
  constructor(
    public company?: Company,
    public stage?: Stage,
    public manager?: Manager,
    public StartDate?: FormControl,//Date,
    public EndDate?: FormControl,//Date
    public pageNumber?: number,
    public onlyNotProcessed?: boolean,
    public period?: string
  ) {
    this.company = new Company();
    this.pageNumber = 1;
    this.onlyNotProcessed = true;
    this.StartDate = new FormControl(moment());
    this.EndDate = new FormControl(moment());
  }
}
