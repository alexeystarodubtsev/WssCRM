import { Company } from "./Company";
import { ChoseFilter } from "./ChoseFilter";
import { Stage } from "./Stage";

export class Filter {
  constructor(
    public Companies?: Company[]
  )
  {
    this.Companies = [];
  }
}
