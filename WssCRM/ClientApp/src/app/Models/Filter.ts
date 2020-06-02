import { Company } from "./Company";

export class Filter {
  constructor(
    public Companies?: Company[]
  )
  {
    this.Companies = [];
  }
}
