import { Company } from "./Company";
import { Stage } from "./Stage";
import { Manager } from "./Manager";

export class ChoseFilter {
  constructor(
    public company?: Company,
    public stage?: Stage,
    public manager?: Manager,
    public StartDate?: Date,
    public EndDate? : Date
  ) {
    this.company = new Company();
  }
}
