import { Company } from "./Company";
import { Stage } from "./Stage";
import { Manager } from "./Manager";
import { FormControl } from "@angular/forms";

export class ChoseFilter {
  constructor(
    public company?: Company,
    public stage?: Stage,
    public manager?: Manager,
    public StartDate?: FormControl,//Date,
    public EndDate?: FormControl//Date
  ) {
    this.company = new Company();
    
  }
}
