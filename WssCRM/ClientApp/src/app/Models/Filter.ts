import { Company } from "./Company";
import { ChoseFilter } from "./ChoseFilter";

export class Filter {
  constructor(
    public pointsFilter?: ChoseFilter[],
  )
  {
    this.pointsFilter = [];
  }
}
